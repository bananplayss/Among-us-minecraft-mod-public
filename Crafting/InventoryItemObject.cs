
using Il2CppInterop.Runtime.Attributes;
using Reactor.Utilities.Attributes;
using System;
using TMPro;
using UnityEngine;

namespace bananplaysshu {
	[RegisterInIl2Cpp]
	public class InventoryItemObject : MonoBehaviour {


		[HideFromIl2Cpp]
		private InventoryStorage invStorage { get; set; }
		private Vector3 originalPos = Vector3.zero;
		private SpriteRenderer spriteRenderer;
		private TextMeshProUGUI quantityText;
		public int quantity { get; set; }
		public bool containsItem = false;
		[HideFromIl2Cpp]
		public InventoryItem item { get; set; }

		public InventoryItemObject(IntPtr pt) : base(pt) { }

		private void Start() {

			invStorage = InventoryStorage.Instance;

			invStorage.AddInventoryItemObjectList(this);

			spriteRenderer = GetComponent<SpriteRenderer>();
			

			quantityText = transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>();

			gameObject.layer = LayerMask.NameToLayer("UI");
			spriteRenderer.sortingOrder = 3;
		}

		public void PurgeObjectInfo() {
			containsItem = false;
			quantity = 0;
			SetQuantity(quantity);
			spriteRenderer.sprite = null;
			item = null;
		}

		public void RemoveOneFromQuantity() {
			quantity--;
			SetQuantity(quantity);
			if(quantity == 0) {
				PurgeObjectInfo();
			}
		}

		public void InitializeOriginalPosition() {
			originalPos = transform.localPosition;
		}

		private void OnMouseDown() {
			if(invStorage.selectedInventoryItem == null && containsItem) {
				invStorage.selectedInventoryItem = this;
				invStorage.lastSelectedInventoryItem = this;
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

		private void OnMouseEnter() {
			if(originalPos == Vector3.zero) originalPos = transform.localPosition;

			if (invStorage.selectedInventoryItem != null && invStorage.selectedInventoryItem != this) {
				invStorage.nextInventoryItem = this;
			}
		}

		private void OnMouseUp() {
			if (InputSlotStorage.Instance.currentInputSlot != null){
				ResetOriginalPosition();
				invStorage.selectedInventoryItem = null;
				return;
			}
			invStorage.RepositionItems();
			ResetOriginalPosition();
			invStorage.selectedInventoryItem = null;
		}

		public void ResetOriginalPosition() {
			transform.localPosition = originalPos;
		}


		public void ChangeSprite(Sprite sprite) {
			spriteRenderer.sprite = sprite;
		}

		public Sprite GetSprite() {
			return spriteRenderer.sprite;
		}
	}

}
