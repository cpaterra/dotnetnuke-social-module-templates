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

using System;
using DotNetNuke.Modules.SocialModule.Components.Common;
using DotNetNuke.Modules.SocialModule.Components.Controllers;
using DotNetNuke.Modules.SocialModule.Components.Models;
using DotNetNuke.Modules.SocialModule.Components.Views;
using DotNetNuke.Modules.SocialModule.Providers.Data.SqlDataProvider;
using DotNetNuke.Web.Mvp;

namespace DotNetNuke.Modules.SocialModule.Components.Presenters
{

	/// <summary>
	/// 
	/// </summary>
	public class DetailPresenter : ModulePresenter<IDetailView, DetailModel>
	{

		#region Members

		protected ISocialModuleController Controller { get; private set; }

		/// <summary>
		/// 
		/// </summary>
		private int HomePageSize
		{
			get
			{
				var pageSize = Constants.DefaultPageSize;
				if (ModuleContext.Settings.ContainsKey(Constants.SettingHomePageSize))
				{
					pageSize = Convert.ToInt32(ModuleContext.Settings[Constants.SettingHomePageSize]);
				}

				return pageSize;
			}
		}

		#endregion

		#region Constructors

		/// <summary>
		/// 
		/// </summary>
		/// <param name="view"></param>
		public DetailPresenter(IDetailView view)
			: this(view, new SocialModuleController(new SqlDataProvider()))
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="view"></param>
		/// <param name="controller"></param>
		public DetailPresenter(IDetailView view, ISocialModuleController controller)
			: base(view)
		{
			if (view == null)
			{
				throw new ArgumentException(@"View is nothing.", "view");
			}

			if (controller == null)
			{
				throw new ArgumentException(@"Controller is nothing.", "controller");
			}

			Controller = controller;
			View.Load += ViewLoad;
		}

		#endregion

		#region Event Handlers

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="eventArgs"></param>
		protected void ViewLoad(object sender, EventArgs eventArgs)
		{
			try
			{
				View.Refresh();
			}
			catch (Exception exc)
			{
				ProcessModuleLoadException(exc);
			}
			
		}

		#endregion

	}
}