using System.Globalization;

namespace TimeForBattle.Converters;

public class RollConverter : IMultiValueConverter
{
    object? IMultiValueConverter.Convert(object[] values, Type targetType, object? parameter, CultureInfo culture)
    {
        //Input values:
        //0: Creature: Creature creature
        //1: Modifier: int modifier
        //2: Roll name: string rollName, or bool false to set roll name to save
        //3: Damage dice number 1: int
        //4: Damage dice size 1: int
        //5: Damage bonus 1: int
        //6: Damage type 1: string
        //7: Damage dice number 2: int
        //8: Damage dice size 2: int
        //9: Damage bonus 2: int
        //10: Damage type 2: string

        if (values[0] is null || values[1] is null || values[2] is null)
            return new Tuple<int?, string?, string?, string?, string?, string?, string?>(null, null, null, null, null, null, null);

        if (values[0] is InitiativeCreature initiativeCreature && values[1] is int modifier && values[2] is string rollName)
        {
            //Create damage roll info string, if damage is part of the roll
            string? damageString1 = null;
            if (values.Length > 3 && values[3] is not null && values[4] is not null && values[5] is not null && values[3] is int && values[4] is int && values[5] is int)
            {
                damageString1 = (int)values[3] + "d" + (int)values[4] + "+" + (int)values[5];
            }
            //Create damage type string
            string? damageType1 = null;
            if (values.Length > 6 && values[6] is not null && values[6] is string)
            {
                damageType1 = (string)values[6];
            }

            //Create damage roll info string, if damage is part of the roll
            string? damageString2 = null;
            if (values.Length > 7 && values[7] is not null && values[8] is not null && values[9] is not null && values[7] is int && values[8] is int && values[9] is int)
            {
                damageString2 = (int)values[7] + "d" + (int)values[8] + "+" + (int)values[9];
            }
            //Create damage type string
            string? damageType2 = null;
            if (values.Length > 10 && values[10] is not null && values[10] is string)
            {
                damageType2 = (string)values[10];
            }

            //Return info to display the roll's results: the final bonus, description of the roll, and name of the creature that made the roll
            return new Tuple<int?, string?, string?, string?, string?, string?, string?>(modifier, rollName, (initiativeCreature.Creature.Name + " " + initiativeCreature.InitiativeCreatureData.NameID), damageString1, damageType1, damageString2, damageType2);
        }

        return new Tuple<int?, string?, string?, string?, string?, string?, string?>(null, null, null, null, null, null, null);
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}