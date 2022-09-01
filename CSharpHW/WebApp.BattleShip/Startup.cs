using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;


namespace WebApp.BattleShip
{
    
    /// <summary>
    /// BattleShip WebApp Startup Point.
    /// </summary>
    public class Startup
    {
        
        /// <summary>
        /// Method Adds Services to The Container.
        /// </summary>
        /// <param name="services">Services to be Added.</param>
        public void ConfigureServices(IServiceCollection services) => services.AddRazorPages();
        
       
        /// <summary>
        /// Method to Configure the HTTP Request Pipeline Called at Runtime.
        /// </summary>
        /// <param name="app">-</param>
        /// <param name="env">-</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapRazorPages(); });
        }
    }
}