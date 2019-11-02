using System;
using System.Collections.Generic;
using Xunit;
using IQM;

namespace IQMTest
{
    public class DataSetTest
    {
        private readonly DataSet set;

        public DataSetTest() 
        {
            set = new DataSet();
        }

        [Fact]
        public void DataSet_AddPoint_IncrementsSet()
        {
            set.AddPoint(1);
            Assert.Equal(1, set.Count());
        }

        [Fact]
        public void DataSet_AddPoint_OrdersSet()
        {
            set.AddPoint(2);
            set.AddPoint(1);
            Assert.Equal(new List<int> { 1, 2 }, set.Points);
        }

        [Fact]
        public void DataSet_GetMean_zer0() {
            set.AddPoint(2);
            set.AddPoint(1);
            Assert.Equal(0, set.GetMean());
        }

        [Fact]
        public void DataSet_GetMean_four() {
            set.AddPoint(1);
            set.AddPoint(2);
            set.AddPoint(3);
            set.AddPoint(4);
            Assert.Equal(2.5, set.GetMean());
        }

        [Fact]
        public void DataSet_GetMean_Not_four() {
            set.AddPoint(1);
            set.AddPoint(2);
            set.AddPoint(4);
            set.AddPoint(3);
            set.AddPoint(5);
            Assert.Equal(3, set.GetMean());
        }
    }
}
