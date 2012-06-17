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

using DotNetNuke.Modules.SocialModule.Components.Controllers;
using DotNetNuke.Modules.SocialModule.Components.Models;
using DotNetNuke.Modules.SocialModule.Components.Views;
using DotNetNuke.Modules.SocialModule.Providers.Data.SqlDataProvider;
using DotNetNuke.Web.Mvp;
using System;
using DotNetNuke.Common.Utilities;

namespace DotNetNuke.Modules.SocialModule.Components.Presenters {

	/// <summary>
	/// 
	/// </summary>
	public class DispatchPresenter : ModulePresenter<IDispatchView, DispatchModel> {

		#region Private Members

		protected ISocialModuleController Controller { get; private set; }

		/// <summary>
		/// 
		/// </summary>
		private string ControlView {
			get {
				var controlView = Null.NullString;
				if (!String.IsNullOrEmpty(Request.Params["view"]))
				{
					controlView = (Request.Params["view"]);
				}
				return controlView;
			}
		}

		private const string CtlHome = "/Home.ascx";
		private const string CtlProfile = "/Profile.ascx";

		#endregion

		#region Constructor

		/// <summary>
		/// 
		/// </summary>
		/// <param name="view"></param>
		public DispatchPresenter(IDispatchView view)
			: this(view, new SocialModuleController(new SqlDataProvider()))
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="view"></param>
		/// <param name="controller"></param>
		public DispatchPresenter(IDispatchView view, ISocialModuleController controller)
			: base(view) {
			if (view == null) {
				throw new ArgumentException(@"View is nothing.", "view");
			}

			if (controller == null) {
				throw new ArgumentException(@"Controller is nothing.", "controller");
			}

			Controller = controller;
		}

		#endregion

		#region Events

		/// <summary>
		/// 
		/// </summary>
		/// <remarks>We use OnInit here because this control has a sole purpose of loading other controls. If set to OnLoad or ViewLoad it would not operate properly.</remarks>
		protected override void OnInit() {
			base.OnInit();

			if ((ModuleContext.PortalSettings.ActiveTab.ParentId == ModuleContext.PortalSettings.UserTabId) || (ModuleContext.TabId == ModuleContext.PortalSettings.UserTabId))
			{
				// profile mode
				View.Model.ControlToLoad = CtlProfile;
				View.Model.InProfileMode = true;
			}
			else
			{
				switch (ControlView.ToLower())
				{
					case "home":
						View.Model.ControlToLoad = CtlHome;
						break;
					default:
						View.Model.ControlToLoad = CtlHome;
						break;
				}
			}

			View.Refresh();
		}

		#endregion

	}
}