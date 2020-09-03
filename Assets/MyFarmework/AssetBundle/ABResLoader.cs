using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ABResLoader : IDisposable
{
    private AssetBundle ABRes;
    public ABResLoader(AssetBundle tmpBundle)
    {
        ABRes = tmpBundle;  
    }
    /// <summary>
    /// 加载单个 资源
    /// </summary>
    /// <param name="resName"></param>
    /// <returns></returns>
    public UnityEngine.Object this[string resName] {

        get {
            if (this.ABRes == null || !this.ABRes.Contains(resName))
            {
                Debug.Log("资源不存在");
                return null;
            }
            return ABRes.LoadAsset(resName);
        }
    }

    /// <summary>
    /// 加载多个  资源
    /// </summary>
    /// <param name="resName"></param>
    /// <returns></returns>
    public UnityEngine.Object[] LoadResources(string resName)
    {
        if (this.ABRes == null || !this.ABRes.Contains(resName))
        {
            Debug.Log("资源不存在");
            return null;
        }
        return ABRes.LoadAssetWithSubAssets(resName);


    }

    /// <summary>
    /// 卸载单个资源
    /// </summary>
    /// <param name="resObj"></param>
    public void Unload(UnityEngine.Object resObj) {
        Resources.UnloadAsset(resObj);
        
    }
    /// <summary>
    /// 卸载 AssetBundle包
    /// </summary>
    /// <param name="resObj"></param>
    public void UnloadRes(UnityEngine.Object resObj)
    {
        ABRes.Unload(false);
    }
    public void Dispose()
    {
        if (this.ABRes == null)
            return;
        ABRes.Unload(false);
    }

    public void DebugAllRes()
    {
        string[] tmpAssetNames = ABRes.GetAllAssetNames();
        for (int i = 0; i < tmpAssetNames.Length; i++)
        {
            Debug.Log("ABRes Contain  Asset Name  =====>" + tmpAssetNames[i]);
        }
    }
}
