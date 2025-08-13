using MiraAPI.Hud;
using MiraAPI.Utilities.Assets;
using Reactor.Networking.Attributes;
using bananplaysshu.Tools;
using UnityEngine;

namespace bananplaysshu.Buttons {

	[RegisterButton]
	internal class PiglinButton : CustomActionButton {

		#region Button Properties
		public static bool canUse = true;

		LoadableAsset<Sprite> buttonSprite = new LoadableResourceAsset("bananplaysshu.Resources.PiglinButton.png");

		public override string Name => "Piglin-Army";

		public override float Cooldown => 0f;

		public override float EffectDuration => 30f;

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
			PlayerControl closestPlayer = ClosestPlayerFinder.FindClosestPlayer(PlayerControl.LocalPlayer);
			if (closestPlayer != null) {
				SpawnPiglinArmyRpc(closestPlayer);
			}
		}

		[MethodRpc((uint)CustomRPC_Enum.SpawnPiglinArmy)]
		public static void SpawnPiglinArmyRpc(PlayerControl closestPlayer) {
			int piglinArmySize = 3;
			GameObject piglinObj = null;
			float offset = .5f;
			for (int i = 0; i < piglinArmySize; i++) {
				piglinObj = GameObject.Instantiate(ThunderzLuckyPlugin.Instance.piglin);
				NPCBehaviour piglinNpc = piglinObj.GetComponent<NPCBehaviour>();
				piglinNpc.target = closestPlayer.transform;
				piglinNpc.mobType = MobSpawnManager.MobType.Piglin;
				piglinNpc.closestPlayer = closestPlayer;
				piglinObj.transform.localPosition = closestPlayer.transform.position + new Vector3(2 + offset, -2f + offset, 0);
				offset += .4f;
				if (i == 0) piglinNpc.isMainPiglin = true;
			}
		}
	}
}