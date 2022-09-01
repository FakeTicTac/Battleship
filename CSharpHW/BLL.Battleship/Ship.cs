using System;
using System.Collections.Generic;


namespace BLL.Battleship
{
    
    /// <summary>
    /// Represents Battleship Game Ship State.
    /// </summary>
    public class Ship
    {
        
        /// <summary>
        /// Ship X Coordinate Lenght.
        /// </summary>
        public int XSize { get; init; }

        
        /// <summary>
        /// Ship Y Coordinate Lenght.
        /// </summary>
        public int YSize { get; init; }
        
        
        /// <summary>
        /// Name of the Ship.
        /// </summary>
        public string? Name { get; init; }


        /// <summary>
        /// Health of the Ship.
        /// </summary>
        public int Health { get; set; }
        
        
        /// <summary>
        /// Indicator of Ship Placement on The Board.
        /// </summary>
        public bool IsPlaced { get; init; }
        
        
        /// <summary>
        /// Ships' Serial Number.
        /// </summary>
        public int ShipNumber { get; init; }
        
        
        /// <summary>
        /// Basic Parameterless Ship Constructor.
        /// </summary>
        public Ship() {}
        
        
        /// <summary>
        /// Basic Ship Constructor. Initializing All Needed for Ship Data.
        /// </summary>
        /// <param name="name">Name of the Current Ship. (Null if AI Generates It Automatically)</param>
        /// <param name="shipNumber">Serial Number of the Ship.</param>
        /// <param name="xSize">Ship X Coordinate Lenght.</param>
        /// <param name="ySize">Ship Y Coordinate Lenght.</param>
        public Ship(string? name, int shipNumber, int xSize, int ySize)
        {
            XSize = xSize;
            YSize = ySize;
            
            Name = name ?? AINameInitialization();

            IsPlaced = false;
            Health = xSize * ySize;
            ShipNumber = shipNumber;
        }
        
        
        /// <summary>
        /// AI Initializes Random Ship Name from Predicated List.
        /// </summary>
        /// <returns>Random Player Name</returns>
        private static string AINameInitialization()
        {
            var names = new List<string> { "Cardiff", "Chevron", "Flora", "Chilton", "Camel", "TThe Black Eagle" };

            return names[new Random().Next(names.Count)];
        }

        
        /// <summary>
        /// Get Sizes of the Ship on X and Y Coordinates.
        /// </summary>
        /// <returns>Sizes of the Ship.</returns>
        public (int, int) GetSizes() => (XSize, YSize);
        
        
        /// <summary>
        /// String representation of the Ship.
        /// </summary>
        public override string ToString() => Name!;
    }
}