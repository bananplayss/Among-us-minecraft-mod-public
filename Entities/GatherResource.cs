using Il2CppInterop.Runtime.Attributes;
using Reactor.Utilities.Attributes;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace bananplaysshu {
	[RegisterInIl2Cpp]
	public class GatherResource : MonoBehaviour{
		public GatherResource(IntPtr pt) : base(pt) { }

		[HideFromIl2Cpp]
		private InventoryItem gatherableResource {  get; set; }
		[HideFromIl2Cpp]
		private InventoryItem requiredResource {  get; set; }
		[HideFromIl2Cpp]
		private List<SpriteRenderer> srs {  get; set; }

		#region Fields

		public static float reourcePosY;

		private static float playerPosY = 0;
		private static float ySubstractFromResourcePos = .75f;
		private static float checkInputCounter = .25f;
		private static float checkInputCounterMax = .25f;
		private static float resourceInteractCooldownCounter = .5f;
		private static float resourceInteractCooldownCounterMax = .5f;

		private static bool canInteract = true;

		public static int interactedCount = 0;
		public static int interactedCountNeeded = 2;
		#endregion



		private void Start() {
			transform.localScale *= .175f;

			srs = new List<SpriteRenderer>();
			foreach (SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>()) {
				srs.Add(sr);
			}
		}

		public void SetGatherableResource(InventoryItem gatherableResource) {
			this.gatherableResource = gatherableResource;
		}

		public void SetRequiredItem(InventoryItem requiredItem) {
			requiredResource = requiredItem;
		}

		private void Update() {

			#region HandleInteractions
			float distanceNeeded = 3f;
			float yPosDiff = .7f;

			if (gatherableResource == null) return;

			Vector3 resourcePos = transform.position;

			
			if (Vector3.Distance(PlayerControl.LocalPlayer.transform.position, resourcePos) < distanceNeeded &&
				
				(playerPosY - reourcePosY - ySubstractFromResourcePos) <= yPosDiff) {

				if (Hand.Instance == null) return;
				if (Hand.Instance.IsMining() && canInteract) {
					checkInputCounter -= Time.deltaTime;
					if (checkInputCounter < 0) {
						checkInputCounter = checkInputCounterMax;
						interactedCount++;
						if (interactedCount > interactedCountNeeded) {

							if (PlayerControl.LocalPlayer.TryGetComponent<InventorySystem>(out InventorySystem inv)) {
								int index = 0;
								if (requiredResource != null) {
									if (InventoryStorage.Instance.HasInventoryItemInventory(requiredResource, ref index)) {
										InventoryStorage.Instance.AddItemToInventoryStorage(gatherableResource);
										InventoryStorage.Instance.RemoveInventoryItemFromStorage(requiredResource);

										
									}
								} else {
									InventoryStorage.Instance.AddItemToInventoryStorage(gatherableResource);
								}
							} else {
								Debug.LogError("Player didn't have an InventorySystem component on 'em. Tragic...");
							}

							interactedCount = 0;

							canInteract = false;

							Hotbar.Instance.RefreshHotbar();
						}
					}
				}
				if (!canInteract) {
					resourceInteractCooldownCounter -= Time.deltaTime;
					if (resourceInteractCooldownCounter < 0) {
						resourceInteractCooldownCounter = resourceInteractCooldownCounterMax;
						canInteract = true;
					}
				}
			}
		}
	}
	#endregion
}

