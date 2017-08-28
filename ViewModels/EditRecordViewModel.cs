using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsBase.ViewModels {
    using Catel.Data;
    using Catel.MVVM;
    using StudentsBase.Models;
    using System.ComponentModel;

    public class EditRecordViewModel : ViewModelBase {

        private string title;


        public EditRecordViewModel(string title, StudentModel student) {
            this.title = title;
            this.Student = student;
        }

        public void SetTitle(string title) {
            this.title = title;
        }

        public override string Title {
            get { return title; }
        }

        public StudentModel Student {
            get { return GetValue<StudentModel>(StudentProperty); }
            set {
                SetValue(StudentProperty, value);
            }
        }

        public static readonly PropertyData StudentProperty = RegisterProperty("Student", typeof(StudentModel), null);


        private IEnumerable<String> EnumToString() {
            foreach (var suit in Enum.GetValues(typeof(StudentModel.GENDER_TYPE))) {
                switch (suit) {
                    case StudentModel.GENDER_TYPE.MALE:
                        yield return ("муж.");
                        break;
                    case StudentModel.GENDER_TYPE.FEMALE:
                        yield return ("жен.");
                        break;

                }
            }
        }

        public IEnumerable<string> GenderTypes {
            get {
                return EnumToString(); 
            }
        }
        
        /*
        public IEnumerable<StudentModel.GENDER_TYPE> GenderTypes {
            get {
                return Enum.GetValues(typeof(StudentModel.GENDER_TYPE)).Cast<StudentModel.GENDER_TYPE>();
            }
        }
        */

    }
}
