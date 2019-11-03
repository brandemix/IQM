using System;
using System.Collections.Generic;
using Xunit;
using IQM;

namespace IQMTest 
{
    public class QuartileTest
    {
        private readonly Quartile q;
        public QuartileTest()
        {
            this.q = new Quartile();
        }
        [Fact]
        public void AddPoint_Single_Equal_Max_Min()
        {
            this.q.AddPoint(2);
            Assert.Equal(1, this.q.Count);
            Assert.Equal(2, this.q.Max());
            Assert.Equal(2, this.q.Min());
            Assert.Equal(new List<int> {2}, this.q.Set);
        }
        [Fact]
        public void AddPoint_Multiple_Max_Min()
        {
            this.q.AddPoint(4);
            this.q.AddPoint(5);
            Assert.Equal(2, this.q.Count);
            Assert.Equal(5, this.q.Max());
            Assert.Equal(4, this.q.Min());
        }
        [Fact]
        public void RetrieveMax_SinglePoint()
        {
            this.q.AddPoint(4);
            Assert.Equal(4, this.q.RetrieveMax());
            Assert.Equal(int.MinValue, this.q.Max());
            Assert.Equal(int.MaxValue, this.q.Min());
        }
        [Fact]
        public void RetrieveMax_MultiplePoint()
        {
            this.q.AddPoint(4);
            this.q.AddPoint(5);
            Assert.Equal(5, this.q.RetrieveMax());
            Assert.Equal(4, this.q.Max());
            Assert.Equal(4, this.q.Min());
            Assert.Equal(new List<int> {4}, this.q.Set);
        }
        [Fact]
        public void RetrieveMin_MultiplePoint()
        {
            this.q.AddPoint(4);
            this.q.AddPoint(5);
            Assert.Equal(4, this.q.RetrieveMin());
            Assert.Equal(new List<int> {5}, this.q.Set);
        }
        [Fact]
        public void RetrieveMin_Duplicates()
        {
            this.q.AddPoint(1);
            this.q.AddPoint(1);
            Assert.Equal(1, this.q.RetrieveMin());
            Assert.Equal(1, this.q.Max());
            Assert.Equal(1, this.q.Min());
        }
        [Fact]
        public void Mean()
        {
            this.q.AddPoint(1);
            this.q.AddPoint(2);
            this.q.AddPoint(3);
            Assert.Equal(2.0, this.q.Mean());
        }
        [Fact]
        public void WeightedMean() {
            this.q.AddPoint(4);
            this.q.AddPoint(3);
            this.q.AddPoint(2);
            Assert.Equal(3, this.q.WeightedMean(1.25));
        }
    }
    public class IQSetTest
    {
        private readonly IQSet set;
        public IQSetTest()
        {
            this.set = new IQSet();
        }
        [Fact]
        public void AddPoint_Count()
        {
            this.set.AddPoint(1);
            Assert.Equal(1, this.set.Count);
        }
        [Fact]
        public void AddPoint_FirstFour()
        {
            this.set.AddPoint(3);
            Assert.Equal(new List<int> {3}, this.set.FourthQuartile);
            this.set.AddPoint(1);
            Assert.Equal(new List<int> {1}, this.set.FirstQuartile);
            this.set.AddPoint(4);
            Assert.Equal(new List<int> {4}, this.set.FourthQuartile);
            Assert.Equal(new List<int> {3}, this.set.InnerQuartile);
            Assert.Equal(new List<int> {1}, this.set.FirstQuartile);
            this.set.AddPoint(2);
            Assert.Equal(new List<int> {4}, this.set.FourthQuartile);
            Assert.Equal(new List<int> {3, 2}, this.set.InnerQuartile);
            Assert.Equal(new List<int> {1}, this.set.FirstQuartile);
        }
        [Fact]
        public void AddPoint_MoreThanFour()
        {
            this.set.AddPoint(3);
            Assert.Equal(new List<int> {3}, this.set.FourthQuartile);
            this.set.AddPoint(4);
            Assert.Equal(new List<int> {4}, this.set.FourthQuartile);
            Assert.Equal(new List<int> {3}, this.set.FirstQuartile);
            this.set.AddPoint(1);
            Assert.Equal(new List<int> {4}, this.set.FourthQuartile);
            Assert.Equal(new List<int> {3}, this.set.InnerQuartile);
            Assert.Equal(new List<int> {1}, this.set.FirstQuartile);
            this.set.AddPoint(2);
            Assert.Equal(new List<int> {4}, this.set.FourthQuartile);
            Assert.Equal(new List<int> {3, 2}, this.set.InnerQuartile);
            Assert.Equal(new List<int> {1}, this.set.FirstQuartile);
            this.set.AddPoint(1);
            Assert.Equal(new List<int> {4}, this.set.FourthQuartile);
            Assert.Equal(new List<int> {3, 2, 1}, this.set.InnerQuartile);
            Assert.Equal(new List<int> {1}, this.set.FirstQuartile);
            this.set.AddPoint(6);
            Assert.Equal(new List<int> {6}, this.set.FourthQuartile);
            Assert.Equal(new List<int> {3, 2, 1, 4}, this.set.InnerQuartile);
            Assert.Equal(new List<int> {1}, this.set.FirstQuartile);
        }
        [Fact]
        public void AddPoint_InnerQuartileMean_LessThanFour() {
            this.set.AddPoint(1);
            Assert.Equal(0, this.set.IQM());
        }
        [Fact]
        public void IQM_MoreThanFour() {
            this.set.AddPoint(1);
            this.set.AddPoint(2);
            this.set.AddPoint(3);
            this.set.AddPoint(4);
            Assert.Equal(2.5, this.set.IQM());
        }
        [Fact]
        public void Q_Weight() {
            this.set.AddPoint(1);
            this.set.AddPoint(2);
            this.set.AddPoint(3);
            this.set.AddPoint(4);
            this.set.AddPoint(5);
            Assert.Equal(1.25, this.set.Q);
        }
        [Fact]
        public void IQM_NotDivisible_ByFour() {
            this.set.AddPoint(1);
            this.set.AddPoint(2);
            this.set.AddPoint(3);
            this.set.AddPoint(4);
            this.set.AddPoint(5);
            Assert.Equal(3, this.set.IQM());
        }
        [Fact]
        public void IQM_NotDivisible_ByFour_Again() {
            this.set.AddPoint(301);
            this.set.AddPoint(286);
            this.set.AddPoint(287);
            this.set.AddPoint(292);
            this.set.AddPoint(311);
            this.set.AddPoint(314);
            Assert.Equal(new List<int> {286}, this.set.FirstQuartile);
            Assert.Equal(new List<int> {287, 292, 301, 311}, this.set.InnerQuartile);
            Assert.Equal(new List<int> {314}, this.set.FourthQuartile);
            Assert.Equal(297.33, Math.Round(this.set.IQM(), 2));
        }
        [Fact]
        public void IQM_Index_9() {
            this.set.AddPoint(301);
            this.set.AddPoint(286);
            this.set.AddPoint(287);
            this.set.AddPoint(292);
            this.set.AddPoint(311);
            this.set.AddPoint(314);
            this.set.AddPoint(303);
            this.set.AddPoint(312);
            this.set.AddPoint(299);
            Assert.Equal(new List<int> {286, 287}, this.set.FirstQuartile);
            Assert.Equal(new List<int> {292, 301, 311, 303, 299}, this.set.InnerQuartile);
            Assert.Equal(new List<int> {314, 312}, this.set.FourthQuartile);
            Assert.Equal(301.17, Math.Round(this.set.IQM(), 2));
        }
    }
}