using UnityEngine;
using HarmonyLib;
using Unity.Netcode;

namespace PreventLoosingScrap {
	public class SPProtectionPatches {

		[HarmonyPatch(typeof(RoundManager), nameof(RoundManager.DespawnPropsAtEndOfRound))]
		[HarmonyPrefix]
		public static bool ProtectionPrefix(RoundManager __instance, bool despawnAllItems) {
			int lostRate = Mathf.RoundToInt(Plugin.Cfg.SCRAP_LOST_RATE_ON_LOOSING.Get<float>(100f));

			Plugin.Log.LogInfo("ProtectionPatch -> Rate = " + lostRate);

			/*if (lostRate <= 1 || despawnAllItems) {
				return true;
			}*/

			if (__instance.IsHost || __instance.IsServer) {
				Plugin.Log.LogInfo("ProtectionPatch -> " + despawnAllItems + " : " + StartOfRound.Instance.allPlayersDead.ToString());

				if (StartOfRound.Instance.allPlayersDead) {
					GrabbableObject[] allItems = GameObject.FindObjectsOfType<GrabbableObject>();

					//System.Random rng = new(StartOfRound.Instance.randomMapSeed + 83);

					void DeleteItem(GrabbableObject item) {
						Plugin.Log.LogInfo("Despawning item: " + item.name + " in ship: " + item.isInShipRoom);

						item.gameObject.GetComponent<NetworkObject>().Despawn();

						if (__instance.spawnedSyncedObjects.Contains(item.gameObject)) {
							__instance.spawnedSyncedObjects.Remove(item.gameObject);
						}
					}

					foreach (GrabbableObject item in allItems) {
						if (item.isInShipRoom) {
							Plugin.Log.LogInfo("Item found : " + item.name);
							if (!item.itemProperties.isScrap || ShouldSaveScrap(/* rng, */lostRate)) {
								Plugin.Log.LogInfo("Preserving ship item: " + item.name);
							}
							else {
								DeleteItem(item);
							}
						}
						else {
							DeleteItem(item);
						}
					}

					GameObject[] tempEffects = GameObject.FindGameObjectsWithTag("TemporaryEffect");
					for (int i = 0; i < tempEffects.Length; i++)
					{
						UnityEngine.Object.Destroy(tempEffects[i]);
					}

					return false;
				}
			}

			return true;
		}

		public static bool ShouldSaveScrap(/*System.Random rng, */int rate) {
			return true;
			//return rng.Next(0, 101) <= rate;
		}
	}
}