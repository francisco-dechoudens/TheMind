using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using TheMind.Models;

namespace TheMind.Services
{
    public class PlayerService
    {
        public ObservableCollection<Player> getPlayers()
        {
            var data = DBClient.client
                .Child("Players")
                .AsObservable<Player>()
                .AsObservableCollection();

            return data;
        }

        public async Task<string> GetKey(string nickname)
        {
            var fireBaseObj = (await DBClient.client
                  .Child("Players")
                  .OnceAsync<Player>())
                  .Where(a => a.Object.NickName == nickname)
                  .FirstOrDefault();

            return fireBaseObj.Key;
        }

        public ObservableCollection<Card> GetPlayerCards(string key)
        {
            var data = DBClient.client
                .Child("Players")
                .Child(key)
                .Child("CardsInHand")
                .AsObservable<Card>()
                .AsObservableCollection();

            return data;
        }

        public IObservable<Firebase.Database.Streaming.FirebaseEvent<Player>> GetPlayerData(string nickname)
        {
            var data = DBClient.client
                .Child("Players")
                .AsObservable<Player>()
                .Where(job => job.Object.NickName == nickname);

            return data;
        }

        public async Task SavePlayerState(Player player)
        {
            player.Id = Guid.NewGuid();

            await DBClient.client
                .Child("Players")
                .PostAsync(player);
        }

        public async Task UpdatePlayerState(Player player)
        {
            var fireBaseObj = (await DBClient.client
                .Child("Players")
                .OnceAsync<Player>())
                .Where(a => a.Object.Id == player.Id)
                .FirstOrDefault();

            await DBClient.client.Child("Players")
                        .Child(fireBaseObj.Key)
                        .PutAsync(player);
        }
    }
}
