﻿using Gordon360.Models.CCT;
using Gordon360.Models.CCT.Context;
using Gordon360.Models.MyGordon.Context;
using Gordon360.Services;
using Gordon360.Static.Methods;
using Gordon360.Static.Names;
using Gordon360.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Gordon360.Authorization
{
    /* Authorization Filter.
     * It is actually an action filter masquerading as an authorization filter. This is because I need access to the 
     * parameters passed to the controller. Authorization Filters don't have that access. Action Filters do.
     * 
     * Because of the nature of how we authorize people, this code might seem very odd, so I'll try to explain. 
     * Proceed at your own risk. If you can understand this code, you can understand the whole project. 
     * 
     * 1st Observation: You can't authorize access to a resource that isn't owned by someone. Resources like Sessions, Participations,
     * and Activity Definitions are accessibile by anyone.
     * 2nd Observation: To Authorize someone to perform an action on a resource, you need to know the following:
     * 1. Who is to be authorized? 2.What resource are they trying to access? 3. What operation are they trying to make on the resource?
     * This "algorithm" uses those three points and decides through a series of switch statements if the current user
     * is authorized.
     */
    public class StateYourBusiness : ActionFilterAttribute
    {
        // Resource to be accessed: Will get as parameters to the attribute
        public string resource { get; set; }
        // Operation to be performed: Will get as parameters to the attribute
        public string operation { get; set; }

        private ActionExecutingContext context;
        private IWebHostEnvironment _webHostEnvironment;
        private CCTContext _CCTContext;
        private MyGordonContext _MyGordonContext;
        private IAccountService _accountService;
        private IMembershipService _membershipService;
        private IMembershipRequestService _membershipRequestService;

        // User position at the college and their id.
        private IEnumerable<AuthGroup> user_groups { get; set; }
        private string user_id { get; set; }
        private string user_name { get; set; }

        public async Task OnActionExecutionAsync(ActionExecutingContext actionContext, ActionExecutionDelegate next, IConfiguration _config)
        {
            context = actionContext;
            _webHostEnvironment = context.HttpContext.RequestServices.GetService<IWebHostEnvironment>();
            // Step 1: Who is to be authorized
            var authenticatedUser = actionContext.HttpContext.User;

            var optionsBuilderCCT = new DbContextOptionsBuilder<CCTContext>();
            optionsBuilderCCT.UseSqlServer(_config.GetConnectionString("CCT"));
            _CCTContext = new CCTContext(optionsBuilderCCT.Options);

            var optionsBuilderMyGordon = new DbContextOptionsBuilder<MyGordonContext>();
            optionsBuilderMyGordon.UseSqlServer(_config.GetConnectionString("MyGordon"));
            _MyGordonContext = new MyGordonContext(optionsBuilderMyGordon.Options);


            _accountService = new AccountService(_CCTContext);
            _membershipService = new MembershipService(_CCTContext, _accountService);
            _membershipRequestService = new MembershipRequestService(_CCTContext, _membershipService, _accountService);

            user_name = AuthUtils.GetUsername(authenticatedUser);
            user_groups = AuthUtils.GetGroups(authenticatedUser);
            user_id = _accountService.GetAccountByUsername(user_name).GordonID;

            if (user_groups.Contains(AuthGroup.SiteAdmin))
            {
                await next();
                return;
            }

            bool isAuthorized = await CanPerformOperationAsync(resource, operation);
            if (!isAuthorized)
            {
                throw new UnauthorizedAccessException("Authorization has been denied for this request.");
            }

            await next();
        }


        private async Task<bool> CanPerformOperationAsync(string resource, string operation)
            => operation switch
            {
                Operation.READ_ONE => await CanReadOneAsync(resource),
                Operation.READ_ALL => CanReadAll(resource),
                Operation.READ_PARTIAL => await CanReadPartialAsync(resource),
                Operation.ADD => await CanAddAsync(resource),
                Operation.DENY_ALLOW => CanDenyAllow(resource),
                Operation.UPDATE => await CanUpdateAsync(resource),
                Operation.DELETE => await CanDeleteAsync(resource),
                Operation.READ_PUBLIC => CanReadPublic(resource),
                _ => false,
            };

        /*
         * Operations
         */
        // This operation is specifically for authorizing deny and allow operations on membership requests. These two operations don't
        // Fit in nicely with the REST specification which is why there is a seperate case for them.
        private bool CanDenyAllow(string resource)
        {
            // User is admin
            if (user_groups.Contains(AuthGroup.SiteAdmin))
                return true;

            switch (resource)
            {

                case Resource.MEMBERSHIP_REQUEST:
                    {
                        var mrID = (int)context.ActionArguments["activityCode"];
                        // Get the view model from the repository
                        var mrToConsider = _membershipRequestService.Get(mrID);
                        // Populate the membershipRequest manually. Omit fields I don't need.
                        var activityCode = mrToConsider.ActivityCode;
                        var is_activityLeader = _membershipService.GetLeaderMembershipsForActivity(activityCode).Any(x => x.Username.ToLower().Equals(user_name.ToLower()));
                        if (is_activityLeader) // If user is the leader of the activity that the request is sent to.
                            return true;
                        var is_activityAdvisor = _membershipService.GetAdvisorMembershipsForActivity(activityCode).Any(x => x.Username.ToLower().Equals(user_name.ToLower()));
                        if (is_activityAdvisor) // If user is the advisor of the activity that the request is sent to.
                            return true;

                        return false;
                    }
                default: return false;

            }
        }

        private async Task<bool> CanReadOneAsync(string resource)
        {
            // User is admin
            if (user_groups.Contains(AuthGroup.SiteAdmin))
                return true;

            switch (resource)
            {
                case Resource.PROFILE:
                    return true;
                case Resource.EMERGENCY_CONTACT:
                    if (user_groups.Contains(AuthGroup.Police))
                        return true;
                    else
                    {
                        var username = (string)context.ActionArguments["username"];
                        var isSelf = username.Equals(user_name.ToLower());
                        return isSelf;
                    }
                case Resource.MEMBERSHIP:
                    return true;
                case Resource.MEMBERSHIP_REQUEST:
                    {
                        // membershipRequest = mr
                        var mrID = (int)context.ActionArguments["id"];
                        var mrToConsider = _membershipRequestService.Get(mrID);
                        var is_mrOwner = mrToConsider.Username.Equals(user_name.ToLower()); // User_id is an instance variable.

                        if (is_mrOwner) // If user owns the request
                            return true;

                        var isGroupAdmin = (_membershipService.GetGroupAdminMembershipsForActivity(mrToConsider.ActivityCode, mrToConsider.SessionCode)).Any(x => x.Username.ToLower().Equals(user_name.ToLower()));
                        if (isGroupAdmin) // If user is a group admin of the activity that the request is sent to
                            return true;

                        return false;
                    }
                case Resource.STUDENT:
                    // To add a membership for a student, you need to have the students identifier.
                    // NOTE: I don't believe the 'student' resource is currently being used in API
                    {
                        return true;
                    }
                case Resource.ADVISOR:
                    return true;
                case Resource.ACCOUNT:
                    {
                        // Membership group admins can access ID of members using their email
                        // NOTE: In the future, probably only email addresses should be stored 
                        // in memberships, since we would rather not give students access to
                        // other students' account information
                        var isGroupAdmin = _membershipService.IsGroupAdmin(user_name);
                        if (isGroupAdmin) // If user is a group admin of the activity that the request is sent to
                            return true;

                        // faculty and police can access student account information
                        if (user_groups.Contains(AuthGroup.FacStaff)
                            || user_groups.Contains(AuthGroup.Police))
                            return true;

                        return false;
                    }
                case Resource.HOUSING:
                    {
                        // The members of the apartment application can only read their application
                        var sess_cde = Helpers.GetCurrentSession(_CCTContext);
                        HousingService housingService = new HousingService(_CCTContext);
                        int? applicationID = housingService.GetApplicationID(user_name, sess_cde);
                        int requestedApplicationID = (int)context.ActionArguments["applicationID"];
                        if (applicationID.HasValue && applicationID.Value == requestedApplicationID)
                        {
                            return true;
                        }
                        return false;
                    }
                case Resource.NEWS:
                    return true;
                default: return false;

            }
        }
        // For reads that access a group of resources filterd in a specific way 
        private async Task<bool> CanReadPartialAsync(string resource)
        {
            // User is admin
            if (user_groups.Contains(AuthGroup.SiteAdmin))
                return true;

            switch (resource)
            {
                case Resource.MEMBERSHIP_BY_ACTIVITY:
                    {
                        // Only people that are part of the activity should be able to see members
                        var activityCode = (string)context.ActionArguments["activityCode"];
                        var activityMembers = _membershipService.GetMembershipsForActivity(activityCode, null);
                        var is_personAMember = activityMembers.Any(x => x.Username.ToLower().Equals(user_name.ToLower()) && x.Participation != "GUEST");
                        if (is_personAMember)
                            return true;
                        return false;
                    }
                case Resource.MEMBERSHIP_BY_STUDENT:
                    {
                        // Only the person itself or an admin can see someone's memberships
                        return (string)context.ActionArguments["id"] == user_id;
                    }

                case Resource.EVENTS_BY_STUDENT_ID:
                    {
                        // Only the person itself or an admin can see someone's chapel attendance
                        var username_requested = context.ActionArguments["username"];
                        var is_creditOwner = username_requested.ToString().ToLower().Equals(user_name.ToLower());
                        return is_creditOwner;
                    }


                case Resource.MEMBERSHIP_REQUEST_BY_ACTIVITY:
                    {
                        // An activity leader should be able to see the membership requests that belong to the activity s/he is leading.
                        var activityCode = (string)context.ActionArguments["id"];
                        var groupAdmins = _membershipService.GetGroupAdminMembershipsForActivity(activityCode);
                        var isGroupAdmin = groupAdmins.Any(x => x.Username.ToLower().Equals(user_name.ToLower()));
                        if (isGroupAdmin) // If user is a group admin of the activity that the request is sent to
                            return true;
                        return false;
                    }
                // Since the API only allows asking for your requests (no ID argument), it's always OK.
                case Resource.MEMBERSHIP_REQUEST_BY_STUDENT:
                    {
                        return true;
                    }
                case Resource.EMAILS_BY_ACTIVITY:
                    {
                        // Anyone can view group-admin and advisor emails
                        var participationType = context.ActionArguments.ContainsKey("participationType") ? context.ActionArguments["participationType"] : null;
                        if (participationType != null && participationType.In("group-admin", "advisor", "leader"))
                            return true;

                        // Only leaders, advisors, and group admins
                        var activityCode = (string?)context.ActionArguments["activityCode"];

                        var leaders = _membershipService.GetLeaderMembershipsForActivity(activityCode);
                        var is_activity_leader = leaders.Any(x => x.Username.ToLower().Equals(user_name.ToLower()));
                        if (is_activity_leader)
                            return true;

                        var advisors = _membershipService.GetAdvisorMembershipsForActivity(activityCode);
                        var is_activityAdvisor = advisors.Any(x => x.Username.ToLower().Equals(user_name.ToLower()));
                        if (is_activityAdvisor)
                            return true;

                        var groupAdmin = _membershipService.GetGroupAdminMembershipsForActivity(activityCode);
                        var is_groupAdmin = groupAdmin.Any(x => x.Username.ToLower().Equals(user_name.ToLower()));
                        if (is_groupAdmin)
                            return true;

                        return false;
                    }
                case Resource.ADVISOR_BY_ACTIVITY:
                    {
                        return true;
                    }
                case Resource.LEADER_BY_ACTIVITY:
                    {
                        return true;
                    }
                case Resource.GROUP_ADMIN_BY_ACTIVITY:
                    {
                        return true;
                    }
                case Resource.NEWS:
                    {
                        return true;
                    }
                default: return false;
            }
        }
        private bool CanReadAll(string resource)
        {
            switch (resource)
            {
                case Resource.MEMBERSHIP:
                    // User is admin
                    if (user_groups.Contains(AuthGroup.SiteAdmin))
                        return true;
                    else
                        return false;
                case Resource.ChapelEvent:
                    // User is admin
                    if (user_groups.Contains(AuthGroup.SiteAdmin))
                        return true;
                    else
                        return false;
                case Resource.EVENTS_BY_STUDENT_ID:
                    // User is admin
                    if (user_groups.Contains(AuthGroup.SiteAdmin))
                        return true;
                    else
                        return false;

                case Resource.MEMBERSHIP_REQUEST:
                    // User is admin
                    if (user_groups.Contains(AuthGroup.SiteAdmin))
                        return true;
                    else
                        return false;
                case Resource.STUDENT:
                    // User is admin
                    if (user_groups.Contains(AuthGroup.SiteAdmin))
                        return true;
                    else
                        return false; // See reasons for this in CanReadOne(). No one (except for super admin) should be able to access student records through
                                      // our API.
                case Resource.ADVISOR:
                    // User is admin
                    if (user_groups.Contains(AuthGroup.SiteAdmin))
                        return true;
                    else
                        return false;
                case Resource.GROUP_ADMIN:
                    // User is site-wide admin
                    if (user_groups.Contains(AuthGroup.SiteAdmin))
                        return true;
                    else
                        return false;
                case Resource.ACCOUNT:
                    return false;
                case Resource.ADMIN:
                    return false;
                case Resource.HOUSING:
                    {
                        // Only the housing admin and super admin can read all of the received applications.
                        // Super admin has unrestricted access by default, so no need to check.
                        if (user_groups.Contains(AuthGroup.HousingAdmin))
                        {
                            return true;
                        }
                        return false;
                    }
                case Resource.NEWS:
                    return true;
                default: return false;
            }
        }

        private bool CanReadPublic(string resource)
        {
            switch (resource)
            {
                case Resource.SLIDER:
                    return true;
                case Resource.NEWS:
                    return false;
                default: return false;

            }
        }
        private async Task<bool> CanAddAsync(string resource)
        {
            switch (resource)
            {
                case Resource.SHIFT:
                    {
                        if (user_groups.Contains(AuthGroup.Student))
                            return true;
                        return false;
                    }
                case Resource.CLIFTON_STRENGTHS:
                    {
                        return user_groups.Contains(AuthGroup.SiteAdmin);
                    }

                case Resource.MEMBERSHIP:
                    {
                        // User is admin
                        if (user_groups.Contains(AuthGroup.SiteAdmin))
                            return true;
                        var membershipToConsider = (MEMBERSHIP)context.ActionArguments["membership"];
                        // A membership can always be added if it is of type "GUEST"
                        var isFollower = membershipToConsider.PART_CDE == Activity_Roles.GUEST && user_id == membershipToConsider.ID_NUM.ToString();
                        if (isFollower)
                            return true;

                        var activityCode = membershipToConsider.ACT_CDE;
                        var sessionCode = membershipToConsider.SESS_CDE;

                        var isGroupAdmin = _membershipService.GetGroupAdminMembershipsForActivity(activityCode, sessionCode).Any(x => x.Username.ToLower().Equals(user_name.ToLower()));
                        if (isGroupAdmin) // If user is the advisor of the activity that the request is sent to.
                            return true;
                        return false;
                    }

                case Resource.MEMBERSHIP_REQUEST:
                    {
                        // User is admin
                        if (user_groups.Contains(AuthGroup.SiteAdmin))
                            return true;
                        var membershipRequestToConsider = (REQUEST)context.ActionArguments["membershipRequest"];
                        // A membership request belonging to the currently logged in student
                        var is_Owner = membershipRequestToConsider.ID_NUM.ToString() == user_id;
                        if (is_Owner)
                            return true;
                        // No one should be able to add requests on behalf of another person.
                        return false;
                    }
                case Resource.STUDENT:
                    return false; // No one should be able to add students through this API
                case Resource.ADVISOR:
                    // User is admin
                    if (user_groups.Contains(AuthGroup.SiteAdmin))
                        return true;
                    else
                        return false; // Only super admin can add Advisors through this API
                case Resource.HOUSING_ADMIN:
                    //only superadmins can add a HOUSING_ADMIN
                    return false;
                case Resource.HOUSING:
                    {
                        // The user must be a student and not a member of an existing application
                        if (user_groups.Contains(AuthGroup.Student))
                        {
                            var sess_cde = Helpers.GetCurrentSession(_CCTContext);
                            var housingService = new HousingService(_CCTContext);
                            int? applicationID = housingService.GetApplicationID(user_name, sess_cde);
                            if (!applicationID.HasValue)
                            {
                                return true;
                            }
                            return false;
                        }
                        return false;
                    }
                case Resource.ADMIN:
                    return false;
                case Resource.ERROR_LOG:
                    return true;
                case Resource.NEWS:
                    return true;
                default: return false;
            }
        }
        private async Task<bool> CanUpdateAsync(string resource)
        {
            switch (resource)
            {
                case Resource.SHIFT:
                    {
                        if (user_groups.Contains(AuthGroup.Student))
                            return true;
                        return false;
                    }
                case Resource.MEMBERSHIP:
                    {
                        // User is admin
                        if (user_groups.Contains(AuthGroup.SiteAdmin))
                            return true;
                        var membershipToConsider = (MEMBERSHIP)context.ActionArguments["membership"];
                        var activityCode = membershipToConsider.ACT_CDE;
                        var sessionCode = membershipToConsider.SESS_CDE;


                        var isGroupAdmin = _membershipService.GetGroupAdminMembershipsForActivity(activityCode, sessionCode).Any(x => x.Username.ToLower().Equals(user_name.ToLower()));
                        if (isGroupAdmin)
                            return true; // Activity Advisors can update memberships of people in their activity.

                        var is_membershipOwner = membershipToConsider.ID_NUM.ToString() == user_id;
                        if (is_membershipOwner)
                        {
                            // Restrict what a regular owner can edit.
                            var originalMembership = _membershipService.GetSpecificMembership(membershipToConsider.MEMBERSHIP_ID);
                            // If they are not trying to change their participation level, then it is ok
                            if (originalMembership.Participation == membershipToConsider.PART_CDE)
                                return true;
                        }


                        return false;
                    }

                case Resource.MEMBERSHIP_REQUEST:
                    {
                        // Once a request is sent, no one should be able to edit its contents.
                        // If a mistake is made in creating the original request, the user can always delete it and make a new one.
                        return false;
                    }
                case Resource.MEMBERSHIP_PRIVACY:
                    {
                        // User is admin
                        if (user_groups.Contains(AuthGroup.SiteAdmin))
                            return true;
                        var membershipID = (int)context.ActionArguments["id"];

                        var membershipToConsider = _membershipService.GetSpecificMembership(membershipID);
                        var is_membershipOwner = membershipToConsider.Username.ToLower().Equals(user_name.ToLower());
                        if (is_membershipOwner)
                            return true;

                        var activityCode = membershipToConsider.ActivityCode;
                        var sessionCode = membershipToConsider.SessionCode;

                        return false;
                    }
                case Resource.STUDENT:
                    return false; // No one should be able to update a student through this API
                case Resource.HOUSING:
                    {
                        // The housing admins can update the application information (i.e. probation, offcampus program, etc.)
                        // If the user is a student, then the user must be on an application and be an editor to update the application
                        HousingService housingService = new HousingService(_CCTContext);
                        if (user_groups.Contains(AuthGroup.HousingAdmin))
                        {
                            return true;
                        }
                        else if (user_groups.Contains(AuthGroup.Student))
                        {
                            var sess_cde = Helpers.GetCurrentSession(_CCTContext);
                            int? applicationID = housingService.GetApplicationID(user_name, sess_cde);
                            int requestedApplicationID = (int)context.ActionArguments["applicationID"];
                            if (applicationID.HasValue && applicationID == requestedApplicationID)
                            {
                                string editorUsername = housingService.GetEditorUsername(applicationID.Value);
                                if (editorUsername.ToLower() == user_name.ToLower())
                                    return true;
                                return false;
                            }
                            return false;
                        }
                        return false;
                    }
                case Resource.ADVISOR:
                    {
                        // User is admin
                        if (user_groups.Contains(AuthGroup.SiteAdmin))
                            return true;

                        var membershipToConsider = (MEMBERSHIP)context.ActionArguments["membership"];
                        var activityCode = membershipToConsider.ACT_CDE;

                        var is_advisor = _membershipService.GetAdvisorMembershipsForActivity(activityCode).Any(x => x.Username.ToLower().Equals(user_name.ToLower()));
                        if (is_advisor)
                            return true; // Activity Advisors can update memberships of people in their activity.

                        return false;
                    }
                case Resource.PROFILE:
                    {
                        // User is admin
                        if (user_groups.Contains(AuthGroup.SiteAdmin))
                            return true;

                        var username = (string)context.ActionArguments["username"];
                        var isSelf = username.Equals(user_name);
                        return isSelf;
                    }

                case Resource.ACTIVITY_INFO:
                    {
                        // User is admin
                        if (user_groups.Contains(AuthGroup.SiteAdmin))
                            return true;
                        var activityCode = (string)context.ActionArguments["id"];

                        var isGroupAdmin = _membershipService.GetGroupAdminMembershipsForActivity(activityCode).Any(x => x.Username.ToLower().Equals(user_name.ToLower()));
                        if (isGroupAdmin)
                            return true;
                        return false;

                    }

                case Resource.ACTIVITY_STATUS:
                    {
                        // User is admin
                        if (user_groups.Contains(AuthGroup.SiteAdmin))
                            return true;
                        var activityCode = (string)context.ActionArguments["id"];
                        var sessionCode = (string)context.ActionArguments["sess_cde"];

                        var isGroupAdmin = _membershipService.GetGroupAdminMembershipsForActivity(activityCode).Any(x => x.Username.ToLower().Equals(user_name.ToLower()));
                        if (isGroupAdmin)
                        {
                            var activityService = context.HttpContext.RequestServices.GetRequiredService<IActivityService>();
                            // If an activity is currently open, then a group admin has the ability to close it
                            if (activityService.IsOpen(activityCode, sessionCode))
                            {
                                return true;
                            }
                        }

                        // If an activity is currently closed, only super admin has permission to edit its closed/open status   

                        return false;
                    }
                case Resource.EMERGENCY_CONTACT:
                    {
                        var username = (string)context.ActionArguments["username"];
                        var isSelf = username.Equals(user_name);
                        return isSelf;
                    }

                case Resource.NEWS:
                    var newsID = context.ActionArguments["newsID"];
                    var newsService = new NewsService(_MyGordonContext, _CCTContext, _webHostEnvironment);
                    var newsItem = newsService.Get((int)newsID);
                    // only unapproved posts may be updated
                    var approved = newsItem.Accepted;
                    if (approved == null || approved == true)
                        return false;
                    // can update if user is admin
                    if (user_groups.Contains(AuthGroup.SiteAdmin))
                        return true;
                    // can update if user is news item author
                    string newsAuthor = newsItem.ADUN;
                    if (user_name == newsAuthor)
                        return true;
                    return false;
                default: return false;
            }
        }
        private async Task<bool> CanDeleteAsync(string resource)
        {
            switch (resource)
            {
                case Resource.SHIFT:
                    if (user_groups.Contains(AuthGroup.Student))
                        return true;
                    return false;
                case Resource.MEMBERSHIP:
                    {
                        // User is admin
                        if (user_groups.Contains(AuthGroup.SiteAdmin))
                            return true;
                        var membershipID = (int)context.ActionArguments["id"];
                        var membershipToConsider = _membershipService.GetSpecificMembership(membershipID);
                        var is_membershipOwner = membershipToConsider.Username.ToLower().Equals(user_name.ToLower());
                        if (is_membershipOwner)
                            return true;

                        var activityCode = membershipToConsider.ActivityCode;

                        var isGroupAdmin = _membershipService.GetGroupAdminMembershipsForActivity(activityCode).Any(x => x.Username.ToLower().Equals(user_name.ToLower()));
                        if (isGroupAdmin)
                            return true;

                        return false;
                    }
                case Resource.MEMBERSHIP_REQUEST:
                    {
                        // User is admin
                        if (user_groups.Contains(AuthGroup.SiteAdmin))
                            return true;
                        // membershipRequest = mr
                        var mrID = (int)context.ActionArguments["id"];
                        var mrToConsider = _membershipRequestService.Get(mrID);
                        var is_mrOwner = mrToConsider.Username.ToLower().Equals(user_name.ToLower());
                        if (is_mrOwner)
                            return true;

                        var activityCode = mrToConsider.ActivityCode;

                        var isGroupAdmin = _membershipService.GetGroupAdminMembershipsForActivity(activityCode).Any(x => x.Username.ToLower().Equals(user_name.ToLower()));
                        if (isGroupAdmin)
                            return true;


                        return false;
                    }
                case Resource.STUDENT:
                    return false; // No one should be able to delete a student through our API
                case Resource.HOUSING:
                    {
                        // The housing admins can update the application information (i.e. probation, offcampus program, etc.)
                        // If the user is a student, then the user must be on an application and be an editor to update the application
                        HousingService housingService = new HousingService(_CCTContext);
                        if (user_groups.Contains(AuthGroup.HousingAdmin))
                        {
                            return true;
                        }
                        else if (user_groups.Contains(AuthGroup.Student))
                        {
                            var sess_cde = Helpers.GetCurrentSession(_CCTContext);
                            int? applicationID = housingService.GetApplicationID(user_name, sess_cde);
                            int requestedApplicationID = (int)context.ActionArguments["applicationID"];
                            if (applicationID.HasValue && applicationID.Value == requestedApplicationID)
                            {
                                var editorUsername = housingService.GetEditorUsername(applicationID.Value);
                                if (editorUsername.ToLower() == user_name.ToLower())
                                    return true;
                                return false;
                            }
                            return false;
                        }
                        return false;
                    }
                case Resource.ADVISOR:
                    return false;
                case Resource.ADMIN:
                    return false;
                case Resource.HOUSING_ADMIN:
                    {
                        // Only the superadmins can remove a housing admin from the whitelist
                        // Super admins have unrestricted access by default: no need to check
                        return false;
                    }
                case Resource.NEWS:
                    {
                        var newsID = context.ActionArguments["newsID"];
                        var newsService = new NewsService(_MyGordonContext, _CCTContext, _webHostEnvironment);
                        var newsItem = newsService.Get((int)newsID);
                        // only expired news items may be deleted
                        var newsDate = newsItem.Entered;
                        if (!newsDate.HasValue || (System.DateTime.Now - newsDate.Value).Days >= 14)
                        {
                            return false;
                        }
                        // user is admin
                        if (user_groups.Contains(AuthGroup.SiteAdmin))
                            return true;
                        // user is news item author
                        string newsAuthor = newsItem.ADUN;
                        if (user_name == newsAuthor)
                            return true;
                        return false;
                    }
                case Resource.SLIDER:
                    {
                        if (user_groups.Contains(AuthGroup.SiteAdmin))
                            return true;
                        return false;
                    }
                default: return false;
            }
        }


    }
}
