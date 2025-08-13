using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bananplaysshu.Patches {
	[HarmonyPatch(typeof(PlayerAnimations), nameof(PlayerAnimations.PlayRunAnimation))]
	public static class PlayerAnimationsRunPatch {
		public static void Postfix() {
			if (Hand.Instance != null) {
				Hand.Instance.SetIsMining(false);
				if (HandParent.Instance.GetFlipX()) {
					const string HAND_RUNNING_RIGHT = "HandRunningRight";
					Hand.Instance.animator.Play(HAND_RUNNING_RIGHT);

				} else {
					const string HAND_RUNNING_LEFT = "HandRunningLeft";
					Hand.Instance.animator.Play(HAND_RUNNING_LEFT);
				}

			}
		}
	}

	[HarmonyPatch(typeof(PlayerAnimations), nameof(PlayerAnimations.PlayIdleAnimation))]
	public static class PlayerAnimationsIdlePatch {
		public static void Postfix() {
			if (Hand.Instance != null) {
				const string HAND_IDLE = "HandIdle";
				Hand.Instance.animator.Play(HAND_IDLE);

				Hand.Instance.SetIsMining(false);
			}
		}
	}
}
