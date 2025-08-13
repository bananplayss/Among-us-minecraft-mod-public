using bananplaysshu;
using MiraAPI.Hud;
using MiraAPI.Utilities.Assets;
using System;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using static bananplaysshu.ThunderzLuckyPlugin;

namespace bananplaysshu.Buttons {

	[RegisterButton]
	internal class SaveButton : CustomActionButton {
		#region Button Properties
		public static bool canUse = false;

		LoadableAsset<Sprite> buttonSprite = new LoadableResourceAsset("bananplaysshu.Resources.SaveButton.png");

		public override string Name => "Save";

		public override float Cooldown => 30f;

		public override float EffectDuration => 2f;

		public override int MaxUses => 2;

		public override LoadableAsset<Sprite> Sprite => buttonSprite;

		public override ButtonLocation Location => ButtonLocation.BottomRight;

		public override bool CanUse() {
			return canUse;
		}
		#endregion

		public override bool Enabled(RoleBehaviour role) {
			return role.TeamType == RoleTeamTypes.Impostor ? false : true;
		}

		protected override void OnClick() {
			PhantomBehaviour p = PhantomManager.Instance.ReturnPhantom();
			p.TryToKillPhantom(PlayerControl.LocalPlayer);
			
		}

		
	}
}
