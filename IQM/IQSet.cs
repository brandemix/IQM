using System;
using System.Collections.Generic;

namespace IQM
{
    public class Quartile
    {
        private List<int> set;
        private int max_index;
        private int min_index;
        public Quartile()
        {
            this.set = new List<int>();
            this.max_index = -1;
            this.min_index = -1;
        }
        public int Count
        {
            get => this.set.Count;
        }
        public List<int> Set {
            get => this.set;
        }
        public int Max()
        {
            if (this.max_index == -1) {
                return int.MinValue;
            }

            return this.set[this.max_index];
        }
        public int Min()
        {
            if (this.min_index == -1) {
                return int.MaxValue;
            }

            return this.set[this.min_index];
        }
        public double Mean()
        {
            if (this.Count == 0) {
                return 0;
            }

            double sum = 0;
            for(int i = 0; i < this.Count; i++) {
                sum += this.set[i];
            }
            return (double)sum / this.Count;
        }
        public double WeightedMean(double q)
        {
            if (this.Count == 0) {
                return 0;
            }

            if (q == 0) {
                throw new DivideByZeroException();
            }

            double sum = 0;
            for (int i = 0; i < this.Count; i++) {
                if (i == this.max_index || i == this.min_index) {
                    continue;
                }
                sum += this.set[i];
            }

            double weight = q - ((this.Count / 2.0) - 1);
            sum += (weight * (this.set[this.max_index] + this.set[this.min_index]));
            return sum / (q * 2.0);
        }
        public void AddPoint(int point)
        {
            this.set.Add(point);
            if (this.max_index == -1) {
                this.max_index = 0;
                this.min_index = this.max_index;
                return;
            }

            if (point > this.set[this.max_index]) {
                this.max_index = this.set.Count - 1;
            } else if (point < this.set[this.min_index]) {
                this.min_index = this.set.Count - 1;
            }
        }
        public int RetrieveMax()
        {
            int max = this.set[this.max_index];
            this.set.RemoveAt(this.max_index);
            this.AdjustMinMax();
            return max;
        }
        public int RetrieveMin()
        {
            int min = this.set[this.min_index];
            this.set.RemoveAt(this.min_index);
            this.AdjustMinMax();
            return min;
        }
        private void AdjustMinMax()
        {
            if (this.set.Count == 0) {
                this.max_index = -1;
                this.min_index = -1;
                return;
            }

            int maxVal = int.MinValue;
            int minVal = int.MaxValue;

            for (int i = 0; i < this.set.Count; i++) {
                if (this.set[i] > maxVal) {
                    this.max_index = i;
                }

                if (this.set[i] < minVal) {
                    this.min_index = i;
                }
            }
        }
    }
    public class IQSet
    {
        private Quartile firstQuartile;
        private Quartile innerQuartile;
        private Quartile fourthQuartile;
        public IQSet() 
        {
            this.firstQuartile = new Quartile();
            this.innerQuartile = new Quartile();
            this.fourthQuartile = new Quartile();
        }
        public int Count 
        {
            get => this.firstQuartile.Count + this.innerQuartile.Count + this.fourthQuartile.Count;
        }
        public double Q
        {
            get => (double)this.Count / 4;
        }
        public List<int> FirstQuartile {
            get => this.firstQuartile.Set;
        }
        public List<int> InnerQuartile {
            get => this.innerQuartile.Set;
        }
        public List<int> FourthQuartile {
            get => this.fourthQuartile.Set;
        }
        public void AddPoint(int point)
        {
            if (this.Count < 4) {
                if (point > this.fourthQuartile.Max()) {
                    this.fourthQuartile.AddPoint(point);
                    if (this.fourthQuartile.Count > 1) {
                        point = this.fourthQuartile.RetrieveMin();
                    } else {
                        return;
                    }
                }

                if (point < this.firstQuartile.Min()) {
                    this.firstQuartile.AddPoint(point);
                    if (this.firstQuartile.Count > 1) {
                        point = this.firstQuartile.RetrieveMax();
                    } else {
                        return;
                    }
                }

                this.innerQuartile.AddPoint(point);
                return;
            }

            // double nextQ = (this.Count + 1) / 4;
            if ((this.Count + 1) % 4 == 0) {
                this.innerQuartile.AddPoint(point);
                int min = this.innerQuartile.RetrieveMin();
                int max = this.innerQuartile.RetrieveMax();
                this.firstQuartile.AddPoint(min);
                this.fourthQuartile.AddPoint(max);
            } else if (point < this.firstQuartile.Max()) {
                this.firstQuartile.AddPoint(point);
                int max = this.firstQuartile.RetrieveMax();
                this.innerQuartile.AddPoint(max);
            } else if (point > this.fourthQuartile.Min()) {
                this.fourthQuartile.AddPoint(point);
                int min = this.fourthQuartile.RetrieveMin();
                this.innerQuartile.AddPoint(min);
            } else {
                this.innerQuartile.AddPoint(point);
            }
        }
        public double IQM()
        {
            if (this.Count < 4) {
                return 0.0;
            }

            if (this.Count % 4 == 0) {
                return this.innerQuartile.Mean();
            }

            return this.innerQuartile.WeightedMean(this.Q);
        }
    }
}