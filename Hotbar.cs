using bananplaysshu.Buttons;
using Il2CppInterop.Runtime.Attributes;
using MiraAPI.Hud;
using Reactor.Utilities.Attributes;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace bananplaysshu {
	[RegisterInIl2Cpp]
	public class Hotbar : MonoBehaviour {
		public Hotbar(IntPtr pt) : base(pt) { }
		#region Fields

		[HideFromIl2Cpp]
		public static Hotbar Instance { get; private set; }
		[HideFromIl2Cpp]
		private SpriteRenderer[] healthPoints { get;set; }
		[HideFromIl2Cpp]
		private SpriteRenderer hunger { get; set; }
		[HideFromIl2Cpp]
		private SpriteRenderer[] hotbarRenderers { get; set; }

		[HideFromIl2Cpp]
		private List<HotBarItemHolder> hotbarItems { get; set; }

		[HideFromIl2Cpp]
		private Transform selector { get; set; }
		public GameObject container { get; set; }

		private GameObject hotbarSpriteHolderGo;
		#endregion

		private int currentSelectorPosition = 0;
		private float prevScroll = 0;

		private int health = 10;

		private float piglinCooldown = 30;
		private float counter;

		private void Awake() {
			Instance = this;

			transform.localScale *= .9f;
		}

		public HotBarItemHolder CurrentHotbarItemHolder() {
			return hotbarItems[currentSelectorPosition];
		}

		public void SetActive(bool active) {
			container.SetActive(active);
		}

		public void Damage(int damage) {
			if (health == 0) return;
			health -= damage;
			healthPoints[health].GetComponent<SpriteRenderer>().color = Color.black;
		}

		public void Heal(int amount) {
			health += amount;
			healthPoints[health-1].GetComponent<SpriteRenderer>().color = Color.white;
		}

		private void Start() {
			CustomButtonSingleton<EscapeButton>.Instance.SetActive(false, PlayerControl.LocalPlayer.Data.Role);
			container = transform.GetChild(0).gameObject;
			hotbarSpriteHolderGo = container.transform.GetChild(0).gameObject;

			Show();
			gameObject.layer = LayerMask.NameToLayer("UI");
			container.gameObject.layer = LayerMask.NameToLayer("UI");
			hotbarSpriteHolderGo.layer = LayerMask.NameToLayer("UI");

			selector = container.transform.Find("HotbarSelect");

			selector.gameObject.layer = LayerMask.NameToLayer("UI");

			healthPoints = container.transform.Find("Health").GetComponentsInChildren<SpriteRenderer>();
			container.transform.Find("Hunger").gameObject.layer = LayerMask.NameToLayer("UI");
			hotbarRenderers = container.transform.Find("HotbarItems").GetComponentsInChildren<SpriteRenderer>();
			container.transform.Find("Xpbar").gameObject.layer = LayerMask.NameToLayer("UI");

			foreach(SpriteRenderer sr in healthPoints) {
				sr.gameObject.layer = LayerMask.NameToLayer("UI");
			}

			hotbarItems = new List<HotBarItemHolder>();
			foreach(SpriteRenderer sr in hotbarRenderers) {
				sr.gameObject.layer = LayerMask.NameToLayer("UI");
				hotbarItems.Add(sr.gameObject.GetComponent<HotBarItemHolder>());
			}

			health = healthPoints.Length;


		}

		private void Update() {

			if (!PiglinButton.canUse) {
				counter += Time.deltaTime;
				if (counter >= piglinCooldown) {
					PiglinButton.canUse = true;
					counter = 0;
				}
			}

			float scrollwheelInput = Input.GetAxis("Mouse ScrollWheel");
			if (scrollwheelInput > prevScroll) // forward
			{
				prevScroll = scrollwheelInput;
				if (currentSelectorPosition < 8) {
					currentSelectorPosition++;
					UpdateSelectorPosition(true);
				}
				prevScroll = 0;

			} else if (scrollwheelInput < prevScroll) // backwards
			  {
				prevScroll = scrollwheelInput;
				if (currentSelectorPosition > 0) {
					currentSelectorPosition--;
					UpdateSelectorPosition(false);
				}
				prevScroll = 0;

			}
		}

		public void UpdateSelectorPosition(bool isForward) {
			Vector3 pos = new Vector3(.8f, 0, 0);

			if (isForward) {
				selector.localPosition += pos;
			} else {
				selector.localPosition -= pos;
			}

			RefreshHotbar();
		}

		public void RefreshHotbar() {
			List<InventoryItemObject> invObjects = InventoryStorage.Instance.hotbarInventoryItemObjects;
			for (int i = 0; i < invObjects.Count; i++) {
				HotBarItemHolder hotbarItemHolder = hotbarRenderers[i].GetComponent<HotBarItemHolder>();

				hotbarItemHolder.currentItem = invObjects[i].item;
				hotbarRenderers[i].sprite = invObjects[i].GetSprite();
			}

			if (Hand.Instance != null) {
				InventoryItem item = hotbarItems[currentSelectorPosition].currentItem;
				Sprite sprite = item != null ? item.sprite : null;
				Hand.Instance.UpdateSprite(sprite);
				
			}
		}

		private void Show() {
			container.SetActive(true);
		}

		private void Hide() {
			container.SetActive(false);
		}
	}
}

