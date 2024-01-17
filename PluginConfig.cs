using Amrv.ConfigurableCompany;
using Amrv.ConfigurableCompany.content.model;

namespace PreventLoosingScrap {
    public class PluginConfig {
		public string GenName(string name) {
			return "neahyu_friends-preventloosingscrap_" + name;
		}

		public ConfigurationPage PAGE;

		public ConfigurationCategory CATEGORY;
		public Configuration SCRAP_LOST_RATE_ON_LOOSING;

		public PluginConfig() {
			PAGE = LethalConfiguration.CreatePage().SetName("VulcaPack").Build();

			CATEGORY = LethalConfiguration.CreateCategory()
                .SetID(GenName("category-gameplay"))
                .SetName("Gameplay")
				.SetPage(PAGE)
                .SetColorRGB(255, 0, 0)
                .HideIfEmpty(false)
                .Build(); 

			SCRAP_LOST_RATE_ON_LOOSING = LethalConfiguration.CreateConfig(GenName("scrap-rate"))
				.SetName("Scrap lost rate")
				.SetCategory(CATEGORY)
				.SetValue((object)100f)
				.SetType(ConfigurationTypes.Percent)
				.SetTooltip(new string[3] { "Percentage of chance to loose item upon party wipe", "", "This rate is the probability of lossing each item inside the ship, with this formula <u>rate <= RAND(0, 100)</u>" })
				.Build();
		}
    }
}