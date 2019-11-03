using System;
using System.IO;

namespace IQM
{
    class Program
    {
        static void Main()
        {
            DateTime beforeTime = DateTime.Now;
            int equalCount = 0;
            int totalCount = 0;
            
            try
            {
                DataSet data = new DataSet();
                using (StreamReader input = new StreamReader("data.txt"))
                {
                    using(StreamReader comparer = new StreamReader("output_orig.txt")) 
                    {
                        String line;
                        while ((line = input.ReadLine()) != null)
                        {
                            data.AddPoint(Convert.ToInt32(line));                        
                            if (data.Count >= 4)
                            {
                                totalCount++;
                                double mean = data.GetIQMean();
                                String output = String.Format("Index => {0}, Mean => {1:F2}", data.Count, mean);
                                String compare;
                                if ((compare = comparer.ReadLine()) != null) {
                                    int equal = String.Compare(compare, output);
                                    if (equal == 0) {
                                        equalCount++;
                                    }
                                } else {
                                    throw new Exception("Couldn't read comparer");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            DateTime afterTime = DateTime.Now;
            TimeSpan diff = afterTime - beforeTime;
            Console.WriteLine("Total Milliseconds: {0}", diff.TotalMilliseconds);
            Console.WriteLine("{0}%", (equalCount / totalCount) * 100);
        }
    }
}