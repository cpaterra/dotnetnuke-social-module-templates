
function Detail($, ko, settings) {
    var opts = $.extend({}, Detail.defaultSettings, settings);
    var serviceManager = new Detail($, ko, settings);

    // represents 1 row of data
    function SocialModuleViewModel(item) {
        var self = this;

        self.PortalId = (item) ? ko.observable(item.PortalId) : ko.observable(opts.portalId);
    };

    // we call this to initialize (from the .ascx file)
    this.init = function () {

    };
}

// Settings passed in from .ascx file
Detail.defaultSettings = {
    portalId: 0
};