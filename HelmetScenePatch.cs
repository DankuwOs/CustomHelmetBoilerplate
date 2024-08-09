using UnityEngine;

namespace CustomHelmetBoilerplate;

public class HelmetScenePatch
{
    
    public static void Postfix(LoadingSceneHelmet __instance)
    {
        var gameObject = Object.Instantiate(Main.helmet);
        var childWithName = Main.GetChildWithName(__instance.gameObject, "hqh");
        Main.DisableMesh(childWithName);
        gameObject.transform.SetParent(childWithName.transform);
        gameObject.transform.localPosition = Main.HelmetPostion;
        gameObject.transform.localEulerAngles = Main.HelmetEuler;
        gameObject.transform.localScale = Main.HelmetScale;
    }
}