using BepInEx;
using BepInEx.Logging;
using UnityEngine;
using HarmonyLib;
using Unity.Netcode;

namespace PreventLoosingScrap
{
	[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
		public static ManualLogSource Log;
		public static PluginConfig Cfg;

        private void Awake()
        {
			Log = Logger;
			Cfg = new PluginConfig();

            // Plugin startup logic
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");

			Harmony patcher = new(PluginInfo.PLUGIN_GUID);
			patcher.PatchAll(typeof(SPProtectionPatches));
        }
    }
}
