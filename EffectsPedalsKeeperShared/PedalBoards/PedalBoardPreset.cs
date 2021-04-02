using EffectsPedalsKeeperShared.Pedals;
using EffectsPedalsKeeperShared.Settings;
using EffectsPedalsKeeperShared.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EffectsPedalsKeeperShared.PedalBoards
{
    public class PedalBoardPreset
    {
        public string Name { get; set; }
        public List<PedalKeeper> PedalKeepers;
        public List<bool> EngagedList;
        public int PedalsEngaged => EngagedList.Where(engaged => engaged).Count();

        public PedalBoardPreset(string name, IList<IPedal> pedals)
        {
            Name = name;
            EngagedList = new List<bool>();
            PedalKeepers = new List<PedalKeeper>();

            AddPedals(pedals);
        }

        public PedalBoardPreset(string name, IList<bool> engagedList, IList<PedalKeeper> pedalKeepers)
        {
            Name = name;
            EngagedList = new List<bool>(engagedList);
            PedalKeepers = new List<PedalKeeper>(pedalKeepers);

        }

        public void AddPedals(IList<IPedal> pedals)
        {
            foreach (IPedal pedal in pedals)
            {
                var pedalKeeper = new PedalKeeper(pedal.Settings.Count);
                EngagedList.Add(pedal.Engaged);
                foreach (ISetting setting in pedal.Settings)
                {
                    pedalKeeper.Add(new ValueKeeper(setting));
                }
                PedalKeepers.Add(pedalKeeper);
            }
        }

        public void MovePedal(int currentIndex, int newIndex)
        {
            if (currentIndex < 0 || newIndex < 0
                || currentIndex >= PedalKeepers.Count || newIndex >= PedalKeepers.Count)
            {
                throw new ArgumentOutOfRangeException();
            }

            var keeperToMove = PedalKeepers[currentIndex];
            PedalKeepers.RemoveAt(currentIndex);
            PedalKeepers.Insert(newIndex, keeperToMove);

            var engagedValue = EngagedList[currentIndex];
            EngagedList.RemoveAt(currentIndex);
            EngagedList.Insert(newIndex, engagedValue);
        }

        public void InsertPedal(IPedal pedal, int position)
        {
            var pedalKeeper = new PedalKeeper(pedal.Settings.Count);
            EngagedList.Insert(position, pedal.Engaged);
            foreach (ISetting setting in pedal.Settings)
            {
                pedalKeeper.Add(new ValueKeeper(setting));
            }
            PedalKeepers.Insert(position, pedalKeeper);
        }

        public void RemovePedal(int position)
        {
            EngagedList.RemoveAt(position);
            PedalKeepers.RemoveAt(position);
        }

        public override string ToString() => $"{Name} | Pedals Engaged: {PedalsEngaged}";
    }

    public class PedalKeeper : List<ValueKeeper>
    {
        public PedalKeeper(int capacity) : base(capacity) { }

        public PedalKeeper(IList<ValueKeeper> valueKeepers) : base(valueKeepers) { }
    }
}
