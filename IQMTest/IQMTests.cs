using System;
using System.IO;
using Xunit;
using IQM;

namespace IQMTest
{
    public class IQMTests
    {
        [Fact]
        public void Run_Compare_Orig()
        {
            DataSet data = new DataSet();
            using(StreamReader input = new StreamReader("data.txt"))
            using(StreamReader comparer = new StreamReader("output_orig.txt"))
            {
                String inputLine;
                String compareLine;
                while((inputLine = input.ReadLine()) != null && (compareLine = comparer.ReadLine()) != null)
                {
                    data.AddPoint(Convert.ToInt32(inputLine));
                    if (data.Count < 4) { continue; }

                    double mean = data.GetIQMean();
                    String output = String.Format("Index => {0}, Mean => {1:F2}", data.Count, mean);

                    int equal = String.Compare(compareLine, output);
                    if (equal != 0) {
                        Console.WriteLine("Found difference:\n  Expected: {0}\n  Got: {1}", compareLine, output);
                    }
                }
            }
        }  
    }
}