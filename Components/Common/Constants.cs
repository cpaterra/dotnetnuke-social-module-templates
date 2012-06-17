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

namespace DotNetNuke.Modules.SocialModule.Components.Common
{

    public class Constants
    {

        #region Misc

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>Rename the "Item" part to something that explains your content item better.</remarks>
        public const string ContentTypeName = "DNN_SocialModule_Item";

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>You need to change this to the appropriate journal type name (see core table).</remarks>
        public const string JournalSocialModuleTypeName = "eventcreate";

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>You will need to change this to something more relevant to your module (if using, may need to add more as well).</remarks>
        public const string NotificationSocialModuleTypeName = "Social_Module_";

        /// <summary>
        /// The relative path to the shared resource file for this module.
        /// </summary>
        public const string SharedResourceFileName = "~/DesktopModules/SocialModule/App_LocalResources/SharedResources.resx";

        /// <summary>
        /// A recommended limit for a meta page title for SEO purposes.
        /// </summary>
        public const int SeoTitleLimit = 64;

        /// <summary>
        /// A recommended limit for a meta page description for SEO purposes.
        /// </summary>
        public const int SeoDescriptionLimit = 150;

        /// <summary>
        /// A recommended limit for meta page keywords for SEO purposes.
        /// </summary>
        public const int SeoKeywordsLimit = 15;

        /// <summary>
        /// Common characters that should be excluded from tag and category names as well as any url parameters.
        /// </summary>
        public const string DisallowedCharacters = "%?*&;:'\\";

        #endregion

        #region Module Settings

        internal const string SettingHomePageSize = "SocialModule_HomePageSize";

        internal const int DefaultPageSize = 20;

        #endregion

        #region Cache Keys

        /// <summary>
        /// The prefix to be applied to all cached objects in this module (to help ensure the name is unique). 
        /// </summary>
        internal const string ModuleCacheKey = "SocialModule_";

        #endregion

        /// <summary>
        /// These are the various user interfaces loaded into the dispatch control. 
        /// </summary>
        /// <remarks>Rename and add to appropriately.</remarks>
        public enum PageScope
        {
            Home = 0,
            Detail = 1
        }

    }
}