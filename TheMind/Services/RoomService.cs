using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using TheMind.Models;

namespace TheMind.Services
{
    public class RoomService
    {
        public RoomService()
        {
        }

        public ObservableCollection<Student> getRoom()
        {
            var data = DBClient.client
                .Child("Room")
                .AsObservable<Student>()
                .AsObservableCollection();

            return data;
        }

        public async Task CreateRoom(Room room)
        {
            await DBClient.client
                .Child("Room")
                .PostAsync(room);
        }
    }
}
