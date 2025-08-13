using bananplaysshu.Tools;
using InnerNet;
using MiraAPI.Hud;
using MiraAPI.Utilities.Assets;
using UnityEngine;

namespace bananplaysshu.Buttons {

	[RegisterButton]
	internal class EscapeButton : CustomActionButton {

		#region Button Properties
		public static bool canUse = true;

		LoadableAsset<Sprite> buttonSprite = new LoadableResourceAsset("bananplaysshu.Resources.EscapeButton.png");

		public override string Name => "Escape";

		public override float Cooldown => 0f;

		public override float EffectDuration => 0f;

		public override int MaxUses => 1;

		public override LoadableAsset<Sprite> Sprite => buttonSprite;

		public static int escapists = 0;

		public override bool CanUse() {
			return canUse;
		}
		#endregion

		public override bool Enabled(RoleBehaviour role) {
			return role.TeamType == RoleTeamTypes.Impostor ? false : true;
		}

		public override ButtonLocation Location => ButtonLocation.BottomRight;

		protected override void OnClick() {
			PlayerControl lp = PlayerControl.LocalPlayer;


			InvisibleButton.InvisibleRpc(lp, true);
			LocalInvisibility(lp);
			lp.CoSetName("Escaped");
			if (EscapeManager.HasAllCrewmatesEscaped()) {
				foreach(var pc in PlayerControl.AllPlayerControls) {
					if(pc.Data.RoleType == AmongUs.GameOptions.RoleTypes.Impostor) {
						CustomRPC.EndGame(pc);
					}
				}
			}
		}

		public override void SetActive(bool visible, RoleBehaviour role) {
			Button?.ToggleVisible(visible && Enabled(role));
		}

		public void LocalInvisibility(PlayerControl pc) {
			pc.cosmetics.SetPhantomRoleAlpha(.4f);
		}
	}
}
