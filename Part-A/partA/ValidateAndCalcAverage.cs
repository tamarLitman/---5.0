using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.VisualBasic;
using Parquet;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Formats.Asn1;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Parquet.Data;
using Microsoft.Spark.Sql;
using Microsoft.Spark.Sql.Streaming;
using System.Collections;


namespace partA
{
    public class ValidateAndCalcAverage
    {
        public static void CalcAverageWithValidateForWholeFile(string filePath)
        {
            string dateFormat = "dd/MM/yyyy HH:mm";
            List<string> invalidRows = new List<string>();
            HashSet<DateTime> uniqueTimestamps = new HashSet<DateTime>();
            List<string> duplicateTimestamps = new List<string>();
            DateTime minDate = new DateTime(2000, 1, 1);
            DateTime maxDate = new DateTime(2100, 12, 31);
            List<string> outOfRangeRows = new List<string>();
            Dictionary<int, List<double>> hourlyData = new Dictionary<int, List<double>>();
            List<string> nanRows = new List<string>();

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line = reader.ReadLine()!;
                    int lineNumber = 0;
                    while ((line = reader.ReadLine()!) != null)
                    {
                        lineNumber++;
                        string[] columns = line.Split(',');
                        string timestamp = columns[0].Trim();
                        string valueString = columns[1].Trim();

                        DateTime parsedDate;
                        if (!DateTime.TryParseExact(timestamp, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
                        {
                            invalidRows.Add($"שורה {lineNumber} אינה תקינה: {line}");
                        }
                        else if (parsedDate < minDate || parsedDate > maxDate)
                        {
                            outOfRangeRows.Add($"שורה {lineNumber} מחוץ לטווח: {timestamp}");
                        }
                        else
                        {
                            if (!uniqueTimestamps.Add(parsedDate))
                            {
                                duplicateTimestamps.Add($"line {lineNumber} with duplicate : {timestamp}");
                            }
                            if (columns.Length > 1)
                            {
                                if (double.TryParse(columns[1].Trim(), out double numericValue))
                                {
                                    if (double.IsNaN(numericValue))
                                    {
                                        nanRows.Add($"שורה {lineNumber} מכילה NaN: {line}");
                                    }
                                    else if (double.TryParse(valueString, out double value))
                                    {
                                        int hour = parsedDate.Hour;
                                        if (!hourlyData.ContainsKey(hour))
                                        {
                                            hourlyData[hour] = new List<double>();
                                        }
                                        hourlyData[hour].Add(value);
                                    }
                                }

                                invalidRows.Add($"שורה {lineNumber} מכילה ערך מספרי לא תקין: {columns[1]}");
                            }
                        }
                    }
                }

                Console.Write(invalidRows.Count > 0 ? "num of invalid line:" : "all line are valid");
                Console.WriteLine(invalidRows.Count());

                Console.WriteLine(duplicateTimestamps.Count > 0 ? "num of duplicates dates:" : "there are no duplicate dates");
                Console.WriteLine(duplicateTimestamps.Count());

                Console.WriteLine(outOfRangeRows.Count > 0 ? "num of out of ranfe dates:" : "all dates are valid");
                Console.WriteLine(outOfRangeRows.Count());

                Console.WriteLine("\naverage per hour:");
                string folderPath = Path.GetDirectoryName(filePath)!;
                string newFilePath = Path.Combine(folderPath, "AveragePerHour.csv");
                using (StreamWriter writer = new StreamWriter(newFilePath))
                {
                    writer.WriteLine("DateTime,Average");
                    foreach (var entry in hourlyData)
                    {
                        string line = $"{entry.Key:dd/MM/yyyy HH:mm} average:{entry.Value.Average():F2}";
                        writer.WriteLine(line);
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"שגיאה: {ex.Message}");
            }
        }
        public async static void CalcAverageWithValidate(string filePath)
        {
            List<Dictionary<DateTime, List<double>>> sumPerSegment = new List<Dictionary<DateTime, List<double>>>();

            List<string[]> splitLog = await SplitFileToSegments(filePath);

            foreach (string[] segment in splitLog)
            {
                sumPerSegment.Add(SumPerSegment(segment));
            }
            Dictionary<DateTime, List<double>> mergedDictionary = sumPerSegment
            .SelectMany(dict => dict)
            .GroupBy(pair => pair.Key)
            .ToDictionary(
               group => group.Key,
               group => group.SelectMany(pair => pair.Value).ToList());
            string folderPath = Path.GetDirectoryName(filePath)!;
            string newFilePath = Path.Combine(folderPath, "AveragePerHour.csv");
            using (StreamWriter writer = new StreamWriter(newFilePath))
            {
                writer.WriteLine("DateTime,Average");
                foreach (var entry in mergedDictionary)
                {
                    string line = $"{entry.Key:dd/MM/yyyy HH:mm} average:{entry.Value.Average():F2}";
                    writer.WriteLine(line);
                }
            }
        }



        public static Dictionary<DateTime, List<double>> SumPerSegment(string[] segment)
        {
            string dateFormat = "dd/MM/yyyy HH:mm";
            Dictionary<DateTime, List<double>> hourlyData = new Dictionary<DateTime, List<double>>();
            foreach (var line in segment)
            {
                string[] columns = line.Split(',');
                string timestamp = columns[0].Trim();
                string valueString = columns[1].Trim();

                DateTime parsedDate;
                if (columns.Length > 1 && DateTime.TryParseExact(timestamp, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
                {
                    if (double.TryParse(columns[1].Trim(), out double numericValue)
                        && !double.IsNaN(numericValue)
                        && double.TryParse(valueString, out double value))
                    {
                        DateTime dateHourKey = new DateTime(parsedDate.Year, parsedDate.Month, parsedDate.Day, parsedDate.Hour, 0, 0);
                        if (!hourlyData.ContainsKey(dateHourKey))
                        {
                            hourlyData[dateHourKey] = new List<double>();
                        }
                        hourlyData[dateHourKey].Add(value);
                    }
                }
            }
            return hourlyData;
        }
        public async static Task<List<string[]>> SplitFileToSegments(string inputFilePath)
        {
            string fileExtension = Path.GetExtension(inputFilePath).ToLower();
            return fileExtension switch
            {
                ".csv" => SplitCSVFileToSegments(inputFilePath),
                ".parquet" =>await SplitParquetFileToSegmentsAsync(inputFilePath),
                _ => throw new NotSupportedException("Unsupported file format: " + fileExtension)
            };
        }
        public static List<string[]> SplitCSVFileToSegments(string inputFilePath)
        {
            string dateFormat = "dd/MM/yyyy HH:mm";
            List<string[]> segments = new List<string[]>();
            List<string> buffer = new List<string>();
            Dictionary<DateTime, double> valuesPerHour = new Dictionary<DateTime, double>();

            if (!File.Exists(inputFilePath))
            {
                Console.WriteLine("The file does not exist.");
            }
            else
            {
                using (StreamReader reader = new StreamReader(inputFilePath))
                {
                    string line;
                    Dictionary<DateTime, List<string>> datelyData = new Dictionary<DateTime, List<string>>();
                    while ((line = reader.ReadLine()!) != null)
                    {
                        string[] columns = line.Split(',');
                        string timestamp = columns[0].Trim();
                        string valueString = columns[1].Trim();

                        DateTime parsedDate;
                        if (columns.Length > 1 && DateTime.TryParseExact(timestamp, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
                        {
                            if (double.TryParse(columns[1].Trim(), out double numericValue)
                                && !double.IsNaN(numericValue)
                                && double.TryParse(valueString, out double value))
                            {
                                DateTime day = parsedDate.Date;
                                if (!datelyData.ContainsKey(day))
                                {
                                    datelyData[day] = new List<string>();
                                }
                                datelyData[day].Add(line);
                            }
                        }

                    }
                    segments = datelyData.Select(kvp => kvp.Value.ToArray()).ToList();
                }
            }
            return segments;
        }
       // קריאת נתונים מקובץ Praquet
       // הנתונים מאוחסנים בעמודות , מה שמאפשר שליפה מהירה של עמודות מסוימות ללא צורך בשליפה של כל הנתונים
       //במקרה הזה אפשרות השליפה של עמודת סוגי השגיאות מועילה וחוסכת בסיבוכיות זמן ריצה
       //ככלל- הפורמט תומך בBIG DATA -קורא במהירות ומשתמש בזיכרון במהירות
        private static async Task<List<string[]>> SplitParquetFileToSegmentsAsync(string inputFilePath)
        {

            List<string[]> segments = new List<string[]>();
            Dictionary<DateTime, List<string>> datelyData = new Dictionary<DateTime, List<string>>();

            //using (Stream fileStream = File.OpenRead(inputFilePath))
            //using (ParquetReader reader = await ParquetReader.CreateAsync(fileStream))
            //{
            //    Console.WriteLine("");
            //}
            SparkSession spark = SparkSession
            .Builder()
            .AppName("calcAverage")
            .GetOrCreate();
            DataFrame df = spark.Read().Parquet(inputFilePath);
            return segments;
        }
   
    }
}
    






