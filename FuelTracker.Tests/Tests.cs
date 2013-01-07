using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FuelTracker.Tests
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void Is_2_plus_2_Equals_to_4()
        {
            int sum = 2 + 2;
            Assert.AreEqual(4, sum);
        }
    }
}