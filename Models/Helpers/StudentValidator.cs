
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsBase.Models.Helpers {
    using Catel.Data;

    class StudentValidator : ValidatorBase<StudentModel> {

        protected override void ValidateFields(StudentModel instance, List<IFieldValidationResult> validationResults) {
            if (string.IsNullOrWhiteSpace(instance.FirstName)) {
                validationResults.Add(FieldValidationResult.CreateError(StudentModel.FirstNameProperty, "First name is required"));
            }

            if (string.IsNullOrWhiteSpace(instance.Last)) {
                validationResults.Add(FieldValidationResult.CreateError(StudentModel.LastProperty, "Last name is required"));
            }

            if (instance.Age < 16 || instance.Age > 100) {
                validationResults.Add(FieldValidationResult.CreateError(StudentModel.AgeProperty, "Age must be between 16 and 100"));
            }

        }

        protected override void ValidateBusinessRules(StudentModel instance, List<IBusinessRuleValidationResult> validationResults) {
            // No business rules validations yet
        }

    }
}
