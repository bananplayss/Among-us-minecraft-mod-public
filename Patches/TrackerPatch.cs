using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bananplaysshu {

	[HarmonyPatch(typeof(RoomTracker), nameof(RoomTracker.Awake))]
	public class RoomTrackerPatch {
		public static void Postfix(RoomTracker __instance) {
			__instance.text.gameObject.SetActive(false);
		}


	}
	[HarmonyPatch(typeof(PingTracker), nameof(PingTracker.Update))]
	public class PingTrackerPatch {

		public static bool disabled = false;
		public static void Postfix(PingTracker __instance) {
			if (!disabled) {
				disabled = true;
				__instance.text.gameObject.SetActive(false);
			}
		}
	}
}
