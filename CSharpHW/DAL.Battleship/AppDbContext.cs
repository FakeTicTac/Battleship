using System.Linq;
using Microsoft.EntityFrameworkCore;

using DB = Domain.Battleship;


namespace DAL.Battleship
{
    
    /// <summary>
    /// Database Layer Implementation: Database Creation from Models.
    /// </summary>
    public class AppDbContext : DbContext
    {
        
        /// <summary>
        /// Database Connection String Definition.
        /// </summary>
        private const string ConnectionString = "Server=barrel.itcollege.ee;User Id=student;Password=Student.Pass.1;Database=student_romakeDbBattleShip;MultipleActiveResultSets=true";
        
        
        /// <summary>
        /// Entity Set for StrategyCoordinates Creation, Reading, Updating, and Deleting.
        /// </summary>
        public DbSet<DB.StrategyCoordinate> StrategyCoordinates { get; set; } = default!;
        
        
        /// <summary>
        /// Entity Set for BattleShips Creation, Reading, Updating, and Deleting.
        /// </summary>
        public DbSet<DB.BattleShip> Battleships { get; set; } = default!;

        
        /// <summary>
        /// Entity Set for BoardSquareStates Creation, Reading, Updating, and Deleting.
        /// </summary>
        public DbSet<DB.BoardSquareState> BoardSquareStates { get; set; } = default!;

        
        /// <summary>
        /// Entity Set for Coordinates Creation, Reading, Updating, and Deleting.
        /// </summary>
        public DbSet<DB.Coordinate> Coordinates { get; set; } = default!;

        
        /// <summary>
        /// Entity Set for GameBoards Creation, Reading, Updating, and Deleting.
        /// </summary>
        public DbSet<DB.GameBoard> GameBoards { get; set; } = default!;
        

        /// <summary>
        /// Entity Set for Players Creation, Reading, Updating, and Deleting.
        /// </summary>
        public DbSet<DB.Player> Players { get; set; } = default!;


        /// <summary>
        /// Entity Set for PlayerShots Creation, Reading, Updating, and Deleting.
        /// </summary>
        public DbSet<DB.PlayerShot> PlayerShots { get; set; } = default!;


        /// <summary>
        /// Entity Set for Settings Creation, Reading, Updating, and Deleting.
        /// </summary>
        public DbSet<DB.Settings> Settings { get; set; } = default!;


        /// <summary>
        /// Entity Set for Ships Creation, Reading, Updating, and Deleting.
        /// </summary>
        public DbSet<DB.Ship> Ships { get; set; } = default!;


        /// <summary>
        /// Entity Set for Strategies Creation, Reading, Updating, and Deleting.
        /// </summary>
        public DbSet<DB.Strategy> Strategies { get; set; } = default!;

        
        /// <summary>
        /// Database Setting Configuration.
        /// </summary>
        /// <param name="optionsBuilder">Database Option Builder.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
            => optionsBuilder.UseSqlServer(ConnectionString , o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
        
        
        /// <summary>
        /// Method Define Models' Configurations.
        /// </summary>
        /// <param name="builder">Define API for Model Configuration.</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }
}