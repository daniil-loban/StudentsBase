using Catel.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catel;
using Catel.Data;
using System.Collections;
using System.ComponentModel;
using System.Windows;

using StudentsBase.Models;
using StudentsBase.Models.Helpers;
using System.Collections.ObjectModel;
using Catel.IoC;
using Catel.Services;

namespace StudentsBase.ViewModels {
    class MainWindowViewModel : ViewModelBase {

        public override string Title { get { return "Students database"; } }


        public FullyObservableCollection<StudentModel> StudentsList {
            get { return GetValue<FullyObservableCollection<StudentModel>>(StudentsListProperty); }
            private set {
                SetValue(StudentsListProperty, value);

            }
        }

        public static readonly PropertyData StudentsListProperty =
            RegisterProperty("StudentsList", typeof(FullyObservableCollection<StudentModel>), null);


        public Command FileNew { get; private set; }
        public Command FileOpen { get; private set; }
        public Command FileClose { get; private set; }
        public Command FileSave { get; private set; }
        public Command FileSaveAs { get; private set; }
        public Command FileExit { get; private set; }
        public TaskCommand EditAdd { get; private set; }
        public TaskCommand EditChange { get; private set; }
        public Command EditDelete   { get; private set; }

        public MainWindowViewModel() {
            FileNew     = new Command(OnFileNewExecute, OnFileNewCanExecute);
            FileOpen    = new Command(OnFileOpenExecute, OnFileOpenCanExecute);
            FileClose   = new Command(OnFileCloseExecute, OnFileCloseCanExecute);
            FileSave    = new Command(OnFileSaveExecute, OnFileSaveCanExecute);
            FileSaveAs  = new Command(OnFileSaveAsExecute, OnFileSaveAsCanExecute);
            FileExit    = new Command(OnFileExitExecute, OnFileExitCanExecute);
            EditAdd     = new TaskCommand(OnEditAddExecute, OnEditAddCanExecute);
            EditChange  = new TaskCommand(OnEditChangeExecuteAsync, OnEditChangeCanExecute);
            EditDelete  = new Command(OnEditDeleteExecute, OnEditDeleteCanExecute);
            
            OnFileOpenExecute();
        }

        public StudentModel SelectedStudent {
            get {
                return GetValue<StudentModel>(SelectedStudentProperty);
            }
            set {

                SetValue(SelectedStudentProperty, value);

                EditChange = new TaskCommand(OnEditChangeExecuteAsync, OnEditChangeCanExecute);
                EditDelete = new Command(OnEditDeleteExecute, OnEditDeleteCanExecute);
            }
        }

        public static readonly PropertyData SelectedStudentProperty =
        RegisterProperty("SelectedStudent", typeof(StudentModel), null);


        private bool OnFileNewCanExecute() => true;
        private void OnFileNewExecute() => MessageBox.Show("File New");
        private bool OnFileOpenCanExecute() => true;
        private void OnFileOpenExecute() {

            Students st = new Students();
            st.ReadFromFile("D://Students.xml");
            StudentsList = new FullyObservableCollection<StudentModel>(st.students);
            
        }
        private bool OnFileCloseCanExecute() => true;
        private void OnFileCloseExecute() => MessageBox.Show("File Close");

        private bool OnFileSaveCanExecute() => true;
        private void OnFileSaveExecute() => MessageBox.Show("File Save");

        private bool OnFileSaveAsCanExecute() => true;
        private void OnFileSaveAsExecute() {

            Students st = new Students();
            st.students = StudentsList;
            st.WriteToFile("D://Students12345.xml");
        }

        private bool OnFileExitCanExecute() => true;
        private void OnFileExitExecute() => MessageBox.Show("File Exit");

        private bool OnEditAddCanExecute() => true;
        private async Task OnEditAddExecute() {

            int? NextId = null;
            if (StudentsList.Count > 0)
                NextId = StudentsList.Last().Id +1 ;

            StudentModel student = new StudentModel() {  Id=(NextId ?? 0) };
            var viewModel = new EditRecordViewModel("New Record", student);

            var dependencyResolver = this.GetDependencyResolver();
            var uiVisualizerService = dependencyResolver.Resolve<IUIVisualizerService>();

            if (await uiVisualizerService.ShowDialogAsync(viewModel) ?? false) {
                if (!viewModel.Student.HasErrors) {
                    StudentsList.Add(viewModel.Student);
                }
            }

        }

        private bool OnEditChangeCanExecute() {

            return (SelectedStudent != null);
        }

        private int CountSelection() {
            return (StudentsList).Where(x => x.IsSelected == true).Count();
        }

        private async Task OnEditChangeExecuteAsync() {

            StudentModel currentStudent = StudentsList.Where<StudentModel>(x => x.IsSelected == true).First<StudentModel>();

            if (currentStudent == null)
                return;

            StudentModel student = new StudentModel(currentStudent);

            var viewModel = new EditRecordViewModel("Change Record", student);

            var dependencyResolver = this.GetDependencyResolver();
            var uiVisualizerService = dependencyResolver.Resolve<IUIVisualizerService>();
            
            if (await uiVisualizerService.ShowDialogAsync(viewModel) ?? false) {
                if (!viewModel.Student.HasErrors) {

                    currentStudent.FirstName = viewModel.Student.FirstName;
                    currentStudent.Last = viewModel.Student.Last;
                    currentStudent.Age = viewModel.Student.Age;
                    currentStudent.GenderInt32 = viewModel.Student.GenderInt32;
                }
            }
        }


        private bool OnEditDeleteCanExecute() => CountSelection()>0;
        private void  OnEditDeleteExecute() {

            if (MessageBox.Show("Do you want remove " + CountSelection() + " record(s)?"
                , "Confirmation", MessageBoxButton.OKCancel) == MessageBoxResult.OK) {
                for (int i = StudentsList.Count - 1; i >= 0; i--) {
                    if ((StudentsList[i].IsSelected == true)) {
                        StudentsList.RemoveAt(i);
                    }
                }
            }
        }
    }
}
