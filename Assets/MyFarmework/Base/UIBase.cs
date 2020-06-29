using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 向UIManager注册mono 和 msgid，一般挂在在Panel根目录上（需要向其他模块或者脚本通讯的）
/// </summary>
public class UIBase : MonoBase {
    public override void ProcessEvent(MessageBase tempMsg)
    {
        //throw new System.NotImplementedException(); 
    }

    public void RegistSelf(MonoBase mono, params ushort[] msgs)
    {
        UIManager.Instance.RegistMsg(mono, msgs);
    }
    public void UnregistSelf(MonoBase mono, params ushort[] msgs) {
        UIManager.Instance.UnRegistMsg(mono, msgs);
    }
    public ushort[] msgIds;

    private void OnDestroy()
    {
        if (msgIds!=null)
        {
            UnregistSelf(this,msgIds);
        }
    }

}
