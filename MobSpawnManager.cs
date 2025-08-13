using bananplaysshu.Tools;
using Il2CppInterop.Runtime.Attributes;
using Reactor.Utilities.Attributes;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace bananplaysshu {
	[RegisterInIl2Cpp]
	public class MobSpawnManager : MonoBehaviour {
		public MobSpawnManager(IntPtr pt) : base(pt) { }
		public static MobSpawnManager Instance { get; private set; }

		public enum MobType {
			Enderman,
			Blaze,
			EnderDragon,
			Piglin,
		}


		public Vector2[] endermenPositions;
		public Vector2[] blazePositions;
		public Vector2 enderDragonPosition;


		[HideFromIl2Cpp]
		private List<NPCBehaviour> npcs { get; set; } = new List<NPCBehaviour>();

		private void Awake() => Instance = this;

		private void Start() {
			InitializePositions();
			SpawnAllMobs();
		}

		private void InitializePositions() {
			endermenPositions = new Vector2[] {
				new Vector2(-9, 1.2f),
				new Vector2(14, -5),
				new Vector2(-2.7f, -11.6f)
			};

			blazePositions = new Vector2[] {
				new Vector2(-52.8f, 31.2f),
				new Vector2(-43.6f, 29.0f),
				new Vector2(-53.8f, 27.2f)
			};

			enderDragonPosition = new Vector2(48, 29);
		}

		public void FirstSword() {
			foreach (var npc in npcs) {
				npc.FirstSwordCrafted();
			}
		}

		private void SpawnMobs(MobType mobType) {
			switch (mobType) {
				case MobType.Enderman:
					SpawnMultipleMobs(mobType, endermenPositions, ThunderzLuckyPlugin.Instance.enderman,
						InventoryItemDatabase.InventoryItemsEnum.EnderPearl, 2);
					break;

				case MobType.Blaze:
					SpawnMultipleMobs(mobType, blazePositions, ThunderzLuckyPlugin.Instance.blaze,
						InventoryItemDatabase.InventoryItemsEnum.BlazeRod, 3);
					break;

				case MobType.EnderDragon:
					SpawnSingleMob(mobType, enderDragonPosition, ThunderzLuckyPlugin.Instance.end_dragon);
					break;
			}
		}

		private void SpawnMultipleMobs(MobType type, Vector2[] positions, GameObject prefab, InventoryItemDatabase.InventoryItemsEnum itemEnum, int itemCount) {
			foreach (var pos in positions) {
				GameObject mob = Instantiate(prefab, pos, Quaternion.identity);
				NPCBehaviour npc = mob.GetComponent<NPCBehaviour>();
				npc.mobType = type;
				npc.SetMobItem(InventoryItemDatabase.Instance.ReturnItemByEnumName(itemEnum), itemCount);
				npcs.Add(npc);
			}
		}

		private void SpawnSingleMob(MobType type, Vector2 position, GameObject prefab) {
			GameObject mob = Instantiate(prefab, position, Quaternion.identity);
			NPCBehaviour npc = mob.GetComponent<NPCBehaviour>();
			npc.mobType = type;
			npcs.Add(npc);
		}

		private void SpawnAllMobs() {
			LateTask.New(() => {
				SpawnMobs(MobType.Enderman);
				SpawnMobs(MobType.Blaze);
				SpawnMobs(MobType.EnderDragon);
			}, 0.001f);
		}
	}
}
