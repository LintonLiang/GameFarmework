using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventNode
{
    //当前数据
    public MonoBase data;
    //下一个节点
    public EventNode NextNode;

    public EventNode(MonoBase tempMono)
    {
        this.data = tempMono;
        this.NextNode = null;
    }
}
/// <summary>
/// 主要负责消息的存储和处理
/// </summary>
public class ManagerBase : MonoBase{

    //
    public Dictionary<ushort, EventNode> eventTree = new Dictionary<ushort, EventNode>();

    public override void ProcessEvent(MessageBase tempMsg)
    {
        if (!eventTree.ContainsKey(tempMsg.msgId))
        {
            Debug.LogError("msg not contain msgid == " + tempMsg.msgId);
            Debug.LogError("msg Manager ==  " + tempMsg.GetManager());
        }
        else
        {
            EventNode temp = eventTree[tempMsg.msgId];
            do
            {
                temp.data.ProcessEvent(tempMsg);
            } while (temp !=null);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="mono">要注册的脚本</param>
    /// <param name="msgs">脚本  可以注册多个msg</param>
    public void RegistMsg(MonoBase mono, params ushort[] msgs)
    {
        for (int i = 0; i < msgs.Length; i++)
        {
            EventNode temp = new EventNode(mono);
            RegistMsg(msgs[i], temp);
        }
    }
    public void RegistMsg(ushort id, EventNode node)
    {
        //数据链路里面没有这个消息的ID
        if (!eventTree.ContainsKey(id))
        {
            eventTree.Add(id, node);
        }
        else
        {
            EventNode temp = eventTree[id];
            //找到最后一个车厢
            while (temp.NextNode != null) {
                temp = temp.NextNode;
            }

            temp.NextNode = node;
        }

    }

    /// <summary>
    /// 去掉脚本的若干个消息
    /// </summary>
    /// <param name="mono"></param>
    /// <param name="msgs"></param>
    public void UnRegistMsg(MonoBase mono, params ushort[] msgs)
    {
        for (int i = 0; i < msgs.Length; i++)
        {
            UnRegistMsg(msgs[i], mono);
        }

    }

    /// <summary>
    /// 去掉一个消息链表
    /// </summary>
    /// <param name="id"></param>
    /// <param name="node"></param>
    public void UnRegistMsg(ushort id, MonoBase node) {
        if (!eventTree.ContainsKey(id))
        {
            Debug.LogWarning("Not  contain  id  ==" + id);
            return;

        }
        else
        {
            EventNode temp = eventTree[id];
            if (temp.data == node)//去掉头部的节点，包含两种情况
            {
                EventNode header = temp;

                //已经存在这个 消息
                if (header.NextNode !=null)
                {
                    //header.data = temp.NextNode.data;
                    //header.NextNode = temp.NextNode.NextNode;
                    eventTree[id] = temp.NextNode;
                }
                else//只有一个节点的情况
                {
                    eventTree.Remove(id);
                }
            }
            else//去掉尾部和中间的节点
            {
                while (temp.NextNode !=null && temp.NextNode.data == node)
                {
                    temp = temp.NextNode;
                }//找到需要修改的节点

                if (temp.NextNode.NextNode !=null)
                {
                    temp.NextNode = temp.NextNode.NextNode;
                }
                else
                {
                    temp.NextNode = null;
                }
            }
        }

    }

}
