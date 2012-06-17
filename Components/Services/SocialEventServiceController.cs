//
// DotNetNuke® - http://www.dotnetnuke.com
// Copyright (c) 2002-2012
// by DotNetNuke Corporation
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
// documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
// the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and 
// to permit persons to whom the Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all copies or substantial portions 
// of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
// CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE.
//

using DotNetNuke.Modules.SocialEvents.Components.Common;
using DotNetNuke.Modules.SocialEvents.Components.Entities;
using DotNetNuke.Modules.SocialEvents.Components.Integration;
using System;
using System.Linq;
using System.Web.Mvc;
using DotNetNuke.Common.Lists;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Security;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Social.Notifications;
using DotNetNuke.Web.Services;

namespace DotNetNuke.Modules.SocialEvents.Components.Services
{

    [DnnModuleAuthorize(AccessLevel = SecurityAccessLevel.View)]
    //[SupportedModules("Social Events")]
    [DnnAuthorize]
    [ValidateAntiForgeryToken]
    public class SocialEventServiceController : DnnController, IServiceRouteMapper
    {

        #region Explicit Interface Methods

        public void RegisterRoutes(IMapRoute mapRouteManager)
        {

            mapRouteManager.MapRoute("SocialEvents", "{controller}.ashx/{action}", new[] { "DotNetNuke.Modules.SocialEvents.Components.Services" });
        }

        #endregion

        #region Private Methods

        private static string GetFilteredValue(PortalSecurity objSecurity, string value)
        {
            return objSecurity.InputFilter(
                value,
                PortalSecurity.FilterFlag.NoScripting | PortalSecurity.FilterFlag.NoAngleBrackets
                | PortalSecurity.FilterFlag.NoMarkup);
        }

        private void ParseKey(string key)
        {
            var keys = key.Split(Convert.ToChar(":"));
            // 0 is content type string, to ensure unique key
            _eventId = int.Parse(keys[1]);
            _groupId = int.Parse(keys[2]);
            _tabId = int.Parse(keys[3]);
        }

        #endregion

        #region Private Members

        private int _eventId = -1;
        private int _groupId = -1;
        private int _tabId = -1;

        #endregion

        #region Public Methods

        public ActionResult DeleteSocialEvent(int portalId, int eventId)
        {
            var controller = new SocialEventsController();
            var @event = controller.GetEvent(
                eventId, UserInfo.UserID, UserInfo.Profile.PreferredTimeZone);

            var cntJournal = new Journal();
            cntJournal.RemoveSocialEventCreateFromJournal(eventId, portalId);
            cntJournal.RemoveSocialEventAttendFromJournal(eventId, portalId);

            var notificationType = NotificationsController.Instance.GetNotificationType(Constants.NotificationEventInviteTypeName);
            var notificationKey = string.Format("{0}:{1}:{2}:{3}", Constants.ContentTypeName, @event.EventId, @event.GroupId, PortalSettings.ActiveTab.TabID);
            var objNotify = NotificationsController.Instance.GetNotificationByContext(notificationType.NotificationTypeId, notificationKey).SingleOrDefault();

            if (objNotify != null)
            {
                NotificationsController.Instance.DeleteNotification(objNotify.NotificationID);
            }

            new SocialEventsController().DeleteEvent(eventId);

            var response = new { eventId, Result = "success" };
            return Json(response);
        }

