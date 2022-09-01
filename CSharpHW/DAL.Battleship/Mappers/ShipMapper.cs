
using DB = Domain.Battleship;
using DL = DAL.Battleship.DTO;


namespace DAL.Battleship.Mappers
{
    
    /// <summary>
    /// Class Defines Ship Mapper.
    /// </summary>
    public static class ShipMapper
    {
        
        /// <summary>
        /// Map Domain Ship Object To The Data Access Layer Ship Object.
        /// </summary>
        /// <returns>Data Access Layer Ship Object.</returns>
        public static DL.Ship MapToDal(DB.Ship ship)
        {
            return new DL.Ship
            {
                XSize = ship.XSize,
                YSize = ship.YSize,
                Health = ship.Health,
                IsPlaced = ship.IsPlaced,
                Name = ship.Name,
                ShipNumber = ship.ShipNumber
            };
        }


        /// <summary>
        /// Map Data Access Layer Ship Object To The Domain Ship Object.
        /// </summary>
        /// <returns>Data Access Layer Ship Object.</returns>
        public static DB.Ship MapToDb(DL.Ship ship)
        {
            return new DB.Ship
            {
                XSize = ship.XSize,
                YSize = ship.YSize,
                Health = ship.Health,
                IsPlaced = ship.IsPlaced,
                Name = ship.Name,
                ShipNumber = ship.ShipNumber
            };
        }
    }
}