using System.Globalization;

namespace TimeForBattle.Converters;

public class AttributeConverter : IMultiValueConverter
{
    object? IMultiValueConverter.Convert(object[] values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values[0] is null || values[1] is null || values[2] is null)
            return 0;

        if (values[0] is Creature creature && values[1] is string attributeName)
        {
            bool isProficient = false;
            if (values[2] is bool)
            {
                isProficient = (bool)values[2];
            }
            else if (values[2] is string && (values[2].Equals("True") || values[2].Equals("true") || values[2].Equals("1")))
                {
                isProficient = true;
            }

            //Set attribute score associated with the roll, and if the roll is a saving throw, set if roll is with proficiency bonus
            int attributeScore = 10;
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

            //Calculate roll modifier from the attribute score
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