using System.Globalization;

namespace TimeForBattle.Converters;

public class IsNotEqualConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        int? number1 = null;
        int? number2 = null;

        if (value is not null)
        {
            if (value is int)
            {
                number1 = (int)value;
            }
            else if (value is String && int.TryParse((String)value, out int result))
            {
                number1 = result;
            }
        }

        if (parameter is not null)
        {
            if (parameter is int)
            {
                number2 = (int)parameter;
            }
            else if (parameter is String && int.TryParse((String)parameter, out int result))
            {
                number2 = result;
            }
        }

        if (number1 is not null && number2 is not null && number1 == number2)
            return false;

        return true;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}