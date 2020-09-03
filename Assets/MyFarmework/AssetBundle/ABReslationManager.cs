using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ABRelationManager
{
    List<string> depedenceBundle;

    List<string> referBundle;

    ABLoader assetLoader;

    bool IsLoadFinish;

    public bool IsBundleLoadFinish()
    {
        return true;
    }
    public void BundleLoadFinish(string bundlName)
    {
        IsLoadFinish = true;

    }
    public void Initial(LoaderProgrecess progrecess)
    {
        IsLoadFinish = false;

        assetLoader = new ABLoader(progrecess, BundleLoadFinish);
    }
    public ABRelationManager()
    {
        depedenceBundle = new List<string>();
        referBundle = new List<string>();
    }

    public void AddRefference(string bundleName) {
         referBundle.Add(bundleName);
    }
    public List<string> GetRefference() {
        return referBundle;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="bundleName"></param>
    /// <returns>是否将包释放掉</returns>
    public bool RemoveRefference(string bundleName) {
        for (int i = 0; i < referBundle.Count; i++)
        {
            if (bundleName.Equals(referBundle[i]))
            {
                referBundle.RemoveAt(i);
            }
        }
        if (referBundle.Count <=0)
        {
            Dispose();
            return true;
        }
        return false;

    }

    public void SetDepedences(string[] depence)
    {
        if (depence.Length >0)
        {
            depedenceBundle.AddRange(depence);
        }
    }
    public List<string> GetDepedence()
    {
        return depedenceBundle;
    }

    public void RemoveDepence(string bundleName)
    {
        for (int i = 0; i < depedenceBundle.Count; i++)
        {
            if (bundleName.Equals(depedenceBundle[i]))
            {
                depedenceBundle.RemoveAt(i);
            }
        }
    }
    #region 由下层提供API

    public void DebuggerAsset()
    {
        if (assetLoader !=null)
        {
            assetLoader.DebugerLoader();
        }
        else
        {
            Debug.Log("asset load is null");

        }
    }
    public IEnumerator LoadAssetBundle()
    {
        yield return assetLoader.CommonLoad();
    }
    public void Dispose() {

        assetLoader.Dispose();
    }

    public UnityEngine.Object GetSingleResource(string bundleName)
    {
        return assetLoader.GetResources(bundleName);

    }
    public UnityEngine.Object[] GetMutiResources(string bundleName)
    {

        return assetLoader.GetMutiRes(bundleName);
    }
    #endregion
}

