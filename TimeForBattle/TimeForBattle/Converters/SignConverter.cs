using System.Globalization;

namespace TimeForBattle.Converters;

public class SignConverter : IValueConverter
{
    object? IValueConverter.Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is null)
            return 0;

        if (value is int modifier)
        {
            if (modifier >= 0)
            {
                return "+" + modifier.ToString();
            }
            else
            {
                return modifier.ToString();
            }
        }

        return 0;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}