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
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.ComponentModel;
using System.Collections.ObjectModel;
using GeomindMe.ViewModels;

namespace GeomindMe.Models
{
    public class ToDoItem : ViewModelBase
    {
        private int _toDoItemId;
        public int ToDoItemId
        {
            get
            {
                return _toDoItemId;
            }

            set
            {
                if (_toDoItemId == value)
                {
                    return;
                }
                _toDoItemId = value;
                RaisePropertyChanged("ToDoItemId");
            }
        }

        private string _text;
        public string Text
        {
            get
            {
                return _text;
            }

            set
            {
                if (_text == value)
                {
                    return;
                }
                _text = value;
                RaisePropertyChanged("Text");
            }
        }

        private int _priority;
        public int Priority
        {
            get
            {
                return _priority;
            }

            set
            {
                if (_priority == value)
                {
                    return;
                }
                _priority = value;
                RaisePropertyChanged("Priority");
            }
        }

        private string _locationAddress;
        public string LocationAddress
        {
            get
            {
                return _locationAddress;
            }

            set
            {
                if (_locationAddress == value)
                {
                    return;
                }
                _locationAddress = value;
                RaisePropertyChanged("Location");
            }
        }

        private double _locationXCoordinate;
        public double LocationXCoordinate
        {
            get
            {
                return _locationXCoordinate;
            }

            set
            {
                if (_locationXCoordinate == value)
                {
                    return;
                }
                _locationXCoordinate = value;
                RaisePropertyChanged("LocationXCoordinate");
            }
        }

        private double _locationYCoordinate;
        public double LocationYCoordinate
        {
            get
            {
                return _locationYCoordinate;
            }

            set
            {
                if (_locationYCoordinate == value)
                {
                    return;
                }
                _locationYCoordinate = value;
                RaisePropertyChanged("LocationYCoordinate");
            }
        }

        private int _reminderRange;
        public int ReminderRange
        {
            get
            {
                return _reminderRange;
            }

            set
            {
                if (_reminderRange == value)
                {
                    return;
                }
                _reminderRange = value;
                RaisePropertyChanged("ReminderRange");
            }
        }

        private bool _reminderIsEnabled;
        public bool ReminderIsEnabled
        {
            get
            {
                return _reminderIsEnabled;
            }

            set
            {
                if (_reminderIsEnabled == value)
                {
                    return;
                }
                _reminderIsEnabled = value;
                RaisePropertyChanged("ReminderIsEnabled");
            }
        }
    }


}
