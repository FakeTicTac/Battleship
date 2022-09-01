using System;
using System.Linq;
using BLL.Battleship.Enums;
using BLL.Battleship.Interfaces;
using BLL.Battleship.Strategies;

using DL = DAL.Battleship.DTO;


namespace BLL.Battleship.Mappers
{
    
    /// <summary>
    /// Class Defines Strategy Business Logic Layer Mapper.
    /// </summary>
    public static class StrategyMapper
    {
        
        /// <summary>
        /// Map Data Access Layer Strategy Object To The Business Logic Layer Strategy Object.
        /// </summary>
        /// <returns>Business Logic Layer Strategy Object.</returns>
        public static IStrategy? MapToBll(DL.Strategy? strategy, EAILevel? aiLevel)
        {
            if (strategy == null) return null;

            return aiLevel switch
            {
                EAILevel.Easy => new AIEasyStrategy
                {
                    XSize = strategy.XSize,
                    YSize = strategy.YSize,
                    AvailableCells =
                        strategy.StrategyCoordinates?
                            .Where(x => x.StrategyCoordinateType == (int) EStrategyCoordinate.Available).ToList()
                            .ConvertAll(x => (x.Coordinate!.xCoordinateValue, x.Coordinate!.yCoordinateValue)),
                    HuntingCells =
                        strategy.StrategyCoordinates?
                            .Where(x => x.StrategyCoordinateType == (int) EStrategyCoordinate.Hunt).ToList()
                            .ConvertAll(x => (x.Coordinate!.xCoordinateValue, x.Coordinate!.yCoordinateValue)),
                    HuntingHits =
                        strategy.StrategyCoordinates?
                            .Where(x => x.StrategyCoordinateType == (int) EStrategyCoordinate.Hit).ToList()
                            .ConvertAll(x => (x.Coordinate!.xCoordinateValue, x.Coordinate!.yCoordinateValue)),
                    HuntingMisses = 
                        strategy.StrategyCoordinates?
                            .Where(x => x.StrategyCoordinateType == (int) EStrategyCoordinate.Miss).ToList()
                            .ConvertAll(x => (x.Coordinate!.xCoordinateValue, x.Coordinate!.yCoordinateValue))
                },
                EAILevel.Medium => new AIMediumStrategy
                {
                    AvailableCells =
                        strategy.StrategyCoordinates?
                            .Where(x => x.StrategyCoordinateType == (int) EStrategyCoordinate.Available).ToList()
                            .ConvertAll(x => (x.Coordinate!.xCoordinateValue, x.Coordinate!.yCoordinateValue)),
                    HuntingCells =
                        strategy.StrategyCoordinates?
                            .Where(x => x.StrategyCoordinateType == (int) EStrategyCoordinate.Hunt).ToList()
                            .ConvertAll(x => (x.Coordinate!.xCoordinateValue, x.Coordinate!.yCoordinateValue)),
                    HuntingHits =
                        strategy.StrategyCoordinates?
                            .Where(x => x.StrategyCoordinateType == (int) EStrategyCoordinate.Hit).ToList()
                            .ConvertAll(x => (x.Coordinate!.xCoordinateValue, x.Coordinate!.yCoordinateValue)),
                    HuntingMisses = 
                        strategy.StrategyCoordinates?
                            .Where(x => x.StrategyCoordinateType == (int) EStrategyCoordinate.Miss).ToList()
                            .ConvertAll(x => (x.Coordinate!.xCoordinateValue, x.Coordinate!.yCoordinateValue))
                },
                EAILevel.Hard => new AIHardStrategy
                {
                    AvailableCells =
                        strategy.StrategyCoordinates?
                            .Where(x => x.StrategyCoordinateType == (int) EStrategyCoordinate.Available).ToList()
                            .ConvertAll(x => (x.Coordinate!.xCoordinateValue, x.Coordinate!.yCoordinateValue)),
                    HuntingCells =
                        strategy.StrategyCoordinates?
                            .Where(x => x.StrategyCoordinateType == (int) EStrategyCoordinate.Hunt).ToList()
                            .ConvertAll(x => (x.Coordinate!.xCoordinateValue, x.Coordinate!.yCoordinateValue)),
                    HuntingHits =
                        strategy.StrategyCoordinates?
                            .Where(x => x.StrategyCoordinateType == (int) EStrategyCoordinate.Hit).ToList()
                            .ConvertAll(x => (x.Coordinate!.xCoordinateValue, x.Coordinate!.yCoordinateValue)),
                    HuntingMisses = 
                        strategy.StrategyCoordinates?
                            .Where(x => x.StrategyCoordinateType == (int) EStrategyCoordinate.Miss).ToList()
                            .ConvertAll(x => (x.Coordinate!.xCoordinateValue, x.Coordinate!.yCoordinateValue))
                },
                null => null,
                _ => throw new ArgumentOutOfRangeException(nameof(aiLevel), aiLevel, null)
            };
        }

        
        /// <summary>
        /// Map Business Logic Layer Strategy Object To Data Access Layer Strategy Object.
        /// </summary>
        /// <returns>Data Access Layer Strategy Object.</returns>
        public static DL.Strategy? MapToDal(IStrategy? strategy)
        {
            if (strategy == null) return null;

            var strategyCoordinates = strategy.HuntingHits!.ConvertAll(x => new DL.StrategyCoordinate
            {
                StrategyCoordinateType = (int)EStrategyCoordinate.Hit,
                Coordinate = new DL.Coordinate
                {
                    xCoordinateValue = x.Item1,
                    yCoordinateValue = x.Item2
                }
            });
            
            strategyCoordinates.AddRange(strategy.AvailableCells!.ConvertAll(x => new DL.StrategyCoordinate
            {
                StrategyCoordinateType = (int) EStrategyCoordinate.Available,
                Coordinate = new DL.Coordinate
                {
                    xCoordinateValue = x.Item1,
                    yCoordinateValue = x.Item2
                }
            }));
            
            strategyCoordinates.AddRange(strategy.HuntingCells!.ConvertAll(x => new DL.StrategyCoordinate
            {
                StrategyCoordinateType = (int) EStrategyCoordinate.Hunt,
                Coordinate = new DL.Coordinate
                {
                    xCoordinateValue = x.Item1,
                    yCoordinateValue = x.Item2
                }
            }));
            
            strategyCoordinates.AddRange(strategy.HuntingMisses!.ConvertAll(x => new DL.StrategyCoordinate
            {
                StrategyCoordinateType = (int) EStrategyCoordinate.Miss,
                Coordinate = new DL.Coordinate
                {
                    xCoordinateValue = x.Item1,
                    yCoordinateValue = x.Item2
                }
            }));
            
            return new DL.Strategy
            {
                XSize = strategy.XSize,
                YSize = strategy.YSize,
                StrategyCoordinates = strategyCoordinates
            };
        }
    }
}