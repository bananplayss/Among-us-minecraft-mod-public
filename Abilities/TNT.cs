using bananplaysshu.Tools;
using Reactor.Utilities.Attributes;
using System;
using UnityEngine;

namespace bananplaysshu {
	[RegisterInIl2Cpp]
	public class TNT : MonoBehaviour{
		public TNT(IntPtr pt) : base(pt) { }

		private void Start() { 
			transform.GetChild(0).GetComponent<SpriteRenderer>().material = ThunderzLuckyPlugin.Instance.tntWhite_MAT;

			GetComponent<SpriteRenderer>().sortingOrder = 20;
			transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 20;
		}

		public void Destroy() {
			PlayerControl victim = null;

			foreach (PlayerControl pc in PlayerControl.AllPlayerControls) {
				if (pc.Data.IsDead) continue;
				if (Vector3.Distance(pc.transform.position, transform.position) < 2f) {
					if (pc.Data.RoleType == AmongUs.GameOptions.RoleTypes.Impostor) continue;
					victim = pc;
				}
			}

			if (victim != null) {
				//KillAnimation.SetMovement(victim, false);

				victim.MurderPlayer(victim, MurderResultFlags.Succeeded);
			}
			

			gameObject.SetActive(false);
			Invoke(nameof(DestroyGO), 4f);
			
		}

		private void DestroyGO() {
			Destroy(gameObject);
		}

		
	}
}
