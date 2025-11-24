using System.Globalization;

namespace TimeForBattle.Converters;

public class CreatureHotKeyWidthConverter : IValueConverter
{
    object? IValueConverter.Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not null && value is InitiativeCreature Creature)
        {
            if (String.IsNullOrEmpty(Creature.Creature.HotKey1Name) || String.IsNullOrEmpty(Creature.Creature.HotKey2Name))
                return 6;
            else
            {
                return 3;
            }
        }
        return value;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}