using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using TheMind.Models;

namespace TheMind.Services
{
    public class FirebaseDatabase
    {
        FirebaseClient client;

        public FirebaseDatabase()
        {
            client = new FirebaseClient("https://themind-baas-default-rtdb.firebaseio.com/");
        }

        public ObservableCollection<Student> getStudent()
        {
            var data = client
                .Child("Students")
                .AsObservable<Student>()
                .AsObservableCollection();

            return data;
        }

        public async Task AddStudent(string firstName)
        {
            Student s = new Student() { FirstName = firstName };
            await client
                .Child("Students")
                .PostAsync(s);
        }
    }
}
