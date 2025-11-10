using System.Globalization;

namespace TimeForBattle.Converters;

public class AttributeConverter : IMultiValueConverter
{
    object? IMultiValueConverter.Convert(object[] values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values[0] is null || values[1] is null || values[2] is null)
            return 0;

        //Get proficiency
        if (values[0] is Creature creature && values[1] is bool isProficient && values[2] is string attributeName)
        {

            //Calculate roll modifier from the attribute score
            int attributeScore = 10;

            {
                switch (attributeName)
                {
                    case "Str":
                        attributeScore = creature.StrScore;
                        break;
                    case "Dex":
                        attributeScore = creature.DexScore;
                        break;
                    case "Con":
                        attributeScore = creature.ConScore;
                        break;
                    case "Int":
                        attributeScore = creature.IntScore;
                        break;
                    case "Wis":
                        attributeScore = creature.WisScore;
                        break;
                    case "Cha":
                        attributeScore = creature.ChaScore;
                        break;
                }
            }

            double x = ((double)attributeScore - 10) / 2;
            int modifier = (int)Math.Floor(x);

            //Add proficiency bonus to the roll, if proficient
            if (isProficient)
                modifier += creature.ProficiencyBonus;

            if (modifier >= 0)
            {
                return "+" + modifier.ToString();
            }
            else
            {
                return modifier.ToString();
            }
        }

        return 0;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}