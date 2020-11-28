﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

#if (NETFX || WINDOWS_PHONE)
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
#elif (NETFX_CORE)
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
#elif (XAMARIN)
using Xamarin.Forms;
#endif

namespace ValueConverters
{
    /// <summary>
    /// Value converters which aggregates the results of a sequence of converters: Converter1 >> Converter2 >> Converter3
    /// The output of converter N becomes the input of converter N+1.
    /// </summary>
#if (NETFX || XAMARIN || WINDOWS_PHONE)
    [ContentProperty(nameof(Converters))]
#elif (NETFX_CORE)
    [ContentProperty(Name = nameof(Converters))]
#endif
    public class ValueConverterGroup : SingletonConverterBase<ValueConverterGroup>
    {
        public List<IValueConverter> Converters { get; set; } = new List<IValueConverter>();

        protected override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return this.Converters?.Aggregate(value, (current, converter) => converter.Convert(current, targetType, parameter, culture));
        }

        protected override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return this.Converters?.Reverse<IValueConverter>().Aggregate(value, (current, converter) => converter.Convert(current, targetType, parameter, culture));
        }
    }
}
