using System;
using System.Collections.Generic;

namespace bananplaysshu {
	public class Recipe {

		public InventoryItem result;
		public InventoryItem[] ingredients = new InventoryItem[9];
		public int quantityCrafted;

		public Recipe(InventoryItem result, InventoryItem[] ingredients, int quantityCrafted) {
			this.result = result;
			this.ingredients = ingredients;
			this.quantityCrafted = quantityCrafted;
		}
	}

}
