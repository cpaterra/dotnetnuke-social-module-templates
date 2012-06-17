//
// DotNetNuke� - http://www.dotnetnuke.com
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
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Entities.Modules;

namespace DotNetNuke.Modules.SocialModule
{

	/// <summary>
	/// The Settings class manages Module Settings
	/// </summary>
	public partial class Settings : ModuleSettingsBase
	{

		#region Base Method Implementations

		/// <summary>
		/// LoadSettings loads the settings from the Database and displays them
		/// </summary>
		public override void LoadSettings()
		{
			try
			{
				if (Page.IsPostBack == false)
				{
					//Check for existing settings and use those on this page
					//Settings["SettingName"]

					/* uncomment to load saved settings in the text boxes
					if(Settings.Contains("Setting1"))
						txtSetting1.Text = Settings["Setting1"].ToString();
			
					if (Settings.Contains("Setting2"))
						txtSetting2.Text = Settings["Setting2"].ToString();
					*/
				}
			}
			catch (Exception exc) 
			{
				Exceptions.ProcessModuleLoadException(this, exc);
			}
		}

		/// <summary>
		/// UpdateSettings saves the modified settings to the Database
		/// </summary>
		public override void UpdateSettings()
		{
			try
			{
				var modules = new ModuleController();

				//the following are two sample Module Settings, using the text boxes that are commented out in the ASCX file.
				//modules.UpdateModuleSetting(ModuleId, "Setting1", txtSetting1.Text);
				//modules.UpdateModuleSetting(ModuleId, "Setting2", txtSetting2.Text);

				//modules.UpdateTabModuleSetting(this.TabModuleId, "ModuleSetting", (control.value ? "true" : "false"));
				//modules.UpdateModuleSetting(this.ModuleId, "LogBreadCrumb", (control.value ? "true" : "false"));
			}
			catch (Exception exc) //Module failed to load
			{
				Exceptions.ProcessModuleLoadException(this, exc);
			}
		}

		#endregion

	}
}