using System.Globalization;

namespace TimeForBattle.Converters;

public class ConditionsConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object? parameter, CultureInfo culture)
    {
        String returnString = "";
        int conditionCount = 0;

        if (values is null || values.Length != 16)
            return returnString;

        String[] conditionNames = ["Blinded", "Charmed","Deafened", "Frightened", "Grappled", "Incapacitated", "Invisible", "Paralyzed", "Petrified", "Poisoned", "Prone", "Restrained", "Stunned", "Unconscious", "A", "B"];

        int i = 0;

        foreach (object value in values)
        {
            if (value is bool isCondition)
            {
                (conditionCount, returnString) = CheckCondition(isCondition, conditionNames[i], returnString, conditionCount);
            }
            i++;
        }

        if (conditionCount > 1)
        {
            returnString += " (+" + (conditionCount - 1).ToString() + ")";
        }

        Console.WriteLine(returnString);

        return returnString;
    }

    static public (int, String) CheckCondition (bool isCondition, String conditionName, String conditionString, int conditionCount)
    {
        if (isCondition)
        {
            if (conditionCount < 1)
            {
                if (conditionCount > 0)
                {
                    conditionString += ", ";
                }
                conditionString += conditionName;
            }

            conditionCount++;
        }

        Console.WriteLine(conditionString);

        return (conditionCount, conditionString);
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}