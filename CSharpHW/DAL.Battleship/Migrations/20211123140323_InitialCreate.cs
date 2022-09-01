using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Battleship.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coordinates",
                columns: table => new
                {
                    CoordinateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    xCoordinateValue = table.Column<int>(type: "int", nullable: false),
                    yCoordinateValue = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coordinates", x => x.CoordinateId);
                });

            migrationBuilder.CreateTable(
                name: "GameBoards",
                columns: table => new
                {
                    GameBoardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    XBoardSize = table.Column<int>(type: "int", nullable: false),
                    YBoardSize = table.Column<int>(type: "int", nullable: false),
                    BoardShipCapacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameBoards", x => x.GameBoardId);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    SettingsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AiLevel = table.Column<int>(type: "int", nullable: true),
                    GameMode = table.Column<int>(type: "int", nullable: false),
                    ShipTouchRule = table.Column<int>(type: "int", nullable: false),
                    ShipUsageRule = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsManualCreation = table.Column<bool>(type: "bit", nullable: false),
                    BoardXSize = table.Column<int>(type: "int", nullable: false),
                    BoardYSize = table.Column<int>(type: "int", nullable: false),
                    SavedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.SettingsId);
                });

            migrationBuilder.CreateTable(
                name: "Strategies",
                columns: table => new
                {
                    StrategyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    XSize = table.Column<int>(type: "int", nullable: false),
                    YSize = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Strategies", x => x.StrategyId);
                });

            migrationBuilder.CreateTable(
                name: "BoardSquareStates",
                columns: table => new
                {
                    BoardSquareStateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsShip = table.Column<bool>(type: "bit", nullable: false),
                    IsBomb = table.Column<bool>(type: "bit", nullable: false),
                    XCoordinate = table.Column<int>(type: "int", nullable: false),
                    YCoordinate = table.Column<int>(type: "int", nullable: false),
                    ReservedByShips = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GameBoardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardSquareStates", x => x.BoardSquareStateId);
                    table.ForeignKey(
                        name: "FK_BoardSquareStates_GameBoards_GameBoardId",
                        column: x => x.GameBoardId,
                        principalTable: "GameBoards",
                        principalColumn: "GameBoardId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    PlayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsTurn = table.Column<bool>(type: "bit", nullable: false),
                    IsLost = table.Column<bool>(type: "bit", nullable: false),
                    IsPlacementCompleted = table.Column<bool>(type: "bit", nullable: false),
                    PlayerType = table.Column<int>(type: "int", nullable: false),
                    StrategyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OwnBoardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FiringBoardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.PlayerId);
                    table.ForeignKey(
                        name: "FK_Players_GameBoards_FiringBoardId",
                        column: x => x.FiringBoardId,
                        principalTable: "GameBoards",
                        principalColumn: "GameBoardId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Players_GameBoards_OwnBoardId",
                        column: x => x.OwnBoardId,
                        principalTable: "GameBoards",
                        principalColumn: "GameBoardId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Players_Strategies_StrategyId",
                        column: x => x.StrategyId,
                        principalTable: "Strategies",
                        principalColumn: "StrategyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StrategyCoordinates",
                columns: table => new
                {
                    StrategyCoordinateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StrategyCoordinateType = table.Column<int>(type: "int", nullable: false),
                    StrategyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CoordinateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StrategyCoordinates", x => x.StrategyCoordinateId);
                    table.ForeignKey(
                        name: "FK_StrategyCoordinates_Coordinates_CoordinateId",
                        column: x => x.CoordinateId,
                        principalTable: "Coordinates",
                        principalColumn: "CoordinateId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StrategyCoordinates_Strategies_StrategyId",
                        column: x => x.StrategyId,
                        principalTable: "Strategies",
                        principalColumn: "StrategyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Battleships",
                columns: table => new
                {
                    BattleShipId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsFinished = table.Column<bool>(type: "bit", nullable: false),
                    SavedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SettingsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlayerOneId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlayerTwoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Battleships", x => x.BattleShipId);
                    table.ForeignKey(
                        name: "FK_Battleships_Players_PlayerOneId",
                        column: x => x.PlayerOneId,
                        principalTable: "Players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Battleships_Players_PlayerTwoId",
                        column: x => x.PlayerTwoId,
                        principalTable: "Players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Battleships_Settings_SettingsId",
                        column: x => x.SettingsId,
                        principalTable: "Settings",
                        principalColumn: "SettingsId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlayerShots",
                columns: table => new
                {
                    PlayerShotId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShotNumber = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CoordinateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerShots", x => x.PlayerShotId);
                    table.ForeignKey(
                        name: "FK_PlayerShots_Coordinates_CoordinateId",
                        column: x => x.CoordinateId,
                        principalTable: "Coordinates",
                        principalColumn: "CoordinateId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlayerShots_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ships",
                columns: table => new
                {
                    ShipId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    XSize = table.Column<int>(type: "int", nullable: false),
                    YSize = table.Column<int>(type: "int", nullable: false),
                    Health = table.Column<int>(type: "int", nullable: false),
                    IsPlaced = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShipNumber = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ships", x => x.ShipId);
                    table.ForeignKey(
                        name: "FK_Ships_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Battleships_PlayerOneId",
                table: "Battleships",
                column: "PlayerOneId");

            migrationBuilder.CreateIndex(
                name: "IX_Battleships_PlayerTwoId",
                table: "Battleships",
                column: "PlayerTwoId");

            migrationBuilder.CreateIndex(
                name: "IX_Battleships_SettingsId",
                table: "Battleships",
                column: "SettingsId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardSquareStates_GameBoardId",
                table: "BoardSquareStates",
                column: "GameBoardId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_FiringBoardId",
                table: "Players",
                column: "FiringBoardId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_OwnBoardId",
                table: "Players",
                column: "OwnBoardId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_StrategyId",
                table: "Players",
                column: "StrategyId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerShots_CoordinateId",
                table: "PlayerShots",
                column: "CoordinateId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerShots_PlayerId",
                table: "PlayerShots",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Ships_PlayerId",
                table: "Ships",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_StrategyCoordinates_CoordinateId",
                table: "StrategyCoordinates",
                column: "CoordinateId");

            migrationBuilder.CreateIndex(
                name: "IX_StrategyCoordinates_StrategyId",
                table: "StrategyCoordinates",
                column: "StrategyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Battleships");

            migrationBuilder.DropTable(
                name: "BoardSquareStates");

            migrationBuilder.DropTable(
                name: "PlayerShots");

            migrationBuilder.DropTable(
                name: "Ships");

            migrationBuilder.DropTable(
                name: "StrategyCoordinates");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Coordinates");

            migrationBuilder.DropTable(
                name: "GameBoards");

            migrationBuilder.DropTable(
                name: "Strategies");
        }
    }
}
