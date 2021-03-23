using System;

namespace EffectsPedalsKeeper.CommandLineUtils
{
    public class AssignmentMenuOption<T> : MenuOption
    {
        public Func<T, bool> Validator { get; set; }
        public Func<string, T> ValueConverter { get; set; }
        public AssignmentMenuOption(ResponseType responseType, string description,
            Func<T, bool> validator, Func<string,T> valueConverter)
            : base(responseType, () => { }, "", description)
        {
            Validator = validator;
        }

        public bool AssignValue(ref T destination, string value)
        {
            T convertedValue = ValueConverter(value);
            if (Validator(convertedValue))
            {
                destination = convertedValue;
                return true;
            }
            return false;
        }
    }
}
