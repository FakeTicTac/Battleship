using System;
using System.Linq;
using BLL.Battleship.Enums;
using System.Collections.Generic;


namespace BLL.Battleship
{
    
    /// <summary>
    /// Describes Battleship Game Logic.
    /// </summary>
    public class BattleShip
    {
        
        /// <summary>
        /// Entity ID in Database.
        /// </summary>
        public Guid BattleShipId { get; init; }
        
        
        /// <summary>
        /// Name of Game Save.
        /// </summary>
        public string? Name { get; init; }
        
        
        /// <summary>
        /// Describes Player One State.
        /// </summary>
        public Player? PLayerOne { get; init; }
        
        
        /// <summary>
        /// Describes Player Two State.
        /// </summary>
        public Player? PlayerTwo { get; init;  }

        
        /// <summary>
        /// Describes Game Setting State.
        /// </summary>
        public Settings? Settings { get; init;  }

        
        /// <summary>
        /// Indicator Of Game End State.
        /// </summary>
        public bool IsFinished { get; set; }
        
        
        /// <summary>
        /// Basic Parameterless BattleShip Constructor.
        /// </summary>
        public BattleShip() {}
        

        /// <summary>
        /// Basic BattleShip Game Constructor. Defines Players, Settings and Game Starting Data.
        /// </summary>
        /// <param name="nameA">NickName of the Player A. (Null if AI Picks Random Name)</param>
        /// <param name="nameB">NickName of the Player B. (Null if AI Picks Random Name)</param>
        /// <param name="settings">Settings State for Current Game.</param>
        
        public BattleShip(string? nameA, string? nameB, Settings settings)
        {
            Settings = settings;

            var (boardSizeX, boardSizeY) = (Settings.XBoardSize, Settings.YBoardSize);
            var AILevel = settings.GameMode == EGameMode.SinglePlayer ? settings.AILevel : null;
            
            PLayerOne = new Player(nameA, true, boardSizeX, boardSizeY, null);
            PlayerTwo = new Player(nameB, false, boardSizeX, boardSizeY, AILevel);
            
            IsFinished = false;
        }
        
        
        /// <summary>
        /// Currently Making Move Player Places the Bomb.
        /// </summary>
        /// <param name="xCoordinate">X Coordinate to Place Bomb.</param>
        /// <param name="yCoordinate">Y Coordinate to Place Bomb.</param>
        public bool PlaceBomb(int? xCoordinate = null, int? yCoordinate = null)
        {
            var shootingPlayer = PlayerByTurn();
            var receivingPlayer = PlayerByTurn(false);

            var (xBombPlace, yBombPlace) = BombCoordinateValidator(xCoordinate, yCoordinate);
            var shootResult = receivingPlayer.ReceiveBomb(xBombPlace, yBombPlace);

            shootingPlayer.AddShot(shootResult);
            
            if (!shootingPlayer.isHuman()) shootingPlayer.AIAnalyzeShoot(shootResult);
            
            IsFinished = receivingPlayer.IsLost;

            if (shootingPlayer.PlaceBomb(shootResult, true)) return true;
            
            SwitchTurns();
            return false;
        }

        
        /// <summary>
        /// Both Players Rewind Their Moves.
        /// </summary>
        public void DoubleRewind() 
        {
            Rewind();
            Rewind();
        }


        /// <summary>
        /// Delete All Currently Making Move Players' Ships.
        /// </summary>
        public void DeleteAllShips() => PlayerByTurn().DeleteAllShips();

        
        /// <summary>
        /// Delete Last Currently Making Move Players' Ship.
        /// </summary>
        public void DeleteLastShip() => PlayerByTurn().DeleteLastShip();
            
        
        /// <summary>
        /// Allows Player To Rewind His Moves.
        /// </summary>
        private void Rewind()
        {
            if (PlayerByTurn().PlayerShots!.Count == 0) return;
            
            var lastShot = PlayerByTurn().PlayerShots![PlayerByTurn().PlayerShots!.Count - 1];
            PlayerByTurn().PlayerShots!.Remove(lastShot);

            lastShot.PlayerShots!.ForEach(x => PlayerByTurn().FiringBoard!.Board![x.Item1, x.Item2].IsBomb = false);
            lastShot.PlayerShots!.ForEach(x => PlayerByTurn().FiringBoard!.Board![x.Item1, x.Item2].IsShip = false);

            lastShot.PlayerShots!.ForEach(x => PlayerByTurn(false).OwnBoard!.Board![x.Item1, x.Item2].IsBomb = false);

            foreach (var (xCoordinate, yCoordinate) in lastShot.PlayerShots!)
            {
                if (!PlayerByTurn(false).OwnBoard!.Board![xCoordinate, yCoordinate].IsShip) continue;
                
                var nr = PlayerByTurn(false).OwnBoard!.Board![xCoordinate, yCoordinate].ReservedByShips[0];
                PlayerByTurn(false).OwnShips!.First(x => x.ShipNumber == nr).Health += 1;
            }

            SwitchTurns();
        }
        
        
        /// <summary>
        /// Process Place Bomb Coordinate Validation. If Needed -> AI Generates Coordinates Based on Difficulty.
        /// </summary>
        /// <param name="xCoordinate">X Coordinate to Place Bomb.</param>
        /// <param name="yCoordinate">Y Coordinate to Place Bomb.</param>
        /// <returns>X and Y Coordinates for Bomb Placement.</returns>
        private (int, int) BombCoordinateValidator(int? xCoordinate = null, int? yCoordinate = null)
        {
            var shootingPlayer = PlayerByTurn();

            return shootingPlayer.PlayerType switch
            {
                EPlayerType.Human when xCoordinate == null || yCoordinate == null => 
                    throw new ArgumentException("It's Human Turn, but Coordinates for Bomb Placement are not Defined."),
                
                EPlayerType.Human => (xCoordinate.GetValueOrDefault(), yCoordinate.GetValueOrDefault()),
                
                _ => shootingPlayer.AIBombPlaceCoordinates()
            };
        }
        
        
        /// <summary>
        /// Create Ship for Currently Making Move Player.
        /// </summary>
        /// <param name="name">Name of the Ship to Create.</param>
        /// <param name="xSize">X Coordinate Size of the Ship to Create.</param>
        /// <param name="ySize">Y Coordinate Size of the Ship to Create.</param>
        /// <returns>Indicator of Creation Success.</returns>
        public bool CreatePlayerShip(string? name, int xSize, int ySize) => PlayerByTurn().CreateShip(name, xSize, ySize);


