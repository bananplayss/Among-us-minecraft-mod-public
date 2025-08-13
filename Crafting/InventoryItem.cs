using UnityEngine;

namespace bananplaysshu {
	public class InventoryItem {
		public string name;
		public Sprite sprite;
		public int maxStack;
		public InventoryItemTypes itemType;

		public InventoryItem(string name, Sprite sprite, int maxStack, InventoryItemTypes itemType) {
			this.name = name;
			this.sprite = sprite;
			this.maxStack = maxStack;
			this.itemType = itemType;
		}
	}

	public enum InventoryItemTypes{
		BlockType,
		ItemType
	}
}
