﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsBase.Models {

    using Catel.Data;
    using Catel.Runtime.Serialization;
    using StudentsBase.Models.Helpers;
    using System.ComponentModel;
    using System.Windows;
    using System.Xml.Serialization;
    
    [ValidateModel(typeof(StudentValidator))]
    public class StudentModel : ValidatableModelBase {

        public StudentModel() {

        }

        public  StudentModel(StudentModel previous) {
            FirstName   = previous.FirstName;
            Last        = previous.Last;
            Age         = previous.Age;
            GenderInt32 = previous.GenderInt32;
            Id          = previous.Id;
        }


        public enum GENDER_TYPE {
            MALE,
            FEMALE }

        [XmlAttribute("Id")]
        public int Id {
            get { return GetValue<int>(IdProperty); }
            set { SetValue(IdProperty, value); }
        }

        [XmlElement("FirstName")]
        public string FirstName {
            get { return GetValue<string>(FirstNameProperty); }
            set { SetValue(FirstNameProperty, value); SetValue(FullNameProperty, value); }
        }

        [XmlElement("Last")]
        public string Last {
            get { return GetValue<string>(LastProperty); }
            set { SetValue(LastProperty, value); SetValue(FullNameProperty, value); }
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
        
        [XmlIgnore]
        public GENDER_TYPE Gender {
            get { return GetValue<GENDER_TYPE>(GenderInt32Property); }
            set { SetValue(GenderInt32Property, (int)value); }
        }        
        
        [XmlIgnore]
        public bool IsSelected {
            get { return GetValue<bool>(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        [XmlIgnore]
        public string FullName {
            get { return GetValue<string>(FirstNameProperty) + " "  + GetValue<string>(LastProperty); }
            set { SetValue(FullNameProperty, value); }
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
        [XmlIgnore]
        public static readonly PropertyData IsSelectedProperty = RegisterProperty("IsSelected", typeof(bool), false);


    }
}