        /// <summary>
        /// AI Creates Random Ships and Randomly Places Them on the Board.
        /// </summary>
        public void AICreatePlaceShip() => PlayerByTurn().AICreatePlaceShip(Settings!.ShipTouchRule);
        
        
        /// <summary>
        /// Place Ship for Currently Making Move Player on the Board.
        /// </summary>
        /// <param name="shipNumber">Number of the Ship to be Placed.</param>
        /// <param name="xCoordinate">X Coordinate to Place Ship.</param>
        /// <param name="yCoordinate">Y Coordinate to Place Ship.</param>
        /// <returns>Indicator of Ship Placement Success.</returns>
        public bool PlacePlayerShip(int shipNumber, int xCoordinate, int yCoordinate) => 
                                PlayerByTurn().LocateShip(shipNumber, xCoordinate, yCoordinate, Settings!.ShipTouchRule);
        
        
        /// <summary>
        /// Find Player in Game Based on It's Turn.
        /// </summary>
        /// <param name="isTurn">Boolean Indicates Players Turn. (Currently Making Turn by Default)</param>
        /// <returns>Player Based on Its' Turn.</returns>
        public Player PlayerByTurn(bool isTurn = true) => new List<Player> {PLayerOne!, PlayerTwo!}.Find(x => x.IsTurn == isTurn)!;

        
        /// <summary>
        /// Get the Needed Copy of the Needed Players' Board.
        /// </summary>
        /// <param name="isTurnPlayer">Needed Players' Board (Whose turn or Not).</param>
        /// <param name="isFiring">Type of the Needed Board.</param>
        /// <returns>Deep Copy of the Needed Board.</returns>
        public BoardSquareState[,] GetPlayerBoard(bool isTurnPlayer, bool isFiring)
        {
            return new List<Player> {PLayerOne!, PlayerTwo!}
                .Single(x => x.IsTurn == isTurnPlayer).GetCopiedBoard(isFiring);
        }
        
        
        /// <summary>
        /// Indicator if Currently Making Move Player Have Enough Ships to Start the Game.
        /// </summary>
        /// <returns>Boolean of Indication.</returns>
        public bool IsPlayerHaveEnoughShips() => PlayerByTurn().IsEnoughShips();
        
        
        /// <summary>
        /// Switch Turns of Game Players.
        /// </summary>
        public void SwitchTurns() => new List<Player> { PLayerOne!, PlayerTwo! }.ForEach(x => x.IsTurn = !x.IsTurn);
        

        /// <summary>
        /// Indicates if Turn is Hold By Human Player.
        /// </summary>
        /// <returns>Indicator if Human Turn.</returns>
        public bool IsHumanTurn() => PlayerByTurn().isHuman();
        
        
        /// <summary>
        /// Get Game Winner Players' Name.
        /// </summary>
        /// <returns>Currently Game Winner Players' Name in String Format.</returns>
        public string WinnerPlayerName() => 
            new List<Player> {PLayerOne!, PlayerTwo!}.Find(x => x.IsLost == false)?.Name 
                                                    ?? throw new ArgumentException("Game is Not Finished Yet.");
        
        
        /// <summary>
        /// Get Currently Making Move Players' Name.
        /// </summary>
        /// <returns>Currently Making Move Players' Name in String Format.</returns>
        public string MakingMovePlayerName() => PlayerByTurn().Name!;
        
        
        /// <summary>
        /// Get Currently Making Move Players' Own Board Ship Capacity.
        /// </summary>
        /// <returns>Currently Making Move Players' Own Board Ship Capacity in Int Format.</returns>
        public int MakingMovePlayerBoardCapacity() => PlayerByTurn().OwnBoard!.BoardShipCapacity;
        
        
        /// <summary>
        /// Indicator of First Player Win.
        /// </summary>
        /// <returns>Indicator of First Player Win.</returns>
        public bool IsPlayerAWon() => !PLayerOne!.IsLost;
    }
}