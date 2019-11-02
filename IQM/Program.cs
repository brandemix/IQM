using System;
using System.IO;

namespace IQM
{
    class Program
    {
        static void Main()
        {
            DateTime beforeTime = DateTime.Now;
            
            // try
            // {
            //     DataSet data = new DataSet();
            //     using (StreamReader sr = new StreamReader("data.txt"))
            //     {
            //         using(StreamWriter sw = new StreamWriter("output.txt")) {

            //             String line;
            //             while ((line = sr.ReadLine()) != null)
            //             {
            //                 data.AddPoint(Convert.ToInt32(line));                        
            //                 if (data.Count() >= 4)
            //                 {
            //                     double mean = data.GetIQMean();
            //                     sw.WriteLine("Index => {0}, Mean => {1:F2}", data.Count(), mean);
            //                     //Console.WriteLine("Index => {0}, Mean => {1:F2}", data.Count(), mean);
            //                 }
            //             }
            //         }
            //     }
            // }
            // catch (Exception e)
            // {
            //     Console.WriteLine("The file could not be read:");
            //     Console.WriteLine(e.Message);
            // }

            DateTime afterTime = DateTime.Now;
            TimeSpan diff = afterTime - beforeTime;
            Console.WriteLine("Total Milliseconds: {0}", diff.TotalMilliseconds);

            beforeTime = DateTime.Now;
            
            try
            {
                IQSet set = new IQSet();
                using (StreamReader sr = new StreamReader("data.txt"))
                {
                    using(StreamWriter sw = new StreamWriter("output2.txt")) {

                        String line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            set.AddPoint(Convert.ToInt32(line));                        
                            if (set.Count >= 4)
                            {
                                double mean = set.IQM();
                                sw.WriteLine("Index => {0}, Mean => {1:F2}", set.Count, mean);
                                //Console.WriteLine("Index => {0}, Mean => {1:F2}", data.Count(), mean);
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

            afterTime = DateTime.Now;
            diff = afterTime - beforeTime;
            Console.WriteLine("Total Milliseconds: {0}", diff.TotalMilliseconds);
        }
    }
}