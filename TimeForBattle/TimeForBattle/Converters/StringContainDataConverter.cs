using System.Globalization;

namespace TimeForBattle.Converters;

public class StringContainDataConverter : IValueConverter
{

    object? IValueConverter.Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string stringVal)
        {
            return !string.IsNullOrEmpty(stringVal);
        } else if (value is int val && val != 0)
        {
            return true;
        }

        return false;
    }

    object? IValueConverter.ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
