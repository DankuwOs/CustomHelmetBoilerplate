using UnityEngine;
using VTOLAPI;

namespace CustomHelmetBoilerplate;

public class PlayerSpawnPatch
{
    public static void Postfix(Actor __instance)
    {
        var isPlayer = __instance.isPlayer;
        if (isPlayer)
        {
            var gameObject = Object.Instantiate(Main.helmet);
            var componentInChildren = gameObject.GetComponentInChildren<Animator>(true);
            var componentInChildren2 = __instance.GetComponentInChildren<HelmetController>(true);
            var flag = componentInChildren != null;
            if (flag)
            {
                componentInChildren2.animator = componentInChildren;
            }
            Main.DisableMesh(componentInChildren2.gameObject);
            gameObject.transform.SetParent(componentInChildren2.transform);
            gameObject.transform.localPosition = Main.HelmetPostion;
            gameObject.transform.localEulerAngles = Main.HelmetEuler;
            gameObject.transform.localScale = Main.HelmetScale;
            var flag2 = VTAPI.GetPlayersVehicleEnum() == VTOLVehicles.F45A || VTAPI.GetPlayersVehicleEnum() == VTOLVehicles.AH94;
            if (flag2)
            {
                foreach (var gameObject2 in Main.GetChildrenWithName(__instance.gameObject, "f45Helmet"))
                {
                    Main.DisableMesh(gameObject2);
                }
                foreach (var gameObject3 in Main.GetChildrenWithName(__instance.gameObject, "headSphere"))
                {
                    gameObject3.GetComponent<MeshRenderer>().enabled = true;
                }
            }
        }
    }
}