using Reactor.Utilities.Attributes;
using System;
using UnityEngine;

namespace bananplaysshu {

	[RegisterInIl2Cpp]
	public class EnderPearlClickCollider : MonoBehaviour {
		public EnderPearlClickCollider(IntPtr pt) : base(pt) { }

		public Vector3 pos;

		void OnMouseDown() {
			PlayerControl.LocalPlayer.transform.position = pos;
			EnderPearlAbility.Instance.Hide();
			InventoryStorage.Instance.RemoveInventoryItemFromStorage(InventoryItemDatabase.Instance.ReturnItemByEnumName(
			InventoryItemDatabase.InventoryItemsEnum.EnderPearl));
			Hotbar.Instance.RefreshHotbar();
		}

	}
}
