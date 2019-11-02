using System;
using System.IO;

namespace IQM
{
    class Program
    {
        static void Main()
        {
            DateTime beforeTime = DateTime.Now;
            
            try
            {
                DataSet data = new DataSet();
                using (StreamReader sr = new StreamReader("data.txt"))
                {
                    String line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        data.AddPoint(Convert.ToInt32(line));                        
                        if (data.Count() >= 4)
                        {
                            double mean = data.GetIQMean();
                            Console.WriteLine("Index => {0}, Mean => {1:F2}", data.Count(), mean);
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
        }
    }
}