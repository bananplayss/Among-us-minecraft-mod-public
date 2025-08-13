using bananplaysshu.Tools;
using Reactor.Networking.Attributes;
using Reactor.Utilities.Attributes;
using System;
using TMPro;
using UnityEngine;

namespace bananplaysshu {
	[RegisterInIl2Cpp]
	public class PhantomBehaviour : MonoBehaviour{

		public PhantomBehaviour(IntPtr pt) : base(pt) { }

		private TextMeshProUGUI timerText;
		private float timer = 30;

		public PlayerControl player;

		private Animator animator;
		private const string PHANTOM_DIE = "Phantom_Die";
		private bool dead = false;


		private void Start() {
			animator = GetComponent<Animator>();
			timerText = transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>();
			transform.localScale *= .2f;
		}

		private void Update() {
			timer -= Time.deltaTime;
			timerText.text = $"death in {Mathf.RoundToInt(timer)}s";
			if(timer < 0 && !dead) {

				//Rpc this shite

				player.MurderPlayer(player,MurderResultFlags.Succeeded);
				KillPhantom();
				dead = true;
			}
		}

		public void KillPhantom() {
			Destroy(this.gameObject);
		}

		public void TryToKillPhantom(PlayerControl saviour) {

			int rand = UnityEngine.Random.Range(0, 1 + 1);
			SaveRpc(saviour, rand);
			KillPhantom();
		}

		[MethodRpc((uint)CustomRPC_Enum.SaveRpc)]
		public static void SaveRpc(PlayerControl saviour, int randInt) {

			if (randInt == 0) { //saves
				if (PhantomManager.Instance.ReturnPhantom() && PhantomManager.Instance.ReturnPhantom().animator != null) {
					PhantomManager.Instance.ReturnPhantom().animator.Play("Phantom_Die");
				}
			
				saviour.MyPhysics.Animations.Animator.Play(ThunderzLuckyPlugin.Instance.saveAnim);
			} else { //dies
					 //Rpc this 
				PlayerControl.LocalPlayer.CmdCheckMurder(saviour);
			}
		}
	}
}
