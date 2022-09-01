using System;
using System.Linq;
using BLL.Battleship.Enums;
using BLL.Battleship.Interfaces;
using BLL.Battleship.Strategies;
using System.Collections.Generic;


namespace BLL.Battleship
{
    
    /// <summary>
    /// Represents Players' State.
    /// </summary>
    public class Player
    {

        /// <summary>
        /// Nickname of the Player.
        /// </summary>
        public string? Name { get; init; }
        
        
        /// <summary>
        /// Indicates if It's Players Turn to Make a Move.
        /// </summary>
        public bool IsTurn { get; set; }
        
        
        /// <summary>
        /// Indicates Type of the Player.
        /// </summary>
        public EPlayerType PlayerType { get; set; }
        
        
        /// <summary>
        /// Indicates if Player Lost the Game.
        /// </summary>
        public bool IsLost { get; set; }
        
        
        /// <summary>
        /// Indicator of Ship Placement Process Completion.
        /// </summary>
        public bool IsPlacementCompleted { get; set; }
        
        
        /// <summary>
        /// Indicates Players' Own Board state.
        /// </summary>
        public GameBoard? OwnBoard { get; set; }
        
        
        /// <summary>
        /// Indicates Firing Board state.
        /// </summary>
        public GameBoard? FiringBoard { get; init; }

        
        /// <summary>
        /// Indicates Players' Own Ships.
        /// </summary>
        public List<Ship>? OwnShips { get; set; }
        
        
        /// <summary>
        /// Indicates Players' Strategy to Perform.
        /// </summary>
        public IStrategy? Strategy { get; set; }


        /// <summary>
        /// List Stores Player Sots During The Game.
        /// </summary>
        public List<PlayerShot>? PlayerShots { get; init; }
        
        
        /// <summary>
        /// Basic Parameterless Player Constructor.
        /// </summary>
        public Player() {}
        

        /// <summary>
        /// Basic Player Constructor. Initializing Players' Needed Data.
        /// </summary>
        /// <param name="name">Nickname of the Player. (Null if AI Generates It Automatically)</param>
        /// <param name="isTurn">Indicator of Users Turn.</param>
        /// <param name="xSize">X Coordinate Size for Board.</param>
        /// <param name="ySize">Y Coordinate Size for Board.</param>
        /// <param name="aiLevel">Indicates AI Difficulty Level to Play With.</param>
        public Player(string? name, bool isTurn, int xSize, int ySize, EAILevel? aiLevel)
        {
            Name = name ?? AINameInitialization();
   
            OwnBoard = new GameBoard(xSize, ySize);
            FiringBoard = new GameBoard(xSize, ySize);
            
            OwnShips = new List<Ship>();

            StrategyInitialization(aiLevel);

            IsTurn = isTurn;
            IsLost = false;
            IsPlacementCompleted = false;
            PlayerShots = new List<PlayerShot>();
        }

        
        /// <summary>
        /// Choose Strategy and Player Type for Game Based on AI Level.
        /// </summary>
        private void StrategyInitialization(EAILevel? aiLevel)
        {
            if (aiLevel == null)
            {
                PlayerType = EPlayerType.Human;
                Strategy = null;
                
            } else {
                
                PlayerType = EPlayerType.AI;
                
                Strategy = aiLevel switch
                {
                    EAILevel.Easy => new AIEasyStrategy(FiringBoard!.XBoardSize, FiringBoard.YBoardSize),
                    EAILevel.Medium => new AIMediumStrategy(FiringBoard!.XBoardSize, FiringBoard.YBoardSize),
                    EAILevel.Hard => new AIHardStrategy(FiringBoard!.XBoardSize, FiringBoard.YBoardSize),
                    _ => throw new ArgumentException("Unsupported AI Level Type.")
                };
            }
        }
        
        
        /// <summary>
        /// AI Initializes Random Player Name from Predicated List.
        /// </summary>
        /// <returns>Random Player Name Generator.</returns>
        public static string AINameInitialization()
        {
            var names = new List<string> { "Matrix", "Phoenix", "Witness", "Life", "Blossom", "Mage" };

            return names[new Random().Next(names.Count)];
        }
        
        
        /// <summary>
        /// Receive Opponents Bomb and Calculate Ship Loses.
        /// </summary>
        /// <param name="xCoordinate">X Coordinate to Place the Bomb.</param>
        /// <param name="yCoordinate">Y Coordinate to Place the Bomb.</param>
        /// <returns>Players' States Changes of the Board After Shot.</returns>
        public List<BoardSquareState> ReceiveBomb(int xCoordinate, int yCoordinate)
        {
            var boardState = OwnBoard!.CopyBoard()[xCoordinate, yCoordinate];
            var boardStates = new List<BoardSquareState> { boardState };
            
            if (boardState.IsShip && !boardState.IsBomb)
            {
                var hitShip = OwnShips!.Find(x => boardState.IsReservedByShip(x.ShipNumber));
                hitShip!.Health -= 1;

                if (hitShip.Health == 0)
                {
                    IsLost = isLost();
                    boardStates.AddRange(OwnBoard.CalculateShipSunkCells(hitShip.ShipNumber));
                }
            }

            PlaceBomb(boardStates, false);
            return boardStates;
        }
        
        
        /// <summary>
        /// AI Creates Position for Shooting on Enemy's Board.
        /// </summary>
        /// <returns>X and Y Coordinate to Place Bomb.</returns>
        /// <exception cref="ArgumentException">If Called During Human Player Turn.</exception>
        public (int, int) AIBombPlaceCoordinates()
        {
            if (PlayerType == EPlayerType.Human)
                throw new ArgumentException("Human Player Can't Be Controlled by AI");

            return Strategy!.AIShootPosition();
        }
        
        
        /// <summary>
        /// AI Analyzes Received Data After Made Shoot.
        /// </summary>
        /// <param name="states">Board States, that Were Affected by Shot.</param>
        /// <exception cref="ArgumentException">If Called During Human Player Turn.</exception>
        public void AIAnalyzeShoot(List<BoardSquareState> states)
        {
            if (PlayerType == EPlayerType.Human)
                throw new ArgumentException("Human Player Can't Be Controlled by AI");

            Strategy!.AIShootAnalyze(states);
        }
        
        
              
