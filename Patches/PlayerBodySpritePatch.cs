using HarmonyLib;

namespace bananplaysshu.Patches {
	[HarmonyPatch(typeof(PlayerBodySprite), nameof(PlayerBodySprite.SetFlipX))]
	public static class PlayerBodySpritePatch {
		public static void Postfix(PlayerBodySprite __instance) {
			if (Hand.Instance != null) {
				HandParent.Instance.animator.SetBool("FlipX", !__instance.BodySprite.flipX);
			}
			__instance.BodySprite.sprite = GurgeUtils.LoadSprite("bananplaysshu.Resources.Skin.png", 170);
		}
	}
}
