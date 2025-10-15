using System.Globalization;

namespace TimeForBattle.Converters;

public class RollConverter : IMultiValueConverter
{
    object? IMultiValueConverter.Convert(object[] values, Type targetType, object? parameter, CultureInfo culture)
    {
        //Input values:
        //0: Creature: Creature creature
        //1: Attribute name: string attributeName, can be Str/Dex/Con/Int/Wis/Cha
        //2: Save: string isSaveString, can be True/False, true/false, 0/1
        //3: Proficiency: bool, can be true/false/null, or string, can be True/False, true/false, 0/1
        //4: Roll name: string rollName

        if (values[0] is null || values[1] is null || values[2] is null || values[4] is null)
            return new Tuple<int?, string?, string?, string?, string?>(null, null, null, null, null);

        if (values[0] is InitiativeCreature initiativeCreature && values[1] is string attributeName && values[2] is bool isSave)
        {

            //If the roll is not a saving throw, set if roll is with proficiency bonus
            bool isProficient = false;
            if (!isSave)
            {
                if (values[3] is bool)
                {
                    isProficient = (bool)values[3];
                }
                else if (values[3] is string && (values[3].Equals("True") || values[3].Equals("true") || values[3].Equals("1")))
                {
                    isProficient = true;
                }
            }

            //Set attribute score associated with the roll, and if the roll is a saving throw, set if roll is with proficiency bonus
            int attributeScore = 10;
            switch (attributeName) {
                case "Str":
                case "0":
                    attributeScore = initiativeCreature.Creature.StrScore;
                    if (isSave) { isProficient = initiativeCreature.Creature.StrSaveProf; }
                    break;
                case "Dex":
                case "1":
                    attributeScore = initiativeCreature.Creature.DexScore;
                    if (isSave) { isProficient = initiativeCreature.Creature.DexSaveProf; }
                    break;
                case "Con":
                case "2":
                    attributeScore = initiativeCreature.Creature.ConScore;
                    if (isSave) { isProficient = initiativeCreature.Creature.ConSaveProf; }
                    break;
                case "Int":
                case "3":
                    attributeScore = initiativeCreature.Creature.IntScore;
                    if (isSave) { isProficient = initiativeCreature.Creature.IntSaveProf; }
                    break;
                case "Wis":
                case "4":
                    attributeScore = initiativeCreature.Creature.WisScore;
                    if (isSave) { isProficient = initiativeCreature.Creature.WisSaveProf; }
                    break;
                case "Cha":
                case "5":
                    attributeScore = initiativeCreature.Creature.ChaScore;
                    if (isSave) { isProficient = initiativeCreature.Creature.ChaSaveProf; }
                    break;
            }

            //Calculate roll modifier from the attribute score
            double x = ((double)attributeScore - 10) / 2;
            int modifier = (int)Math.Floor(x);

            //Add proficiency bonus to the roll, if proficient
            if (isProficient)
                modifier += initiativeCreature.Creature.ProficiencyBonus;

            //Create damage roll info string, if damage is part of the roll
            string? damageString = null;
            if (values.Length > 5 && values[5] is int && values[6] is int && values[7] is int)
            {
                damageString = (int)values[5] + "d" + (int)values[6] + "+" + (int)values[7];
            }
            //Create damage type string
            string? damageType = null;
            if (values.Length > 8 && values[8] is not null && values[8] is string)
            {
                damageType = (string)values[8];
            }

            //Set roll name
            string rollName = "";
            if (values[4] is string)
            {
                rollName = (string)values[4];
            } else if (values[4] is bool && !(bool)values[4])
            {
                rollName = attributeName + " Save";
            }

                //Return info to display the roll's results: the final bonus, description of the roll, and name of the creature that made the roll
                return new Tuple<int?, string?, string?, string?, string?>(modifier, rollName, (initiativeCreature.Creature.Name + " " + initiativeCreature.InitiativeCreatureData.NameID), damageString, damageType);
        }

        return new Tuple<int?, string?, string?, string?, string?>(null, null, null, null, null);
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}