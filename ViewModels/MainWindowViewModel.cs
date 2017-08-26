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

namespace StudentsBase.ViewModels {
    class MainWindowViewModel : ViewModelBase {

        public override string Title { get { return "Students database"; } }

        public static readonly PropertyData StudentsListProperty = RegisterProperty("StudentsList", typeof(FullyObservableCollection<StudentModel>), null);

        public FullyObservableCollection<StudentModel> StudentsList {
            get { return GetValue<FullyObservableCollection<StudentModel>>(StudentsListProperty); }
            private set {
                SetValue(StudentsListProperty, value);
            }
        }

        public Command FileNew      { get; private set; }
        public Command FileOpen     { get; private set; }
        public Command FileClose    { get; private set; }
        public Command FileSave     { get; private set; }
        public Command FileSaveAs   { get; private set; }
        public Command FileExit     { get; private set; }
        public Command EditAdd      { get; private set; }
        public Command EditChange   { get; private set; }
        public Command EditDelete   { get; private set; }
        

        public MainWindowViewModel() {
            FileNew     = new Command(OnFileNewExecute, OnFileNewCanExecute);
            FileOpen    = new Command(OnFileOpenExecute, OnFileOpenCanExecute);
            FileClose   = new Command(OnFileCloseExecute, OnFileCloseCanExecute);
            FileSave    = new Command(OnFileSaveExecute, OnFileSaveCanExecute);
            FileSaveAs  = new Command(OnFileSaveAsExecute, OnFileSaveAsCanExecute);
            FileExit    = new Command(OnFileExitExecute, OnFileExitCanExecute);
            EditAdd     = new Command(OnEditAddExecute, OnEditAddCanExecute);
            EditChange  = new Command(OnEditChangeExecute, OnEditChangeCanExecute);
            EditDelete  = new Command(OnEditDeleteExecute, OnEditDeleteCanExecute);
            OnFileOpenExecute();
        }

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
        private void OnEditAddExecute() {
            StudentModel sm = new StudentModel() {  Age = 16,
                                                    FirstName = "fdsfds",
                                                    Last = "dfsfdf",
                                                    Id = StudentsList.Last<StudentModel>().Id+1 ,
                                                    GenderInt32 = 0 };
            if (sm.HasErrors)
                MessageBox.Show(sm.GetErrorMessage());
            else
                StudentsList.Add(sm);
        }

        private bool OnEditChangeCanExecute() => StudentsList.Count > 0;
        private void OnEditChangeExecute() => MessageBox.Show("Edit Change");

        private bool OnEditDeleteCanExecute() => StudentsList.Count > 0;
        private void OnEditDeleteExecute() => MessageBox.Show("Edit Delete");
    }
}
