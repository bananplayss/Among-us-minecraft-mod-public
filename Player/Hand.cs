using bananplaysshu.Tools;
using Il2CppInterop.Runtime.Attributes;
using Reactor.Networking.Attributes;
using Reactor.Utilities.Attributes;
using System;
using UnityEngine;

namespace bananplaysshu {

	[RegisterInIl2Cpp]
	public class Hand : MonoBehaviour {
		public Hand(IntPtr pt) : base(pt) { }

		#region Fields
		[HideFromIl2Cpp]
		public static Hand Instance { get; set; }

		[HideFromIl2Cpp]
		public SpriteRenderer sr { get; set; }

		[HideFromIl2Cpp]
		public Animator animator { get; set; }

		private bool isMining = false;
		#endregion

		private void Awake() {
			Instance = this;

			sr = GetComponent<SpriteRenderer>();
			animator = GetComponent<Animator>();
			sr.sortingOrder = 0;
		}



		public void UpdateSprite(Sprite sprite) {
			if (transform.parent.GetComponent<SpriteRenderer>() == null) {
				transform.parent.gameObject.AddComponent<SpriteRenderer>();
			}

			GetComponentInParent<SpriteRenderer>().sprite = sprite;

			sr.sprite = sprite;
		}



		public void SetIsMining(bool isMining) {
			animator.SetBool("IsMining", isMining);
			this.isMining = isMining;
		}

		public bool IsMining() {
			return isMining;
		}

		[MethodRpc((uint)CustomRPC_Enum.InitializeHand)]
		public static void InitializeHandRpc(PlayerControl pc) {
			GameObject handObj = GameObject.Instantiate(ThunderzLuckyPlugin.Instance.hand);
			handObj.name = "Hand";
			handObj.AddComponent<HandParent>();
			handObj.GetComponent<HandParent>().hand = handObj.transform.GetChild(0).gameObject.AddComponent<Hand>();
			handObj.transform.parent = PlayerControl.LocalPlayer.transform;
			handObj.transform.localScale *= .5f;
			handObj.transform.localPosition = Vector3.zero;
		}
	}
}
