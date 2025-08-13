using HarmonyLib;
using Il2CppInterop.Generator.Passes;
using Il2CppInterop.Runtime.Attributes;
using Reactor.Utilities.Attributes;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace bananplaysshu {
	[RegisterInIl2Cpp]
	public class RecipeDatabase : MonoBehaviour {
		public RecipeDatabase(IntPtr pt) : base(pt) { }

		[HideFromIl2Cpp]
		private List<Recipe> recipes { get; set; }

		[HideFromIl2Cpp]
		public static RecipeDatabase Instance { get; set; }

		private void Awake() {
			Instance = this; 
			recipes = new List<Recipe>();
		}

		public void AddRecipe(Recipe recipe) {
			recipes.Add(recipe);
		}

		public List<Recipe> ReturnRecipeDatabase() {
			return recipes;
		}


	}
}
