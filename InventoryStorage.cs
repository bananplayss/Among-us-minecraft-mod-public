using bananplaysshu.Buttons;
using Il2CppInterop.Runtime.Attributes;
using Il2CppInterop.Runtime.Injection;
using Reactor.Utilities.Attributes;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

namespace bananplaysshu {
	[RegisterInIl2Cpp]
	
	public class InventoryStorage : MonoBehaviour {

		public event EventHandler OnPickupItem;

		[HideFromIl2Cpp]
		public static InventoryStorage Instance { get;  set; }

		[HideFromIl2Cpp]
		public InventoryItemObject selectedInventoryItem { get;  set; }

		[HideFromIl2Cpp]
		public InventoryItemObject lastSelectedInventoryItem { get; set; }
		[HideFromIl2Cpp]
		public InventoryItemObject nextInventoryItem { get;  set; }
		[HideFromIl2Cpp]
		public List<InventoryItemObject> inventoryItemObjects { get;  set; }

		[HideFromIl2Cpp]
		public List<InventoryItemObject> hotbarInventoryItemObjects { get; set; }

		[HideFromIl2Cpp]
		public InputSlot slot { get; set; }


		public InventoryStorage(IntPtr pt) : base(pt) { }

		private void Awake() {
			Instance= this;
			inventoryItemObjects = new List<InventoryItemObject>();
			hotbarInventoryItemObjects = new List<InventoryItemObject>();
		}


		private void Start() {
			Debug.Log("Inventory Storage Initialized for local player...");
		}

		private void LateUpdate() {
			if (Input.GetKeyDown(KeyCode.X)) {
				int refindex = 0;
				InventoryItem item = InventoryItemDatabase.Instance.ReturnItemByEnumName(InventoryItemDatabase.InventoryItemsEnum.Plushie);
				if (HasInventoryItemInventory(item, ref refindex)) {
					RemoveInventoryItemFromStorage(item);

					GameObject plushie = new GameObject();
					plushie.transform.position = PlayerControl.LocalPlayer.transform.position;
					SpriteRenderer sr = plushie.AddComponent<SpriteRenderer>();
					sr.sprite = GurgeUtils.LoadSprite("bananplaysshu.Resources.PlushieDropped.png", 1200);
					sr.sortingOrder = 0;
				}
			}

			if(selectedInventoryItem!= null) {
				selectedInventoryItem.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			}

			if (Input.GetMouseButtonUp(0)) {
				lastSelectedInventoryItem = null;
			}
		}

		public void AddInventoryItemObjectList(InventoryItemObject itemObj) {
			inventoryItemObjects.Add(itemObj);
			if (itemObj.GetComponent<HotbarInventoryItemObjectHolder>()) {
				hotbarInventoryItemObjects.Add(itemObj);
			}
		}

		public bool HasInventoryItemInventory(InventoryItem item, ref int index) {
			foreach (InventoryItemObject inventoryItemObject in inventoryItemObjects) {
				if (!inventoryItemObject.containsItem) continue;
				if (inventoryItemObject.item.name == item.name) {
					index = inventoryItemObjects.IndexOf(inventoryItemObject);
					return true;
				}
			}
			return false;
		}

		public void RemoveInventoryItemFromStorage(InventoryItem item) {

			foreach (InventoryItemObject inventoryItemObject in inventoryItemObjects) {
				if (!inventoryItemObject.containsItem) continue;
				if (inventoryItemObject.item.name == item.name) {
					inventoryItemObject.RemoveOneFromQuantity();
					break;
				}
				int count = 0;
				if (!HasInventoryItemInventory(InventoryItemDatabase.Instance.ReturnItemByEnumName(InventoryItemDatabase.InventoryItemsEnum.Gunpowder),ref count)) {
					TNTBuutton.canUse = false;
				}
			}
		}

