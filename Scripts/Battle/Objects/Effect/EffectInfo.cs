using System;
using System.Collections.Generic;
using UnityEngine;

//特效基类
public class EffectInfo
{
    public EffectView effectView;
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
    //标记 0-正常 1-添加 2-删除
    public int dirtySign;

    public D_Effect effectData;

    public float effectLength = 0;
    
    public EffectInfo(int effectIndexId, int effId)
    {
        Id = effectIndexId;
        effectId = effId;
        effectData = J_Effect.GetData(effId);
        effectName = effectData._model;
        dirtySign = 0;
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
        temp.z = y / 20f;
        pos = temp;
    }

    public void SetPosition(Vector3 _pos)
    {
        _pos.z = _pos.y / 20f;
        pos = _pos;
    }

    //将位置更新到View层
    public void UpdatePositionToView()
    {
        this.effectView.SetPosition(pos);
    }
    //将角度更新到View层
    public void UpdateRotationToView()
    {
        this.effectView.SetRotation(angle);
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

    //设置标记，true-移除 false-添加
    public void SetDirtySign(bool isDirty)
    {
        if (isDirty)
        {
            dirtySign--;
        }
        else
        {
            dirtySign++;
        }
    }

    protected float angle_360(Vector3 _from, Vector3 _to)
    {
        Vector3 cross = Vector3.Cross(_from, _to);
        float angle;
        if (cross.z > 0)
            angle = Vector3.Angle(_from, _to);
        else
            angle = 360 - Vector3.Angle(_from, _to);
        return angle;
    }
}

