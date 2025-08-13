using bananplaysshu.Buttons;
using Reactor.Networking.Attributes;
using UnityEngine;

namespace bananplaysshu.Tools {
	public enum CustomRPC_Enum : uint {
		DeathRpc = 0,
		SpawnPhantomRpc = 1,
		InstantiateTridentRpc = 2,
		InvisibleRpc = 3,
		InitializeHand = 4,
		SpawnPiglinArmy = 5,
		SaveRpc = 6,
		SpeedPotionRpc = 7,
		Escape = 8,
		SendEscapistData = 8,
		EndGame = 9,
		SendTridentVictimData = 10,
	}

	public class CustomRPC {

		[MethodRpc((uint)CustomRPC_Enum.DeathRpc)]
		public static void DeathRpc(PlayerControl pc) {
			if(PlayerControl.LocalPlayer == pc) {
				pc.CmdCheckMurder(pc);
			} else {
				pc.MurderPlayer(pc,MurderResultFlags.Succeeded);
			}
			
		}

		[MethodRpc((uint)CustomRPC_Enum.EndGame)]
		public static void EndGame(PlayerControl pc) {
			GameManager.Instance.RpcEndGame(GameOverReason.HumansByTask,true);

		}

		[MethodRpc((uint)CustomRPC_Enum.SendTridentVictimData)]
		public static void SendTridentVictimData(PlayerControl pc) {
			pc.MurderPlayer(pc,MurderResultFlags.Succeeded);
		}

		[MethodRpc((uint)CustomRPC_Enum.SpeedPotionRpc)]
		public static void SpeedPotion(PlayerControl pc, bool speeding) {
			float speed = speeding == true ? 4f : 2.5f;
			pc.MyPhysics.Speed = speed;
			
		}
	}
}
