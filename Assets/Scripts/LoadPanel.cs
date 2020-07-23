using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LoadPanel : UIBase {

    // Use this for initialization
    void Start() {
        msgIds = new ushort[] {
            (ushort)ManagerID.UIManager
        };
        RegistSelf(this, msgIds);

        //用空间换取方便和运行时性能，不需要再去查找
        UIManager.Instance.GetGameObject("LightOn").GetComponent<UIBehaviour>().AddButtonListener(ButtonClick);



    }
    public override void ProcessEvent(MessageBase tempMsg)
    {
        switch (tempMsg.msgId)
        {
            case (ushort)ManagerID.UIManager:

                Debug.Log("接受到了");
                break;
            default:
                break;
        }
    }
    public void ButtonClick()
    {
        MessageBase temp = new MessageBase((ushort)ManagerID.UIManager);
        SendMsg(temp);
    }
	// Update is called once per frame
	void Update () {
		
	}
}
