using bananplaysshu.Tools;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Unity.IL2CPP;
using BetterVanilla.Core.Helpers;
using HarmonyLib;
using MiraAPI;
using MiraAPI.PluginLoading;
using Reactor;
using Reactor.Networking;
using Reactor.Networking.Patches;
using Reactor.Utilities;
using Reactor.Utilities.Extensions;
using UnityEngine;

namespace bananplaysshu {
	[BepInAutoPlugin]
	[BepInProcess("Among Us.exe")]
	[BepInDependency(ReactorPlugin.Id)]
	[BepInDependency(MiraApiPlugin.Id)]
	public partial class ThunderzLuckyPlugin : BasePlugin, IMiraPlugin {

		public static ThunderzLuckyPlugin Instance { get; private set; }

		public Harmony Harmony { get; } = new(Id);

		public ConfigEntry<string> ConfigName { get; private set; }

		public AnimationClip idleAnim { get; private set; }
		public AnimationClip runAnim { get; private set; }
		public AnimationClip attackAnim { get; private set; }
		public AnimationClip saveAnim { get; private set; }
		public GameObject inventory { get; private set; }
		public GameObject craftingInventory { get; private set; }
		public GameObject hotbar { get; private set; }
		public GameObject hand { get; private set; }
		public GameObject stonePack { get; private set; }
		public GameObject ironPack { get; private set; }
		public GameObject lavaPool { get; private set; }
		public GameObject phantom { get; private set; }
		public GameObject phantomManager { get; private set; }
		public GameObject impostorItem{ get; private set; }
		public GameObject impostorItemSpawnManager { get; private set; }
		public GameObject tnt { get; private set; }
		public Material tntWhite_MAT { get; private set; }
		public GameObject trident { get; private set; }
		public GameObject piglin { get; private set; }
		public GameObject blaze { get; private set; }
		public GameObject enderman { get; private set; }
		public GameObject end_dragon{ get; private set; }
		public GameObject mobSpawnManager{ get; private set; }
		public GameObject nether{ get; private set; }
		public GameObject end{ get; private set; }
		public GameObject netherPortal{ get; private set; }
		public GameObject endPortal{ get; private set; }
		public GameObject portalManager{ get; private set; }
		public GameObject waterPack{ get; private set; }
		public GameObject enderPearlMap{ get; private set; }

		public GameObject pickup;

		public string OptionsTitleText => "Mira API\nMinecraft Mod";

		public override void Load() {
			Instance = this;

			Harmony.PatchAll();

			var assetBundle = AssetBundleUtils.LoadFromExecutingAssembly("bananplaysshu.Resources.animations-win.bundle");

			#region Assetbundle loading
			idleAnim = assetBundle.LoadAsset<AnimationClip>("Idle").DontDestroy();
			runAnim = assetBundle.LoadAsset<AnimationClip>("Run").DontDestroy();
			attackAnim = assetBundle.LoadAsset<AnimationClip>("Mine").DontDestroy();
			saveAnim = assetBundle.LoadAsset<AnimationClip>("ShootPhantom").DontDestroy();

			inventory = assetBundle.LoadAsset<GameObject>("Inventory").DontDestroy();
			craftingInventory = assetBundle.LoadAsset<GameObject>("CraftingInventory").DontDestroy();
			hotbar = assetBundle.LoadAsset<GameObject>("Hotbar").DontDestroy();
			hand = assetBundle.LoadAsset<GameObject>("Hand").DontDestroy();
			stonePack = assetBundle.LoadAsset<GameObject>("StonePack").DontDestroy();
			ironPack = assetBundle.LoadAsset<GameObject>("IronPack").DontDestroy();
			lavaPool = assetBundle.LoadAsset<GameObject>("LavaPool").DontDestroy();
			phantom = assetBundle.LoadAsset<GameObject>("Phantom").DontDestroy();
			phantomManager = assetBundle.LoadAsset<GameObject>("PhantomManager").DontDestroy();
			impostorItem = assetBundle.LoadAsset<GameObject>("ImpostorItem").DontDestroy();
			impostorItemSpawnManager = assetBundle.LoadAsset<GameObject>("ImpostorItemSpawnManager").DontDestroy();
			tnt = assetBundle.LoadAsset<GameObject>("TNT").DontDestroy();
			tntWhite_MAT = assetBundle.LoadAsset<Material>("TNT").DontDestroy();
			trident = assetBundle.LoadAsset<GameObject>("Trident").DontDestroy();
			piglin = assetBundle.LoadAsset<GameObject>("Piglin").DontDestroy();
			blaze = assetBundle.LoadAsset<GameObject>("Blaze").DontDestroy();
			enderman = assetBundle.LoadAsset<GameObject>("Enderman").DontDestroy();
			end_dragon = assetBundle.LoadAsset<GameObject>("End_Dragon").DontDestroy();
			mobSpawnManager = assetBundle.LoadAsset<GameObject>("MobSpawnManager").DontDestroy();
			nether = assetBundle.LoadAsset<GameObject>("Nether").DontDestroy();
			end = assetBundle.LoadAsset<GameObject>("End").DontDestroy();
			netherPortal = assetBundle.LoadAsset<GameObject>("NetherPortal").DontDestroy();
			endPortal = assetBundle.LoadAsset<GameObject>("EndPortal").DontDestroy();
			portalManager = assetBundle.LoadAsset<GameObject>("PortalManager").DontDestroy();
			waterPack = assetBundle.LoadAsset<GameObject>("WaterPack").DontDestroy();
			enderPearlMap = assetBundle.LoadAsset<GameObject>("EnderPearlMap").DontDestroy();
			pickup = assetBundle.LoadAsset<GameObject>("PickupSoundManager").DontDestroy();

			#endregion


		}

		public ConfigFile GetConfigFile() {
			return Config;
		}

		[HarmonyPatch(typeof(IGameOptionsExtensions), nameof(IGameOptionsExtensions.GetAdjustedNumImpostors))]
		class UnrestrictedNumImpostorsPatch {
			public static bool Prefix(ref int __result) {
				__result = 1;
				return false;
			}
		}

		public class TestingAttribute : System.Attribute { }
		public class BonusContentAttribute : System.Attribute { }

	}
}
