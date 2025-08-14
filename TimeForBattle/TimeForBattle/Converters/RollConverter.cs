using System.Globalization;

namespace TimeForBattle.Converters;

public class RollConverter : IMultiValueConverter
{
    object? IMultiValueConverter.Convert(object[] values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values[0] is null || values[1] is null || values[2] is null || values[4] is null)
            return new Tuple<int?, string?, string?>(null, null, null);

        if (values[0] is InitiativeCreature creature && values[1] is string attributeName && values[2] is string isSaveString && values[3] is string isProficientString && values[4] is string rollName)
        {
            bool isSave = false;
            bool isProficient = false;
            int attributeScore = 10;

            if (isSaveString.Equals("True") || isSaveString.Equals("true") || isSaveString.Equals("1")) { isSave = true; }
            if (!isSave && (isProficientString.Equals("True") || isProficientString.Equals("true") || isProficientString.Equals("1"))) { isProficient = true; }

            switch(attributeName) {
                case "Str":
                case "1":
                    attributeScore = creature.StrScore;
                    if (isSave) { isProficient = creature.StrSaveProf; }
                    break;
                case "Dex":
                case "2":
                    attributeScore = creature.DexScore;
                    if (isSave) { isProficient = creature.DexSaveProf; }
                    break;
                case "Con":
                case "3":
                    attributeScore = creature.ConScore;
                    if (isSave) { isProficient = creature.ConSaveProf; }
                    break;
                case "Int":
                case "4":
                    attributeScore = creature.IntScore;
                    if (isSave) { isProficient = creature.IntSaveProf; }
                    break;
                case "Wis":
                case "5":
                    attributeScore = creature.WisScore;
                    if (isSave) { isProficient = creature.WisSaveProf; }
                    break;
                case "Cha":
                case "6":
                    attributeScore = creature.ChaScore;
                    if (isSave) { isProficient = creature.ChaSaveProf; }
                    break;
            }

            double x = ((double)attributeScore - 10) / 2;
            int modifier = (int)Math.Floor(x);

            if (isProficient)
                modifier += creature.ProficiencyBonus;

            return new Tuple<int?, string?, string?>(modifier, rollName, creature.Name);
        }

        return new Tuple<int?, string?, string?>(null, null, null);
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}