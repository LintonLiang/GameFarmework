using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageBase
{
    //ushort 表示  65535个消息  占两个字节  int占用四个字节
    public ushort msgId;
    public ManagerID GetManager() {

        int tempId = msgId / FrameTools.MsgSpan;

        return (ManagerID)(tempId * FrameTools.MsgSpan);
    }
    public MessageBase(ushort tempMsg) {
        msgId = tempMsg;
    }
}
