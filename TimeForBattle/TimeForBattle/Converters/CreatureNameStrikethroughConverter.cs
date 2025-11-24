using System.Globalization;

namespace TimeForBattle.Converters;

public class CreatureNameStrikethroughConverter : IValueConverter
{

    object? IValueConverter.Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not null && value is int CurrentHitpoints)
        {
            if (CurrentHitpoints <= 0)
            {
                return TextDecorations.Strikethrough;
            }
            else
            {
                return TextDecorations.None;
            }
        }

        return value;
    }

    object? IValueConverter.ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value;
    }
}