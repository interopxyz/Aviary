using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Parrot.Collections
{
    public class pModifiers
    {
        public pModifiers()
        {
        }

        public CalendarMode CalendarDisplayMode(int mode)
        {
            switch (mode)
            {
                case (0):
                    return CalendarMode.Month;
                case (1):
                    return CalendarMode.Year;
                case (2):
                    return CalendarMode.Decade;
                default:
                    return CalendarMode.Month;
            }
        }

        public CalendarSelectionMode CalendarSelection(int mode)
        {
            switch (mode)
            {
                case (0):
                    return CalendarSelectionMode.SingleDate;
                case (1):
                    return CalendarSelectionMode.SingleRange;
                default:
                    return CalendarSelectionMode.SingleDate;
            }
        }

        public HorizontalAlignment Halign(int direction)
        {
            switch (direction)
            {
                case (1):
                    return HorizontalAlignment.Left;
                case (2):
                    return HorizontalAlignment.Center;
                case (3):
                    return HorizontalAlignment.Right;
                default:
                    return HorizontalAlignment.Stretch;
            }
        }

        public VerticalAlignment Valign(int direction)
        {
            switch (direction)
            {
                case (1):
                    return VerticalAlignment.Top;
                case (2):
                    return VerticalAlignment.Center;
                case (3):
                    return VerticalAlignment.Bottom;
                default:
                    return VerticalAlignment.Stretch;
            }
        }
    }
}
