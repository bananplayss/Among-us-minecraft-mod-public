using bananplaysshu.Tools;
using Reactor.Utilities.Attributes;
using System;
using UnityEngine;

namespace bananplaysshu {
	[RegisterInIl2Cpp]
	public class Trident : MonoBehaviour{
		public Trident(IntPtr pt) : base(pt) { }

		Vector3 target;

		private void Awake() {
			GetComponent<BoxCollider2D>().size *= .013f;
		}

		private void Start() {
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			target = mousePos - transform.position;
		}

		private void Update() {
			transform.position += target*.05f;
		}

		private void OnTriggerEnter2D(Collider2D other) {

			if(other.TryGetComponent<PlayerControl>(out PlayerControl pc)){
				if (pc.Data.IsDead || pc.Data.Role.IsImpostor) return;
				Debug.Log(pc.Data.PlayerName + " " + pc.Data.PlayerName);
				pc.RpcMurderPlayer(pc,true);
				Destroy(gameObject);
			}	
		}
	}
}
