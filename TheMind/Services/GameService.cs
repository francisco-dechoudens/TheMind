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

        public IObservable<Firebase.Database.Streaming.FirebaseEvent<Game>> GetGameData(string tableName)
        {
            var data = DBClient.client
                .Child("Games")
                .AsObservable<Game>()
                .Where(job => job.Object.TableName == tableName);

            return data;
        }


        public async Task SaveGameState(Game game)
        {
            game.Id = Guid.NewGuid();

            await DBClient.client
                .Child("Games")
                .PostAsync(game);
        }

        public async Task UpdateGameState(Game game)
        {
            var fireBaseObj = (await DBClient.client
                .Child("Games")
                .OnceAsync<Game>())
                .Where(a => a.Object.Id == game.Id)
                .FirstOrDefault();

            await DBClient.client.Child("Games")
                        .Child(fireBaseObj.Key)
                        .PutAsync(game);
        }
    }
}
