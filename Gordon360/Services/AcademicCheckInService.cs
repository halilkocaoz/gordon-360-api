using Gordon360.Exceptions.CustomExceptions;
using Gordon360.Models;
using Gordon360.Models.ViewModels;
using Gordon360.Services.ComplexQueries;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace Gordon360.Services
{
    /// <summary>
    /// Service Class that facilitates data transactions between the AcademicCheckInController and the CheckIn database model.
    /// </summary>
	/// 
    public class AcademicCheckInService : IAcademicCheckInService
    {
        public AcademicCheckInService()
        {

        }

        /*
        public IEnumerable<AcademicCheckInViewModel> GetHolds(string id)
        {
            return RawSqlQuery<AcademicCheckInViewModel>.query("HOLDS");
        }

        public IEnumerable<AcademicCheckInViewModel> GetDemographic(string id)
        {
            return RawSqlQuery<AcademicCheckInViewModel>.query("DEMOGRAPHIC");
        }
        */

        /// <summary> Stores the emergency contact information of a particular user </summary>
        /// <param name="data"> The object that stores the contact info </param>
        /// <param name="id"> The students id number</param>
        /// <returns> The stored data </returns>
        public EmergencyContact PutEmergencyContact(EmergencyContact data, string id)
        {
            var studentIDParam = new SqlParameter("@StudentID", id);
            var contactIDParam = new SqlParameter("@ContactNum", data.SEQ_NUM);
            var contactLastNameParam = new SqlParameter("@ContactLastName", data.lastname);
            var contactFirstNameParam = new SqlParameter("@ContactFirstName", data.firstname);
            var contactHomePhoneParam = new SqlParameter("@ContactHomePhone", FormatNumber(data.HomePhone));
            var contactMobilePhoneParam = new SqlParameter("@ContactMobilePhone", FormatNumber(data.MobilePhone));
            var contactRelationshipParam = new SqlParameter("@ContactRelationship", data.relationship);
            var notesParam = new SqlParameter("@Notes", data.notes);
            var usernameParam = new SqlParameter("@Username", "360Web (" + data.lastname + ", " + data.lastname + ")");
            var jobNameParam = new SqlParameter("@JobName", "Enrollment-Checkin");

            // Run stored procedure
            var result = RawSqlQuery<EmergencyContactViewModel>.query("UPDATE_EMRGCONTACT @StudentID, @ContactNum, @ContactLastName, @ContactFirstName, @ContactHomePhone, @ContactMobilePhone, @ContactRelationship, @Notes, @Username, @JobName", studentIDParam, contactIDParam, contactLastNameParam, contactFirstNameParam, contactHomePhoneParam, contactMobilePhoneParam, contactRelationshipParam, notesParam, usernameParam, jobNameParam);
            if (result == null)
            {
                throw new ResourceNotFoundException() { ExceptionMessage = "The data was not found." };
            }
            return data;
        }

        // need user ID, unformatted number, whether they would like their phone number private, and whether or not they have a phone number


        /*
        /// <summary> Stores the emergency contact information of a particular user </summary>
        /// <param name="data"> The phone number object for the user </param>
        /// <param name="id"> The id of the student to be updated
        /// <returns> The stored data </returns>
        public IEnumerable<AcademicCheckInViewModel> PutCellPhone(object data, string id)
        {
            var studentIDParam = new SqlParameter("@UserID", id);
            var phoneNumParam = new SqlParameter("@PhoneUnformatted", FormatNumber(data.phoneNum));
            var isPrivateParam = new SqlParameter("@DoNotPublish", data.isPrivate);
            var noPhoneParam = new SqlParameter("@NoneProvided", data.noPhone);

            // Run stored procedure
            var result = RawSqlQuery<AcademicCheckInViewModel>.query("FINALIZATION_UPDATECELLPHONE @UserID, @PhoneUnformatted, @DoNotPublish, @NoneProvided", studentIDParam, phoneNumParam, isPrivateParam, noPhoneParam);
            {
                throw new ResourceNotFoundException() { ExceptionMessage = "The data was not found." };
            }
            return data;
        }
        */

        /// <summary> Formats a phone number for insertion into the database </summary>
        /// <param name="phoneNum"> The phone number to be formatted </param>
        /// <returns> The formatted number </returns>
        public string FormatNumber(string phoneNum)
        {
            phoneNum = phoneNum.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "").Replace("+", "");
            if (!Regex.IsMatch(phoneNum, @"^\d+$"))
            {
                throw new BadInputException() { ExceptionMessage = "Phone Numbers must only be numerical digits." };
            }
            else
            {
                return phoneNum;
            }
        }
        

    }
}
