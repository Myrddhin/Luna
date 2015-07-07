using System;
using System.Globalization;
using System.Windows.Data;
using Loki.Common;

namespace Luna.UI.Converters
{
    public class PhoneNumberConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string buffer = value.SafeToString();
            if (!string.IsNullOrEmpty(buffer))
            {
                int jumps = 0;
                int group = 0;
                string temp = string.Empty;
                for (int i = buffer.Length - 1; i >= 0; i--)
                {
                    temp = buffer[i] + temp;
                    group++;
                    if (group != 0 && group % 2 == 0 && jumps <= 3)
                    {
                        temp = " " + temp;
                        group = 0;
                        jumps++;
                    }
                }

                buffer = temp;
            }

            return buffer;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string buffer = value.SafeToString();
            string temp = string.Empty;
            foreach (var character in buffer)
            {
                if (char.IsDigit(character) || character == '+')
                {
                    temp += character;
                }
            }

            return temp;
        }
    }
}