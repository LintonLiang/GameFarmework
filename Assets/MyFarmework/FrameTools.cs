﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//192.168.0
public enum ManagerID
{
    GameManager = 0,
    UIManager = FrameTools.MsgSpan,
    AudioManager = FrameTools.MsgSpan * 2,
    NPCManager = FrameTools.MsgSpan * 3,
    CharactorManager = FrameTools.MsgSpan * 4,
    AssetManager = FrameTools.MsgSpan * 5,
    NetManager = FrameTools.MsgSpan * 6

}
public class FrameTools {

    public const int MsgSpan = 3000;

    
}
