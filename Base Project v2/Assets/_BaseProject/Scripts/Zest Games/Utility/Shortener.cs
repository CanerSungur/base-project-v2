
namespace ZestGames.Utility
{
    public static class Shortener
    {
        /// <summary>
        /// This function converts given int value to K and M type.
        /// i.e. 10.000 to 10K, 1.500.000 to 1.5M
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string IntToStringShortener(int value)
        {
            if (value < 10000)
                return value.ToString();
            else if (value >= 10000 && value < 1000000)
                return (value / 1000).ToString() + "K";
            else if (value >= 1000000 && value < 1000000000)
                return (value / 1000000).ToString() + "M";
            else if (value >= 1000000000)
                return (value / 1000000000).ToString() + "B";
            else
                return "";
        }

        /// <summary>
        /// This function converts given float value to K and M type.
        /// i.e. 10.000 to 10K, 1.500.000 to 1.5M
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string FloatToStringShortener(float value)
        {
            if (value < 10000)
                return value.ToString();
            else if (value >= 10000 && value < 1000000)
                return (value / 1000).ToString() + "K";
            else if (value >= 1000000 && value < 1000000000)
                return (value / 1000000).ToString() + "M";
            else if (value >= 1000000000)
                return (value / 1000000000).ToString() + "B";
            else
                return "";
        }
    }
}