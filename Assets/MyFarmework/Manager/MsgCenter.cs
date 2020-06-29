using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 消息的转发中心，负责向各个manager转发消息
/// </summary>
public class MsgCenter : MonoBehaviour {

    public static MsgCenter Instance = null;

    private void Awake()
    {
        Instance = this;
    }
    // Use this for initialization
    void Start () {
		
	}

    public void SendToMsg(MessageBase tempMsg)
    {
        AnasysisMsg(tempMsg);

    }
    public void AnasysisMsg(MessageBase tempMsg)
    {
        ManagerID tempId = tempMsg.GetManager();
        switch (tempId)
        {
            case ManagerID.GameManager:
                break;
            case ManagerID.UIManager:
                break;
            case ManagerID.AudioManager:
                break;
            case ManagerID.NPCManager:
                break;
            case ManagerID.CharactorManager:
                break;
            case ManagerID.AssetManager:
                break;
            case ManagerID.NetManager:
                break;
            default:
                break;
        }

    }
	// Update is called once per frame
	void Update () {
		
	}
}
