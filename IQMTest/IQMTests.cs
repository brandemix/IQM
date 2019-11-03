using System;
using System.IO;
using System.Reflection;
using Xunit;
using IQM;

namespace IQMTest
{
    public class IQMTests
    {
        string dataPath;
        string comparerPath;
        public IQMTests()
        {
            var codeBaseUrl = new Uri(Assembly.GetExecutingAssembly().CodeBase);
            var codeBasePath = Uri.UnescapeDataString(codeBaseUrl.AbsolutePath);
            var dirPath = Path.GetDirectoryName(codeBasePath);
            this.dataPath = Path.Combine(dirPath, "../../../data.txt");
            this.comparerPath = Path.Combine(dirPath, "../../../output_orig.txt");
        }
        [Fact]
        public void Run_Compare_Orig()
        {
            DataSet data = new DataSet();
            using(StreamReader input = new StreamReader(this.dataPath))
            using(StreamReader comparer = new StreamReader(this.comparerPath))
            {
                String inputLine;
                String compareLine;
                while((inputLine = input.ReadLine()) != null)
                {
                    data.AddPoint(Convert.ToInt32(inputLine));
                    if (data.Count < 4) { continue; }

                    double mean = data.GetIQMean();
                    String output = String.Format("Index => {0}, Mean => {1:F2}", data.Count, mean);

                    compareLine = comparer.ReadLine();

                    int equal = String.Compare(compareLine, output);
                    if (equal != 0) {
                        Console.WriteLine("Found difference:\n  Expected: {0}\n  Got: {1}", compareLine, output);
                    }
                }
            }
        }  
    }
}