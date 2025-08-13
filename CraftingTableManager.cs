using bananplaysshu.Buttons;
using HarmonyLib;
using Il2CppInterop.Runtime.Attributes;
using Reactor.Utilities.Attributes;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace bananplaysshu {
	[RegisterInIl2Cpp]
	internal class CraftingTableManager : MonoBehaviour{

		#region Fields
		[HideFromIl2Cpp]
		public static CraftingTableManager Instance { get; private set; }

		[HideFromIl2Cpp]
		InventoryItem craftedItem { get; set; }

		public CraftingTableManager(IntPtr pt) : base(pt) { }

		[HideFromIl2Cpp]
		public OutputSlot outputSlot { get; set; }

		public static CraftingTableManager currentTable;
		#endregion

		private void Awake() {
			if (CraftingTableManager.Instance != null) return;
			Instance = this;
		}

		private void Start() {
			transform.localScale *= 1.25f;
		}
		
		private void Update() {
			if (Vector3.Distance(transform.position, PlayerControl.LocalPlayer.transform.position) <= 2f) {
				CraftingTableButton.canUse = true;
				currentTable = this;
			} else {
				if(currentTable == this) {
					CraftingTableButton.canUse = false;
					currentTable = null;
				}
			}
			
		}

		public void UpdateCraftingOutput() {
			craftedItem = null;
			foreach(Recipe recipe in RecipeDatabase.Instance.ReturnRecipeDatabase()) {
				if (CheckRecipeMatch(recipe)){
					craftedItem = recipe.result;
					outputSlot.UpdateOutput(craftedItem, recipe.quantityCrafted);
					break;
				}
			}
		}

		bool CheckRecipeMatch(Recipe recipe) {
			List<InputSlot> craftingSlots = InputSlotStorage.Instance.ReturnCraftingSlots();
			int checksNeeded = 0;
			int checks = 0;
			foreach ( var ingredient in recipe.ingredients) {
				if (ingredient.name != "None") checksNeeded++;	
			}

			for (int i = 0; i < craftingSlots.Count; i++) {
				InventoryItem slotItem = craftingSlots[i].currentItem;
				
				InventoryItem requiredItem = i < recipe.ingredients.Length ? recipe.ingredients[i] : null;

				if(slotItem != null) {
					if (slotItem.name == requiredItem.name && slotItem.name != "None") {
						checks++;
					}
				}
				
			}
			if (checks >= checksNeeded) return true;
			return false;
		}
	}

	#region Saving methods
	[HarmonyPatch(typeof(AmongUs.Data.Player.PlayerData), nameof(AmongUs.Data.Player.PlayerData.FileName), MethodType.Getter)]
	[HarmonyPatch(typeof(AmongUs.Data.Settings.SettingsData), nameof(AmongUs.Data.Settings.SettingsData.FileName), MethodType.Getter)]
	public static class SaveManagerPatch {
		public static void Postfix(ref string __result) {
			__result += "_dexmods";
		}
	}
	[HarmonyPatch(typeof(AmongUs.Data.Legacy.LegacySaveManager), nameof(AmongUs.Data.Legacy.LegacySaveManager.GetPrefsName))]
	public static class LegacySaveManagerPatch {
		public static void Postfix(ref string __result) {
			__result += "_dexmods";
		}
	}
	#endregion
}
