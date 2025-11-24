using System.Globalization;

namespace TimeForBattle.Converters;

public class CreatureNameBoldConverter : IValueConverter
{

    object? IValueConverter.Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not null && value is bool isTurn)
            {
                if (isTurn)
                    return FontAttributes.Bold;
                else
                {
                    return FontAttributes.None;
                }
            }
            return value;
    }
        object? IValueConverter.ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value;
    }
}