        /// <summary>
        /// Create Ship for Player.
        /// </summary>
        /// <param name="name">Name of the Ship to Create.</param>
        /// <param name="xSize">X Coordinate Size of the Ship to Create.</param>
        /// <param name="ySize">Y Coordinate Size of the Ship to Create.</param>
        /// <returns>Indicator of Creation Success.</returns>
        public bool CreateShip(string? name, int xSize, int ySize)
        {
            var canShipBeCreated = OwnBoard!.CanShipFit(xSize, ySize);

            if (canShipBeCreated) OwnShips!.Add(new Ship(name, OwnShips.Count, xSize, ySize));
            
            return canShipBeCreated;
        }


        /// <summary>
        /// AI Creates Random Ships and Randomly Places Them on the Board.
        /// </summary>
        /// <param name="shipTouchRule">Ship Touching Rule to Consider when Placing on the Board.</param>
        public void AICreatePlaceShip(EShipTouchRule shipTouchRule)
        {
            var random = new Random();
            
            DeleteAllShips();
            
            OwnBoard!.RandomShipGenerator().ForEach(x => OwnShips!.Add(new Ship(null, OwnShips.Count, x.Item1, x.Item2)));

            foreach (var ship in OwnShips!)
            {
                var freeCells = OwnBoard.FreeBoardCells();
                
                do
                {
                    var (xPos, yPos) = freeCells[random.Next(freeCells.Count)];

                    if (OwnBoard.CanShipBeLocated(ship, xPos, yPos))
                    {
                        OwnBoard.LocateShip(ship, xPos, yPos, shipTouchRule);
                        break;
                    }
                    
                } while (true);
            }

            if (PlayerType == EPlayerType.AI) IsPlacementCompleted = true;
        }


