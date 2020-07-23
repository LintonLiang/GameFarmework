using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using System;

public class AssetbundleBuildTool
{
    /// <summary>
    /// 打出的ab包保存的位置
    /// </summary>
    static string ABSavePath = Application.streamingAssetsPath + "/AssetBundle";

    [MenuItem("Tools/AssetBundle/BuildAssetBundle")]
    public static void BuildAssetBundle()
    {
        
        if ( !Directory.Exists(ABSavePath))
        {
            Directory.CreateDirectory(ABSavePath);
        }
        BuildPipeline.BuildAssetBundles(ABSavePath, BuildAssetBundleOptions.None,BuildTarget.StandaloneWindows64);
    }
    /// <summary>
    /// 给所有资源批量加上AssetBundle的tag
    /// </summary>
    [MenuItem("Tools/AssetBundle/MarkAssetBundle")]
    public static void MarkAssetBundle()
    {
        AssetDatabase.RemoveUnusedAssetBundleNames();
        string path = Application.dataPath + "/Artwork";
        DirectoryInfo directoryInfo = new DirectoryInfo(path);
        FileSystemInfo[] fileSystemInfos = directoryInfo.GetFileSystemInfos();

        for (int i = 0; i < fileSystemInfos.Length; i++)
        {
            FileSystemInfo tempFile = fileSystemInfos[i];
            if (tempFile is DirectoryInfo)
            {
                string tempPath = path + "/" + tempFile.Name;
                SceneOverView(tempPath);
            }
            
        }
        AssetDatabase.Refresh();
    }
    /// <summary>
    /// 删除之前打出的AB包
    /// </summary>
    [MenuItem("Tools/AssetBundle/ClearAssetBundle")]
    public static void ClearAssetBundle() {

        if (!Directory.Exists(ABSavePath))
        {
            Directory.CreateDirectory(ABSavePath);
        }
        DeleteDir(ABSavePath);
        
    }
    /// <summary>
    /// 删除指定路径下的文件及文件夹
    /// </summary>
    /// <param name="path"></param>
    public static void DeleteDir(string path){

        try
        {
            System.IO.DirectoryInfo fileInfo = new DirectoryInfo(path);
            fileInfo.Attributes = FileAttributes.Normal & FileAttributes.Directory;

            //去除文件的只读属性
            System.IO.File.SetAttributes(ABSavePath, System.IO.FileAttributes.Normal);

            //判断文件夹是否还存在
            if (Directory.Exists(path))
            {
                foreach (string f in Directory.GetFileSystemEntries(path))
                {
                    if (File.Exists(f))
                    {
                        //如果有子文件删除文件
                        File.Delete(f);

                    }
                    else
                    {
                        //循环递归删除子文件夹
                        DeleteDir(f);
                    }
                }

                //删除空文件夹
                Directory.Delete(path);
                Console.WriteLine(path);
            }

        }
        catch (Exception ex) // 异常处理
        {
            Console.WriteLine(ex.Message.ToString());// 异常信息
        }

    }
    /// <summary>
    /// 对整个场景进行 遍历
    /// </summary>
    /// <param name="scenePath"></param>
    public static void SceneOverView(string scenePath)
    {
        string textFileName = "/Record.txt";
        string tempPath = scenePath + textFileName;

        FileStream fs = new FileStream(tempPath,FileMode.OpenOrCreate);
        StreamWriter sw = new StreamWriter(fs);
        //存储 对应关系
        Dictionary<string, string> reaDict = new Dictionary<string, string>();

        ChangeHead(scenePath,reaDict);
        foreach (var item in reaDict)
        {
            sw.WriteLine(item.Key + "=========>" + item.Value);
        }

        sw.Close();
        fs.Close();
        
    }

    /// <summary>
    /// 截取 相对路径    
    /// </summary>
    /// <param name="fullPath"></param>
    /// <param name="theWriter">文本记录</param>
    public static void ChangeHead(string fullPath, Dictionary<string, string> theWriter) {
        
        int tempCount = fullPath.IndexOf("Assets");
        int tempLength = fullPath.Length;


        string repalcePath = fullPath.Substring(tempCount, tempLength - tempCount);

        DirectoryInfo dir = new DirectoryInfo(repalcePath);

        if (dir!=null)
        {
            ListFiles(dir, repalcePath, theWriter);
        }
        else
        {
            Debug.Log("This path is not exit");
        }

    }
    /// <summary>
    /// 遍历fullpath下的每一个文件
    /// </summary>
    public static void ListFiles(FileSystemInfo info,string replacePath,Dictionary<string,string> theWriter)
    {
        if (!info.Exists)
        {
            Debug.Log("is not exit");

        }
        DirectoryInfo dir = info as DirectoryInfo;
        
        
        FileSystemInfo[] files = dir.GetFileSystemInfos();

        for (int i = 0; i < files.Length; i++)
        {
            FileInfo file = files[i] as FileInfo;
            //对于文件的操作
            if (file != null)
            {
                ChangeMark(file, replacePath, theWriter);
            }
            else  //对于目录操作
            {
                ListFiles(files[i], replacePath, theWriter);
            }
        }


    }
    public static string FixedWindowPath(string path) {

        return path.Replace("\\", "/");
    }

    /// <summary>
    /// 计算出AssetBundle的tag
    /// </summary>
    /// <param name="file"></param>
    /// <param name="replacePath"></param>
    /// <returns></returns>
    public static string GetBundlePath(FileInfo file, string replacePath)
    {
        string tempPath = FixedWindowPath(file.FullName);
        Debug.Log("tempPath == " + tempPath);

        int assetCount = tempPath.IndexOf(replacePath);
        assetCount += replacePath.Length + 1;
        int nameCount = tempPath.LastIndexOf(file.Name);
        int tempCount = replacePath.LastIndexOf("/");

        string sceneHead = replacePath.Substring(tempCount + 1, replacePath.Length - tempCount - 1);

        int tempLength = nameCount - assetCount;

        if (tempLength >0)
        {
            string subString = tempPath.Substring(assetCount, tempPath.Length - assetCount);

            string[] result = subString.Split("/".ToCharArray());
            return sceneHead + "/" + result[0];
        }
        else
        {
            return sceneHead;
        }

    }
    public static void ChangeAssetMark(FileInfo tempFile, string markStr, Dictionary<string, string> theWriter)
    {
        string fullPath = tempFile.FullName;
        int assetCount = fullPath.IndexOf("Assets");
        string assetPath = fullPath.Substring(assetCount, fullPath.Length - assetCount);

        AssetImporter importer = AssetImporter.GetAtPath(assetPath);

        importer.assetBundleName = markStr;
        if (tempFile.Extension==".unity")
        {
            importer.assetBundleVariant = "u3d";
        }
        else
        {
            importer.assetBundleVariant = "ld";
        }

        string modleName = "";
        string[] subMark = markStr.Split("/".ToCharArray());
        if (subMark.Length>1)
        {
            modleName = subMark[1];
        }
        else
        {
            modleName = markStr;
        }

        string modlePath = markStr.ToLower() + "." + importer.assetBundleVariant;

        if (!theWriter.ContainsKey(modleName))
        {
            theWriter.Add(modleName, modlePath);
        }
    }

    public static void ChangeMark(FileInfo tempFile,string replacePath, Dictionary<string, string> theWriter)
    {
        if (tempFile.Extension == ".meta")
        {
            return;
        }
        string markStr = GetBundlePath(tempFile, replacePath);
        ChangeAssetMark(tempFile, markStr, theWriter);
        Debug.Log("MarkString  ===>" + markStr);
    }
}