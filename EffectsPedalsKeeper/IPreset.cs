using System.Collections.Generic;

namespace EffectsPedalsKeeper
{
    public interface IPreset : IList<IPedal>
    {
        public string Name { get; }

        public string Description { get; set; }
    }
}
