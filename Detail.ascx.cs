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
using DotNetNuke.Framework;
using DotNetNuke.Modules.SocialModule.Components.Models;
using DotNetNuke.Modules.SocialModule.Components.Presenters;
using DotNetNuke.Modules.SocialModule.Components.Views;
using DotNetNuke.Web.Client.ClientResourceManagement;
using DotNetNuke.Web.Mvp;
using WebFormsMvp;

namespace DotNetNuke.Modules.SocialModule
{

	/// <summary>
	/// This is the detail view of the module. 
	/// </summary>
	[PresenterBinding(typeof(DetailPresenter))]
	public partial class Detail : ModuleView<DetailModel>, IDetailView
	{

		#region Constructor

		public Detail()
		{
			AutoDataBind = false;
		}

		#endregion

		#region Event Handlers

		protected override void OnLoad(EventArgs e)
		{
			ServicesFramework.Instance.RequestAjaxAntiForgerySupport();
			jQuery.RequestUIRegistration();

			ClientResourceManager.RegisterScript(Page, "~/Resources/Shared/scripts/knockout.js");
			ClientResourceManager.RegisterScript(Page, "~/DesktopModules/SocialModule/Scripts/Detail.js");
		}

		#endregion

		#region Methods

		/// <summary>
		/// 
		/// </summary>
		public void Refresh()
		{

		}

		#endregion

	}
}