		public void RepositionItems() {
			
			if (nextInventoryItem == selectedInventoryItem) return;

			if (nextInventoryItem != null && selectedInventoryItem != null) {
				foreach (InventoryItemObject invObj in inventoryItemObjects) {
					invObj.GetComponent<SpriteRenderer>().sortingOrder = 2;
				}

				if(nextInventoryItem != null&& selectedInventoryItem != null && 
					nextInventoryItem.item != null && selectedInventoryItem.item != null) {
					if (nextInventoryItem.item.name == selectedInventoryItem.item.name) {
						nextInventoryItem.SetQuantity(nextInventoryItem.quantity + selectedInventoryItem.quantity);
						selectedInventoryItem.SetQuantity(0);

						nextInventoryItem.containsItem = true;
						selectedInventoryItem.containsItem = false;
						selectedInventoryItem.item = null;

						selectedInventoryItem.ChangeSprite(null);

						selectedInventoryItem.ResetOriginalPosition();
						return;
					}
				}
				

				Debug.Log("Repositoning Inventory Items");
				Sprite invStorageitemSprite = selectedInventoryItem.GetSprite();
				Sprite nextInvStorageitemSprite = nextInventoryItem.GetSprite();


				int nextInventoryQuantity = nextInventoryItem.quantity;
				nextInventoryItem.SetQuantity(selectedInventoryItem.quantity);
				selectedInventoryItem.SetQuantity(nextInventoryQuantity);


				bool selectedInventoryItemContainsItem = selectedInventoryItem.containsItem;
				selectedInventoryItem.containsItem = nextInventoryItem.containsItem;
				nextInventoryItem.containsItem = selectedInventoryItemContainsItem;

				InventoryItem selectedInventoryItemCurrentItem = selectedInventoryItem.item;
				selectedInventoryItem.item = nextInventoryItem.item;
				nextInventoryItem.item = selectedInventoryItemCurrentItem;

				selectedInventoryItem.ResetOriginalPosition();

				nextInventoryItem.ChangeSprite(invStorageitemSprite);
				selectedInventoryItem.ChangeSprite(nextInvStorageitemSprite);

			} else if(selectedInventoryItem != null && nextInventoryItem == null){
				selectedInventoryItem.ResetOriginalPosition();
			} else {

			}
		}

		bool initializedOriginalPos = false;
		public void AddItemToInventoryStorage(InventoryItem item) {
			OnPickupItem?.Invoke(this, EventArgs.Empty);

			foreach (InventoryItemObject inventoryItemObject in inventoryItemObjects) {
				inventoryItemObject.GetComponent<SpriteRenderer>().sortingOrder = 2;

				if(item == InventoryItemDatabase.Instance.ReturnItemByEnumName(InventoryItemDatabase.InventoryItemsEnum.TNT)) {
					TNTBuutton.canUse = true;
				}
				if (item == InventoryItemDatabase.Instance.ReturnItemByEnumName(InventoryItemDatabase.InventoryItemsEnum.IronSword)) {
					MobSpawnManager.Instance.FirstSword();
				}

				if (inventoryItemObject.item == item) {
					if (inventoryItemObject.quantity >= item.maxStack) continue;
					else AddItem(inventoryItemObject, item);
					break;
				}
				if (inventoryItemObject.containsItem) continue;
				else AddItem(inventoryItemObject, item);
				break;
			}
		}

		public void ResetAllInventoryItems() {
			selectedInventoryItem = null;
			nextInventoryItem = null;
			lastSelectedInventoryItem = null;
		}

		private void AddItem(InventoryItemObject inventoryItemObject, InventoryItem item) {
			inventoryItemObject.IncreaseQuantity();
			inventoryItemObject.containsItem = true;
			inventoryItemObject.item = item;
			inventoryItemObject.ChangeSprite(item.sprite);
		}


		public InventoryItemObject GetFirstEmptyInventoryItemObject() {
			foreach(InventoryItemObject inventoryItemObject in inventoryItemObjects) {
				if (!inventoryItemObject.containsItem) return inventoryItemObject;
			}
			return null;
		}

	}
}
