using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EffectsPedalsKeeperShared.Models
{
    public class PedalBoardPedal
    {
        public int Id { get; set; }
        public int PedalBoardId { get; set; }
        public int PedalId { get; set; }
        public int Order { get; set; }

        public PedalBoard PedalBoard { get; set; }
        public Pedal Pedal { get; set; }
    }
}