        /// <summary>
        /// Place Players Ship on Players' Own Board.
        /// </summary>
        /// <param name="ShipNumber">Number of the Ship to Place.</param>
        /// <param name="xCoordinate">X Coordinate to Place Ship.</param>
        /// <param name="yCoordinate">Y Coordinate to Place Ship.</param>
        /// <param name="shipTouchRule">Ship Touching Rule to Consider when Placing on the Board.</param>
        /// <returns>Indicator of Location Success.</returns>
        public bool LocateShip(int ShipNumber, int xCoordinate, int yCoordinate, EShipTouchRule shipTouchRule)
        {
            var shipToLocate = OwnShips!.Find(x => x.ShipNumber == ShipNumber);
            
            if (shipToLocate == null) throw new ArgumentException("Ship with Given Serial Number Doesn't Exist.");

            var canBeLocated = OwnBoard!.CanShipBeLocated(shipToLocate, xCoordinate, yCoordinate);

            if (canBeLocated) OwnBoard.LocateShip(shipToLocate, xCoordinate, yCoordinate, shipTouchRule);
            
            else OwnShips.Remove(OwnShips.Find(x => x.ShipNumber == ShipNumber)!);
            
            return canBeLocated;
        }
        
        
        /// <summary>
        /// Delete All Players Ships.
        /// </summary>
        public void DeleteAllShips() 
        {
            OwnShips = new List<Ship>();
            OwnBoard = new GameBoard(OwnBoard!.XBoardSize, OwnBoard.YBoardSize);
        }
        
        
        /// <summary>
        /// Delete Last Players' Ship.
        /// </summary>
        public void DeleteLastShip() 
         {
             if (OwnShips!.Count == 0) return;

             var shipNumber = OwnShips.Last().ShipNumber;
             OwnShips!.RemoveAt(OwnShips.Count - 1);

             OwnBoard!.BoardShipCapacity += OwnShips.Last().Health;
             
             OwnBoard!.EraseShip(shipNumber);
         }
        
        
        /// <summary>
        /// Places the Bomb on Players' the Board.
        /// </summary>
        /// <param name="receivedStates">Received States from The Board to be Placed.</param>
        /// <param name="isFiring">Indicator of Board, where Bombs Should be Placed.</param>
        /// <returns>Boolean as Indicator of Preserving the Turn.</returns>
        public bool PlaceBomb(List<BoardSquareState> receivedStates, bool isFiring)
        {
            var boardToHit = isFiring ? FiringBoard : OwnBoard;
            
            receivedStates.ForEach(x => boardToHit!.PlaceBomb(x.XCoordinate, x.YCoordinate, x));

            return receivedStates.Any(x => x.IsShip && !x.IsBomb);
        }
        
        
        /// <summary>
        /// Add Shot To The Players Moves.
        /// </summary>
        public void AddShot(List<BoardSquareState> states)
        {
            PlayerShots!.Add(new PlayerShot());

            foreach (var state in states)
            {
                var isAdd = true;
                
                foreach (var shot in PlayerShots)
                {
                    isAdd = !shot.PlayerShots!.Any(y => y.Item1 == state.XCoordinate && y.Item2 == state.YCoordinate);
                    
                    if(!isAdd) break;
                }

                if (!isAdd) continue;
                
                PlayerShots.Last().PlayerShots!.Add((state.XCoordinate, state.YCoordinate));
            }

            if (PlayerShots[0].PlayerShots!.Count == 0) PlayerShots.RemoveAt(0);
        }
        
        
        /// <summary>
        /// Get the Needed Board Instance.
        /// </summary>
        /// <param name="isFiring">Defines Firing or Own Board Copy Needs.</param>
        /// <returns>Copy of the Needed Board.</returns>
        public BoardSquareState[,] GetCopiedBoard(bool isFiring) => isFiring ? FiringBoard!.CopyBoard() : OwnBoard!.CopyBoard();

        
        /// <summary>
        /// Indicates if Turn is Hold By Human Player.
        /// </summary>
        /// <returns>Indicator if Human Turn.</returns>
        public bool isHuman() => PlayerType == EPlayerType.Human;
        
        
        /// <summary>
        /// Player Game Loss Calculator.
        /// </summary>
        /// <returns>Boolean, if Game is Lost by Player.</returns>
        private bool isLost() => OwnShips!.Sum(x => x.Health) == 0;
        
        
        /// <summary>
        /// Indicator if Players Ship Quantity is Enough to Start Game.
        /// </summary>
        /// <returns>Indicator if Ship Quantity is Enough to Start Game.</returns>
        public bool IsEnoughShips() => OwnShips!.Count > 0;
        
        
        /// <summary>
        /// String Representation of the Player.
        /// </summary>
        public override string ToString() => Name!;
    }
}