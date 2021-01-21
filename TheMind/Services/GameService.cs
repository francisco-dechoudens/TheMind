using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using TheMind.Models;

namespace TheMind.Services
{
    public class GameService
    {
        public ObservableCollection<Game> GetGameData()
        {
            var data = DBClient.client
                .Child("Games")
                .AsObservable<Game>()
                .AsObservableCollection();

            return data;
        }


        public async Task SaveGameState(Game game)
        {
            await DBClient.client
                .Child("Games")
                .PostAsync(game);
        }
    }
}
