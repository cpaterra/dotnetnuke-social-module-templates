<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="Detail.ascx.cs" Inherits="DotNetNuke.Modules.SocialModule.Detail" %>
<div class="dnnSocialModuleDetail">
    
</div>
<script language="javascript" type="text/javascript">
    jQuery(document).ready(function ($) {

        var smd = new Detail($, ko, {
            portalId: '<%= ModuleContext.PortalId %>'
        });

        smd.init();
    });
</script>