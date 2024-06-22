using Amrv.ConfigurableCompany.API;
using UnityEngine;

namespace PreventLoosingScrap {
    public class PluginConfig {
		public CPage     PAGE;
		public CCategory CATEGORY;
		public CConfig   SCRAP_LOST_RATE_ON_LOOSING;

		public PluginConfig() {
			PAGE = ConfigAPI.CreatePageAuto("Prevent Loosing Scrap", "");

			CATEGORY = ConfigAPI.CreateCategoryAuto("Gameplay", PAGE, Color.red);

			SCRAP_LOST_RATE_ON_LOOSING = new CConfigBuilder() {
				ID = "vulcapack-scrap-lost-rate",
				Name = "Scrap lost rate",
				BCategory = CATEGORY,
				DefaultValue = 100f,
				BTooltip = new string[3] { "Percentage of chance to loose item upon party wipe", "", "This rate is the probability of lossing each item inside the ship, with this formula <u>rate <= RAND(0, 100)</u>" },
				Type = CTypes.WholePercent()
			}.Build();
		}
    }
}