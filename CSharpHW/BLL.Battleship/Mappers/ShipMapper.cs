
using DL = DAL.Battleship.DTO;


namespace BLL.Battleship.Mappers
{
    
    /// <summary>
    /// Class Defines Ship Business Logic Layer Mapper.
    /// </summary>
    public static class ShipMapper
    {
        
        /// <summary>
        /// Map Data Access Layer Ship Object To The Business Logic Layer Ship Object.
        /// </summary>
        /// <returns>Business Logic Layer Ship Object.</returns>
        public static Ship MapToBll(DL.Ship ship)
        {
            return new Ship
            {
                XSize = ship.XSize,
                YSize = ship.YSize,
                Name = ship.Name,
                Health = ship.Health,
                IsPlaced = ship.IsPlaced,
                ShipNumber = ship.ShipNumber
            };
        }


        /// <summary>
        /// Map Business Logic Layer Ship Object To Data Access Layer Ship Object.
        /// </summary>
        /// <returns>Data Access Layer Ship Object.</returns>
        public static DL.Ship MapToDal(Ship ship)
        {
            return new DL.Ship
            {
                XSize = ship.XSize,
                YSize = ship.YSize,
                Name = ship.Name,
                Health = ship.Health,
                IsPlaced = ship.IsPlaced,
                ShipNumber = ship.ShipNumber
            };
        }
    }
}