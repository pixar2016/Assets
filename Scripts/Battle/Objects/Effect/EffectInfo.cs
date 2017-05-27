using System;
using System.Collections.Generic;
using UnityEngine;

//特效基类
public class EffectInfo
{
    //索引ID
    public int Id;
    //特效ID
    public int effectId;
    //特效名称
    public string effectName;
    //特效位置
    public Vector3 pos;
    //特效角度
    public Vector3 angle;
    
    public EffectInfo(int effectIndexId, int effId)
    {
        Id = effectIndexId;
        effectId = effId;
        effectName = "arrow";
    }

    public Vector3 GetPosition()
    {
        return pos;
    }
    public void SetPosition(float x, float y, float z)
    {
        Vector3 temp = pos;
        temp.x = x;
        temp.y = y;
        temp.z = z;
        pos = temp;
    }
    public Vector3 GetEulerAngles()
    {
        return angle;
    }
    public virtual void Update()
    {
        
    }

    public virtual void Release()
    {

    }
}

