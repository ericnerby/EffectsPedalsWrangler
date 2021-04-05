using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace EffectsPedalsKeeperShared.Models
{
    public class PedalBoard
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public PedalBoard()
        {
            Pedals = new List<PedalBoardPedal>();
            Presets = new List<Preset>();
        }

        public override string ToString() => $"{Name} | Number of Pedals: {Pedals.Count}";

        public ICollection<PedalBoardPedal> Pedals;
        public IList<Preset> Presets { get; private set; }
    }
}
