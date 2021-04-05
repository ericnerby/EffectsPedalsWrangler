using EffectsPedalsKeeperShared.Models;
using EffectsPedalsKeeperShared.Models.Settings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EffectsPedalsKeeperShared.Data
{
    public class Context : DbContext
    {
        public DbSet<Pedal> Pedals { get; set; }
        public DbSet<PedalBoard> PedalBoards { get; set; }
        public DbSet<PedalBoardPedal> PedalBoardPedals { get; set; }
        public DbSet<PedalPreset> PedalPresets { get; set; }
        public DbSet<Preset> Presets { get; set; }
        public DbSet<SettingPreset> SettingPresets { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<OptionSetting> OptionSettings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Server=(localdb)\mssqllocaldb;Database=EffectsPedals;Integrated Security=True");
        }
    }
}

