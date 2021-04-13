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
    public class EffectsPedalsContext : DbContext
    {
        public EffectsPedalsContext(DbContextOptions<EffectsPedalsContext> options)
            : base(options)
        {
        }

        public DbSet<Pedal> Pedals { get; set; }
        public DbSet<PedalBoard> PedalBoards { get; set; }
        public DbSet<PedalBoardPedal> PedalBoardPedals { get; set; }
        public DbSet<PedalPreset> PedalPresets { get; set; }
        public DbSet<Preset> Presets { get; set; }
        public DbSet<SettingPreset> SettingPresets { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Option> Options { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Setting>()
                .HasOne(s => s.Pedal)
                .WithMany(p => p.Settings)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}

