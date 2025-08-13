using bananplaysshu.Tools;
using HarmonyLib;
using Reactor.Utilities;

namespace bananplaysshu.Patches {
	[HarmonyPatch(typeof(IntroCutscene), nameof(IntroCutscene.ShowRole))]
	internal class IntroCutscenePatch {
		public static void Postfix(IntroCutscene __instance) {
			LateTask.New(() => {
				if (PlayerControl.LocalPlayer.Data.RoleType == AmongUs.GameOptions.RoleTypes.Impostor) {
					__instance.RoleText.text = "Minecraft";
				}
				__instance.RoleBlurbText.text = "";

				//Logger<ThunderzLuckyPlugin>.Info(string.Join("\n", typeof(ThunderzLuckyPlugin).Assembly.GetManifestResourceNames()));


			}, 0.001f);
		}
	}
}
