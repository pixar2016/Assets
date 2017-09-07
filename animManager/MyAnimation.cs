using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//某个动画对应的所有动作图需要在同一张图中（重要）
public class MyAnimation
{
    //不同的动作对应着不同的动画
    public Dictionary<string, MeshAnimation> actionFrameDict;
    //动画帧所在的大图名字
    public string pictName;

    public float imageWidth;
    public float imageHeight;

    public void AddAnimation(MeshAnimation anim, string animName)
    {
        actionFrameDict.Add(animName, anim);
        pictName = anim.texture;
        imageWidth = anim.imageWidth;
        imageHeight = anim.imageHeight;
    }
    public void AddAnimation(MeshAnimation anim)
    {
        actionFrameDict.Add("normal", anim);
        pictName = anim.texture;
        imageWidth = anim.imageWidth;
        imageHeight = anim.imageHeight;
    }

    public MyAnimation()
    {
        actionFrameDict = new Dictionary<string, MeshAnimation>();
    }

    public MeshAnimation getMeshAnimation(string actionName)
    {
        if (actionFrameDict.ContainsKey(actionName))
        {
            return actionFrameDict[actionName];
        }
        return null;
    }

    public MeshAnimation getMeshAnimation()
    {
        if (actionFrameDict.ContainsKey("normal"))
        {
            return actionFrameDict["normal"];
        }
        return null;
    }
}

