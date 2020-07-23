using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
        AssetBundle ab = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/cube");
        GameObject[] gos = ab.LoadAllAssets<GameObject>();
        foreach (GameObject item in gos)
        {

            
            GameObject go = Instantiate(item);
            go.name = go.name.Replace("(Clone)", "");
        }

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
