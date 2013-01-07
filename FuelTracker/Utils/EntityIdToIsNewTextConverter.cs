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

namespace FuelTracker
{
    public class EntityIdToIsNewTextConverter : IValueConverter
    {
        private const string IS_NOT_NEW_MESSAGE = "";
        private const string IS_NEW_MESSAGE = "Нов";

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null||!(value is int))
            {
                return string.Empty;
            }

            int entityId = (int)value;
            if (entityId > 0)
            {
                return IS_NOT_NEW_MESSAGE;
            }

            return IS_NEW_MESSAGE;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
