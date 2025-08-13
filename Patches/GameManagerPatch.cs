using bananplaysshu.Tools;
using HarmonyLib;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace bananplaysshu.Patches {
	[HarmonyPatch(typeof(GameManager), nameof(GameManager.StartGame))]
	public class GameManagerPatch {
			public static void Postfix() {
			#region CraftingInventory
				GameObject craftinginventoryGO = GameObject.Instantiate(ThunderzLuckyPlugin.Instance.craftingInventory);
				CraftingInventory craftingInventory = craftinginventoryGO.GetComponent<CraftingInventory>();

				craftinginventoryGO.transform.parent = HudManager.Instance.transform;
				//craftinginventoryGO.transform.localScale *= .5f;

				craftinginventoryGO.layer = LayerMask.NameToLayer("UI");

				CraftingInventory.Instance.SetActive(false);
				#endregion

			#region CraftingTableManager
					Vector3[] wbPosArray = new Vector3[] { new Vector3(6.2f, -9.3f, 0f), new Vector3(16.9f, -5.8f),
					new Vector3(4.6f, 3.0f, 0.0f), new Vector3(-5.9f, 3.9f, 0.0f), new Vector3(6.2f, -3.2f, 0.0f) };


					for (int i = 0; i < 5; i++) {
						GameObject workbench = new GameObject("Workbench");
						SpriteRenderer sr = workbench.AddComponent<SpriteRenderer>();
						sr.sprite = GurgeUtils.LoadSprite("bananplaysshu.Resources.CraftingTable.png", 180);
						workbench.transform.position = wbPosArray[i];
						workbench.AddComponent<CraftingTableManager>();
					}
				#endregion

			#region EnderPearlAbility

				GameObject enderPearlMap = GameObject.Instantiate(ThunderzLuckyPlugin.Instance.enderPearlMap);
				EnderPearlAbility ab = enderPearlMap.GetComponent<EnderPearlAbility>();

				enderPearlMap.transform.parent = HudManager.Instance.transform;
				enderPearlMap.transform.localScale *= .6f;
				enderPearlMap.transform.position += new Vector3(-1.5f, .2f, -100);

				enderPearlMap.layer = LayerMask.NameToLayer("UI");

				ab.Hide();
			#endregion

			#region GatherResource

			Vector3[] stonePackPosArray = new Vector3[] { new Vector3(9.2f, -12.9f, 1f), new Vector3(-13.4f, -7f, 0.0f) };
			Vector3[] ironPackPosArray = new Vector3[] { new Vector3(-22.1f, -8.1f, 1f), new Vector3(3f, -16f, 0.0f) };

			for (int i = 0; i < 2; i++) {
				//Stone packs
				GameObject stonePack = GameObject.Instantiate(ThunderzLuckyPlugin.Instance.stonePack, stonePackPosArray[i], Quaternion.identity);
				InventoryItem cobblestone = InventoryItemDatabase.Instance.ReturnItemByEnumName(InventoryItemDatabase.InventoryItemsEnum.Cobblestone);
				stonePack.GetComponent<GatherResource>().SetGatherableResource(cobblestone);
				stonePack.transform.localScale *= .5f;
				stonePack.GetComponent<DynamicSorting>().SetOffset(.8f);

				//Iron packs
				GameObject ironPack = GameObject.Instantiate(ThunderzLuckyPlugin.Instance.ironPack, new Vector3(-22.1f, -8.1f, 1f), Quaternion.identity);
				ironPack.GetComponent<GatherResource>().SetGatherableResource(InventoryItemDatabase.Instance.ReturnItemByEnumName(InventoryItemDatabase.InventoryItemsEnum.Iron));
				ironPack.transform.localScale *= .6f;
				ironPack.GetComponent<DynamicSorting>().SetOffset(.4f);

			}



			//Iron pack
			GameObject lavaPool = GameObject.Instantiate(ThunderzLuckyPlugin.Instance.lavaPool, new Vector3(-1.5f, -16.7f, 1f), Quaternion.identity);
			lavaPool.GetComponent<GatherResource>().SetGatherableResource(InventoryItemDatabase.Instance.ReturnItemByEnumName(
				InventoryItemDatabase.InventoryItemsEnum.BucketOfLava));
			lavaPool.GetComponent<GatherResource>().SetRequiredItem(
				InventoryItemDatabase.Instance.ReturnItemByEnumName(InventoryItemDatabase.InventoryItemsEnum.Bucket));
			lavaPool.GetComponent<DynamicSorting>().SetToLavaPool();
			#endregion

			#region Hand
			Hand.InitializeHandRpc(PlayerControl.LocalPlayer);
			#endregion

			#region Hotbar
			GameObject hotbarGO = GameObject.Instantiate(ThunderzLuckyPlugin.Instance.hotbar);
			Hotbar hotbar = hotbarGO.GetComponent<Hotbar>();

			hotbarGO.transform.localScale *= .5f;
			hotbarGO.transform.parent = HudManager.Instance.transform;
			hotbarGO.transform.localPosition = new Vector3(0.6f, -2.6f, 10.0f);


			hotbarGO.layer = LayerMask.NameToLayer("UI");
			#endregion

			#region InventorySystem
			InventorySystem invSystem = PlayerControl.LocalPlayer.gameObject.AddComponent<InventorySystem>();
			GameObject invObj = GameObject.Instantiate(ThunderzLuckyPlugin.Instance.inventory);
			Inventory inv = invObj.GetComponent<Inventory>();


			invSystem.SetInventoryGo(invObj);
			invObj.transform.parent = HudManager.Instance.transform;
			invObj.transform.localScale *= .5f;

			invObj.layer = LayerMask.NameToLayer("UI");

			AspectPosition invObjPos = invObj.AddComponent<AspectPosition>();

			invObjPos.DistanceFromEdge = new Vector3(0, -.66f, -200);
			invObjPos.Update();
			#endregion

			#region MobSpawnManager
			GameObject mobSpawnManagerObj = GameObject.Instantiate(ThunderzLuckyPlugin.Instance.mobSpawnManager);
			#endregion

			#region PickupSoundManager
			GameObject pickupSoundManagerGo = GameObject.Instantiate(ThunderzLuckyPlugin.Instance.pickup);
			#endregion

			#region PortalManager
			GameObject portalManager = GameObject.Instantiate(ThunderzLuckyPlugin.Instance.portalManager);

			GameObject nether = GameObject.Instantiate(ThunderzLuckyPlugin.Instance.nether, new Vector3(-50, 30, 0), Quaternion.identity);
			GameObject end = GameObject.Instantiate(ThunderzLuckyPlugin.Instance.end, new Vector3(50, 30, 0), Quaternion.identity);

			PortalManager.Instance.nether = nether;
			PortalManager.Instance.end = end;

			nether.transform.localScale *= .6f;
			end.transform.localScale *= .6f;


			GameObject endPortal = GameObject.Instantiate(ThunderzLuckyPlugin.Instance.endPortal, new Vector3(-1.2f, 4.4f, 0f), Quaternion.identity);
			GameObject waterPack = GameObject.Instantiate(ThunderzLuckyPlugin.Instance.waterPack, new Vector3(-10.1f, -4.1f, 0), Quaternion.identity);
			GameObject netherPortal = GameObject.Instantiate(ThunderzLuckyPlugin.Instance.netherPortal, waterPack.transform.position + new Vector3(0, .7f), Quaternion.identity);

			waterPack.transform.localScale *= .3f;
			endPortal.transform.localScale *= .26f;
			netherPortal.transform.localScale *= .35f;

			List<Transform> switchTransforms = new List<Transform>();
			for (int i = 0; i < end.transform.GetChild(0).childCount; i++) {
				switchTransforms.Add(end.transform.GetChild(0).GetChild(i).transform);
			}

			portalManager.GetComponent<PortalManager>().switchTransforms = switchTransforms;
			#endregion

			#region ImpostorSpawnManager
			GameObject.Instantiate(ThunderzLuckyPlugin.Instance.impostorItemSpawnManager);
			#endregion

			#region ItemAdder
			GameObject itemAdder = new GameObject("ItemAdder");
			itemAdder.AddComponent<ItemAdder>();
			#endregion

			#region RecipeDatabase
			GameObject recipeDatabaseGo = new GameObject("RecipeDatabase");
			RecipeDatabase recipeDatabase = recipeDatabaseGo.AddComponent<RecipeDatabase>();


			#region Wooden Planks Recipe

			InventoryItem woodenPlankResult = InventoryItemDatabase.Instance.ReturnItemByEnumName(
				InventoryItemDatabase.InventoryItemsEnum.WoodenPlanks);

			InventoryItem None = InventoryItemDatabase.Instance.ReturnItemByEnumName(
				InventoryItemDatabase.InventoryItemsEnum.None);
			InventoryItem Wood = InventoryItemDatabase.Instance.ReturnItemByEnumName(
				InventoryItemDatabase.InventoryItemsEnum.Wood);
			InventoryItem[] woodenPlankIngredients = new[]{
				None, None,None,None, Wood, None, None, None, None
			};
			Recipe woodenPlanksRecipe = new Recipe(woodenPlankResult, woodenPlankIngredients, 4);
			recipeDatabase.AddRecipe(woodenPlanksRecipe);

			#endregion

			#region Stick Recipe
			InventoryItem stickResult = InventoryItemDatabase.Instance.ReturnItemByEnumName(
				InventoryItemDatabase.InventoryItemsEnum.Stick);

			InventoryItem WoodenPlanks = InventoryItemDatabase.Instance.ReturnItemByEnumName(
				InventoryItemDatabase.InventoryItemsEnum.WoodenPlanks);
			InventoryItem[] stickIngredients = new[]{
				None, None,None,None, WoodenPlanks, None, None, WoodenPlanks, None
			};
			Recipe stickRecipe = new Recipe(stickResult, stickIngredients, 2);
			recipeDatabase.AddRecipe(stickRecipe);
			#endregion

			#region Wooden Pickaxe Recipe
			InventoryItem woodenPickaxeResult = InventoryItemDatabase.Instance.ReturnItemByEnumName(
				InventoryItemDatabase.InventoryItemsEnum.WoodenPickaxe);

			InventoryItem Stick = InventoryItemDatabase.Instance.ReturnItemByEnumName(
				InventoryItemDatabase.InventoryItemsEnum.Stick);
			InventoryItem[] woodenPickaxeIngredients = new[]{
				WoodenPlanks, WoodenPlanks,WoodenPlanks,None, Stick, None, None, Stick, None
			};
			Recipe woodenPickaxeRecipe = new Recipe(woodenPickaxeResult, woodenPickaxeIngredients, 1);
			recipeDatabase.AddRecipe(woodenPickaxeRecipe);
			#endregion

			#region Stone Pickaxe Recipe
			InventoryItem stonePickaxeResult = InventoryItemDatabase.Instance.ReturnItemByEnumName(
				InventoryItemDatabase.InventoryItemsEnum.StonePickaxe);

			InventoryItem Cobblestone = InventoryItemDatabase.Instance.ReturnItemByEnumName(
				InventoryItemDatabase.InventoryItemsEnum.Cobblestone);
			InventoryItem[] stonePickaxeIngredients = new[]{
				Cobblestone, Cobblestone,Cobblestone,None, Stick, None, None, Stick, None
			};
			Recipe stonePickaxeRecipe = new Recipe(stonePickaxeResult, stonePickaxeIngredients, 1);
			recipeDatabase.AddRecipe(stonePickaxeRecipe);
			#endregion

			#region Bucket Recipe
			InventoryItem bucketResult = InventoryItemDatabase.Instance.ReturnItemByEnumName(
				InventoryItemDatabase.InventoryItemsEnum.Bucket);

			InventoryItem Iron = InventoryItemDatabase.Instance.ReturnItemByEnumName(
				InventoryItemDatabase.InventoryItemsEnum.Iron);
			InventoryItem[] bucketIngredients = new[]{
				None, None,None,Iron, None, Iron, None, Iron, None
			};
			Recipe bucketRecipe = new Recipe(bucketResult, bucketIngredients, 1);
			recipeDatabase.AddRecipe(bucketRecipe);
			#endregion

			#region Iron Sword Recipe
			InventoryItem ironSwordResult = InventoryItemDatabase.Instance.ReturnItemByEnumName(
				InventoryItemDatabase.InventoryItemsEnum.IronSword);

			InventoryItem[] ironSwordIngredients = new[]{
				None, Iron,None,None, Iron, None, None, Stick, None
			};
			Recipe ironSwordRecipe = new Recipe(ironSwordResult, ironSwordIngredients, 1);
			recipeDatabase.AddRecipe(ironSwordRecipe);
			#endregion

			#region blazePowder Recipe
			InventoryItem bpResult = InventoryItemDatabase.Instance.ReturnItemByEnumName(
				InventoryItemDatabase.InventoryItemsEnum.BlazePowder);

			InventoryItem blazeRod = InventoryItemDatabase.Instance.ReturnItemByEnumName(
				InventoryItemDatabase.InventoryItemsEnum.BlazeRod);

			InventoryItem[] bpIngredients = new[]{
				None, None,None,None, blazeRod, None, None, None, None
			};
			Recipe bpRecipe = new Recipe(bpResult, bpIngredients, 2);
			recipeDatabase.AddRecipe(bpRecipe);
			#endregion

			#region Eye of Ender Recipe
			InventoryItem eoeResult = InventoryItemDatabase.Instance.ReturnItemByEnumName(
				InventoryItemDatabase.InventoryItemsEnum.EyeOfEnder);

			InventoryItem enderPearl = InventoryItemDatabase.Instance.ReturnItemByEnumName(
				InventoryItemDatabase.InventoryItemsEnum.EnderPearl);

			InventoryItem blazePowder = InventoryItemDatabase.Instance.ReturnItemByEnumName(
				InventoryItemDatabase.InventoryItemsEnum.BlazePowder);

			InventoryItem[] eoeIngredients = new[]{
				None, None,None,None, enderPearl, blazePowder, None, None, None
			};
			Recipe eoeRecipe = new Recipe(eoeResult, eoeIngredients, 1);
			recipeDatabase.AddRecipe(eoeRecipe);
			#endregion

			#region TNT Recipe
			InventoryItem tntResult = InventoryItemDatabase.Instance.ReturnItemByEnumName(
				InventoryItemDatabase.InventoryItemsEnum.TNT);

			InventoryItem sand = InventoryItemDatabase.Instance.ReturnItemByEnumName(
				InventoryItemDatabase.InventoryItemsEnum.Sand);

			InventoryItem gunpowder = InventoryItemDatabase.Instance.ReturnItemByEnumName(
				InventoryItemDatabase.InventoryItemsEnum.Gunpowder);

			InventoryItem[] tntIngredients = new[]{
				gunpowder, sand,gunpowder,sand, gunpowder, sand, gunpowder, sand, gunpowder
			};
			Recipe tntRecipe = new Recipe(tntResult, tntIngredients, 1);
			recipeDatabase.AddRecipe(tntRecipe);
			#endregion
			#endregion

			#region ShipStatus
			ShipStatus.Instance.BreakEmergencyButton();
			#endregion

		}
	}
}
