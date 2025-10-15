using System.Globalization;

namespace TimeForBattle.Converters;

public class AttributeConverter : IMultiValueConverter
{
    object? IMultiValueConverter.Convert(object[] values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values[0] is null || values[1] is null || values[2] is null)
            return 0;

        //Get proficiency
        if (values[0] is Creature creature && values[1] is bool isSave && values[2] is string attributeName)
        {

            //Calculate roll modifier from the attribute score
            int attributeScore = 10;
            bool isProficient = false;

            {
                switch (attributeName)
                {
                    case "Str":
                        attributeScore = creature.StrScore;
                        isProficient = creature.StrSaveProf;
                        break;
                    case "Dex":
                        attributeScore = creature.DexScore;
                        isProficient = creature.DexSaveProf;
                        break;
                    case "Con":
                        attributeScore = creature.ConScore;
                        isProficient = creature.ConSaveProf;
                        break;
                    case "Int":
                        attributeScore = creature.IntScore;
                        isProficient = creature.IntSaveProf;
                        break;
                    case "Wis":
                        attributeScore = creature.WisScore;
                        isProficient = creature.WisSaveProf;
                        break;
                    case "Cha":
                        attributeScore = creature.ChaScore;
                        isProficient = creature.ChaSaveProf;
                        break;
                }
            }

            double x = ((double)attributeScore - 10) / 2;
            int modifier = (int)Math.Floor(x);

            //Add proficiency bonus to the roll, if proficient
            if (isSave && isProficient)
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