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

using System.Collections.Generic;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Data;
using DotNetNuke.Modules.SocialModule.Components.Common;
using DotNetNuke.Modules.SocialModule.Components.Entities;
using DotNetNuke.Modules.SocialModule.Components.Integration;
using DotNetNuke.Modules.SocialModule.Providers.Data;
using DotNetNuke.Modules.SocialModule.Providers.Data.SqlDataProvider;

namespace DotNetNuke.Modules.SocialModule.Components.Controllers {

	public class SocialModuleController : ISocialModuleController
	{

		private readonly IDataProvider _dataProvider;

		#region Constructors

		/// <summary>
		/// 
		/// </summary>
		public SocialModuleController() {
			_dataProvider = ComponentModel.ComponentFactory.GetComponent<IDataProvider>();
			if (_dataProvider != null) return;

			// get the provider configuration based on the type
			var defaultprovider = DataProvider.Instance().DefaultProviderName;
			const string dataProviderNamespace = "DotNetNuke.Modules.SocialModule.Providers.Data";

			if (defaultprovider == "SqlDataProvider") {
				_dataProvider = new SqlDataProvider();
			} else {
				var providerType = dataProviderNamespace + "." + defaultprovider;
				_dataProvider = (IDataProvider)Framework.Reflection.CreateObject(providerType, providerType, true);
			}

			ComponentModel.ComponentFactory.RegisterComponentInstance<IDataProvider>(_dataProvider);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="dataProvider"></param>
		public SocialModuleController(IDataProvider dataProvider)
		{
			DotNetNuke.Common.Requires.NotNull("dataProvider", dataProvider);
			_dataProvider = dataProvider;
		}

		#endregion

		#region Public Methods

		public EntityInfo AddItem(EntityInfo objEntity, int tabId)
		{
			DotNetNuke.Common.Requires.PropertyNotNegative("portalID", "", objEntity.PortalId);

			objEntity.Id = _dataProvider.AddItem(objEntity.PortalId);

			if (objEntity.ContentItemId < 1)
			{
				objEntity.ContentItemId = CompleteItemCreation(objEntity, tabId);
			}

			//// handle cache clearing
			//DataCache.RemoveCache(Constants.ModuleCacheKey + Constants.ItemCacheKey + objEntity.ModuleID);

			return objEntity;
		}

		public List<EntityInfo> GetItems(int portalID)
		{
			DotNetNuke.Common.Requires.PropertyNotNegative("postID", "", portalID);

			// TODO: Implement caching here
			return CBO.FillCollection<EntityInfo>(_dataProvider.GetItems(portalID));
		}

		public EntityInfo GetItem(int id)
		{
			DotNetNuke.Common.Requires.PropertyNotNegative("id", "", id);

			return (EntityInfo)CBO.FillObject(_dataProvider.GetItem(id), typeof(EntityInfo));
		}

		public void UpdateItem(EntityInfo objEntity, int tabId)
		{
			DotNetNuke.Common.Requires.PropertyNotNegative("Id", "", objEntity.Id);
			DotNetNuke.Common.Requires.PropertyNotNullOrEmpty("title", "", objEntity.Title);

			_dataProvider.UpdateItem(objEntity.Id);

			CompleteItemUpdate(objEntity, tabId);
		}

		public void DeleteItem(int id, int portalId, int contentItemId)
		{
			DotNetNuke.Common.Requires.PropertyNotNegative("Id", "", id);
			DotNetNuke.Common.Requires.PropertyNotNegative("portalId", "", portalId);

			_dataProvider.DeleteItem(id, portalId);

			if (contentItemId > 0)
			{
				CompleteItemDelete(contentItemId);
			}
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// This completes the things necessary for creating a content item in the data store. 
		/// </summary>
		/// <param name="objPost">The EntityInfo entity we just created in the data store.</param>
		/// <param name="tabId">The page we will associate with our content item.</param>
		/// <returns>The ContentItemId primary key created in the Core ContentItems table.</returns>
		private static int CompleteItemCreation(EntityInfo objPost, int tabId)
		{
			var cntTaxonomy = new Content();
			var objContentItem = cntTaxonomy.CreateContentItem(objPost, tabId);

			return objContentItem.ContentItemId;
		}

		/// <summary>
		/// Handles any content item/taxonomy updates, then deals with cache clearing. 
		/// </summary>
		/// <param name="objPost"></param>
		/// <param name="tabId"></param>
		private static void CompleteItemUpdate(EntityInfo objPost, int tabId)
		{
			var cntTaxonomy = new Content();
			cntTaxonomy.UpdateContentItem(objPost, tabId);

			//DataCache.RemoveCache(Constants.ModuleCacheKey + Constants.ItemCacheKey + objPost.ModuleID);
		}

		/// <summary>
		/// Cleanup any taxonomy related items.
		/// </summary>
		/// <param name="contentItemID"></param>
		private static void CompleteItemDelete(int contentItemID)
		{
			var cntTaxonomy = new Content();
			cntTaxonomy.DeleteContentItem(contentItemID);
		}

		#endregion

	}
}