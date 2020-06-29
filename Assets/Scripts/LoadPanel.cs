using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPanel : UIBase {

    // Use this for initialization
    void Start() {
        msgIds = new ushort[] {};
        RegistSelf(this, msgIds);

        //用空间换取方便和运行时性能，不需要再去查找
        UIManager.Instance.GetGameObject("LightOn").GetComponent<UIBehaviour>().AddButtonListener(ButtonClick);



    }
    public void ButtonClick()
    {
        Debug.Log("点击了");
    }
	// Update is called once per frame
	void Update () {
		
	}
}
