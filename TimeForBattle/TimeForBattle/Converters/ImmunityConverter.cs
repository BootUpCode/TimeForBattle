using System.Globalization;

namespace TimeForBattle.Converters;

public class ImmunityConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object? parameter, CultureInfo culture)
    {
        String returnString = "";

        if (values is null)
            return returnString;

        if (values[0] is String condition && values[1] is String immunityString)
        {
            if (immunityString.Contains(condition) || immunityString.Contains(char.ToLower(condition[0]) + condition.Substring(1))) {
                returnString = "(Immune)";
            }
        }

        return returnString;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}