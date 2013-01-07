using System;
using System.Windows.Data;
using System.Globalization;
using System.Windows;

namespace FuelTracker
{
    public class IsZeroToTextConverter : IValueConverter
    {
        string falseValue;
        public string FalseValue
        {
            get { return falseValue; }
            set { falseValue = value; }
        }

        string trueValue;
        public string TrueValue
        {
            get { return trueValue; }
            set { trueValue = value; }
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return FalseValue;
            }

            int intValue = (int)value;
            string convertedValue = (intValue == 0) ? TrueValue : FalseValue;

            return convertedValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

}
