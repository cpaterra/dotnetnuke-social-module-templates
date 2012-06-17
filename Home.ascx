<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="Home.ascx.cs" Inherits="DotNetNuke.Modules.SocialModule.Home" %>
<div class="dnnSocialModuleHome">
	
</div>
<script language="javascript" type="text/javascript">
	jQuery(document).ready(function ($) {

		var smh = new Home($, ko, {
			portalId: '<%= ModuleContext.PortalId %>'
		});

		smh.init();
	});
</script>