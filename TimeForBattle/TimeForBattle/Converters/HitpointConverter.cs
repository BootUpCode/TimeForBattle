using System.Globalization;

namespace TimeForBattle.Converters;

public class HitpointConverter : IValueConverter
{

    object? IValueConverter.Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is int currentHP && parameter is Label param)
        {
            if (param.Text is not null && int.Parse(param.Text) is int maximumHP)
            {
                float widthPercentage = 220 * ((float)currentHP / (float)maximumHP);
                return (int)widthPercentage;
            }
        }

        return value;
    }

    object? IValueConverter.ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value;
    }
}
