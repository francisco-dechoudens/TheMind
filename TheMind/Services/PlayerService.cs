using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using TheMind.Models;

namespace TheMind.Services
{
    public class PlayerService
    {
        public ObservableCollection<Player> getPlayerData()
        {
            var data = DBClient.client
                .Child("Player")
                .AsObservable<Player>()
                .AsObservableCollection();

            return data;
        }


        public async Task SavePlayerState(Player player)
        {
            await DBClient.client
                .Child("Player")
                .PostAsync(player);
        }
    }
}
