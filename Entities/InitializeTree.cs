using bananplaysshu.Tools;
using HarmonyLib;
using UnityEngine;


namespace bananplaysshu {
	[HarmonyPatch(typeof(GameManager), nameof(GameManager.StartGame))]
	public static class InitializeTree {
		#region Fields

		private static Vector3[] treePosArray = new Vector3[] {new Vector2(2.7f, -8.5f), new Vector2(2.2f, 0.7f),
		new Vector2(-3.8f, 0.7f), new Vector2(-21.2f, -3.7f), new Vector2(.3f, -14f), new Vector2(5.2f, -14.5f), new Vector2(-8.5f, -2.4f)};


		public static Transform[] treeTransforms = new Transform[7];

		public static SpriteRenderer[] treeRenderers = new SpriteRenderer[7];
		#endregion


		[HarmonyPostfix]
		public static void InitializeTreeObject() {
			LateTask.New(() => {
				for (int i = 0; i < treeTransforms.Length; i++) {
					GameObject newTree = new GameObject();
					treeTransforms[i] = newTree.transform;
					newTree.transform.position = treePosArray[i];
					HandleTreeInteraction.treePosYArray[i] = treeTransforms[i].position.y;
					treeRenderers[i] = newTree.AddComponent<SpriteRenderer>();
					treeRenderers[i].sprite = GurgeUtils.LoadSprite("bananplaysshu.Resources.Tree.png", 180);
					newTree.name = "Tree";
				}
			}, 0.001f);
		}

		#region Sorting Methods
		public static void MoveTreeSortOrderUp(SpriteRenderer renderer) {
			renderer.sortingOrder = 10;
		}

		public static void MoveTreeSortOrderDown(SpriteRenderer renderer) {
			renderer.sortingOrder = 0;
		}
		#endregion
	}


	[HarmonyPatch(typeof(ModManager), nameof(ModManager.LateUpdate))]
	static class HandleTreeInteraction {

		#region Fields

		public static float[] treePosYArray = new float[7];

		private static float checkInputCounter = .25f;
		private static float checkInputCounterMax = .25f;
		private static float playerPosY = 0;
		private static float ySubstractFromTreePos = .75f;
		private static float treeInteractCooldownCounter = 3f;
		private static float treeInteractCooldownCounterMax = 3f;

		private static bool canCut = true;

		public static int interactedCount = 0;
		public static int interactedCountNeeded = 3;
		#endregion

		[HarmonyPostfix]
		public static void HandleTreeBehaviourPostfix() {
			#region UpdateSortingOrder
			foreach (var tree in InitializeTree.treeTransforms) {
				if (tree == null) return;
			}
			if(PlayerControl.LocalPlayer!= null) {
				playerPosY = PlayerControl.LocalPlayer.transform.position.y;
			}
			

			for (int i = 0; i < treePosYArray.Length; i++) {
				if (playerPosY > treePosYArray[i]) {
					InitializeTree.MoveTreeSortOrderUp(InitializeTree.treeRenderers[i]);
				} else {
					InitializeTree.MoveTreeSortOrderDown(InitializeTree.treeRenderers[i]);
				}
			}
			#endregion
			#region HandleInteractions for trees
			float distanceNeeded = .9f;
			float yPosDiff = .5f;
			for (int i = 0; i < InitializeTree.treeTransforms.Length; i++) {
				Vector3 treePos = InitializeTree.treeTransforms[i].transform.position - new Vector3(0, ySubstractFromTreePos, 0);
				if (Vector3.Distance(PlayerControl.LocalPlayer.transform.position, treePos) < distanceNeeded &&
				(playerPosY - treePosYArray[i] - ySubstractFromTreePos) <= yPosDiff) {
					if (Hand.Instance == null) return;
					if (Hand.Instance.IsMining() && canCut) {
						checkInputCounter -= Time.deltaTime;
						if (checkInputCounter < 0) {
							checkInputCounter = checkInputCounterMax;

							interactedCount++;

							if (interactedCount > interactedCountNeeded) {
								if (PlayerControl.LocalPlayer.TryGetComponent<InventorySystem>(out InventorySystem inv)) {
									InventoryItem wood = InventoryItemDatabase.Instance.ReturnItemByEnumName(InventoryItemDatabase.InventoryItemsEnum.Wood);

									InventoryStorage.Instance.AddItemToInventoryStorage(wood);
								} else {
									Debug.LogError("Player didn't have an InventorySystem component on 'em. Tragic...");
								}

								InitializeTree.treeRenderers[i].sprite = GurgeUtils.LoadSprite("bananplaysshu.Resources.CutTree.png", 180);

								interactedCount = 0;
								treeInteractCooldownCounter = treeInteractCooldownCounterMax;
								canCut = false;

							}
						}
					}

					if (!canCut) {
						treeInteractCooldownCounter -= Time.deltaTime;
						if (treeInteractCooldownCounter < 0) {
							InitializeTree.treeRenderers[i].sprite = GurgeUtils.LoadSprite("bananplaysshu.Resources.Tree.png", 180);
							treeInteractCooldownCounter = treeInteractCooldownCounterMax;
							canCut = true;
						}
					}
				}
			}
		}
	} 
}
#endregion
