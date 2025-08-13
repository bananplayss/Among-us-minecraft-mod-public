using HarmonyLib;
using PowerTools;
using System;
using System.Collections.Generic;

namespace bananplaysshu.Patches {
	[HarmonyPatch(typeof(SpriteAnimNodes), nameof(SpriteAnimNodes.GetAngle))]
	public static class SpriteAnimNodesPatch {
		public static void Postfix(SpriteAnimNodes __instance) {
			__instance.m_spriteRenderer.sortingOrder = 1;
		}
	}
}
