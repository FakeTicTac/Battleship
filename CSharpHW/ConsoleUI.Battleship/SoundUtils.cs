using System;
using System.Media;
using System.Runtime.InteropServices;

namespace ConsoleUI.Battleship
{
    public static class SoundUtils
    {
        
        /// <summary>
        /// Play Game Music.
        /// </summary>
        public static void PlayMusic(string musicName)
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) return;
            
            var musicPath = AppContext.BaseDirectory.Replace("\\ConsoleApp.BattleShip\\bin\\Debug\\net5.0", "") 
                            + "\\Sounds.Battleship\\" + musicName;
            
            var player = new SoundPlayer { SoundLocation = musicPath};
                
            player.Play();
        }
    }
}