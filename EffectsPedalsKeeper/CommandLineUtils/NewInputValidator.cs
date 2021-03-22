using System.Text.RegularExpressions;

namespace EffectsPedalsKeeper.CommandLineUtils
{
    public static class NewInputValidator
    {
        private static Regex _dashFormat = new Regex(@"^-\w$");
        private static Regex _intFormat = new Regex(@"^[0-9]+$");
        private static Regex _doubleFormat = new Regex(@"^\d+\.\d+$");
        private static Regex _clockFormat = new Regex(@"(\d+):(\d{2})");

        public static InputResponse ParseInput(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return new InputResponse(ResponseType.Empty, null);
            }
            Match match;
            match = _dashFormat.Match(input);
            if (match.Success)
            {
                return new InputResponse(ResponseType.DashOption, match.Value.ToLower());
            }
            match = _intFormat.Match(input);
            if (match.Success)
            {
                int value;
                if (int.TryParse(match.Value, out value))
                {
                    return new InputResponse(ResponseType.Int, match.Value);
                }
                else
                {
                    return new InputResponse(ResponseType.Misc, input);
                }
            }
            match = _doubleFormat.Match(input);
            if (match.Success)
            {
                double value;
                if (double.TryParse(match.Value, out value))
                {
                    return new InputResponse(ResponseType.Double, match.Value);
                }
                else
                {
                    return new InputResponse(ResponseType.Misc, input);
                }
            }
            match = _clockFormat.Match(input);
            if (match.Success)
            {
                int hour = int.Parse(match.Groups[1].Value);
                int minute = int.Parse(match.Groups[2].Value);
                if (hour > 0 && hour <= 12 && minute >= 0 && minute < 60)
                {
                    return new InputResponse(ResponseType.TwelveClock, match.Value);
                }
                else
                {
                    return new InputResponse(ResponseType.Misc, input);
                }
            }

            return new InputResponse(ResponseType.Misc, input);
        }
    }

    public enum ResponseType
    {
        DashOption,
        Int,
        Double,
        TwelveClock,
        Misc,
        Empty
    }

    public class InputResponse
    {
        public ResponseType ResponseType { get; set; }
        public string Value { get; set; }

        public InputResponse(ResponseType responseType, string value)
        {
            ResponseType = responseType;
            Value = value;
        }
    }
}
