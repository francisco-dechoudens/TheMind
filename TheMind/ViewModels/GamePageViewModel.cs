using System;
using MvvmHelpers;
using TheMind.Models;

namespace TheMind.ViewModels
{
    public class GamePageViewModel : BaseViewModel
    {
        public GamePageViewModel()
        {
            var newGame = new Game(1);
        }
    }
}
