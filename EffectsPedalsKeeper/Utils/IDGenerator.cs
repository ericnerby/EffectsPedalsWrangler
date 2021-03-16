using System;
using System.Collections.Generic;
using System.Text;

namespace EffectsPedalsKeeper.Utils
{
    public static class IDGenerator
    {
        public static HashSet<int> LoggedValues { get; } = new HashSet<int>();
        private static int _nextID = 0;

        public static int GenerateID()
        {
            if (LogID(_nextID))
            {
                var newID = _nextID;
                _nextID += 1;
                return newID;
            }
            _nextID += 1;
            return GenerateID();
        }

        public static bool LogID(int iD) => LoggedValues.Add(iD);

        public static int PassThroughID(int iD)
        {
            if (LogID(iD))
            {
                return iD;
            }
            else
            {
                return GenerateID();
            }
        }
    }
}
