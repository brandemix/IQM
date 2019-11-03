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
            Assert.Equal(1, set.Count);
        }

        [Fact]
        public void DataSet_AddPoint_OrdersSet()
        {
            set.AddPoint(2);
            set.AddPoint(1);
            Assert.Equal(new List<int> { 1, 2 }, set.Points);
        }

        [Fact]
        public void DataSet_AddPoint_Orders_Many() 
        {
            set.AddPoint(23);
            set.AddPoint(0);
            set.AddPoint(40);
            set.AddPoint(20);
            set.AddPoint(3);
            set.AddPoint(8);
            set.AddPoint(13);
            set.AddPoint(33);
            Assert.Equal(new List<int> {0, 3, 8, 13, 20, 23, 33, 40}, set.Points);
        }

        [Fact]
        public void DataSet_GetIQMean_Returns_0() 
        {
            set.AddPoint(2);
            set.AddPoint(1);
            Assert.Equal(0, set.GetIQMean());
        }

        [Fact]
        public void DataSet_GetIQMean_Returns_Mean() 
        {
            set.AddPoint(1);
            set.AddPoint(2);
            set.AddPoint(3);
            set.AddPoint(4);
            Assert.Equal(2.5, set.GetIQMean());
        }

        [Fact]
        public void DataSet_GetIQMean_Returns_Mean_Weighted() 
        {
            set.AddPoint(1);
            set.AddPoint(2);
            set.AddPoint(4);
            set.AddPoint(3);
            set.AddPoint(5);
            Assert.Equal(3, set.GetIQMean());
        }
    }
}
