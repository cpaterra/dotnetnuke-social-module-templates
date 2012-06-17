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

using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Content;

namespace DotNetNuke.Modules.SocialModule.Components.Entities
{

    /// <summary>
    /// Represents columns in our data store that are associated with the Content Item (and the table associated with it in this module). 
    /// </summary>
    public class EntityInfo : ContentItem
    {

        #region Public Properties

        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int PortalId { get; set; }

        // this is pulled in retrieval sprocs, not stored as an actual column
        public int TotalRecords { get; set; }

        #endregion

        #region IHydratable Implementation

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dr"></param>
        public override void Fill(System.Data.IDataReader dr)
        {
            //Call the base classes fill method to populate base class proeprties
            base.FillInternal(dr);

            Id = Null.SetNullInteger(dr["Id"]);
            PortalId = Null.SetNullInteger(dr["PortalId"]);
            Title = Null.SetNullString(dr["Title"]);
            //Active = Null.SetNullBoolean(dr["Active"]);
            //ApprovedOnDate = Null.SetNullDateTime(dr["ApprovedOnDate"]);
            TotalRecords = Null.SetNullInteger(dr["TotalRecords"]);
        }

        /// <summary>
        /// Gets and sets the Key ID
        /// </summary>
        /// <returns>An Integer</returns>
        public override int KeyID
        {
            get { return Id; }
            set { Id = value; }
        }

        #endregion

    }

}