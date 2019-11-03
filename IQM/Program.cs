using System;
using System.IO;

namespace IQM
{
    class Program
    {
        static void StreamIQMData(IDataRead reader, IDataWrite writer)
        {
            DataSet data = new DataSet();

            String line;
            while ((line = reader.Read()) != null)
            {
                data.AddPoint(Convert.ToInt32(line));
                if (data.Count < 4) { continue; }

                double mean = data.GetIQMean();
                String output = String.Format("Index => {0}, Mean => {1:F2}", data.Count, mean);
                writer.Write(output);
            }
        }
        static void Main()
        {
            DateTime beforeTime = DateTime.Now;
            try
            {
                using (FileRead reader = new FileRead("data.txt"))
                {
                    ConsoleWriter writer = new ConsoleWriter();
                    StreamIQMData(reader, writer);
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