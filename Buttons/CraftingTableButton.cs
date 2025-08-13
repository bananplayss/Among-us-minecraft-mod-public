using bananplaysshu.Tools;
using MiraAPI.Hud;
using MiraAPI.Utilities.Assets;
using UnityEngine;

namespace bananplaysshu.Buttons {

	[RegisterButton]
	internal class CraftingTableButton : CustomActionButton {
		#region Button Properties

		public static bool canUse = false;

		LoadableAsset<Sprite> buttonSprite = new LoadableResourceAsset("bananplaysshu.Resources.CraftingTable.png");

		public override string Name => "Craft";

		public override float Cooldown => 0f;
		
		public override float EffectDuration => 0f;

		public override int MaxUses => 0;

		public override LoadableAsset<Sprite> Sprite => buttonSprite;

		public override bool CanUse() {
			return canUse;
		}
		#endregion

		public override bool Enabled(RoleBehaviour role) {

			if(role.TeamType== RoleTeamTypes.Impostor) {
				Button.buttonLabelText.outlineColor = Color.red;
			}
			
			return true;
		}

		public override ButtonLocation Location => ButtonLocation.BottomLeft;

		protected override void OnClick() {
			if (CraftingInventory.Instance != null || Inventory.Instance != null) {

				bool active = CraftingInventory.Instance.gameObject.activeSelf;
				CraftingInventory.Instance.SetActive(!active);
				Hotbar.Instance.SetActive(active);
				//KillAnimation.SetMovement(PlayerControl.LocalPlayer, active);
			} else {
				ErrorCodeGenerator.PrintNewErrorMessage("CraftingTableButton");
			}
		}
	}
}
