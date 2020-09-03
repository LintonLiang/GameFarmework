using System.Collections;
using UnityEngine;

//每一帧 回调
public delegate void LoaderProgrecess(string bundle, float progress);

//load 完成时 回调
public delegate void LoadFinish(string bundle);


public class ABLoader{

    private string bundleName;
    private string commonBundlePath;
    private WWW commonLoader;

    private float commonResLoaderProcess;

    public LoaderProgrecess LoaderProgressing;
    private LoadFinish LoadFinished;


    private ABResLoader abResloader;
    public ABLoader(LoaderProgrecess lp, LoadFinish lf)
    {
        commonBundlePath = "";
        bundleName = "";

        commonResLoaderProcess = 0;

        LoaderProgressing = lp;
        LoadFinished = lf;
        abResloader = null;
    }
    /// <summary>
    /// 要求上层传递  完整的路径
    /// </summary>
    /// <param name="path"></param>
    public void LoadResources(string path)
    {

        commonBundlePath = path;
    }
    public void SetBundleName(string name)
    {

        bundleName = name;
    }
    /// <summary>
    /// 协程加载
    /// </summary>
    /// <returns></returns>
    public IEnumerator CommonLoad() {
        commonLoader = new WWW(commonBundlePath);

        while (!commonLoader.isDone)
        {
            commonResLoaderProcess = commonLoader.progress;

            if (LoaderProgressing != null)
            {
                LoaderProgressing(bundleName, commonResLoaderProcess);
                
            }
            yield return commonLoader.progress;

        }

        if (commonResLoaderProcess >=1.0f)//表示已经加载完成
        {
            if (LoaderProgressing != null)
                LoaderProgressing(bundleName, commonResLoaderProcess);
            if (LoadFinished != null)
                LoadFinished(bundleName);
            abResloader = new ABResLoader(commonLoader.assetBundle);
        }
        else
        {
            Debug.LogError("load bundle error == " + bundleName);
        }
        commonLoader = null;

    }

    //Debug

    public void DebugerLoader()
    {

        if (commonLoader !=null)
        {
            abResloader.DebugAllRes();
        }
    }

    #region 下层提供功能
    //获取单个资源
    public UnityEngine.Object GetResources(string name) {
        
        if (abResloader != null)
        {
            return abResloader[name];
        }
        else
        {
            return null;
        }
    }
    public UnityEngine.Object[] GetMutiRes(string name)
    {
        if (abResloader !=null)
        {
            return abResloader.LoadResources(name);
        }
        else
        {
            return null;
        }

        
    }
    public void Unload(UnityEngine.Object resObj)
    {
        if (abResloader != null)
        {
            abResloader.Unload(resObj);
        }

    }
    /// <summary>
    /// 卸载 AssetBundle包
    /// </summary>
    /// <param name="resObj"></param>
    public void UnloadRes(UnityEngine.Object resObj)
    {
        if (abResloader != null)
        {
            abResloader.UnloadRes(resObj);
        }
       
    }
    //释放
    public void Dispose() {
        if (abResloader != null)
        {
            abResloader.Dispose();

        }
    }

    #endregion
}
