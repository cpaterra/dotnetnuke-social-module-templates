
function Home($, ko, settings) {
	var opts = $.extend({}, Home.defaultSettings, settings);
	var serviceManager = new Home($, ko, settings);

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
Home.defaultSettings = {
	portalId: 0
};