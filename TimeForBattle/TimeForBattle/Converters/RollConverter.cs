using System.Globalization;

namespace TimeForBattle.Converters;

public class RollConverter : IValueConverter
{
    object? IValueConverter.Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        //Input:
        //value: InitiativeCreature
        //parameter: int rollChoice
            //0: Str Save
            //1: Dex Save
            //2: Con Save
            //3: Int Save
            //4: Wis Save
            //5: Cha Save
            //6: Hotkey roll 1
            //7: Hotkey roll 2

        if (value is null)
            return new Tuple<InitiativeCreature?, int>(null, 0);

        if (value is InitiativeCreature initiativeCreature)
        {
            int rollChoice = 0;

            if (parameter is not null)
            {
                if (parameter is int)
                {
                    rollChoice = (int)parameter;
                }
                else if (parameter is String && int.TryParse((String)parameter, out int result))
                {
                    rollChoice = result;
                }
            }

            return new Tuple<InitiativeCreature?, int>(initiativeCreature, rollChoice);
        }

        return new Tuple<InitiativeCreature?, int>(null, 0);
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}