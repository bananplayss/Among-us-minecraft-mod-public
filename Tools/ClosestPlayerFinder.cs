using System;
using UnityEngine;

namespace bananplaysshu.Tools {
	public static class ClosestPlayerFinder {

		public static PlayerControl FindClosestPlayer(PlayerControl playerFrom) {
			PlayerControl closestPlayer = null;
			float distance = 0f, closestDistance = 999f, maxDistance = 5f;
			foreach (PlayerControl pc in PlayerControl.AllPlayerControls) {
				if (pc == playerFrom || pc.Data.IsDead || !pc.cosmetics.nameText.gameObject.activeSelf) continue;
				distance = Vector3.Distance(playerFrom.transform.position, pc.transform.position);
				if (distance < closestDistance) {
					closestPlayer = pc;
					closestDistance = distance;
				}
				if (closestDistance > maxDistance) {
					Debug.LogWarning("No Player is in range for ability");
					continue;
				}
			}
			return closestPlayer; 

		}
	}
}
