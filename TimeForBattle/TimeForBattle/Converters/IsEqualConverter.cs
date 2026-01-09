using System.Globalization;

namespace TimeForBattle.Converters;

public class IsEqualConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values[0] is null || values[1] is null)
            return false;

        int? number1 = null;
        if (values[0] is int)
        {
            number1 = (int)values[0];
        } else if (values[0] is String && int.TryParse((String)values[0], out int result))
        {
            number1 = result;
        }

        int? number2 = null;
        if (values[1] is int)
        {
            number2 = (int)values[1];
        }
        else if (values[1] is String && int.TryParse((String)values[1], out int result))
        {
            number2 = result;
        }

        if (number1 is not null && number2 is not null && number1 == number2)
            return true;

        return false;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}