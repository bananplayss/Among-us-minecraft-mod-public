
using Il2CppInterop.Runtime.Attributes;
using Reactor.Utilities.Attributes;
using System;
using TMPro;
using UnityEngine;

namespace bananplaysshu {
	[RegisterInIl2Cpp]
	public class InputSlot : MonoBehaviour {
		public InputSlot(IntPtr pt) : base(pt) { }
		[HideFromIl2Cpp]
		public InventoryItem currentItem { get; set; }
		[HideFromIl2Cpp]
		public SpriteRenderer sr { get; set; }

		private TextMeshProUGUI quantityText;
		public int quantity { get; set; }

		private void Start() {
			sr = GetComponent<SpriteRenderer>();
			sr.sortingOrder = 2;

			quantityText = transform.GetComponentInChildren<TextMeshProUGUI>();

			gameObject.layer = LayerMask.NameToLayer("UI");

		}

		private void OnMouseEnter() {
			InputSlotStorage.Instance.currentInputSlot = this;
		}

		private void OnMouseDown() {
			if (Input.GetKey(Interaction.quickGetCraftedItem) && currentItem != null) {
				for (int i = 0; i < quantity; i++) {
					InventoryStorage.Instance.AddItemToInventoryStorage(currentItem);

				}
				PurgeObjectInfo();
				CraftingTableManager.Instance.UpdateCraftingOutput();
			}
		}

		private void PurgeObjectInfo() {
			sr.sprite = null;
			currentItem = null;
			SetQuantity(0);
		}

		private void OnMouseOver() {
			if (currentItem != null && Input.GetMouseButton(2) && !InputSlotStorage.Instance.isItemActive && quantity > 1) {
				InventoryStorage.Instance.slot = this;
				InputSlotStorage.Instance.isItemActive = true;
				InventoryItemObject emptyItemObject = InventoryStorage.Instance.GetFirstEmptyInventoryItemObject();
				emptyItemObject.containsItem = true;
				emptyItemObject.item = currentItem;
				int emptyItemObjectQuantity = 0;
				if (quantity % 2 == 0) emptyItemObjectQuantity = quantity / 2;
				else emptyItemObjectQuantity = quantity / 2 - 1;
				int remainingQuantity = Math.Abs(quantity - emptyItemObjectQuantity);
				emptyItemObject.SetQuantity(emptyItemObjectQuantity);
				SetQuantity(remainingQuantity);
				emptyItemObject.ChangeSprite(sr.sprite);
				emptyItemObject.transform.position = transform.position;
				InventoryStorage.Instance.selectedInventoryItem = emptyItemObject;
				InventoryStorage.Instance.lastSelectedInventoryItem = emptyItemObject;
			}
		}

		public void IncreaseQuantity() {
			quantity++; ;
			if (quantity == 1 || quantity == 0) quantityText.text = "";
			else quantityText.text = quantity.ToString();
		}

		public void SetQuantity(int quantity) {
			this.quantity = quantity;
			if (quantity == 1 || quantity == 0) quantityText.text = "";
			else quantityText.text = quantity.ToString();
		}
	}
}
