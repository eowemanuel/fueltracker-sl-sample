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
using System.Windows.Data;
using System.Globalization;

namespace FuelTracker
{
    public class BoolToTextConverter : IValueConverter
    {
        string _falseValue;
        public string FalseValue
        {
            get
            {
                return _falseValue;
            }
            set
            {
                _falseValue = value;
            }
        }

        string _trueValue;
        public string TrueValue
        {
            get
            {
                return _trueValue;
            }
            set
            {
                _trueValue = value;
            }
        }

        private string _defaultValue;
        public string DefaultValue
        {
            get
            {
                return _defaultValue;
            }
            set
            {
                _defaultValue = value;
            }
        }

        public BoolToTextConverter()
        {
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            
            if (value == null)
            {
                return DefaultValue;
            }

            bool boolValue = (bool)value;
            string convertedValue = boolValue ? TrueValue : FalseValue;

            return convertedValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
