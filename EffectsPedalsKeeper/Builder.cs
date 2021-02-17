using System;

namespace EffectsPedalsKeeper
{
    public class Builder
    {
        public static Pedal BuildPedal()
        {
            Console.Write("What is the name of the pedal?  ");
            var name = Console.ReadLine();

            Console.Write("What is the maker of the pedal?  ");
            var maker = Console.ReadLine();

            EffectType effectType = GetEffectType();

            return new Pedal(maker, name, effectType);
        }

        private static EffectType GetEffectType()
        {

            Console.WriteLine("What type of Effect is it?\nChoose a number:");
            var index = 0;
            foreach (EffectType type in Enum.GetValues(typeof(EffectType)))
            {
                Console.WriteLine($"{index + 1}. {type}");
                index++;
            }
            var input = Console.ReadLine();
            int typeIndex;
            if(int.TryParse(input, out typeIndex))
            {
                return (EffectType)(typeIndex - 1);
            }
            Console.WriteLine("Please select a number from the list.");
            return GetEffectType();
        }
    }
}
