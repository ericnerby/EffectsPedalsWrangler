﻿// <auto-generated />
using System;
using EffectsPedalsKeeperShared.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EffectsPedalsKeeperWebApp.Migrations
{
    [DbContext(typeof(EffectsPedalsContext))]
    [Migration("20210407145712_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EffectsPedalsKeeperShared.Models.Pedal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EffectType")
                        .HasColumnType("int");

                    b.Property<string>("Maker")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Pedals");
                });

            modelBuilder.Entity("EffectsPedalsKeeperShared.Models.PedalBoard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PedalBoards");
                });

            modelBuilder.Entity("EffectsPedalsKeeperShared.Models.PedalBoardPedal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<int>("PedalBoardId")
                        .HasColumnType("int");

                    b.Property<int>("PedalId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PedalBoardId");

                    b.HasIndex("PedalId");

                    b.ToTable("PedalBoardPedals");
                });

            modelBuilder.Entity("EffectsPedalsKeeperShared.Models.PedalPreset", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Engaged")
                        .HasColumnType("bit");

                    b.Property<int?>("PedalId")
                        .HasColumnType("int");

                    b.Property<int?>("PresetId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PedalId");

                    b.HasIndex("PresetId");

                    b.ToTable("PedalPresets");
                });

            modelBuilder.Entity("EffectsPedalsKeeperShared.Models.Preset", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PedalBoardId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PedalBoardId");

                    b.ToTable("Presets");
                });

            modelBuilder.Entity("EffectsPedalsKeeperShared.Models.SettingPreset", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("PedalPresetId")
                        .HasColumnType("int");

                    b.Property<int?>("SettingId")
                        .HasColumnType("int");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PedalPresetId");

                    b.HasIndex("SettingId");

                    b.ToTable("SettingPresets");
                });

            modelBuilder.Entity("EffectsPedalsKeeperShared.Models.Settings.Option", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Label")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("OptionSettingId")
                        .HasColumnType("int");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OptionSettingId");

                    b.ToTable("Options");
                });

            modelBuilder.Entity("EffectsPedalsKeeperShared.Models.Settings.Setting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Label")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaxValue")
                        .HasColumnType("int");

                    b.Property<int>("MinValue")
                        .HasColumnType("int");

                    b.Property<int?>("PedalId")
                        .HasColumnType("int");

                    b.Property<int>("SettingType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PedalId");

                    b.ToTable("Settings");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Setting");
                });

            modelBuilder.Entity("EffectsPedalsKeeperShared.Models.Settings.OptionSetting", b =>
                {
                    b.HasBaseType("EffectsPedalsKeeperShared.Models.Settings.Setting");

                    b.Property<int?>("PedalId1")
                        .HasColumnType("int");

                    b.HasIndex("PedalId1");

                    b.HasDiscriminator().HasValue("OptionSetting");
                });

            modelBuilder.Entity("EffectsPedalsKeeperShared.Models.PedalBoardPedal", b =>
                {
                    b.HasOne("EffectsPedalsKeeperShared.Models.PedalBoard", "PedalBoard")
                        .WithMany()
                        .HasForeignKey("PedalBoardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EffectsPedalsKeeperShared.Models.Pedal", "Pedal")
                        .WithMany()
                        .HasForeignKey("PedalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pedal");

                    b.Navigation("PedalBoard");
                });

            modelBuilder.Entity("EffectsPedalsKeeperShared.Models.PedalPreset", b =>
                {
                    b.HasOne("EffectsPedalsKeeperShared.Models.Pedal", "Pedal")
                        .WithMany()
                        .HasForeignKey("PedalId");

                    b.HasOne("EffectsPedalsKeeperShared.Models.Preset", "Preset")
                        .WithMany("PedalPresets")
                        .HasForeignKey("PresetId");

                    b.Navigation("Pedal");

                    b.Navigation("Preset");
                });

            modelBuilder.Entity("EffectsPedalsKeeperShared.Models.Preset", b =>
                {
                    b.HasOne("EffectsPedalsKeeperShared.Models.PedalBoard", "PedalBoard")
                        .WithMany("Presets")
                        .HasForeignKey("PedalBoardId");

                    b.Navigation("PedalBoard");
                });

            modelBuilder.Entity("EffectsPedalsKeeperShared.Models.SettingPreset", b =>
                {
                    b.HasOne("EffectsPedalsKeeperShared.Models.PedalPreset", "PedalPreset")
                        .WithMany("SettingPresets")
                        .HasForeignKey("PedalPresetId");

                    b.HasOne("EffectsPedalsKeeperShared.Models.Settings.Setting", "Setting")
                        .WithMany()
                        .HasForeignKey("SettingId");

                    b.Navigation("PedalPreset");

                    b.Navigation("Setting");
                });

            modelBuilder.Entity("EffectsPedalsKeeperShared.Models.Settings.Option", b =>
                {
                    b.HasOne("EffectsPedalsKeeperShared.Models.Settings.OptionSetting", "OptionSetting")
                        .WithMany("Options")
                        .HasForeignKey("OptionSettingId");

                    b.Navigation("OptionSetting");
                });

            modelBuilder.Entity("EffectsPedalsKeeperShared.Models.Settings.Setting", b =>
                {
                    b.HasOne("EffectsPedalsKeeperShared.Models.Pedal", "Pedal")
                        .WithMany("Settings")
                        .HasForeignKey("PedalId");

                    b.Navigation("Pedal");
                });

            modelBuilder.Entity("EffectsPedalsKeeperShared.Models.Settings.OptionSetting", b =>
                {
                    b.HasOne("EffectsPedalsKeeperShared.Models.Pedal", null)
                        .WithMany("OptionSettings")
                        .HasForeignKey("PedalId1");
                });

            modelBuilder.Entity("EffectsPedalsKeeperShared.Models.Pedal", b =>
                {
                    b.Navigation("OptionSettings");

                    b.Navigation("Settings");
                });

            modelBuilder.Entity("EffectsPedalsKeeperShared.Models.PedalBoard", b =>
                {
                    b.Navigation("Presets");
                });

            modelBuilder.Entity("EffectsPedalsKeeperShared.Models.PedalPreset", b =>
                {
                    b.Navigation("SettingPresets");
                });

            modelBuilder.Entity("EffectsPedalsKeeperShared.Models.Preset", b =>
                {
                    b.Navigation("PedalPresets");
                });

            modelBuilder.Entity("EffectsPedalsKeeperShared.Models.Settings.OptionSetting", b =>
                {
                    b.Navigation("Options");
                });
#pragma warning restore 612, 618
        }
    }
}
