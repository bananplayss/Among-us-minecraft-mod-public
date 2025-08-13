using HarmonyLib;
using Reactor.Utilities.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace bananplaysshu {

	[RegisterInIl2Cpp]
	public class EnderPearlAbility : MonoBehaviour{
		public EnderPearlAbility(IntPtr pt) : base(pt) { }
		public static EnderPearlAbility Instance { get; private set; }

		public bool usingPearl;


		//Bunch of data I could've handled better
		private Vector3[] allEnderPearlColliderTargetPos = {
			new Vector3(-3.5f, 5.8f, 0.0f),
			new Vector3(-1.7f, 5.8f, 0.0f),
			new Vector3(0.5f, 5.8f, 0.0f),
			new Vector3(0.1f, 3.1f, 0.0f),
			new Vector3(0.6f, 0.6f, 0.0f),
			new Vector3(0.0f, -1.4f, 0.0f),
			new Vector3(-3.5f, -3.5f, 0.0f),
			new Vector3(-1.1f, -3.5f, 0.0f),
			new Vector3(0.7f, -3.5f, 0.0f),
			new Vector3(-5.3f, 3.3f, 0.0f),
			new Vector3(-5.3f, 1.2f, 0.0f),
			new Vector3(-5.3f, -0.8f, 0.0f),
			new Vector3(4.7f, 1.4f, 0.0f),
			new Vector3(6.9f, 1.4f, 0.0f),
			new Vector3(9.2f, 1.4f, 0.0f),
			new Vector3(9.3f, -0.3f, 0.0f),
			new Vector3(9.3f, -2.8f, 0.0f),
			new Vector3(11.2f, -3.1f, 0.0f),
			new Vector3(11.7f, -5.1f, 0.0f),
			new Vector3(13.9f, -4.7f, 0.0f),
			new Vector3(15.5f, -4.5f, 0.0f),
			new Vector3(17.2f, -4.5f, 0.0f),
			new Vector3(17.0f, -2.8f, 0.0f),
			new Vector3(17.0f, -6.0f, 0.0f),
			new Vector3(11.8f, -6.2f, 0.0f),
			new Vector3(9.5f, -6.2f, 0.0f),
			new Vector3(9.5f, -8.7f, 0.0f),
			new Vector3(9.5f, -10.8f, 0.0f),
			new Vector3(9.5f, -12.5f, 0.0f),
			new Vector3(5.8f, -12.0f, 0.0f),
			new Vector3(3.7f, -12.0f, 0.0f),
			new Vector3(1.9f, -12.0f, 0.0f),
			new Vector3(5.1f, -15.8f, 0.0f),
			new Vector3(3.2f, -15.8f, 0.0f),
			new Vector3(-0.1f, -12.0f, 0.0f),
			new Vector3(-0.1f, -9.6f, 0.0f),
			new Vector3(-0.8f, -7.7f, 0.0f),
			new Vector3(-0.8f, -5.4f, 0.0f),
			new Vector3(-0.8f, -3.7f, 0.0f),
			new Vector3(0.9f, -6.9f, 0.0f),
			new Vector3(2.9f, -6.9f, 0.0f),
			new Vector3(5.0f, -6.9f, 0.0f),
			new Vector3(5.7f, -9.2f, 0.0f),
			new Vector3(3.2f, -9.2f, 0.0f),
			new Vector3(6.9f, -3.2f, 0.0f),
			new Vector3(6.9f, -3.2f, 0.0f),
			new Vector3(-2.4f, -14.2f, 0.0f),
			new Vector3(-2.3f, -16.2f, 0.0f),
			new Vector3(-0.4f, -16.2f, 0.0f),
			new Vector3(0.0f, -14.1f, 0.0f),
			new Vector3(-4.4f, -14.4f, 0.0f),
			new Vector3(-4.5f, -12.3f, 0.0f),
			new Vector3(-4.1f, -10.1f, 0.0f),
			new Vector3(-6.4f, -14.2f, 0.0f),
			new Vector3(-8.3f, -14.2f, 0.0f),
			new Vector3(-10.3f, -14.2f, 0.0f),
			new Vector3(-12.3f, -14.2f, 0.0f),
			new Vector3(-12.1f, -12.3f, 0.0f),
			new Vector3(-14.3f, -11.9f, 0.0f),
			new Vector3(-15.7f, -12.7f, 0.0f),
			new Vector3(-15.8f, -9.7f, 0.0f),
			new Vector3(-17.0f, -8.4f, 0.0f),
			new Vector3(-17.0f, -6.7f, 0.0f),
			new Vector3(-17.0f, -5.1f, 0.0f),
			new Vector3(-17.0f, -2.5f, 0.0f),
			new Vector3(-17.0f, -0.7f, 0.0f),
			new Vector3(-15.0f, 1.1f, 0.0f),
			new Vector3(-13.4f, 1.1f, 0.0f),
			new Vector3(-10.9f, 1.1f, 0.0f),
			new Vector3(-9.0f, 1.1f, 0.0f),
			new Vector3(-6.9f, 1.1f, 0.0f),
			new Vector3(-9.1f, -1.0f, 0.0f),
			new Vector3(-9.1f, -3.2f, 0.0f),
			new Vector3(-9.1f, -4.9f, 0.0f),
			new Vector3(-7.2f, -4.9f, 0.0f),
			new Vector3(-7.6f, -2.1f, 0.0f),
			new Vector3(-5.4f, -5.1f, 0.0f),
			new Vector3(-9.6f, -11.5f, 0.0f),
			new Vector3(-7.7f, -11.5f, 0.0f),
			new Vector3(-7.4f, -9.9f, 0.0f),
			new Vector3(-9.4f, -8.1f, 0.0f),
			new Vector3(-7.4f, -7.9f, 0.0f),
			new Vector3(-5.4f, -7.9f, 0.0f),
			new Vector3(8.9f, 3.7f, 0.0f),
			new Vector3(-15.1f, -5.0f, 0.0f),
			new Vector3(-15.1f, -5.0f, 0.0f),
			new Vector3(-13.4f, -3.3f, 0.0f),
			new Vector3(-13.4f, -6.8f, 0.0f),
			new Vector3(-18.9f, -5.3f, 0.0f),
			new Vector3(-20.6f, -5.3f, 0.0f),
			new Vector3(-22.6f, -3.2f, 0.0f),
			new Vector3(-21.2f, -3.2f, 0.0f),
			new Vector3(-21.4f, -6.9f, 0.0f),
			new Vector3(-22.7f, -6.5f, 0.0f),
			new Vector3(-21.8f, -8.3f, 0.0f),
			new Vector3(-21.7f, -1.7f, 0.0f),
			new Vector3(3.3f, 3.6f, 0.0f),
			new Vector3(3.3f, 1.6f, 0.0f),
			new Vector3(3.3f, -0.7f, 0.0f),
			new Vector3(-0.8f, 3.0f, 0.0f),
			new Vector3(-1.0f, -0.2f, 0.0f),
			new Vector3(-1.0f, -1.9f, 0.0f),
			new Vector3(-4.5f, 2.8f, 0.0f),
			new Vector3(-3.8f, 1.1f, 0.0f),
			new Vector3(-3.3f, -0.2f, 0.0f),
			new Vector3(3.2f, -2.8f, 0.0f),
			new Vector3(-5.1f, -2.5f, 0.0f),
			new Vector3(-5.2f, 5.0f, 0.0f),
			new Vector3(3.0f, 5.0f, 0.0f),
			new Vector3(4.7f, 3.3f, 0.0f),
			new Vector3(4.8f, -0.6f, 0.0f),
			new Vector3(1.2f, -4.0f, 0.0f),
			new Vector3(-2.8f, -4.0f, 0.0f),
			new Vector3(11.0f, -10.3f, 0.0f),
			new Vector3(-19.1f, -0.9f, 0.0f),
			new Vector3(-17.3f, 3.0f, 0.0f),
			new Vector3(-15.1f, 3.0f, 0.0f),
			new Vector3(-15.4f, -1.0f, 0.0f),
		
		};

		private void Awake() {
			Instance = this;
		}

		private void Update() {
			if (Input.GetMouseButtonDown(0) && usingPearl) {
				Hide();
			}
		}

		private void Start() {
			Transform amongUsMap = transform.Find("AmongUsMap");
			Transform colliders = transform.Find("AmongUsMap").Find("Colliders");
			Transform container = transform.Find("AmongUsMap").Find("Colliders").Find("Container");

			amongUsMap.gameObject.layer = LayerMask.NameToLayer("UI");
			colliders.gameObject.layer = LayerMask.NameToLayer("UI");
			container.gameObject.layer = LayerMask.NameToLayer("UI");

			for (int i = 0; i < container.childCount; i++) {
				container.GetChild(i).gameObject.layer = LayerMask.NameToLayer("UI");
				container.GetChild(i).GetComponent<SpriteRenderer>().sortingOrder = 10;
				container.GetChild(i).GetComponent<EnderPearlClickCollider>().pos = allEnderPearlColliderTargetPos[i];
			}
		}

		public void Show() {
			transform.GetChild(0).gameObject.SetActive(true);
			usingPearl = true;
		}

		public void Hide() {
			transform.GetChild(0).gameObject.SetActive(false);
			usingPearl = false;
		}
	}
}
