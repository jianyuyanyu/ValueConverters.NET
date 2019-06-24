﻿using System;
using System.Globalization;

namespace ValueConverters
{
    public class AddConverter : SingletonConverterBase<AddConverter>
    {
        protected override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (double.TryParse(value?.ToString(), NumberStyles.Any, culture, out var basis)
              && double.TryParse(parameter?.ToString(), NumberStyles.Any, culture, out var subtract))
                return basis + subtract;

            return value;
        }
    }
}
