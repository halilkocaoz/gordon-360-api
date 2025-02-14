﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Gordon360.Models.CCT;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Gordon360.Models.CCT.Context
{
    public partial interface ICCTContextProcedures
    {
        Task<List<ACTIVE_CLUBS_PER_SESS_IDResult>> ACTIVE_CLUBS_PER_SESS_IDAsync(string SESS_CDE, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<ADVISOR_EMAILS_PER_ACT_CDEResult>> ADVISOR_EMAILS_PER_ACT_CDEAsync(string ACT_CDE, string SESS_CDE, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<ADVISOR_SEPARATEResult>> ADVISOR_SEPARATEAsync(int? STUDENT_ID, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<ALL_BASIC_INFOResult>> ALL_BASIC_INFOAsync(OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<ALL_BASIC_INFO_NOT_ALUMNIResult>> ALL_BASIC_INFO_NOT_ALUMNIAsync(OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<ALL_MEMBERSHIPSResult>> ALL_MEMBERSHIPSAsync(OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<ALL_REQUESTSResult>> ALL_REQUESTSAsync(OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<int> CANCEL_RIDEAsync(int? STUDENT_ID, string RIDE_ID, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<int> CHECK_IDAsync(string _id, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<COURSES_FOR_PROFESSORResult>> COURSES_FOR_PROFESSORAsync(int? professor_id, string sess_cde, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<int> CREATE_BOOKINGAsync(string ID, string RIDEID, byte? ISDRIVER, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<CREATE_MESSAGE_ROOMResult>> CREATE_MESSAGE_ROOMAsync(string name, bool? group, byte[] roomImage, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<int> CREATE_MYSCHEDULEAsync(string EVENTID, string GORDONID, string LOCATION, string DESCRIPTION, string MON_CDE, string TUE_CDE, string WED_CDE, string THU_CDE, string FRI_CDE, string SAT_CDE, string SUN_CDE, int? IS_ALLDAY, TimeSpan? BEGINTIME, TimeSpan? ENDTIME, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<int> CREATE_RIDEAsync(string RIDEID, string DESTINATION, string MEETINGPOINT, DateTime? STARTTIME, DateTime? ENDTIME, int? CAPACITY, string NOTES, byte? CANCELED, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<int> CREATE_SOCIAL_LINKSAsync(string USERNAME, string FACEBOOK, string TWITTER, string INSTAGRAM, string LINKEDIN, string HANDSHAKE, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<CURRENT_SESSIONResult>> CURRENT_SESSIONAsync(OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<int> DELETE_AA_ADMINAsync(string ADMIN_ID, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<int> DELETE_AA_APARTMENT_CHOICEAsync(int? APPLICATION_ID, string HALL_NAME, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<int> DELETE_AA_APPLICANTAsync(int? APPLICATION_ID, string USERNAME, string SESS_CDE, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<int> DELETE_AA_APPLICATIONAsync(int? APP_ID, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<int> DELETE_BOOKINGAsync(string RIDE_ID, string ID, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<int> DELETE_BOOKINGSAsync(string RIDE_ID, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<int> DELETE_CLOCK_INAsync(string ID_Num, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<int> DELETE_MYSCHEDULEAsync(string EVENTID, string GORDONID, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<int> DELETE_NEWS_ITEMAsync(int? SNID, string Username, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<int> DELETE_RIDEAsync(string RIDE_ID, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<int> DELETE_USER_CONNECTION_IDAsync(string connection_id, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<int> DELETE_USER_ROOMAsync(string room_id, string user_id, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<int> DINING_INFO_BY_STUDENT_IDAsync(int? STUDENT_ID, string SESS_CDE, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<DISTINCT_ACT_TYPEResult>> DISTINCT_ACT_TYPEAsync(string SESS_CDE, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<EMAILS_PER_ACT_CDEResult>> EMAILS_PER_ACT_CDEAsync(string ACT_CDE, string SESS_CDE, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<FINALIZATION_GET_FINALIZATION_STATUSResult>> FINALIZATION_GET_FINALIZATION_STATUSAsync(int? UserID, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<FINALIZATION_GETDEMOGRAPHICResult>> FINALIZATION_GETDEMOGRAPHICAsync(string UserID, string FeatureID, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<FINALIZATION_GETHOLDSBYIDResult>> FINALIZATION_GETHOLDSBYIDAsync(int? ID_NUM, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<FINALIZATION_MARK_AS_CURRENTLY_COMPLETEDResult>> FINALIZATION_MARK_AS_CURRENTLY_COMPLETEDAsync(string UserID, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<FINALIZATION_UPDATECELLPHONEResult>> FINALIZATION_UPDATECELLPHONEAsync(string UserID, string PhoneUnformatted, bool? DoNotPublish, bool? NoneProvided, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<FINALIZATION_UPDATEDEMOGRAPHICResult>> FINALIZATION_UPDATEDEMOGRAPHICAsync(string UserID, string RaceValue, int? EthnicityValue, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<GET_AA_ADMINResult>> GET_AA_ADMINAsync(string ADMIN_ID, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<GET_AA_APARTMENT_CHOICES_BY_APP_IDResult>> GET_AA_APARTMENT_CHOICES_BY_APP_IDAsync(int? APPLICATION_ID, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<GET_AA_APARTMENT_HALLSResult>> GET_AA_APARTMENT_HALLSAsync(OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<GET_AA_APPID_BY_NAME_AND_DATEResult>> GET_AA_APPID_BY_NAME_AND_DATEAsync(DateTime? NOW, string EDITOR_USERNAME, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<GET_AA_APPID_BY_STU_ID_AND_SESSResult>> GET_AA_APPID_BY_STU_ID_AND_SESSAsync(string SESS_CDE, string STUDENT_USERNAME, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<GET_AA_APPLICANTS_BY_APPIDResult>> GET_AA_APPLICANTS_BY_APPIDAsync(int? APPLICATION_ID, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<GET_AA_APPLICANTS_DETAILSResult>> GET_AA_APPLICANTS_DETAILSAsync(OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<GET_AA_APPLICATIONSResult>> GET_AA_APPLICATIONSAsync(OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<GET_AA_APPLICATIONS_BY_IDResult>> GET_AA_APPLICATIONS_BY_IDAsync(int? APPLICATION_ID, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<GET_AA_CURRENT_APP_IDSResult>> GET_AA_CURRENT_APP_IDSAsync(OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<GET_AA_CURRENT_APPLICATIONSResult>> GET_AA_CURRENT_APPLICATIONSAsync(OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<GET_AA_EDITOR_BY_APPIDResult>> GET_AA_EDITOR_BY_APPIDAsync(int? APPLICATION_ID, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<GET_ALL_CONNECTION_IDS_BY_IDResult>> GET_ALL_CONNECTION_IDS_BY_IDAsync(int? user_id, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<GET_ALL_CONNECTION_IDS_BY_ID_LISTResult>> GET_ALL_CONNECTION_IDS_BY_ID_LISTAsync(string user_id_list, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<GET_ALL_MESSAGES_BY_IDResult>> GET_ALL_MESSAGES_BY_IDAsync(int? room_id, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<GET_ALL_ROOMS_BY_IDResult>> GET_ALL_ROOMS_BY_IDAsync(string user_id, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<GET_ALL_USERS_BY_ROOM_IDResult>> GET_ALL_USERS_BY_ROOM_IDAsync(int? room_id, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<GET_BIRTH_DATE_BY_IDResult>> GET_BIRTH_DATE_BY_IDAsync(string ID, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<GET_HEALTH_CHECK_QUESTIONResult>> GET_HEALTH_CHECK_QUESTIONAsync(OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<GET_LATEST_ROOMResult>> GET_LATEST_ROOMAsync(OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<GET_ROOM_BY_IDResult>> GET_ROOM_BY_IDAsync(int? room_id, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<GET_SINGLE_MESSAGE_BY_IDResult>> GET_SINGLE_MESSAGE_BY_IDAsync(int? room_id, string message_id, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<GET_TIMESHEETS_CLOCK_IN_OUTResult>> GET_TIMESHEETS_CLOCK_IN_OUTAsync(int? ID_NUM, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<GRP_ADMIN_EMAILS_PER_ACT_CDEResult>> GRP_ADMIN_EMAILS_PER_ACT_CDEAsync(string ACT_CDE, string SESS_CDE, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<int> INSERT_AA_ADMINAsync(string ADMIN_ID, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<int> INSERT_AA_APARTMENT_CHOICEAsync(int? APPLICATION_ID, int? RANKING, string HALL_NAME, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<int> INSERT_AA_APPLICANTAsync(int? APPLICATION_ID, string USERNAME, string APRT_PROGRAM, string SESS_CDE, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<int> INSERT_AA_APPLICATIONAsync(DateTime? NOW, string EDITOR_USERNAME, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<int> INSERT_HEALTH_QUESTIONAsync(string Question, string YesPrompt, string NoPrompt, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<int> INSERT_MESSAGEAsync(string _id, string room_id, string text, DateTime? createdAt, string user_id, byte[] image, byte[] video, byte[] audio, bool? system, bool? sent, bool? received, bool? pending, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<INSERT_NEWS_ITEMResult>> INSERT_NEWS_ITEMAsync(string Username, int? CategoryID, string Subject, string Body, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<int> INSERT_TIMESHEETS_CLOCK_IN_OUTAsync(int? ID_NUM, bool? State, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<int> INSERT_USERAsync(string _id, string name, byte[] avatar, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<int> INSERT_USER_CONNECTION_IDAsync(string user_id, string connection_id, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<int> INSERT_USER_ROOMSAsync(string user_id, string _id, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<INSTRUCTOR_COURSES_BY_ID_NUM_AND_SESS_CDEResult>> INSTRUCTOR_COURSES_BY_ID_NUM_AND_SESS_CDEAsync(int? instructor_id, string sess_cde, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<LEADER_EMAILS_PER_ACT_CDEResult>> LEADER_EMAILS_PER_ACT_CDEAsync(string ACT_CDE, string SESS_CDE, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<LEADER_MEMBERSHIPS_PER_ACT_CDEResult>> LEADER_MEMBERSHIPS_PER_ACT_CDEAsync(string ACT_CDE, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<MEMBERSHIPS_PER_ACT_CDEResult>> MEMBERSHIPS_PER_ACT_CDEAsync(string ACT_CDE, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<MEMBERSHIPS_PER_STUDENT_IDResult>> MEMBERSHIPS_PER_STUDENT_IDAsync(int? STUDENT_ID, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<MYSCHEDULE_BY_IDResult>> MYSCHEDULE_BY_IDAsync(int? ID_NUM, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<NEWS_CATEGORIESResult>> NEWS_CATEGORIESAsync(OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<NEWS_NEWResult>> NEWS_NEWAsync(OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<NEWS_NOT_EXPIREDResult>> NEWS_NOT_EXPIREDAsync(OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<NEWS_PERSONAL_UNAPPROVEDResult>> NEWS_PERSONAL_UNAPPROVEDAsync(string Username, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<PHOTO_INFO_PER_USER_NAMEResult>> PHOTO_INFO_PER_USER_NAMEAsync(int? ID, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<REQUEST_PER_REQUEST_IDResult>> REQUEST_PER_REQUEST_IDAsync(int? REQUEST_ID, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<REQUESTS_PER_ACT_CDEResult>> REQUESTS_PER_ACT_CDEAsync(string ACT_CDE, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<REQUESTS_PER_STUDENT_IDResult>> REQUESTS_PER_STUDENT_IDAsync(int? STUDENT_ID, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<RIDERS_BY_RIDE_IDResult>> RIDERS_BY_RIDE_IDAsync(string RIDE_ID, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<STUDENT_COURSES_BY_ID_NUM_AND_SESS_CDEResult>> STUDENT_COURSES_BY_ID_NUM_AND_SESS_CDEAsync(int? id_num, string sess_cde, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<STUDENT_JOBS_PER_ID_NUMResult>> STUDENT_JOBS_PER_ID_NUMAsync(int? ID_NUM, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<int> TRUNCATE_AA_ALL_APPLICATION_TABLESAsync(OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<UPCOMING_RIDESResult>> UPCOMING_RIDESAsync(int? STUDENT_ID, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<UPCOMING_RIDES_BY_STUDENT_IDResult>> UPCOMING_RIDES_BY_STUDENT_IDAsync(int? STUDENT_ID, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<int> UPDATE_AA_APARTMENT_CHOICESAsync(int? APPLICATION_ID, int? RANKING, string HALL_NAME, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<int> UPDATE_AA_APPLICANTAsync(int? APPLICATION_ID, string USERNAME, string APRT_PROGRAM, string SESS_CDE, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<int> UPDATE_AA_APPLICATION_DATEMODIFIEDAsync(int? APPLICATION_ID, DateTime? NOW, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<int> UPDATE_AA_APPLICATION_DATESUBMITTEDAsync(int? APPLICATION_ID, DateTime? NOW, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<int> UPDATE_AA_APPLICATION_EDITORAsync(int? APPLICATION_ID, string EDITOR_USERNAME, DateTime? NOW, string NEW_EDITOR_USERNAME, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<int> UPDATE_ACT_INFOAsync(OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<UPDATE_CELL_PHONEResult>> UPDATE_CELL_PHONEAsync(string UserID, string PhoneUnformatted, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<int> UPDATE_DESCRIPTIONAsync(int? ID, string VALUE, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<int> UPDATE_EMRGCONTACTAsync(int? StudentID, int? ContactNum, string ContactLastName, string ContactFirstName, string ContactHomePhone, string ContactMobilePhone, string ContactRelationship, string Notes, string Username, string JobName, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<int> Update_Health_Status_Upon_Form_CompletionAsync(string ResponderEmail, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<int> UPDATE_MYSCHEDULEAsync(string EVENTID, string GORDONID, string LOCATION, string DESCRIPTION, string MON_CDE, string TUE_CDE, string WED_CDE, string THU_CDE, string FRI_CDE, string SAT_CDE, string SUN_CDE, int? IS_ALLDAY, TimeSpan? BEGINTIME, TimeSpan? ENDTIME, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<int> UPDATE_NEWS_ITEMAsync(int? SNID, string Username, int? CategoryID, string Subject, string Body, string Image, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<int> UPDATE_PHONE_PRIVACYAsync(int? ID, string VALUE, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<int> UPDATE_PHOTO_PATHAsync(int? ID, string FILE_PATH, string FILE_NAME, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<int> UPDATE_ROOMAsync(int? room_id, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<int> UPDATE_SCHEDULE_PRIVACYAsync(int? ID, string VALUE, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<int> UPDATE_SHOW_PICAsync(int? ACCOUNT_ID, string VALUE, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<int> UPDATE_TIMESTAMPAsync(int? ID, DateTime? VALUE, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<VALID_DRIVES_BY_IDResult>> VALID_DRIVES_BY_IDAsync(string DRIVERID, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<VICTORY_PROMISE_BY_STUDENT_IDResult>> VICTORY_PROMISE_BY_STUDENT_IDAsync(int? STUDENT_ID, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
    }
}
