using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsBase.Models {

    using Catel.Data;
    using Catel.Runtime.Serialization;
    using StudentsBase.Models.Helpers;
    using System.Windows;
    using System.Xml.Serialization;

    [ValidateModel(typeof(StudentValidator))]
    public class StudentModel : ValidatableModelBase {

        public enum GENDER_TYPE { MALE, FEMALE }

        [XmlAttribute("Id")]
        public int Id {
            get { return GetValue<int>(IdProperty); }
            set { SetValue(IdProperty, value); }
        }

        [XmlElement("FirstName")]
        public string FirstName {
            get { return GetValue<string>(FirstNameProperty); }
            set { SetValue(FirstNameProperty, value); }
        }

        [XmlElement("Last")]
        public string Last {
            get { return GetValue<string>(LastProperty); }
            set { SetValue(LastProperty, value); }
        }

        [XmlElement("Age")]
        public int Age {
            get { return GetValue<int>(AgeProperty); }
            set { SetValue(AgeProperty, value); }
        }

        [XmlElement("Gender")]
        public int GenderInt32 {
            get { return GetValue<int>(GenderInt32Property); }
            set { SetValue(GenderInt32Property, value); }
        }
        /*
        [XmlIgnore]
        public GENDER_TYPE Gender {
            get { return GetValue<GENDER_TYPE>(GenderInt32Property); }
            set { SetValue(GenderInt32Property, (int)value); }
        }        
        */
        [XmlIgnore]
        public string FullName {
            get { return GetValue<string>(FirstNameProperty) + " " + GetValue<string>(LastProperty); }
        }

        [XmlIgnore]
        public static readonly PropertyData IdProperty = RegisterProperty("Id", typeof(int), 0);
        [XmlIgnore]
        public static readonly PropertyData FullNameProperty = RegisterProperty("FullName", typeof(string), "");
        [XmlIgnore]
        public static readonly PropertyData FirstNameProperty = RegisterProperty("FirstName", typeof(string), "");
        [XmlIgnore]
        public static readonly PropertyData LastProperty = RegisterProperty("Last", typeof(string), "");
        [XmlIgnore]
        public static readonly PropertyData AgeProperty = RegisterProperty("Age", typeof(int), 0);
        [XmlIgnore]
        public static readonly PropertyData GenderInt32Property = RegisterProperty("GenderInt32", typeof(int), 0);


    }
}