        public ActionResult Get(int eventId)
        {
            var timeZone = PortalSettings.TimeZone;
            if (Request.IsAuthenticated) timeZone = UserInfo.Profile.PreferredTimeZone;
            return Json(
                new SocialEventsController().GetEvent(eventId, UserInfo.UserID, timeZone),
                JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAttendees(int eventId, int status)
        {
            var controller = new SocialEventsController();
            return Json(controller.GetAttendees(eventId, status));
        }

        public ActionResult GetCountries()
        {
            var controller = new ListController();
            var list = controller.GetListEntryInfoItems("Country");
            var tempList = list.Select(x => new { x.Text, x.Value }).ToList();
            var noneText = DotNetNuke.Services.Localization.Localization.GetString(
                "None_Specified", DotNetNuke.Services.Localization.Localization.SharedResourceFile);
            tempList.Insert(0, new { Text = "<" + noneText + ">", Value = "" });
            return Json(tempList);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public ActionResult Save(
            int eventId,
            int portalId,
            int tabId,
            int groupId,
            string name,
            string details,
            DateTime startTime,
            DateTime endTime,
            string street,
            string city,
            string region,
            string postalCode,
            string country,
            int maxAttendees,
            bool enableRsvp,
            bool showGuestList)
        {
            try
            {
                var objSecurity = new PortalSecurity();

                name = GetFilteredValue(objSecurity, name);
                details = GetFilteredValue(objSecurity, details);
                street = GetFilteredValue(objSecurity, street);
                city = GetFilteredValue(objSecurity, city);
                region = GetFilteredValue(objSecurity, region);
                postalCode = GetFilteredValue(objSecurity, postalCode);
                country = GetFilteredValue(objSecurity, country);

                if (string.IsNullOrEmpty(name)) return Json(new { Result = "failure" });

                var objEvent = new EventInfo
                    {
                        EventId = eventId,
                        PortalId = portalId,
                        GroupId = groupId,
                        Name = name,
                        Details = details,
                        StartTime = startTime,
                        EndTime = endTime,
                        Street = street,
                        City = city,
                        Region = region,
                        Country = country,
                        PostalCode = postalCode,
                        MaxAttendees = maxAttendees,
                        EnableRSVP = enableRsvp,
                        ShowGuestList = showGuestList,
                        CreatedByUserId = UserInfo.UserID,
                        CreatedOnDate = DateTime.Now,
                        LastModifiedByUserId = UserInfo.UserID,
                        LastModifiedOnDate = DateTime.Now
                    };

                var controller = new SocialEventsController();
                if (eventId != Null.NullInteger) controller.UpdateEvent(objEvent);
                else eventId = controller.AddEvent(objEvent);

                objEvent.EventId = eventId; //for add event;

                var url = DotNetNuke.Common.Globals.NavigateURL(tabId, "", "eventid=" + eventId);
                if (groupId > Null.NullInteger) url = DotNetNuke.Common.Globals.NavigateURL(tabId, "", "eventid=" + eventId, "groupid=" + groupId);

                var cntJournal = new Journal();
                cntJournal.AddSocialEventCreateToJournal(objEvent, tabId, objEvent.Name, objEvent.PortalId, objEvent.CreatedByUserId, url);

                var subject = "New Event Invitation";
                var body = "<a href=\"" + url + "\" >" + objEvent.Name + "</a>";

                var cntNotifications = new Notifications();
                cntNotifications.EventInvite(objEvent, objEvent.PortalId, tabId, subject, body);


                var response = new { EventId = eventId, Result = "success" };
                return Json(response);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);

                var response = new { Result = "failure", ex.Message };
                return Json(response);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateAttendStatus(int portalId, int tabId, int groupId, int eventId, int status)
        {
            var controller = new SocialEventsController();
            var @event = controller.GetEvent(
                eventId, UserInfo.UserID, UserInfo.Profile.PreferredTimeZone);
            if (@event == null) return Json(new { Result = "success" });

            var objGuest = new EventGuestInfo
                               {
                                   EventId = @event.EventId,
                                   UserId = UserInfo.UserID,
                                   Email = UserInfo.Email,
                                   InvitedOnDate = @event.CreatedOnDate,
                                   RepliedOnDate = DateTime.Now,
                                   RSVPStatus = status
                               };
            controller.UpdateGuestStatus(objGuest);

            var url = DotNetNuke.Common.Globals.NavigateURL(tabId, "", "eventid=" + eventId);
            if (groupId > Null.NullInteger) url = DotNetNuke.Common.Globals.NavigateURL(tabId, "", "eventid=" + eventId, "groupid=" + groupId);

            var cntJournal = new Journal();
            cntJournal.AddSocialEventAttendToJournal(objGuest, @event.Name, @event.GroupId, tabId, @event.PortalId, objGuest.UserId, url);

            // Notification Integration
            var notificationType = NotificationsController.Instance.GetNotificationType(Constants.NotificationEventInviteTypeName);
            var notificationKey = string.Format("{0}:{1}:{2}:{3}", Constants.ContentTypeName, @event.EventId, @event.GroupId, PortalSettings.ActiveTab.TabID);
            var objNotify = NotificationsController.Instance.GetNotificationByContext(notificationType.NotificationTypeId, notificationKey).SingleOrDefault();

            if (objNotify != null)
            {
                NotificationsController.Instance.DeleteNotificationRecipient(objNotify.NotificationID, UserInfo.UserID);
            }

            var response = new { Value = eventId, Result = "success" };

            return Json(response);
        }

        #endregion

        public ActionResult AcceptInvite(int notificationId)
        {
            var notify = NotificationsController.Instance.GetNotification(notificationId);
            ParseKey(notify.Context);

            var controller = new SocialEventsController();
            var objEvent = controller.GetEvent(_eventId, UserInfo.UserID, UserInfo.Profile.PreferredTimeZone);
            if (objEvent == null)
            {
                return Json(new { Result = "error" });
            }

            var objGuest = new EventGuestInfo
            {
                EventId = objEvent.EventId,
                UserId = UserInfo.UserID,
                Email = UserInfo.Email,
                InvitedOnDate = objEvent.CreatedOnDate,
                RepliedOnDate = DateTime.Now,
                RSVPStatus = (int)Constants.AttendingStatus.Yes
            };
            controller.UpdateGuestStatus(objGuest);

            var url = DotNetNuke.Common.Globals.NavigateURL(_tabId, "", "eventid=" + objEvent.EventId);
            if (objEvent.GroupId > Null.NullInteger) url = DotNetNuke.Common.Globals.NavigateURL(_tabId, "", "eventid=" + objEvent.EventId, "groupid=" + objEvent.GroupId);

            var cntJournal = new Journal();
            cntJournal.AddSocialEventAttendToJournal(objGuest, objEvent.Name, objEvent.GroupId, _tabId, objEvent.PortalId, objGuest.UserId, url);

            NotificationsController.Instance.DeleteNotificationRecipient(notificationId, UserInfo.UserID);

            return Json(new { Result = "success" });
        }

        public ActionResult DeclineInvite(int notificationId)
        {
            var notify = NotificationsController.Instance.GetNotification(notificationId);
            ParseKey(notify.Context);

            var controller = new SocialEventsController();
            var objEvent = controller.GetEvent(_eventId, UserInfo.UserID, UserInfo.Profile.PreferredTimeZone);
            if (objEvent == null)
            {
                return Json(new { Result = "error" });
            }

            // update the user status to 'not attending', NO journal integration here.
            var objGuest = new EventGuestInfo
            {
                EventId = objEvent.EventId,
                UserId = UserInfo.UserID,
                Email = UserInfo.Email,
                InvitedOnDate = objEvent.CreatedOnDate,
                RepliedOnDate = DateTime.Now,
                RSVPStatus = (int)Constants.AttendingStatus.No
            };
            controller.UpdateGuestStatus(objGuest);

            NotificationsController.Instance.DeleteNotificationRecipient(notificationId, UserInfo.UserID);


            return Json(new { Result = "success" });
        }

    }
}