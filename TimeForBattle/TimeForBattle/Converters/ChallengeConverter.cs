using System.Globalization;

namespace TimeForBattle.Converters;

public class ChallengeConverter : IValueConverter
{
    object? IValueConverter.Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is null)
            return "";

        if (value is int challengeRating)
        {
            float challengeFloat = (float)challengeRating;

            if (challengeFloat < 8)
            {
                if (challengeFloat == 1)
                {
                    return "1/8";
                } else if (challengeFloat == 2)
                {
                    return "1/4";
                }
                else if (challengeFloat == 4)
                {
                    return "1/2";
                }
            }

            return (challengeFloat / 8).ToString();
        }

        return value;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is null)
            return 0;

        if (value is string challengeString)
        {
            if (int.TryParse(challengeString, out int result))
            {
                return 8 * result;
            }

            string[] split = challengeString.Split(new char[] { '/' });

            if (split.Length == 2)
            {
                int a, b;

                if (int.TryParse(split[0].Trim(), out a) && int.TryParse(split[1].Trim(), out b))
                {
                    if (split.Length == 2)
                    {
                        return 8 * a / b;
                    }
                }
            }
        }

        return value;
    }
}