using Il2CppInterop.Runtime.Attributes;
using Reactor.Utilities.Attributes;
using Rewired;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace bananplaysshu {
	[RegisterInIl2Cpp]
	public class InputSlotStorage : MonoBehaviour {


		private int slotCount = 9;

		[HideFromIl2Cpp]
		private List<InputSlot> slots { get; set; }

		[HideFromIl2Cpp]
		public static InputSlotStorage Instance { get; private set; }

		[HideFromIl2Cpp]
		public InputSlot currentInputSlot { get; set; }

		public InputSlotStorage(IntPtr pt) : base(pt) { }

		public bool isItemActive;

		private void Awake() {
			Instance = this;
			slots = new List<InputSlot>();
		}

		private void Start() {

			int childCount = transform.childCount;
			for (int i = 0; i < childCount; i++) {
				slots.Add(transform.GetChild(i).GetComponent<InputSlot>());
			}
		}

		private void Update() {
			if (Input.GetMouseButtonUp(0) && currentInputSlot != null) {
				UpdateCraftingSlots();
				if (InventoryStorage.Instance.selectedInventoryItem != null) {
					InventoryStorage.Instance.selectedInventoryItem.ResetOriginalPosition();
					InventoryStorage.Instance.selectedInventoryItem = null;
				}

			} else if (Input.GetMouseButtonUp(2) && isItemActive) {
				isItemActive = false;
				UpdateCraftingSlots();
				if (InventoryStorage.Instance.selectedInventoryItem != null) {
					InventoryStorage.Instance.selectedInventoryItem.ResetOriginalPosition();
					InventoryStorage.Instance.selectedInventoryItem = null;
				}
			}
		}

		public void CraftFromSlot() {
			foreach (InputSlot slot in slots) {
				if(slot.quantity > 0 ) slot.SetQuantity(slot.quantity-1);

				if (slot.quantity == 0) {
					slot.sr.sprite = null;
					slot.currentItem = null;
				}
			}

			ReturnAllInputSlotItemsToInventory();


			currentInputSlot = null;
			InventoryStorage.Instance.ResetAllInventoryItems();


		}

		private void ReturnAllInputSlotItemsToInventory() {
			foreach(InputSlot slot in slots) {
				if(slot.quantity > 0) {
					slot.sr.sprite = null;
					for (int i = 0; i < slot.quantity; i++) {
						InventoryStorage.Instance.AddItemToInventoryStorage(slot.currentItem);
					}
					

					slot.SetQuantity(0);
					
					slot.currentItem = null;
				}
			}
		}


		private void UpdateCraftingSlots() {
			

			if (currentInputSlot != null && InventoryStorage.Instance.lastSelectedInventoryItem != null) {

				if (currentInputSlot.currentItem != null) return;


				currentInputSlot.currentItem = InventoryStorage.Instance.lastSelectedInventoryItem.item;
				currentInputSlot.SetQuantity(InventoryStorage.Instance.lastSelectedInventoryItem.quantity);
				currentInputSlot.sr.sprite = currentInputSlot.currentItem.sprite;

				CraftingTableManager.Instance.UpdateCraftingOutput();
				InventoryStorage.Instance.lastSelectedInventoryItem.PurgeObjectInfo();
				InventoryStorage.Instance.lastSelectedInventoryItem = null;				
			}
		}

		public List<InputSlot> ReturnCraftingSlots() {
			return slots;
		}

		public bool IsAllSlotEmpty() {
			foreach(InputSlot slot in slots) {
				if (slot.currentItem == null) return true; break;
			}

			return false;
		}
	}


}
