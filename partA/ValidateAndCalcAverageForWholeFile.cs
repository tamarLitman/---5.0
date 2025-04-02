using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                foreach (var entry in hourlyData)
                {
                    double average = entry.Value.Average();
                    Console.WriteLine($"hour {entry.Key}: {average:F2}");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"שגיאה: {ex.Message}");
            }
        }
        public static void CalcAverageWithValidate(string filePath)
        {
            List<Dictionary<int, List<double>>> sumPerSegment = new List<Dictionary<int, List<double>>>();

            List<string[]> splitLog = FindNCommonErrors.SplitTextFileToSegments(filePath, 1000);

            foreach (string[] segment in splitLog)
            {
                sumPerSegment.Add(calcAveragePerSegment(segment));
            }
            Dictionary<int, List<double>> mergedDictionary = sumPerSegment
            .SelectMany(dict => dict) 
            .GroupBy(pair => pair.Key) 
            .ToDictionary(
           group => group.Key,
           group => group.SelectMany(pair => pair.Value).ToList() 
       );
        }

        public static Dictionary<int, List<double>> calcAveragePerSegment(string[] segment){
            string dateFormat = "dd/MM/yyyy HH:mm";
            HashSet<DateTime> uniqueTimestamps = new HashSet<DateTime>();
            Dictionary<int, List<double>> hourlyData = new Dictionary<int, List<double>>();
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
                            int hour = parsedDate.Hour;
                            if (!hourlyData.ContainsKey(hour))
                            {
                                hourlyData[hour] = new List<double>();
                            }
                            hourlyData[hour].Add(value);
                        }
                    }
                }
            return hourlyData;
        }
    }
}




