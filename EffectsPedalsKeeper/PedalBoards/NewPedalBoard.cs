using EffectsPedalsKeeper.Interfaces;
using EffectsPedalsKeeper.Pedals;
using EffectsPedalsKeeper.Settings;
using EffectsPedalsKeeper.Utils;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace EffectsPedalsKeeper.PedalBoards
{
    public class NewPedalBoard : IList<IPedal>, IInteractiveEditable
    {
        public string Name { get; set; }
        public List<PedalBoardPreset> Presets { get; private set; }

        public NewPedalBoard(string name)
        {
            Name = name;
            _pedals = new List<IPedal>();
            Presets = new List<PedalBoardPreset>();
        }

        public NewPedalBoard(string name, IList<IPedal> pedals)
        {
            Name = name;
            if(pedals.Count > 0) { _pedals = new List<IPedal>(pedals); }
            else { _pedals = new List<IPedal>(); }
            Presets = new List<PedalBoardPreset>();
        }

        public void PresetAdd(string name)
        {
            Presets.Add(new PedalBoardPreset(name, _pedals));
        }

        public bool PresetRemove(PedalBoardPreset preset) => Presets.Remove(preset);

        public void PresetRemoveAt(int index) => Presets.RemoveAt(index);

        private void ExpandPresetOptions(IPedal pedal)
        {

            foreach (PedalBoardPreset preset in Presets)
            {
                var valueKeepers = new List<ValueKeeper<ISetting>>(pedal.Settings.Count);
                foreach (ISetting setting in pedal.Settings)
                {
                    valueKeepers.Add(new ValueKeeper<ISetting>(setting));
                }
                preset.SettingValues.AddRange(valueKeepers);
                preset.EngagedList.Add(pedal, pedal.Engaged);
            }
        }

        private void RemovePresetOptions(IPedal pedal)
        {
            foreach (PedalBoardPreset preset in Presets)
            {
                foreach (ISetting setting in pedal.Settings)
                {
                    preset.SettingValues.RemoveAll(value => value.Item == setting);
                }
            }
        }

        //IList Implementation
        private List<IPedal> _pedals;

        public IPedal this[int index] { get => _pedals[index]; set => throw new NotImplementedException(); }

        public int Count => _pedals.Count;

        public bool IsReadOnly => false;

        public void Add(IPedal item)
        {
            ExpandPresetOptions(item);
            _pedals.Add(item);
        }

        public void Clear()
        {
            _pedals.Clear();
            Presets.Clear();
        }

        public bool Contains(IPedal item) => _pedals.Contains(item);

        public void CopyTo(IPedal[] array, int arrayIndex)
        {
            _pedals.CopyTo(array, arrayIndex);
        }

        public IEnumerator<IPedal> GetEnumerator() => _pedals.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public int IndexOf(IPedal item) => _pedals.IndexOf(item);

        public void Insert(int index, IPedal item)
        {
            ExpandPresetOptions(item);
            _pedals.Insert(index, item);
        }

        public bool Remove(IPedal item)
        {
            if (_pedals.Remove(item))
            {
                RemovePresetOptions(item);
                return true;
            }
            return false;
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= _pedals.Count)
            {
                throw new ArgumentOutOfRangeException();
            }

            var pedal = _pedals[index];
            RemovePresetOptions(pedal);
            _pedals.RemoveAt(index);
        }

        //Interactive Editing
        public void InteractiveViewEdit(Action<string> checkQuit, Dictionary<string, object> additionalArgs)
        {
            throw new NotImplementedException();
        }
    }
}
