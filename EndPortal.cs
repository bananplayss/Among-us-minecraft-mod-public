using Il2CppInterop.Runtime.Attributes;
using Reactor.Utilities.Attributes;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace bananplaysshu {
	[RegisterInIl2Cpp]
	public class EndPortal : MonoBehaviour{
		public EndPortal(IntPtr pt) : base(pt) { }

		#region Fields
		public static float reourcePosY;

		private static float playerPosY = 0, ySubstractFromResourcePos = .75f, checkInputCounter = .25f, checkInputCounterMax = .25f,
			resourceInteractCooldownCounter = .7f, resourceInteractCooldownCounterMax = .7f;
		public static int index = 0, index2 = 0, interactedCount = 0, interactedCountNeeded = 2;
		#endregion

		[HideFromIl2Cpp]
		private List<GameObject> eyes {  get; set; }

		private void Start() {
			eyes = new List<GameObject>();
			for (int i = 0; i < transform.GetChild(1).childCount; i++) {
				eyes.Add(transform.GetChild(1).GetChild(i).gameObject);
			}
			foreach(GameObject go in eyes) {
				go.SetActive(false);
			}

		}

		private void Update() {
			#region HandleInteractions
			float distanceNeeded = 4f;
			float yPosDiff = .7f;

			Vector3 resourcePos = transform.position;


			if (Vector3.Distance(PlayerControl.LocalPlayer.transform.position, resourcePos) < distanceNeeded &&

				(playerPosY - reourcePosY - ySubstractFromResourcePos) <= yPosDiff) {

				if (Hand.Instance == null) return;
				if (Hand.Instance.IsMining()) {
					checkInputCounter -= Time.deltaTime;
					if (checkInputCounter < 0) {
						checkInputCounter = checkInputCounterMax;
						if (PlayerControl.LocalPlayer.TryGetComponent<InventorySystem>(out InventorySystem inv)) {
							if (InventoryStorage.Instance.HasInventoryItemInventory(InventoryItemDatabase.Instance.ReturnItemByEnumName(InventoryItemDatabase.InventoryItemsEnum.EyeOfEnder)
							,ref index)) {
							if (index == eyes.Count) return;
							eyes[index2].SetActive(true);
							index2++;
								
							if (index2 == eyes.Count - 1) { eyes[index2].SetActive(true); GetComponent<Collider2D>().enabled = true; index2 = 0; }
							InventoryStorage.Instance.RemoveInventoryItemFromStorage(InventoryItemDatabase.Instance.ReturnItemByEnumName(InventoryItemDatabase.InventoryItemsEnum.EyeOfEnder));
							}
						} else {
							UnityEngine.Debug.LogError("Player didn't have an InventorySystem component on 'em. Tragic...");
						}

						interactedCount = 0;
						}
					}
				}
			}

		private void OnTriggerEnter2D(Collider2D other) {
			if (other.transform == PlayerControl.LocalPlayer.transform) {
				PlayerControl.LocalPlayer.transform.position = PortalManager.Instance.end.transform.position;
				foreach (PlayerBodySprite sprite in PlayerControl.LocalPlayer.cosmetics.bodySprites) {
					sprite.BodySprite.rendererPriority = 5;
				}
			}
		}
	}
}
#endregion