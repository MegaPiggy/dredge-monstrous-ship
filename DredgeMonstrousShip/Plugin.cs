using BepInEx;
using HarmonyLib;
using System;
using System.Reflection;
using UnityEngine;

namespace DredgeMonstrousShip
{
	[BepInPlugin("com.xen-42.dredge.monstrousship", "Monstrous Ship", "0.0.1")]
	[BepInProcess("DREDGE.exe")]
	[HarmonyPatch]
	public class Plugin : BaseUnityPlugin
	{
		public static Plugin Instance { get; private set; }

		private void Awake()
		{
			Instance = this;

			// Plugin startup logic
			Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
			Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
		}

		[HarmonyPostfix]
		[HarmonyPatch(typeof(Player), "Start")]
		public static void Player_Start(Player __instance)
		{
			try
			{
				// Update graphics here!
				var monsterManager = GameManager.Instance.MonsterManager.GaleCliffsMonsterManager;
				var monsterDataField = monsterManager.GetType().GetField("monsterData", BindingFlags.NonPublic | BindingFlags.Instance);
				var monsterData = monsterDataField.GetValue(monsterManager) as MonsterData;
				var monsterGraphics = monsterData.prefab.transform.Find("PivotTarget/GaleCliffMonster");
				var monsterBody = GameObject.Instantiate(monsterGraphics);
				monsterBody.transform.parent = __instance.transform;
				monsterBody.transform.localPosition = Vector3.forward;
				monsterBody.transform.localRotation = Quaternion.identity;

				// Remove boat graphics
				__instance.transform.Find("Boat1").gameObject.SetActive(false);
				__instance.transform.Find("Boat2").gameObject.SetActive(false);
				__instance.transform.Find("Boat3").gameObject.SetActive(false);
				__instance.transform.Find("Boat4").gameObject.SetActive(false);
			}
			catch (Exception e)
			{
				Instance.Logger.LogError(e);
			}
		}
	}
}
