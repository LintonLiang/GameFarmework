using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AssetObj {

    public List<Object> objs;
    public AssetObj(params Object[] tmpObj)
    {
        objs = new List<Object>();
        objs.AddRange(tmpObj);
    }
    public void ReleaseObj()
    {

        for (int i = 0; i < objs.Count; i++)
        {
            Resources.UnloadAsset(objs[i]);
        }
    }

}

public class AssetResObj {

    public Dictionary<string, AssetObj> resObjs;
    public AssetResObj(string name,AssetObj tmp)
    {
        resObjs = new Dictionary<string, AssetObj>();
        resObjs.Add(name, tmp);

    }
    public void AddResObj(string name, AssetObj tmpObj)
    {
        resObjs.Add(name, tmpObj);

    }
    //释放所有
    public void ReleaseAllResObj(string name, AssetObj tmpObj) {
        List<string> keys = new List<string>();

        keys.AddRange(resObjs.Keys);

        for (int i = 0; i < keys.Count; i++)
        {
            ReleaseResObj(keys[i]);
        }
    }
    //释放单个
    public void ReleaseResObj(string name)
    {
        if (resObjs.ContainsKey(name))
        {
            AssetObj tmpObj = resObjs[name];
            tmpObj.ReleaseObj();
        }
        else
        {
            Debug.Log("release  object  name  is  not  exit  ==> " + name);
        }
    }
    public List<Object> GetResObj(string name)
    {
        if (resObjs.ContainsKey(name))
        {
            AssetObj tmpObj = resObjs[name];
            return tmpObj.objs;
        }
        else
        {
            
            return null;
        }
    }
}


/// <summary>
/// 对一个场景中的 所有bundle 包的管理
/// </summary>
public class ABManager  {

    Dictionary<string, ABRelationManager> loadHelper = new Dictionary<string, ABRelationManager>();



    /// <summary>
    /// 表示是否加载了  bundle
    /// </summary>
    /// <param name="bundleName"></param>
    /// <returns></returns>
    public bool IsLoadingAssetBundle(string bundleName)
    {
        if (!loadHelper.ContainsKey(bundleName))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    #region 由下层 提供API
    public void DebugAssetBundle(string bundleName)
    {
        if (loadHelper.ContainsKey(bundleName))
        {
            ABRelationManager loader = loadHelper[bundleName];
            loader.DebuggerAsset();
            
        }
        
    }

    public bool IsLoadingFinish(string bundleName)
    {
        if (loadHelper.ContainsKey(bundleName))
        {
            ABRelationManager loader = loadHelper[bundleName];
            return loader.IsBundleLoadFinish();
        }
        else
        {
            Debug.Log("ABBelation no contain bundle ==>" + bundleName);
            return false;
        }
    }
    public Object GetSingleResources(string bundleName,AssetObj tmpObj)
    {
        return null;
    }

    #endregion
}
