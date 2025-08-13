using HarmonyLib;
using Il2CppInterop.Runtime.Attributes;
using Reactor.Utilities.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace bananplaysshu {
	[RegisterInIl2Cpp]
	internal class InventoryItemDatabase : MonoBehaviour {

		[HideFromIl2Cpp]
		public static InventoryItemDatabase Instance { get; private set; }

		[HideFromIl2Cpp]
		public List<InventoryItem> inventoryItems { get; set; }

		public InventoryItemDatabase(IntPtr pt) : base(pt) { }

		public enum InventoryItemsEnum {
			None, Wood, WoodenPlanks,
			Stone, Cobblestone,	Stick,
			WoodenPickaxe, StonePickaxe,
			Bucket, BucketOfLava, Iron,
			Spyglass, Gunpowder, Trident,
			IronSword, EyeOfEnder, BlazeRod,
			EnderPearl, BlazePowder, TNT,
			Sand, Plushie,

		}

		private void Awake() {
			Instance = this;
			inventoryItems = new List<InventoryItem>();
		}

		public void AddInventoryItemToDatabase(InventoryItem item) {
			inventoryItems.Add(item);
		}

		public InventoryItem ReturnItemByEnumName(InventoryItemsEnum itemToReturn) {
			foreach (InventoryItem item in inventoryItems) {

				if (item.name.ToString().ToUpper() == itemToReturn.ToString().ToUpper()) {
					return item;
				}
			}
			return null;
		}


	}


	[HarmonyPatch(typeof(GameManager), nameof(GameManager.StartGame))]
	public static class InventoryItemPatch {
		[HarmonyPriority(Priority.First)]
		public static void Postfix() {
			#region Initialize Database 
			GameObject dbGo = new GameObject("InventoryItemDatabase");
			dbGo.AddComponent<InventoryItemDatabase>();
			#endregion

			#region Add Items to database
			var enumNames = Enum.GetNames(typeof(InventoryItemDatabase.InventoryItemsEnum));

			//Bonus content
			bool includePlushie = true;
			//
			for (int i = 0; i < enumNames.Length; i++) {
				Sprite itemSprite = GurgeUtils.LoadSprite($"bananplaysshu.Resources.{enumNames[i]}.png",160);
				string itemName = enumNames[i].ToString();
				if (itemName == "Plushie" && !includePlushie) continue;
				itemSprite.name = itemName;
				int stackSize = 64;
				if (itemName.Contains("Pickaxe")) stackSize = 1; if (itemName.Contains("Bucket")) stackSize = 10; if (itemName.Equals("Spyglass")) stackSize = 1; 
				if (itemName.Equals("Trident")) stackSize = 1; if (itemName.Contains("Sword")) stackSize = 1; if (itemName.Contains("Ender")) stackSize = 16;
				InventoryItem item = new InventoryItem(itemName, itemSprite, stackSize, InventoryItemTypes.BlockType);

				InventoryItemDatabase db = InventoryItemDatabase.Instance;
				db.AddInventoryItemToDatabase(item);
			}
			#endregion

		}
	}
}
