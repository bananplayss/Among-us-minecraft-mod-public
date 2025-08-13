using bananplaysshu.Tools;
using HarmonyLib;
using Il2CppInterop.Runtime.Attributes;
using MiraAPI.Hud;
using MiraAPI.Utilities.Assets;
using Reactor.Networking.Attributes;
using System;
using UnityEngine;

namespace bananplaysshu.Buttons {

	[RegisterButton]
	internal class SpeedPotionButton : CustomActionButton {

		#region Button Properties
		public static bool canUse = true;

		LoadableAsset<Sprite> buttonSprite = new LoadableResourceAsset("bananplaysshu.Resources.SpeedPotion.png");

		public override string Name => "Speed";

		public override float Cooldown => 5f;

		public override float EffectDuration => 0f;

		public override int MaxUses => 0;

		public override LoadableAsset<Sprite> Sprite => buttonSprite;

		public override ButtonLocation Location => ButtonLocation.BottomLeft;

		public override bool CanUse() {
			return canUse;

		}
		#endregion

		public static bool isSpeeding = false;

		public override bool Enabled(RoleBehaviour role) {
			Button.buttonLabelText.outlineColor = role.TeamType == RoleTeamTypes.Impostor ? Color.red : Button.buttonLabelText.outlineColor;
			return role.TeamType == RoleTeamTypes.Impostor ? true : false;
		}

		
		protected override void OnClick() {
			isSpeeding = !isSpeeding;
			CustomRPC.SpeedPotion(PlayerControl.LocalPlayer, isSpeeding);
		}
	}
}
