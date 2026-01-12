using System.Globalization;

namespace TimeForBattle.Converters;

public class ConditionsConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object? parameter, CultureInfo culture)
    {
        String returnString = "";
        int conditionCount = 0;

        if (values is null)
            return returnString;

        if (values[0] is String condition1 && values[1] is String condition2 && values[2] is String condition3 && values[3] is String condition4 && values[4] is String condition5)
        {
            String[] conditionStrings = [condition1, condition2, condition3, condition4, condition5];
            
            foreach (String conditionString in conditionStrings) {
                if (!String.IsNullOrEmpty(conditionString))
                {
                    if (String.IsNullOrEmpty(returnString))
                    {
                        returnString = conditionString;
                    }
                    conditionCount++;
                }
            }
        }

        if (conditionCount > 1)
        {
            returnString += " (+" + (conditionCount - 1).ToString() + ")";
        }

        return returnString;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}