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
        //3: Damage dice number: int
        //4: Damage dice size: int
        //5: Damage bonus: int
        //6: Damage type: string

        if (values[0] is null || values[1] is null || values[2] is null)
            return new Tuple<int?, string?, string?, string?, string?>(null, null, null, null, null);

        if (values[0] is InitiativeCreature initiativeCreature && values[1] is int modifier && values[2] is string rollName)
        {
            //Create damage roll info string, if damage is part of the roll
            string? damageString = null;
            if (values.Length > 3 && values[3] is int && values[4] is int && values[5] is int)
            {
                damageString = (int)values[3] + "d" + (int)values[4] + "+" + (int)values[5];
            }
            //Create damage type string
            string? damageType = null;
            if (values.Length > 6 && values[6] is not null && values[6] is string)
            {
                damageType = (string)values[6];
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