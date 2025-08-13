using Reactor.Utilities.Attributes;
using System;
using UnityEngine;

namespace bananplaysshu.Tools {
	[RegisterInIl2Cpp]
	public class ItemAdder : MonoBehaviour{

		bool includeBonus = false;

		public void Update() {
			if (Input.GetKeyDown(KeyCode.O)) {
				InventoryStorage.Instance.AddItemToInventoryStorage(InventoryItemDatabase.Instance.ReturnItemByEnumName(InventoryItemDatabase.InventoryItemsEnum.Trident));
			}else if (Input.GetKeyDown(KeyCode.P)) {
				InventoryStorage.Instance.AddItemToInventoryStorage(InventoryItemDatabase.Instance.ReturnItemByEnumName(InventoryItemDatabase.InventoryItemsEnum.Spyglass));
			} else if (Input.GetKeyDown(KeyCode.L)) {
				InventoryStorage.Instance.AddItemToInventoryStorage(InventoryItemDatabase.Instance.ReturnItemByEnumName(InventoryItemDatabase.InventoryItemsEnum.Gunpowder));
			} else if (Input.GetKeyDown(KeyCode.Alpha9)) {
				InventoryStorage.Instance.AddItemToInventoryStorage(InventoryItemDatabase.Instance.ReturnItemByEnumName(InventoryItemDatabase.InventoryItemsEnum.BucketOfLava));
			} else if (Input.GetKeyDown(KeyCode.Alpha8)) {
				InventoryStorage.Instance.AddItemToInventoryStorage(InventoryItemDatabase.Instance.ReturnItemByEnumName(InventoryItemDatabase.InventoryItemsEnum.IronSword));
			} else if (Input.GetKeyDown(KeyCode.Alpha7)) {
				InventoryStorage.Instance.AddItemToInventoryStorage(InventoryItemDatabase.Instance.ReturnItemByEnumName(InventoryItemDatabase.InventoryItemsEnum.EyeOfEnder));
			} else if (Input.GetKeyDown(KeyCode.Alpha6)) {
				InventoryStorage.Instance.AddItemToInventoryStorage(InventoryItemDatabase.Instance.ReturnItemByEnumName(InventoryItemDatabase.InventoryItemsEnum.EnderPearl));
			} else if (Input.GetKeyDown(KeyCode.Alpha5)) {
				Hotbar.Instance.Heal(1);

			} else if (Input.GetKeyDown(KeyCode.Alpha4)) {
				InventoryStorage.Instance.AddItemToInventoryStorage(InventoryItemDatabase.Instance.ReturnItemByEnumName(InventoryItemDatabase.InventoryItemsEnum.Iron));
			} else if (Input.GetKeyDown(KeyCode.Alpha3)) {
				InventoryStorage.Instance.AddItemToInventoryStorage(InventoryItemDatabase.Instance.ReturnItemByEnumName(InventoryItemDatabase.InventoryItemsEnum.Sand));
			} else if (Input.GetKeyDown(KeyCode.K)) {
				if(includeBonus) 
					InventoryStorage.Instance.AddItemToInventoryStorage(InventoryItemDatabase.Instance.ReturnItemByEnumName(InventoryItemDatabase.InventoryItemsEnum.Plushie));
			} else if (Input.GetKeyDown(KeyCode.Alpha2)) {
				InventoryStorage.Instance.AddItemToInventoryStorage(InventoryItemDatabase.Instance.ReturnItemByEnumName(InventoryItemDatabase.InventoryItemsEnum.Sand));
			} 
		}
	}
}
