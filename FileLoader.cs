using System;
using UnityEngine;

namespace CustomHelmetBoilerplate;

public static class FileLoader
{
    public static GameObject GetAssetBundleAsGameObject(string path, string name)
    {
        Debug.Log("AssetBundleLoader: Attempting to load AssetBundle...");
        AssetBundle assetBundle = null;
        try
        {
            assetBundle = AssetBundle.LoadFromFile(path);
            Debug.Log("AssetBundleLoader: Success.");
        }
        catch (Exception ex)
        {
            Debug.Log("AssetBundleLoader: Couldn't load AssetBundle from path: '" + path + "'. Exception details: e: " + ex.Message);
        }
        Debug.Log("AssetBundleLoader: Attempting to retrieve: '" + name + "' as type: 'GameObject'.");
        GameObject gameObject;
        try
        {
            var @object = assetBundle.LoadAsset(name, typeof(GameObject));
            Debug.Log("AssetBundleLoader: Success.");
            assetBundle.Unload(false);
            gameObject = (GameObject)@object;
        }
        catch (Exception ex2)
        {
            Debug.Log("AssetBundleLoader: Couldn't retrieve GameObject from AssetBundle.");
            gameObject = null;
        }
        return gameObject;
    }
}