global using static CustomHelmetBoilerplate.Logger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using HarmonyLib;
using ModLoader.Framework;
using ModLoader.Framework.Attributes;
using UnityEngine;

namespace CustomHelmetBoilerplate;

[Serializable]
public class HelmetJson
{
	public string prefabName;

	public string bundleName;

	public string harmonyId;

	public float PosX;

	public float PosY;

	public float PosZ;

	public float ScaleX;

	public float ScaleY;

	public float ScaleZ;

	public float EulerX;

	public float EulerY;

	public float EulerZ;
}

[ItemId("danku.customhelmetboilerplate")] // Harmony ID for your mod, make sure this is unique
public class Main : VtolMod
{
	public string ModFolder;

	public const string JsonFileName = "helmet_info.json";

	public static GameObject helmet;

	public static Main instance;

	public static string AssetbundleName = "crusader";

	public static string PrefabName = "CrusaderHelmet.prefab";

	public static Vector3 HelmetPostion = new Vector3(0f, -0.0299f, 0.0024f);

	public static Vector3 HelmetEuler = new Vector3(0f, -180f, 0f);

	public static Vector3 HelmetScale = Vector3.one;

	public static string HarmonyId = "myName.modName";

	private Harmony _harmonyInstance;

	private void Awake()
	{
		ModFolder = Assembly.GetExecutingAssembly().Location;
		Log($"Awake at {ModFolder}");

		instance = this;
		ReadJson();
		_harmonyInstance = new Harmony(HarmonyId);

		_harmonyInstance.Patch(AccessTools.Method(typeof(Actor), nameof(Actor.Start)),
			postfix: AccessTools.Method(typeof(PlayerSpawnPatch), nameof(PlayerSpawnPatch.Postfix)));
		_harmonyInstance.Patch(AccessTools.Method(typeof(LoadingSceneHelmet), nameof(LoadingSceneHelmet.Start)),
			postfix: AccessTools.Method(typeof(HelmetScenePatch), nameof(HelmetScenePatch.Postfix)));

		var text = Path.Combine(instance.ModFolder, AssetbundleName);
		Debug.Log(text);
		helmet = FileLoader.GetAssetBundleAsGameObject(text, PrefabName);
		Debug.Log("Got le " + helmet.name);
	}

	public override void UnLoad()
	{
		// Destroy any objects
		_harmonyInstance.UnpatchSelf();
	}

	private void ReadJson()
	{
		var text = File.ReadAllText(Path.Combine(instance.ModFolder, "helmet_info.json"));
		var helmetJson = JsonUtility.FromJson<HelmetJson>(text);
		AssetbundleName = helmetJson.bundleName;
		PrefabName = helmetJson.prefabName;
		HarmonyId = helmetJson.harmonyId;
		HelmetPostion = new Vector3(helmetJson.PosX, helmetJson.PosY, helmetJson.PosZ);
		HelmetEuler = new Vector3(helmetJson.EulerX, helmetJson.EulerY, helmetJson.EulerZ);
		HelmetScale = new Vector3(helmetJson.ScaleX, helmetJson.ScaleY, helmetJson.ScaleZ);
	}

	public static void SaveIntoJson(HelmetJson json)
	{
		var text = JsonUtility.ToJson(json);
		File.WriteAllText(Path.Combine(instance.ModFolder, "helmet_info.json"), text);
	}

	public static GameObject GetChildWithName(GameObject obj, string name)
	{
		var componentsInChildren = obj.GetComponentsInChildren<Transform>(true);
		foreach (var transform in componentsInChildren)
		{
			var flag = transform.name == name || transform.name.Contains(name + "(clone");
			if (flag)
			{
				return transform.gameObject;
			}
		}

		return null;
	}

	public static List<GameObject> GetChildrenWithName(GameObject obj, string name)
	{
		var componentsInChildren = obj.GetComponentsInChildren<Transform>(true);
		var list = new List<GameObject>();
		foreach (var transform in componentsInChildren)
		{
			var flag = transform.name == name || transform.name.Contains(name + "(clone");
			if (flag)
			{
				list.Add(transform.gameObject);
			}
		}

		return list;
	}

	public static void DisableMesh(GameObject parent)
	{
		var componentsInChildren = parent.GetComponentsInChildren<MeshRenderer>(true);
		foreach (var meshRenderer in componentsInChildren)
		{
			meshRenderer.enabled = false;
		}
	}
}