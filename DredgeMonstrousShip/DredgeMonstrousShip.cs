using HarmonyLib;
using System;
using UnityEngine;
using Winch.Core;

namespace DredgeMonstrousShip;

[HarmonyPatch]
public class DredgeMonstrousShip : MonoBehaviour
{
    public static DredgeMonstrousShip Instance { get; private set; }

    private void Awake()
    {
        Instance = this;

        new Harmony(nameof(DredgeMonstrousShip)).PatchAll();
    }

    [HarmonyPostfix]
    [HarmonyPatch(typeof(Player), nameof(Player.Start))]
    public static void Player_Start(Player __instance)
    {
        try
        {
            // Update graphics here!
            var monsterData = GameManager.Instance.MonsterManager.GaleCliffsMonsterManager.monsterData;
            var monsterGraphics = monsterData.prefab.transform.Find("PivotTarget");
            var monsterBody = GameObject.Instantiate(monsterGraphics);
            monsterBody.name = "MonsterPivotTarget";
            monsterBody.parent = __instance.transform;
            monsterBody.localPosition = Vector3.forward;
            monsterBody.localRotation = Quaternion.identity;
            foreach (var collider in monsterBody.GetComponentsInChildren<Collider>())
            {
                collider.enabled = false;
            }
            monsterBody.Find("GaleCliffMonster").GetComponent<Animator>().SetBool("detectsPlayer", true); // Open eye

            // Remove boat graphics
            __instance.transform.Find("Boat1").gameObject.SetActive(false);
            __instance.transform.Find("Boat2").gameObject.SetActive(false);
            __instance.transform.Find("Boat3").gameObject.SetActive(false);
            __instance.transform.Find("Boat4").gameObject.SetActive(false);
        }
        catch (Exception e)
        {
            WinchCore.Log.Error(e);
        }
    }
}
