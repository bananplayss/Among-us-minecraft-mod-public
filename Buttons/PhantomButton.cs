using MiraAPI.Hud;
using MiraAPI.Utilities.Assets;
using Reactor.Networking.Attributes;
using bananplaysshu.Tools;
using UnityEngine;

namespace bananplaysshu.Buttons {

	[RegisterButton]
	internal class PhantomButton : CustomActionButton {
		#region Button Properties
		public static bool canUse = true;

		LoadableAsset<Sprite> buttonSprite = new LoadableResourceAsset("bananplaysshu.Resources.Phantom.png");

		public override string Name => "Phantom";

		public override float Cooldown => 10f;

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
			SpawnPhantomRpc(closestPlayer);
		}

		[MethodRpc((uint)CustomRPC_Enum.SpawnPhantomRpc)]
		private static void SpawnPhantomRpc(PlayerControl closestPlayer) {
			GameObject phantomObj = GameObject.Instantiate(ThunderzLuckyPlugin.Instance.phantom);
			phantomObj.GetComponent<PhantomBehaviour>().player = closestPlayer;
			phantomObj.transform.parent = closestPlayer.transform;
			phantomObj.transform.localPosition = new Vector3(0, .8f, 0);

			PhantomManager.Instance.phantom = phantomObj.GetComponent<PhantomBehaviour>();
		}

		
	}
}
