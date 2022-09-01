using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;


namespace WebApp.BattleShip
{

    /// <summary>
    /// BattleShip WebApp Startup Point.
    /// </summary>
    public static class Program
    {
        
        /// <summary>
        /// Run Battleship WebApp.
        /// </summary>
        /// <param name="args">RunTime Argument Values.</param>
        public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();


        /// <summary>
        /// App Resources Encapsulation Creation. 
        /// </summary>
        /// <param name="args">RunTime Argument Values.</param>
        /// <returns>Resources Encapsulation</returns>
        private static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}