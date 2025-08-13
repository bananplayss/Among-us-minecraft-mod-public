using bananplaysshu.Tools;
using HarmonyLib;
using UnityEngine;

namespace bananplaysshu.Patches {
	public class ModManagerPatch {
		[HarmonyPatch(typeof(ModManager), nameof(ModManager.LateUpdate))]
		internal static class ModManagerLateUpdatePatch {
			public static bool Prefix(ModManager __instance) {
				LateTask.Update(Time.deltaTime);

				return false;
			}
		}

		[HarmonyPatch(typeof(ModManager), nameof(ModManager.LateUpdate))]
		internal static class ModManagerLateUpdate {
			public static void Postfix(ModManager __instance) {
				__instance.ShowModStamp();

				__instance.localCamera = Camera.main;

				__instance.ModStamp.transform.position = AspectPosition.ComputeWorldPosition(__instance.localCamera,
					AspectPosition.EdgeAlignments.RightTop, new Vector3(.6f, 1.2f, __instance.localCamera.nearClipPlane + .1f));
			}
		}
	}
}
