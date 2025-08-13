using Reactor.Utilities.Attributes;
using System.Collections.Generic;
using UnityEngine;

namespace bananplaysshu {
	[RegisterInIl2Cpp]
	public class DynamicSorting : MonoBehaviour {
		#region UpdateSortingOrder
		private float playerPosY = 0;

		private float offset = 0;

		private List<SpriteRenderer> srs;
		bool isLavaPool = false;

		private void Start() {
			srs = new List<SpriteRenderer>();
			foreach (SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>()) {
				srs.Add(sr);
			}
		}

		public void SetOffset(float offset) {
			this.offset = offset;
		}

		public void SetToLavaPool() {
			isLavaPool = true;
		}

		private void Update() {
			if (isLavaPool) return;
			playerPosY = PlayerControl.LocalPlayer.transform.position.y;

			if (playerPosY > transform.position.y + offset) {
				MoveSortOrderUp();

			} else {
				MoveSortOrderDown();
			}

		}
		#endregion

		#region Sorting Methods
		private void MoveSortOrderUp() {
			foreach(SpriteRenderer sr in srs) {
				sr.sortingOrder = 10;
			}
		}

		private void MoveSortOrderDown() {
			foreach (SpriteRenderer sr in srs) {
				sr.sortingOrder = 0;
			}
		}
		#endregion
	}

}
