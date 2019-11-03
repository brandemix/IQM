using System;
using System.Collections.Generic;
using System.Linq;

namespace IQM
{
    /// <summary>Class <c>DataSet</c> represents a set of data points.</summary>
    public class DataSet
    {
        private List<int> data;

        public DataSet() {
            this.data = new List<int>();
        }

        public int Count
        {
            get => this.data.Count();
        }

        public List<int> Points {
            get => data;
        }

        ///<summary>Method <c>AddPoint</c> adds a data point in order.</summary>
        public void AddPoint(int point)
        {
            int start = 0;
            int count = this.Count;
            while (true) {
                if (count - start == 0) {
                    this.data.Add(point);
                    break;
                }

                if (count - start == 1) {
                    int element = this.data.ElementAt(start);
                    if (point >= element) {
                        this.data.Insert(start + 1, point);
                    } else {
                        this.data.Insert(start, point);
                    }
                    break;
                }

                List<int> chunk = this.data.GetRange(start, (count - start));
                int middleElement = chunk.ElementAt(chunk.Count / 2);
                if (point == middleElement) {
                    this.data.Insert((count - start) / 2, point);
                    break;
                } else if (point > middleElement) {
                    start += chunk.Count / 2 + 1;
                } else {
                    count -= chunk.Count / 2;
                }
            }
        }

        ///<summary>Method <c>GetIQMean</c> calculates the Innerquartile Mean for the data
        /// set. Shall return 0 when less than four points in the data set.</summary>
        public double GetIQMean() {
            double q = data.Count() / 4.0;
            if (q < 1) {
                return 0.0;
            }

            int i = (int)Math.Ceiling(q) - 1;
            int c = (int)Math.Floor(q*3) - i + 1;

            List<int> ys = data.GetRange(i, c);
            double factor = q - ((ys.Count() / 2.0) - 1);
            
            int sum = 0;
            
            var listEnumerator = ys.GetEnumerator();
            for (var j = 0; listEnumerator.MoveNext() == true; j++)
            {
                if (j == 0)
                {
                    continue;
                }
                else if (j == (ys.Count() - 1))
                {
                    break; 
                }
                
                sum += listEnumerator.Current;
            }

            double mean = (sum + (ys.First() + ys.Last()) * factor) / (2 * q);
            return mean;
        }
    }
}