using Il2CppInterop.Runtime.Attributes;
using Reactor.Utilities.Attributes;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace bananplaysshu {
	[RegisterInIl2Cpp]
	public class NetherPortal : MonoBehaviour{
		public NetherPortal(IntPtr pt) : base(pt) { }
		public static NetherPortal Instance {  get; private set; }

		#region Fields

		public static float reourcePosY;

		private static float playerPosY = 0, ySubstractFromResourcePos = .75f, checkInputCounter = .25f, checkInputCounterMax = .25f,
			resourceInteractCooldownCounter = .5f, resourceInteractCooldownCounterMax = .5f;

		public static int interactedCount = 0, interactedCountNeeded = 2, index = 0, index2 = 0;
		#endregion

		[HideFromIl2Cpp]
		private List<GameObject> blocks { get; set; }

		private void Awake() {
			Instance = this;

			blocks = new List<GameObject>();
			for (int i = 0; i < transform.childCount; i++) {
				if (transform.GetChild(i).gameObject.name == "Spawn") continue;
				blocks.Add(transform.GetChild(i).gameObject);
			}
			foreach(GameObject go in blocks) {
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
							
							if (InventoryStorage.Instance.HasInventoryItemInventory(InventoryItemDatabase.Instance.ReturnItemByEnumName(InventoryItemDatabase.InventoryItemsEnum.BucketOfLava)
							, ref index)) {
								blocks[index2].SetActive(true);
								index2++;
								if (index2 == blocks.Count-1) { blocks[index2].SetActive(true); GetComponent<Collider2D>().enabled = true; index2 = 0; }
								InventoryStorage.Instance.RemoveInventoryItemFromStorage(InventoryItemDatabase.Instance.ReturnItemByEnumName(InventoryItemDatabase.InventoryItemsEnum.BucketOfLava));
							}
						} else {
							Debug.LogError("Player didn't have an InventorySystem component on 'em. Tragic...");
						}

						interactedCount = 0;
					}
				}
			}
		}

		private void OnTriggerEnter2D(Collider2D other) {
			if (other.transform == PlayerControl.LocalPlayer.transform) {
				PlayerControl.LocalPlayer.transform.position = NetherPortalBack.Instance.ReturnSpawnPos();
				foreach (PlayerBodySprite sprite in PlayerControl.LocalPlayer.cosmetics.bodySprites) {
					sprite.BodySprite.rendererPriority = 5;
				}
			}
		}

		public Vector3 ReturnSpawnPos() {
			return transform.GetChild(0).position;
		}
	}
}
#endregion
