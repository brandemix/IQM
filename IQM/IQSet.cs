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
        public int Max()
        {
            if (this.max_index == -1) {
                return 0;
            }

            return this.set[this.max_index];
        }
        public int Min()
        {
            if (this.min_index == -1) {
                return 0;
            }

            return this.set[this.min_index];
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
    }
}