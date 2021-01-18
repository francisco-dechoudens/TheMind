using System;
using Firebase.Database;
using TheMind.Infrastructure;

namespace TheMind.Services
{
    public class DBClient
    {
        public static FirebaseClient client;

        public static void Init()
        {
            client = new FirebaseClient(Config.FirebaseApiUrl);
        }
    }
}
