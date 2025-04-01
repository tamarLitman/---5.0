using partA;

using static partA.FindNCommonErrors;


Console.WriteLine("Hello, World!");
string filePath = @"C:\Users\123\Desktop\Hadasim\logs.txt";
List<string[]> segments = SplitTextFileToSegments(filePath, 2);
Dictionary<string,int> sum = findCommonErrors(filePath,3);
foreach (var s in sum)
{
    Console.WriteLine(s.Key,s.Value);
}
