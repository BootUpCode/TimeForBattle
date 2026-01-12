using System.Globalization;

namespace TimeForBattle.Converters;

public class StringsContainDataConverter : IMultiValueConverter
{
    object? IMultiValueConverter.Convert(object[] values, Type targetType, object? parameter, CultureInfo culture)
    {
        bool returnBool = false;

        foreach (object obj in values)
        {
            if (obj is string checkString && !String.IsNullOrEmpty(checkString)) {
                returnBool = true;
            }
        }

        return returnBool;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
