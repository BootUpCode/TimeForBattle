using System.Globalization;

namespace TimeForBattle.Converters;

public class AttributeConverter : IMultiValueConverter
{
    object? IMultiValueConverter.Convert(object[] values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values[0] is null || values[1] is null || values[2] is null)
            return 0;

        //Get proficiency
        if (values[2] is int proficiencyBonus)
        {
            bool isProficient = false;
            if (values[1] is bool)
            {
                isProficient = (bool)values[1];
            }
            else if (values[1] is string && (values[1].Equals("True") || values[1].Equals("true") || values[1].Equals("1")))
                {
                isProficient = true;
            }

            //Calculate roll modifier from the attribute score
            int attributeScore = 10;
            if (values[0] is int)
            {
                attributeScore = (int)values[0];
            }
            else if (values[0] is string attributeName && values[3] is Creature creature)
            {
                switch (attributeName)
                {
                    case "Str":
                    case "1":
                        attributeScore = creature.StrScore;
                        break;
                    case "Dex":
                    case "2":
                        attributeScore = creature.DexScore;
                        break;
                    case "Con":
                    case "3":
                        attributeScore = creature.ConScore;
                        break;
                    case "Int":
                    case "4":
                        attributeScore = creature.IntScore;
                        break;
                    case "Wis":
                    case "5":
                        attributeScore = creature.WisScore;
                        break;
                    case "Cha":
                    case "6":
                        attributeScore = creature.ChaScore;
                        break;
                }
            }

            double x = ((double)attributeScore - 10) / 2;
            int modifier = (int)Math.Floor(x);

            //Add proficiency bonus to the roll, if proficient
            if (isProficient)
                modifier += proficiencyBonus;

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