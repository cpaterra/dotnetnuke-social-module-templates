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

using System.Linq;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Modules.SocialModule.Components.Common;
using DotNetNuke.Modules.SocialModule.Components.Entities;
using DotNetNuke.Services.Journal;

namespace DotNetNuke.Modules.SocialModule.Components.Integration
{
    public class Journal
    {

        #region Internal Methods

        /// <summary>
        /// Informs the core journal that the user has created an item
        /// </summary>
        /// <param name="objEntity"></param>
        /// <param name="tabId"></param>
        /// <param name="title"></param>
        /// <param name="portalId"></param>
        /// <param name="journalUserId"></param>
        /// <param name="url"></param>
        /// <remarks>This may need to be updated based on what you intent to send to the jorunal.</remarks>
        internal void AddItemToJournal(EntityInfo objEntity, int tabId, string title, int portalId, int journalUserId, string url)
        {
            var objectKey = Constants.ContentTypeName + "_" + Constants.JournalSocialModuleTypeName + "_" + string.Format("{0}:{1}", objEntity.Id, portalId);
            var ji = JournalController.Instance.GetJournalItemByKey(portalId, objectKey);

            if ((ji != null))
            {
                JournalController.Instance.DeleteJournalItemByKey(portalId, objectKey);
            }

            ji = new JournalItem
            {
                PortalId = portalId,
                ProfileId = journalUserId,
                UserId = journalUserId,
                //SocialGroupId = objEntity.GroupId,
                //ContentItemId = objEntity.ContentItemId,
                Title = title,
                ItemData = new ItemData { Url = url },
                //Summary = objEntity.Details,
                Body = null,
                JournalTypeId = GetCreateEventJournalType(portalId),
                ObjectKey = objectKey,
                SecuritySet = "E,"
            };

            //if (objEntity.GroupId > Null.NullInteger) ji.SocialGroupId = objEntity.GroupId;

            JournalController.Instance.SaveJournalItem(ji, tabId);
        }

        /// <summary>
        /// Deletes a journal item associated with a specific id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="portalId"></param>
        internal void RemoveItemFromJournal(int id, int portalId)
        {
            var objectKey = Constants.ContentTypeName + "_" + Constants.JournalSocialModuleTypeName + "_" + string.Format("{0}:{1}", id, portalId);
            JournalController.Instance.DeleteJournalItemByKey(portalId, objectKey);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Returns a journal type associated with creating an item (using one of the core built in journal types).
        /// </summary>
        /// <param name="portalId"></param>
        /// <returns></returns>
        /// <remarks>Sub in the appropriate journal type constant, id below.</remarks>
        private static int GetCreateEventJournalType(int portalId)
        {
            var colJournalTypes = (from t in JournalController.Instance.GetJournalTypes(portalId) where t.JournalType == Constants.JournalSocialModuleTypeName select t);
            int journalTypeId;

            if (colJournalTypes.Count() > 0)
            {
                var journalType = colJournalTypes.Single();
                journalTypeId = journalType.JournalTypeId;
            }
            else
            {
                journalTypeId = 21;
            }

            return journalTypeId;
        }

        #endregion

    }
}