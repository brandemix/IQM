using System;
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
        }
        [Fact]
        public void AddPoint_Multiple_Max_Min()
        {
            this.q.AddPoint(5);
            this.q.AddPoint(1);
            Assert.Equal(2, this.q.Count);
            Assert.Equal(5, this.q.Max());
            Assert.Equal(1, this.q.Min());
        }
        [Fact]
        public void RetrieveMax_SinglePoint()
        {
            this.q.AddPoint(4);
            int max = this.q.RetrieveMax();
            Assert.Equal(4, max);
            Assert.Equal(0, this.q.Max());
            Assert.Equal(0, this.q.Min());
        }
        [Fact]
        public void RetrieveMax_MultiplePoint()
        {
            this.q.AddPoint(1);
            this.q.AddPoint(2);
            int max = this.q.RetrieveMax();
            Assert.Equal(2, max);
            Assert.Equal(1, this.q.Max());
            Assert.Equal(1, this.q.Min());
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
    }
}