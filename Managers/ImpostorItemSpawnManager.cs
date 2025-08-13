using HarmonyLib;
using Il2CppInterop.Runtime.Attributes;
using Reactor.Utilities.Attributes;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace bananplaysshu {
	[RegisterInIl2Cpp]
	public class ImpostorItemSpawnManager : MonoBehaviour {
		public ImpostorItemSpawnManager(IntPtr pt) : base(pt) { }

		[HideFromIl2Cpp]
		public static ImpostorItemSpawnManager Instance { get; private set; }

		[HideFromIl2Cpp]
		public List<ImpostorItem> impostorItemList { get; private set; }

		public Vector3[] impostorItemInventoryItemsSpawns;

		private InventoryItem[] inventoryItems = new InventoryItem[10];
		private float spawnRandomImpostorItemTimer = 50f;
		private float spawnRandomImpostorItemTimerMax = 50f;

		InventoryItemDatabase.InventoryItemsEnum itemsEnum;


		private void Awake() {
			Instance = this;

			impostorItemList = new List<ImpostorItem>();
			impostorItemInventoryItemsSpawns = new Vector3[10];

			#region ImpostorItem data
			string[] impostorItemNames = new string[] {"Spyglass", "Spyglass", "Gunpowder", "Gunpowder",
				"Trident", "Trident", "Sand", "Sand", "Gunpowder", "Gunpowder"};
			Vector3[] impostorItemPositions = new Vector3[] {
				new Vector3(-0.8f, -8.0f), new Vector3(10.5f, -6.3f), new Vector3(8.3f, 2.3f), new Vector3(-19.9f, -6.6f),
					new Vector3(-6.6f, -8.4f), new Vector3(-16.1f, 2.4f), new Vector3(2.2f, -15.1f), new Vector3(-12.6f, -3.1f),
					new Vector3(-17.4f, -13.4f), new Vector3(0.5f, -9.5f, 0.0f)};
			#endregion

			for (int i = 0; i < impostorItemNames.Length; i++) {
				itemsEnum = (InventoryItemDatabase.InventoryItemsEnum)Enum.Parse(typeof(InventoryItemDatabase.InventoryItemsEnum),
				impostorItemNames[i]);

				inventoryItems[i] = InventoryItemDatabase.Instance.ReturnItemByEnumName(itemsEnum);
				impostorItemInventoryItemsSpawns[i] = impostorItemPositions[i];
			}

		}

		private void Start() {
			spawnRandomImpostorItemTimer = spawnRandomImpostorItemTimerMax;
			int numOfImpostorItems = 10;
			for (int i = 0; i < numOfImpostorItems; i++) {
				GameObject impostorItem = GameObject.Instantiate(ThunderzLuckyPlugin.Instance.impostorItem);
				impostorItem.GetComponent<ImpostorItem>().SetItem(inventoryItems[i], impostorItemInventoryItemsSpawns[i]);
			}
		}
	}
}
