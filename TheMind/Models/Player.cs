using System;
using MvvmHelpers;

namespace TheMind.Models
{
    public class Player : ObservableObject
    {
        public Player()
        {
        }

        private bool isSeated;
        public bool IsSeated
        {
            get => isSeated;
            set => SetProperty(ref isSeated, value);
        }

        private string nickName;
        public string NickName
        {
            get => nickName;
            set => SetProperty(ref nickName, value);
        }
    }
}
