using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : ManagerBase {
    public static UIManager Instance = null;


    private void Awake()
    {
        Instance = this;    
    }

    public GameObject GetGameObject(string name)
    {
        if (sonMenbers.ContainsKey(name))
        {
            return sonMenbers[name];
        }
        else
        {
            return null;
        }
        
    }
    public void SendMsg(MessageBase msg)
    {
        if (msg.GetManager() == ManagerID.UIManager)//是本模块的，自己处理
        {
            ProcessEvent(msg);
        }
        else//如果不是本模块的，那就交给Msg
        {
            MsgCenter.Instance.SendToMsg(msg);
        }
    }
    //规定了  开发方式  消耗内存  换取  速度和方便
    public Dictionary<string, GameObject> sonMenbers = new Dictionary<string, GameObject>();
    public void RegistGameObject(string name,GameObject obj) {
        if (!sonMenbers.ContainsKey(name))
        {
            sonMenbers.Add(name, obj);

        }

    }

    public void UnRegistGameObject(string name)
    {
        if (sonMenbers.ContainsKey(name))
        {
            sonMenbers.Remove(name);   
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
