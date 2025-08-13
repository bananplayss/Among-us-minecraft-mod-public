using bananplaysshu.Buttons;
using HarmonyLib;
using Il2CppInterop.Runtime.Attributes;
using Reactor.Utilities.Attributes;
using System;
using UnityEngine;

namespace bananplaysshu {
	[RegisterInIl2Cpp]
	public class PhantomManager : MonoBehaviour{
		public PhantomManager(IntPtr pt) : base(pt) { }

		[HideFromIl2Cpp]
		public static PhantomManager Instance { get; set; }

		public PhantomBehaviour phantom;

		[HideFromIl2Cpp]
		public PhantomBehaviour ReturnPhantom() {
			return phantom;
		}

		private void Awake() {
			Instance = this;
		}

		private void Update() {
			if(phantom != null) {
				SaveButton.canUse = true;
			}
		}
	}

	[HarmonyPatch(typeof(GameManager), nameof(GameManager.StartGame))]
	public static class PhantomManagerPatch {
		[HarmonyPriority(Priority.HigherThanNormal)]
		public static void Postfix() {
			GameObject phantomManager = GameObject.Instantiate(ThunderzLuckyPlugin.Instance.phantomManager);

		}
	}
}
