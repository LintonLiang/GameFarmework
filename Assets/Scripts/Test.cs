using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
        AssetBundle depe = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/AssetBundle/scenes/common.ld");
        //depe.LoadAsset("red.mat");
        AssetBundle ab = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/AssetBundle/scenes/scenetwo.ld");
        GameObject gos = ab.LoadAsset<GameObject>("Sphere");
        //foreach (GameObject item in gos)
        //{

            
        //    GameObject go = Instantiate(item);
        //    go.name = go.name.Replace("(Clone)", "");
        //}

        GameObject go = Instantiate(gos);
        go.name = go.name.Replace("(Clone)", "");


    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
