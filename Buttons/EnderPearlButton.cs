using AsmResolver.Collections;
using bananplaysshu;
using MiraAPI.Hud;
using MiraAPI.Utilities.Assets;
using System;
using UnityEngine;
using static bananplaysshu.ThunderzLuckyPlugin;

namespace bananplaysshu.Buttons {

	[RegisterButton]
	internal class EnderPearlButton : CustomActionButton {
		#region Button Properties

		public static bool canUse = true;

		LoadableAsset<Sprite> buttonSprite = new LoadableResourceAsset("bananplaysshu.Resources.EnderPearl.png");

		public override string Name => "Ender Pearl";

		public override float Cooldown => 30f;

		public override float EffectDuration => 0f;

		public override int MaxUses => 0;

		public override LoadableAsset<Sprite> Sprite => buttonSprite;

		public override ButtonLocation Location => ButtonLocation.BottomLeft;

		public override bool CanUse() {
			return canUse;
		}
		#endregion

		public override bool Enabled(RoleBehaviour role) {
			Button.buttonLabelText.outlineColor = role.TeamType == RoleTeamTypes.Impostor ? Color.red : Button.buttonLabelText.outlineColor;
			return role.TeamType == RoleTeamTypes.Impostor ? true : false;
		}

		

		protected override void OnClick() {
			int refInt = 0;
			if (!InventoryStorage.Instance.HasInventoryItemInventory(InventoryItemDatabase.Instance.ReturnItemByEnumName(
				InventoryItemDatabase.InventoryItemsEnum.EnderPearl), ref refInt)) return;
			

			EnderPearlAbility.Instance.Show();
		}

		
	}
}
