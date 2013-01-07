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

namespace FuelTracker.Models
{
    public class Ninja
    {
        public int NinjaId { get; set; }
        public int Name { get; set; }
        public Master Master { get; set; }
        public int MasterId { get; set; }
    }
}
