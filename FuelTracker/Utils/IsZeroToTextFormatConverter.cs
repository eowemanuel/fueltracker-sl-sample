using System;
using System.Windows.Data;
using System.Globalization;
using System.Windows;

namespace FuelTracker
{
    public class IsZeroToTextFormatConverter : IValueConverter
    {
        string falseValueFormat;
        public string FalseValueFormat
        {
            get { return falseValueFormat; }
            set { falseValueFormat = value; }
        }

        string trueValueFormat;
        public string TrueValueFormat
        {
            get { return trueValueFormat; }
            set { trueValueFormat = value; }
        }

        string defaultValue;
        public string DefaultValueFormat
        {
            get { return defaultValue; }
            set { defaultValue = value; }
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string convertedValueFormat = string.Empty;
            if (value == null)
            {
                convertedValueFormat = DefaultValueFormat;
            }
            else
            {
                int intValue = (int)value;
                convertedValueFormat = (intValue == 0) ? TrueValueFormat : FalseValueFormat;
            }

            string strParameter = string.Empty;
            if(parameter != null)
            {
                strParameter = (string)parameter;
            }

            string convertedValue = string.Format(convertedValueFormat, strParameter);

            return convertedValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

}
