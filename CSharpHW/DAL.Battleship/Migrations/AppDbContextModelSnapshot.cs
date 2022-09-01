﻿// <auto-generated />
using System;
using DAL.Battleship;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DAL.Battleship.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.Battleship.BattleShip", b =>
                {
                    b.Property<Guid>("BattleShipId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsFinished")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("PlayerOneId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PlayerTwoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("SavedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("SettingsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("BattleShipId");

                    b.HasIndex("PlayerOneId");

                    b.HasIndex("PlayerTwoId");

                    b.HasIndex("SettingsId");

                    b.ToTable("Battleships");
                });

            modelBuilder.Entity("Domain.Battleship.BoardSquareState", b =>
                {
                    b.Property<Guid>("BoardSquareStateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GameBoardId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsBomb")
                        .HasColumnType("bit");

                    b.Property<bool>("IsShip")
                        .HasColumnType("bit");

                    b.Property<string>("ReservedByShips")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("XCoordinate")
                        .HasColumnType("int");

                    b.Property<int>("YCoordinate")
                        .HasColumnType("int");

                    b.HasKey("BoardSquareStateId");

                    b.HasIndex("GameBoardId");

                    b.ToTable("BoardSquareStates");
                });

            modelBuilder.Entity("Domain.Battleship.Coordinate", b =>
                {
                    b.Property<Guid>("CoordinateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("xCoordinateValue")
                        .HasColumnType("int");

                    b.Property<int>("yCoordinateValue")
                        .HasColumnType("int");

                    b.HasKey("CoordinateId");

                    b.ToTable("Coordinates");
                });

            modelBuilder.Entity("Domain.Battleship.GameBoard", b =>
                {
                    b.Property<Guid>("GameBoardId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("BoardShipCapacity")
                        .HasColumnType("int");

                    b.Property<int>("XBoardSize")
                        .HasColumnType("int");

                    b.Property<int>("YBoardSize")
                        .HasColumnType("int");

                    b.HasKey("GameBoardId");

                    b.ToTable("GameBoards");
                });

            modelBuilder.Entity("Domain.Battleship.Player", b =>
                {
                    b.Property<Guid>("PlayerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("FiringBoardId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsLost")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPlacementCompleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsTurn")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OwnBoardId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("PlayerType")
                        .HasColumnType("int");

                    b.Property<Guid?>("StrategyId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("PlayerId");

                    b.HasIndex("FiringBoardId");

                    b.HasIndex("OwnBoardId");

                    b.HasIndex("StrategyId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("Domain.Battleship.PlayerShot", b =>
                {
                    b.Property<Guid>("PlayerShotId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CoordinateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PlayerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ShotNumber")
                        .HasColumnType("int");

                    b.HasKey("PlayerShotId");

                    b.HasIndex("CoordinateId");

                    b.HasIndex("PlayerId");

                    b.ToTable("PlayerShots");
                });

            modelBuilder.Entity("Domain.Battleship.Settings", b =>
                {
                    b.Property<Guid>("SettingsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("AiLevel")
                        .HasColumnType("int");

                    b.Property<int>("BoardXSize")
                        .HasColumnType("int");

                    b.Property<int>("BoardYSize")
                        .HasColumnType("int");

                    b.Property<int>("GameMode")
                        .HasColumnType("int");

                    b.Property<bool>("IsManualCreation")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("SavedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("ShipTouchRule")
                        .HasColumnType("int");

                    b.Property<int>("ShipUsageRule")
                        .HasColumnType("int");

                    b.HasKey("SettingsId");

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("Domain.Battleship.Ship", b =>
                {
                    b.Property<Guid>("ShipId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Health")
                        .HasColumnType("int");

                    b.Property<bool>("IsPlaced")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("PlayerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ShipNumber")
                        .HasColumnType("int");

                    b.Property<int>("XSize")
                        .HasColumnType("int");

                    b.Property<int>("YSize")
                        .HasColumnType("int");

                    b.HasKey("ShipId");

                    b.HasIndex("PlayerId");

                    b.ToTable("Ships");
                });

            modelBuilder.Entity("Domain.Battleship.Strategy", b =>
                {
                    b.Property<Guid>("StrategyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("XSize")
                        .HasColumnType("int");

                    b.Property<int>("YSize")
                        .HasColumnType("int");

                    b.HasKey("StrategyId");

                    b.ToTable("Strategies");
                });

            modelBuilder.Entity("Domain.Battleship.StrategyCoordinate", b =>
                {
                    b.Property<Guid>("StrategyCoordinateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CoordinateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("StrategyCoordinateType")
                        .HasColumnType("int");

                    b.Property<Guid>("StrategyId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("StrategyCoordinateId");

                    b.HasIndex("CoordinateId");

                    b.HasIndex("StrategyId");

                    b.ToTable("StrategyCoordinates");
                });

            modelBuilder.Entity("Domain.Battleship.BattleShip", b =>
                {
                    b.HasOne("Domain.Battleship.Player", "PlayerOne")
                        .WithMany()
                        .HasForeignKey("PlayerOneId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Domain.Battleship.Player", "PlayerTwo")
                        .WithMany()
                        .HasForeignKey("PlayerTwoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Domain.Battleship.Settings", "Settings")
                        .WithMany("BattleShips")
                        .HasForeignKey("SettingsId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("PlayerOne");

                    b.Navigation("PlayerTwo");

                    b.Navigation("Settings");
                });

            modelBuilder.Entity("Domain.Battleship.BoardSquareState", b =>
                {
                    b.HasOne("Domain.Battleship.GameBoard", "GameBoard")
                        .WithMany("BoardSquareStates")
                        .HasForeignKey("GameBoardId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("GameBoard");
                });

            modelBuilder.Entity("Domain.Battleship.Player", b =>
                {
                    b.HasOne("Domain.Battleship.GameBoard", "FiringBoard")
                        .WithMany()
                        .HasForeignKey("FiringBoardId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Domain.Battleship.GameBoard", "OwnBoard")
                        .WithMany()
                        .HasForeignKey("OwnBoardId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Domain.Battleship.Strategy", "Strategy")
                        .WithMany()
                        .HasForeignKey("StrategyId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("FiringBoard");

                    b.Navigation("OwnBoard");

                    b.Navigation("Strategy");
                });

            modelBuilder.Entity("Domain.Battleship.PlayerShot", b =>
                {
                    b.HasOne("Domain.Battleship.Coordinate", "Coordinate")
                        .WithMany()
                        .HasForeignKey("CoordinateId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Domain.Battleship.Player", "Player")
                        .WithMany("PlayerShots")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Coordinate");

                    b.Navigation("Player");
                });

            modelBuilder.Entity("Domain.Battleship.Ship", b =>
                {
                    b.HasOne("Domain.Battleship.Player", "Player")
                        .WithMany("Ships")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Player");
                });

            modelBuilder.Entity("Domain.Battleship.StrategyCoordinate", b =>
                {
                    b.HasOne("Domain.Battleship.Coordinate", "Coordinate")
                        .WithMany("StrategyCoordinates")
                        .HasForeignKey("CoordinateId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Domain.Battleship.Strategy", "Strategy")
                        .WithMany("StrategyCoordinates")
                        .HasForeignKey("StrategyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Coordinate");

                    b.Navigation("Strategy");
                });

            modelBuilder.Entity("Domain.Battleship.Coordinate", b =>
                {
                    b.Navigation("StrategyCoordinates");
                });

            modelBuilder.Entity("Domain.Battleship.GameBoard", b =>
                {
                    b.Navigation("BoardSquareStates");
                });

            modelBuilder.Entity("Domain.Battleship.Player", b =>
                {
                    b.Navigation("PlayerShots");

                    b.Navigation("Ships");
                });

            modelBuilder.Entity("Domain.Battleship.Settings", b =>
                {
                    b.Navigation("BattleShips");
                });

            modelBuilder.Entity("Domain.Battleship.Strategy", b =>
                {
                    b.Navigation("StrategyCoordinates");
                });
#pragma warning restore 612, 618
        }
    }
}