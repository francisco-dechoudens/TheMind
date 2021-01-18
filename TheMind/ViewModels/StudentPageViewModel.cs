using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MvvmHelpers;
using MvvmHelpers.Commands;
using TheMind.Models;
using TheMind.Services;

namespace TheMind.ViewModels
{
    public class StudentPageViewModel : BaseViewModel
    {
        public string FirstName { get; set; }

        private FirebaseDatabase services;

        public Command AddStudentCommand { get; }

        private ObservableCollection<Student> students = new ObservableCollection<Student>();
        public ObservableCollection<Student> Students
        {
            get { return students; }
            set {
                students = value;
                OnPropertyChanged();
            }
        }

        public StudentPageViewModel()
        {
            services = new FirebaseDatabase();
            Students = services.getStudent();
            AddStudentCommand = new Command(async () => await addStudentAsync(FirstName));
        }

        public async Task addStudentAsync(string FirstName)
        {
            await services.AddStudent(FirstName);
        }
    }
}
