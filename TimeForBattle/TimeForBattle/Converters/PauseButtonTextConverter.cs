using System.Globalization;

namespace TimeForBattle.Converters;

public class PauseButtonTextConverter : IValueConverter
{
    object? IValueConverter.Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not null && value is bool combatIsStarted)
        {
            if (!combatIsStarted)
                return "Manage Creatures";
            else
            {
                return "Pause Combat to Manage Creatures";
            }
        }
        return value;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}