using HarmonyLib;
using UnityEngine;

namespace bananplaysshu.Patches {
	[HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.Start))]
	static class PlayerControlPatch {
		public static void Postfix(PlayerControl __instance) {

			__instance.MyPhysics.Animations.group.IdleAnim = ThunderzLuckyPlugin.Instance.idleAnim;
			__instance.MyPhysics.Animations.group.RunAnim = ThunderzLuckyPlugin.Instance.runAnim;
		}

		[HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.FixedUpdate))]
		static class PlayAttackAnimation {
			public static void Postfix(PlayerControl __instance) {

				if (Input.GetKeyDown(Interaction.interactKey)) {


					HandParent handParent = HandParent.Instance;
					Hand hand = Hand.Instance;

					if (!hand.IsMining()) {
						if (handParent.GetFlipX()) {
							const string HAND_MINE_RIGHT = "HandMineRight";
							hand.animator.Play(HAND_MINE_RIGHT);

						} else {
							const string HAND_MINE_LEFT = "HandMineLeft";
							hand.animator.Play(HAND_MINE_LEFT);
						}
						hand.SetIsMining(true);

						PlayerControl.LocalPlayer.MyPhysics.Animations.Animator.Play(ThunderzLuckyPlugin.Instance.attackAnim);
					}
				}
			}
		}
	}
}
