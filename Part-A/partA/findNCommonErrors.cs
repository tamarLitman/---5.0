using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace partA
{
    public class FindNCommonErrors
    {
        public static Dictionary<string,int> findCommonErrors(string logFilePath,int n)
        {
            List<string[]> splitLog = SplitTextFileToSegments(logFilePath, 1000);
            List<Dictionary<string, int>> sumPerSegment = new List<Dictionary<string, int>>();
            Dictionary<string, int> res = new Dictionary<string, int>();
            foreach (string[] segment in splitLog)
            {
                sumPerSegment.Add(sumSegmentErrors(segment));
            }

           res = sumPerSegment
            .SelectMany(dict => dict)
            .GroupBy(kvp => kvp.Key)
            .ToDictionary(g => g.Key, g => g.Sum(kvp => kvp.Value));
            res= res.OrderByDescending(error => error.Value)  
            .Take(n)
            .ToDictionary(); 
            return res;
        }
        public static Dictionary<string,int> sumSegmentErrors(string[] errors)
        {
            Dictionary<string, int> res = new Dictionary<string, int>();
            string errorCode = "";
            foreach (string error in errors)
            {
                Regex regex = new Regex(@"Error:\s*(\S+)");

                Match match = regex.Match(error);
                if (match.Success)
                {
                    errorCode = match.Groups[1].Value;
                    if (res.ContainsKey(errorCode))
                    {
                        res[errorCode]++;
                    }
                    else
                    {
                        res.Add(errorCode, 1);
                    }
                }
            }
            return res;
        }
        public static List<string[]> SplitTextFileToSegments(string inputFilePath, int linesPerSegment)
        {
            List<string[]> segments = new List<string[]>();
            List<string> buffer = new List<string>();

            if (!File.Exists(inputFilePath))
            {
                Console.WriteLine("The file does not exist.");
            }
            else
            {
                using (StreamReader reader = new StreamReader(inputFilePath))
                {
                    string line;
                    while ((line = reader.ReadLine()!) != null)
                    {
                        buffer.Add(line);

                        if (buffer.Count >= linesPerSegment)
                        {
                            segments.Add(buffer.ToArray());
                            buffer.Clear();
                        }
                    }

                    if (buffer.Count > 0)
                    {
                        segments.Add(buffer.ToArray());
                    }
                }
            }

            return segments;
        }



    }
}
