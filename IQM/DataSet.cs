using System;
using System.Collections.Generic;
using System.Linq;

namespace IQM
{
    ///<summary>Class <c>DataSet</c> represents a set of data points.</summary>
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
            /// Starting with the full list, break it in half depending on whether
            /// the point is greater than or less than the middle value. Do this until
            /// there is one value and insert the point before or after.
            int start = 0;
            int end = this.Count;
            while (true) 
            {
                /// When there are no values in the list yet, just insert
                if (end - start == 0) 
                {
                    this.data.Add(point);
                    break;
                }

                /// When there is just one value remaining to compare against, insert
                /// to the left or right of it.
                if (end - start == 1)
                {
                    int element = this.data.ElementAt(start);
                    if (point >= element) 
                    {
                        this.data.Insert(start + 1, point);
                    } 
                    else
                    {
                        this.data.Insert(start, point);
                    }
                    break;
                }

                /// Get the middle element of the current range.
                int middleIndex = start + (end - start) / 2;
                int middleElement = this.data.ElementAt(middleIndex);
                if (point == middleElement) 
                {
                    this.data.Insert(middleIndex, point);
                    break;
                } 
                else if (point > middleElement)
                {
                    start += (middleIndex - start);
                } 
                else
                {
                    end -= (end - middleIndex);
                }
            }
        }

        ///<summary>Method <c>GetIQMean</c> calculates the Innerquartile Mean for the data
        /// set. Shall return 0 when less than 1 data point in each quartile.</summary>
        public double GetIQMean() {
            double q = this.Count / 4.0;
            if (q < 1) 
            {
                return 0.0;
            }

            int startIndex = (int)Math.Ceiling(q) - 1;
            int rangeCount = (int)Math.Floor(q*3) - startIndex + 1;
            double factor = q - ((rangeCount / 2.0) - 1);
            
            int sum = 0;

            /// Loop over data range skipping the first and last points to get the sum
            for (int i = startIndex + 1; i < startIndex + rangeCount - 1; i++) {
                sum += this.data[i];
            }
            
            /// Calculate the mean, adding the first and last data points multiplied by their factor.
            double mean = (sum + (this.data[startIndex] + this.data[startIndex + rangeCount - 1]) * factor) / (2 * q);
            return mean;
        }
    }
}