using HarmonyLib;
using Il2CppInterop.Runtime.Attributes;
using Reactor.Utilities.Attributes;
using System;
using UnityEngine;

namespace bananplaysshu {
	[RegisterInIl2Cpp]
	class InventorySystem : MonoBehaviour {
		[HideFromIl2Cpp]
		public static InventorySystem Instance { get; private set; }

		private int maxStorage = 64;

		public GameObject inv;

		public InventorySystem(IntPtr pt) : base(pt) { }

		private void Awake() {
			Instance = this;
		}


		public void SetInventoryGo(GameObject inv) {
			this.inv = inv;
		}

		private void Update() {
			if (Input.GetKeyDown(Interaction.toggleInventory)) {
				Hotbar.Instance.RefreshHotbar();
				inv.SetActive(!inv.activeSelf);
				KillAnimation.SetMovement(PlayerControl.LocalPlayer,!inv.activeSelf);
				if (CraftingInventory.Instance.gameObject.activeSelf) CraftingInventory.Instance.SetActive(false);
				if (Hotbar.Instance.container.activeSelf) Hotbar.Instance.SetActive(false); else Hotbar.Instance.SetActive(true);

			}
		}
	}

}
