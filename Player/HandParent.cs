using Il2CppInterop.Runtime.Attributes;
using Reactor.Utilities.Attributes;
using System;
using UnityEngine;

namespace bananplaysshu {
	[RegisterInIl2Cpp]
	public class HandParent : MonoBehaviour {
		public HandParent(IntPtr pt) : base(pt) { }

		[HideFromIl2Cpp]
		public static HandParent Instance { get; set; }

		[HideFromIl2Cpp]
		public Animator animator { get; set; }

		public Hand hand;

		private void Awake() {
			Instance = this;
			animator = GetComponent<Animator>();
		}

		public bool GetFlipX() {
			return animator.GetBool("FlipX");
		}
	}
}
