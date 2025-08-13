using bananplaysshu.Buttons;
using bananplaysshu.Tools;
using MiraAPI.Hud;
using Reactor.Utilities.Attributes;
using System;
using System.Collections;
using UnityEngine;

namespace bananplaysshu {
	[RegisterInIl2Cpp]
	public class NPCBehaviour : MonoBehaviour {
		public NPCBehaviour(IntPtr pt) : base(pt) { }

		public InventoryItem mobItem;
		public PlayerControl closestPlayer;
		public Transform target;
		public MobSpawnManager.MobType mobType;
		private bool killedPlayer = false;
		public bool isMainPiglin = false;

		private Vector3 originalPos, destination = Vector3.zero;
		private SpriteRenderer sr;
		private int health = 5,mobItemQuantity, damage = 1;
		private float maxAggroDistance = 4.5f, damageDistance = .5f, damageTimer = 2f, damageTimerMax = 2f, detectionDistance = 5f, minDistance = .35f, multiplier = 8f;
		private bool started, dead, knocked;
		

		private void Start() {
			transform.localScale *= .25f;
			sr = GetComponent<SpriteRenderer>();
			originalPos = transform.position;
			if (mobType == MobSpawnManager.MobType.EnderDragon) health *= 3;
			if (mobType == MobSpawnManager.MobType.Piglin) {
				damageTimer *= .4f;
				damageTimerMax *= .4f;
			}
		}

		public void SetMobItem(InventoryItem mobItem, int quantity) {
			this.mobItem = mobItem;
			this.mobItemQuantity = quantity;
		}

		public void FirstSwordCrafted() {
			GetComponent<Animator>().SetBool("hasSword", true);
		}

		private void Update() {
			if (target == null && mobType == MobSpawnManager.MobType.Piglin) Destroy(this.gameObject);
			if (dead) return;

			if (target != null && !knocked) {
				if(Vector3.Distance(target.position, transform.position) > maxAggroDistance) {
					target = null;
				}

				if(target != null) {
					if (Vector3.Distance(target.position, transform.position) < damageDistance) {
						damageTimer -= Time.deltaTime;
						if (damageTimer < 0) {
							if (mobType == MobSpawnManager.MobType.Piglin) {
								Die();
								if (isMainPiglin) {
									closestPlayer.MurderPlayer(closestPlayer,MurderResultFlags.Succeeded);
								}
							} else {
								Hotbar.Instance.Damage(damage);
							}
							damageTimer = damageTimerMax;
						}
					}
				}
				
				GetComponent<Animator>().SetBool("hasSword", true);
				float x = transform.position.x;
				if (target != null && Vector3.Distance(transform.position, target.position) > minDistance) {
					float normalMSpeed = 1.7f, edMSpeed = 1.3f;
					float moveSpeed = mobType == MobSpawnManager.MobType.EnderDragon ? edMSpeed : normalMSpeed;
					transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * moveSpeed);
				}
				sr.flipX = transform.position.x < x ? true : false;

			} else {
				if (GetComponent<Animator>().GetBool("hasSword") != true) return;
				
					if (Vector3.Distance(PlayerControl.LocalPlayer.transform.position, transform.position) < detectionDistance) {
						target = PlayerControl.LocalPlayer.transform;
					}
			}
			if (knocked) {
				
				if (!started) {
					Vector3 vector = sr.flipX == true ? Vector3.right : Vector3.left;
					float vectorMultiplier = 3.5f;

					started = true;
					destination = transform.position + (vector * vectorMultiplier);
				}

				transform.position = Vector3.MoveTowards(transform.position, destination, Time.deltaTime * multiplier);
				

				if (Vector3.Distance(transform.position, destination) < .5f) {
					knocked = false;
					started = true;
				}
			}
		}

		private void OnTriggerEnter2D(Collider2D other) {
			if (other.TryGetComponent<Hand>(out Hand hp)) {
				if (knocked) return;
				Damage(1);
			}
		}

		private void Damage(int dmg) {
			health -= dmg;
			knocked = true;
			if (health <= 0) { Die(); }
		}

		private void Die() {
			if (mobType == MobSpawnManager.MobType.EnderDragon) {
				CustomButtonSingleton<EscapeButton>.Instance.SetActive(true, PlayerControl.LocalPlayer.Data.Role);
				for (int i = 0; i < PortalManager.Instance.switchTransforms.Count; i++) {
					if (i == PortalManager.Instance.switchTransforms.Count-1) PortalManager.Instance.switchTransforms[i].gameObject.SetActive(true);
					else PortalManager.Instance.switchTransforms[i].gameObject.SetActive(false);
				}
				
			}

			target = null;
			knocked = false;
			health = 0;
			
			if (mobItem != null && !dead) {
				for (int i = 0; i < mobItemQuantity+1; i++) {
					if (mobItem == InventoryItemDatabase.Instance.ReturnItemByEnumName(InventoryItemDatabase.InventoryItemsEnum.EnderPearl)) EnderPearlButton.canUse = true;

					InventoryStorage.Instance.AddItemToInventoryStorage(mobItem);

				}
			}
			dead = true;
			if(mobType == MobSpawnManager.MobType.Piglin) {
				gameObject.SetActive(false);
			} else {
				GetComponent<Animator>().Play(mobType.ToString() + "Death");
			}
		}

		private IEnumerator RespawnCoroutine() {
			if (MobSpawnManager.Instance == null) yield return null;
			if (mobType == MobSpawnManager.MobType.EnderDragon) yield  return null;
			gameObject.SetActive(false);
			yield return new WaitForSeconds(40f);
			transform.position = originalPos;
			gameObject.SetActive(true);
			
		}
	}
}
