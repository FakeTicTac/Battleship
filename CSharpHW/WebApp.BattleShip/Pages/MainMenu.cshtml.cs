using BLL.Menu;
using WebApp.BattleShip.Initializers;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace WebApp.BattleShip.Pages
{
    
    /// <summary>
    /// Main Menu Page: Represents Game Menu View.
    /// </summary>
    public class MainMenu : PageModel
    {

        /// <summary>
        /// Assembled Page View Menu Logic.
        /// </summary>
        public Menu Menu { get; } = MenuInitializers.MainMenuInitialize();
    }
}