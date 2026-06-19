using System.Globalization;

namespace GP.Utils;

public class EmptyToZeroDecimalConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var amount = (decimal)(value ?? 0m);
        return amount == 0m ? string.Empty : amount.ToString(culture);
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var text = value as string;
        return decimal.TryParse(text, NumberStyles.Number, culture, out var result)
            ? result
            : 0m;
    }
}