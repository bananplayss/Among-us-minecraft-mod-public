using HarmonyLib;
using Il2CppInterop.Runtime.Attributes;
using Reactor.Utilities.Attributes;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace bananplaysshu {
	[RegisterInIl2Cpp]
	public class PortalManager : MonoBehaviour {
		public PortalManager(IntPtr pt) : base(pt) { }

		[HideFromIl2Cpp]
		public static PortalManager Instance { get; private set; }

		public GameObject nether;
		public GameObject end;

		public List<Transform> switchTransforms;

		private void Awake() {
			Instance = this;
		}
	}
}
