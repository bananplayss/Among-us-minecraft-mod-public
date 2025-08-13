using Reactor.Utilities.Attributes;
using System;
using UnityEngine;

namespace bananplaysshu {

	[RegisterInIl2Cpp]
	public class NetherPortalBack : MonoBehaviour {
		public NetherPortalBack(IntPtr pt) : base(pt) { }

		public static NetherPortalBack Instance { get; private set; }

		private void Awake() {
			Instance = this;
		}

		public Vector3 ReturnSpawnPos() {
			return transform.GetChild(0).position;
		}


		private void OnTriggerEnter2D(Collider2D other) {
			if (other.transform == PlayerControl.LocalPlayer.transform) {
				PlayerControl.LocalPlayer.transform.position = NetherPortal.Instance.ReturnSpawnPos();

				

				foreach (PlayerBodySprite sprite in PlayerControl.LocalPlayer.cosmetics.bodySprites) {
					sprite.BodySprite.rendererPriority = 1;
				}
			}
		}
	}
}