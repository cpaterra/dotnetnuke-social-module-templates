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

namespace DotNetNuke.Modules.SocialModule.Components.Controllers
{

    /// <summary>
    /// This controller class is set as the BusinessController in the module's manifest. This enables support for built-in DNN core interfaces: IPortable, ISearchable, IUpgradeable.
    /// </summary>
    /// <remarks>Uncomment the interfaces to add the support</remarks>
    public class BusinessController //: IPortable, ISearchable, IUpgradeable
    {

        #region Optional Interfaces

        /// <summary>
        /// ExportModule implements the IPortable ExportModule Interface
        /// </summary>
        /// <param name="moduleID">The Id of the module to be exported</param>
        public string ExportModule(int moduleID)
        {
            //string strXML = "";

            //List<SocialModuleInfo> colSocialModules = GetSocialModules(ModuleID);
            //if (colSocialModules.Count != 0)
            //{
            //    strXML += "<SocialModules>";

            //    foreach (SocialModuleInfo objSocialModule in colSocialModules)
            //    {
            //        strXML += "<SocialModule>";
            //        strXML += "<content>" + DotNetNuke.Common.Utilities.XmlUtils.XMLEncode(objSocialModule.Content) + "</content>";
            //        strXML += "</SocialModule>";
            //    }
            //    strXML += "</SocialModules>";
            //}

            //return strXML;

            throw new System.NotImplementedException("The method or operation is not implemented.");
        }

        /// <summary>
        /// ImportModule implements the IPortable ImportModule Interface
        /// </summary>
        /// <param name="moduleID">The Id of the module to be imported</param>
        /// <param name="content">The content to be imported</param>
        /// <param name="version">The version of the module to be imported</param>
        /// <param name="UserId">The Id of the user performing the import</param>
        public void ImportModule(int moduleID, string content, string version, int userID)
        {
            //XmlNode xmlSocialModules = DotNetNuke.Common.Globals.GetContent(Content, "SocialModules");
            //foreach (XmlNode xmlSocialModule in xmlSocialModules.SelectNodes("SocialModule"))
            //{
            //    SocialModuleInfo objSocialModule = new SocialModuleInfo();
            //    objSocialModule.ModuleId = ModuleID;
            //    objSocialModule.Content = xmlSocialModule.SelectSingleNode("content").InnerText;
            //    objSocialModule.CreatedByUser = UserID;
            //    AddSocialModule(objSocialModule);
            //}

            throw new System.NotImplementedException("The method or operation is not implemented.");
        }

        /// <summary>
        /// GetSearchItems implements the ISearchable Interface
        /// </summary>
        /// <param name="modInfo">The ModuleInfo for the module to be Indexed</param>
        public Services.Search.SearchItemInfoCollection GetSearchItems(DotNetNuke.Entities.Modules.ModuleInfo modInfo)
        {
            //SearchItemInfoCollection SearchItemCollection = new SearchItemInfoCollection();

            //List<SocialModuleInfo> colSocialModules = GetSocialModules(ModInfo.ModuleID);

            //foreach (SocialModuleInfo objSocialModule in colSocialModules)
            //{
            //    SearchItemInfo SearchItem = new SearchItemInfo(ModInfo.ModuleTitle, objSocialModule.Content, objSocialModule.CreatedByUser, objSocialModule.CreatedDate, ModInfo.ModuleID, objSocialModule.ItemId.ToString(), objSocialModule.Content, "ItemId=" + objSocialModule.ItemId.ToString());
            //    SearchItemCollection.Add(SearchItem);
            //}

            //return SearchItemCollection;

            throw new System.NotImplementedException("The method or operation is not implemented.");
        }

        /// <summary>
        /// UpgradeModule implements the IUpgradeable Interface
        /// </summary>
        /// <param name="version">The current version of the module</param>
        public string UpgradeModule(string version)
        {
            throw new System.NotImplementedException("The method or operation is not implemented.");
        }

        #endregion

    }
}