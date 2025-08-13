using bananplaysshu.Tools;
using Il2CppInterop.Runtime.Attributes;
using Reactor.Networking.Attributes;
using Reactor.Utilities.Attributes;
using UnityEngine;

namespace bananplaysshu {
	[RegisterInIl2Cpp]
	public class HotBarItemHolder : MonoBehaviour {

		public HotBarItemHolder(System.IntPtr pt) : base(pt) { }

		[HideFromIl2Cpp]
		public InventoryItem currentItem { get; set; }

		private bool zoomed = false;
		private float timeTillZoomOut = 8;
		private float timeTillZoomOutMax = 8;

		

		private void Update() {
			if (zoomed) {
				timeTillZoomOut -= Time.deltaTime;
				if (timeTillZoomOut <= 0) {
					Camera.main.orthographicSize /= 3f;
					timeTillZoomOut = timeTillZoomOutMax;
					zoomed = false;
					HudManager.Instance.ShadowQuad.enabled = true;
				}
			}

			if (currentItem == null) return;
			if (Hotbar.Instance.CurrentHotbarItemHolder() != this) return;
			if (Input.GetMouseButtonDown(1)) {
				if(currentItem.name == InventoryItemDatabase.Instance.ReturnItemByEnumName(InventoryItemDatabase.InventoryItemsEnum.Trident).name){
					InstantiateTridentRpc(PlayerControl.LocalPlayer);
					Hotbar.Instance.RefreshHotbar();

					InventoryStorage.Instance.RemoveInventoryItemFromStorage(currentItem);
					currentItem = null;
				}else if(currentItem.name == InventoryItemDatabase.Instance.ReturnItemByEnumName(InventoryItemDatabase.InventoryItemsEnum.Spyglass).name){
					zoomed = true;
					Camera.main.orthographicSize *= 3f;
					HudManager.Instance.ShadowQuad.enabled = false;
					
					InventoryStorage.Instance.RemoveInventoryItemFromStorage(currentItem);
					currentItem = null;
				}

				Hotbar.Instance.RefreshHotbar();
			}
			

			
		}

		[MethodRpc((uint)CustomRPC_Enum.InstantiateTridentRpc)]
		public static void InstantiateTridentRpc(PlayerControl localp) {
			GameObject tridentObj = GameObject.Instantiate(ThunderzLuckyPlugin.Instance.trident,
			localp.transform.position, Quaternion.identity);
			
			tridentObj.transform.LookAt2d(Camera.main.ScreenToWorldPoint(Input.mousePosition));
		}
	}
}

