using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBase : MonoBase {

    public override void ProcessEvent(MessageBase tempMsg)
    {
        //throw new System.NotImplementedException(); 
    }

    public void RegistSelf(MonoBase mono, params ushort[] msgs)
    {
        NPCManager.Instance.RegistMsg(mono, msgs);
    }
    public void UnregistSelf(MonoBase mono, params ushort[] msgs)
    {
        NPCManager.Instance.UnRegistMsg(mono, msgs);
    }
    public ushort[] msgIds;

    private void OnDestroy()
    {
        if (msgIds != null)
        {
            UnregistSelf(this, msgIds);
        }
    }
    public void SendMsg(MessageBase message)
    {
        NPCManager.Instance.SendMsg(message);

    }
}
