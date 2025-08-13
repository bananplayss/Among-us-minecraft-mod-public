using HarmonyLib;

namespace bananplaysshu.Patches {
	[HarmonyPatch(typeof(EndGameManager), nameof(EndGameManager.SetEverythingUp))]
	public static class EndGameManagerPatch {
		public static void Postfix(EndGameManager __instance) {
			if (PlayerControl.LocalPlayer != null) { if (PlayerControl.LocalPlayer.Data.RoleType == AmongUs.GameOptions.RoleTypes.Impostor) return; }

			if (EndGameResult.CachedGameOverReason == GameOverReason.HumansByTask) {
				__instance.WinText.text = "You have beaten Minecraft!";
			}
		}
	}
}
