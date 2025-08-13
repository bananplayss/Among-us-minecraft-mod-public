using Reactor.Utilities.Attributes;
using System;
using TMPro;
using UnityEngine;

namespace bananplaysshu {
	[RegisterInIl2Cpp]
	public class OutputSlot : MonoBehaviour {
		public OutputSlot(IntPtr pt) : base(pt) { }

		private SpriteRenderer sr;
		private InventoryItem currentItem;
		private TextMeshProUGUI quantityText;
		private int quantity;

		private void Start() { 
			CraftingTableManager.Instance.outputSlot = this;

			sr = GetComponent<SpriteRenderer>();
			sr.sortingOrder = 2;

			quantityText = transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>();

			gameObject.layer = LayerMask.NameToLayer("UI");
		}

		public void UpdateOutput(InventoryItem craftedItem, int quantityCrafted) {
			Debug.Log("Updating output");

			if(InventoryStorage.Instance.selectedInventoryItem != null && InventoryStorage.Instance.lastSelectedInventoryItem != null) {
				Debug.Log("Everything reset");
				InventoryStorage.Instance.lastSelectedInventoryItem.ResetOriginalPosition();
				InventoryStorage.Instance.selectedInventoryItem = null;
			}


			currentItem = craftedItem;
			sr.sprite = currentItem.sprite;
			
			SetQuantity(quantityCrafted);
		}

		public void SetQuantity(int quantity) {
			this.quantity = quantity;
			if (quantity == 1 || quantity == 0) quantityText.text = "";
			else quantityText.text = quantity.ToString();
		}

		private void OnMouseDown() {
			if(Input.GetKey(Interaction.quickGetCraftedItem) && currentItem != null) {
				InputSlotStorage.Instance.CraftFromSlot(); 

				for (int i = 0; i < quantity; i++) {
					InventoryStorage.Instance.AddItemToInventoryStorage(currentItem);

				}

				sr.sprite = null;
				currentItem = null;
				SetQuantity(0);
			}
		}
	}
}
