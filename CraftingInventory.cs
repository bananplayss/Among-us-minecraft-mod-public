using Il2CppInterop.Runtime.Attributes;
using Reactor.Utilities.Attributes;
using System;
using UnityEngine;

namespace bananplaysshu {
	[RegisterInIl2Cpp]
	public class CraftingInventory : MonoBehaviour {

		public CraftingInventory(IntPtr pt) : base(pt) { }

		[HideFromIl2Cpp]
		public static CraftingInventory Instance { get; set; }

		private bool initialized = false;

		private delegate void FunctionToExectute();
		private FunctionToExectute functionToExecute;

		private void Awake() {
			Instance = this;
		}

		private void Start() { 
			transform.localScale *= .5f;
			transform.parent = HudManager.Instance.transform;

			gameObject.layer = LayerMask.NameToLayer("UI");
			gameObject.SetActive(false);
		}

		public void SetActive(bool active) {
			functionToExecute = active == true ? Show : Hide;
			functionToExecute();
		}

		private void Show() {
			gameObject.SetActive(true);
			if (PlayerControl.LocalPlayer.TryGetComponent<InventorySystem>(out InventorySystem inventorySystem)) {
				inventorySystem.inv.SetActive(true);
				if (!initialized) {
					initialized= true;
					GetComponent<SpriteRenderer>().sortingOrder = 1;
					transform.position = InventorySystem.Instance.inv.transform.position;
					transform.position += new Vector3(0, 2.2f, 20);
				}
			}
		}

		private void Hide() {
			gameObject.SetActive(false);
			if (PlayerControl.LocalPlayer.TryGetComponent<InventorySystem>(out InventorySystem inventorySystem)) {
				inventorySystem.inv.SetActive(false);
			}
		}
	}
}
