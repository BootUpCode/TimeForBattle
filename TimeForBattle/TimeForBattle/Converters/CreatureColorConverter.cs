using System.Globalization;

namespace TimeForBattle.Converters;

public class CreatureColorConverter : IValueConverter
{
    object? IValueConverter.Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not null && value is bool isTurn)
        {
            if (isTurn)
                return Colors.Black;
            else
            {
                return Colors.DarkRed;
            }
        }
        return value;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}