using HarmonyLib;
using UnityEngine;

namespace bananplaysshu.Patches {

	[HarmonyPatch(typeof(HudManager), nameof(HudManager.Start))]
	public static class HUDManagerPatch {

		public static void Postfix(HudManager __instance) {
			__instance.transform.Find("Buttons").Find("BottomLeft").GetComponent<GridArrange>().MaxColumns = 2;
			__instance.transform.Find("Buttons").Find("BottomLeft").GetComponent<GridArrange>().CellSize *= .8f;

			__instance.transform.Find("Buttons").Find("BottomLeft").GetComponent<GridArrange>().transform.localScale *= .9f;

			__instance.KillButton.GetComponent<SpriteRenderer>().sprite = GurgeUtils.LoadSprite("bananplaysshu.Resources.KillButton.png", 100);
		}
	}

	[HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
	public static class HUDManagerPatchUpdate {

		public static void Postfix(HudManager __instance) {

			__instance.SabotageButton.gameObject.SetActive(false);
			__instance.ReportButton.gameObject.SetActive(false);
			__instance.ImpostorVentButton.gameObject.SetActive(false);
			__instance.KillOverlay.gameObject.SetActive(false);
		}
	}
}
