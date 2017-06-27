using System;
using System.Collections.Generic;
using UnityEngine;

public class BezierEffectInfo : EffectInfo
{
    CharacterInfo charInfo;
    int curPathNum;
    public float speed;
    public int triggerGroupId;
    public Bezier bezierPath;
    public BezierEffectInfo(int effectIndexId, int effId, CharacterInfo _charInfo, CharacterInfo _targetInfo, float _speed, int _triggerGroupId)
        : base(effectIndexId, effId)
    {
        speed = _speed;
        charInfo = _charInfo;
        angle = Vector3.zero;
        triggerGroupId = _triggerGroupId;
        bezierPath = new Bezier();
        bezierPath.AddPath(_charInfo.GetPosition(), _targetInfo.GetPosition(), 60);
        curPathNum = 0;
        SetPosition(bezierPath.GetPath(curPathNum));
    }

    public override void Update()
    {
        if (curPathNum < bezierPath.GetCount() - 1)
        {
            Vector3 _pos = bezierPath.GetPath(curPathNum);
            Vector3 _targetPos = bezierPath.GetPath(curPathNum + 1);
            angle.z = angle_360(Vector3.left, _targetPos - _pos);
            this.pos = _targetPos;
            curPathNum++;
        }
        else
        {
            this.charInfo.eventDispatcher.Broadcast("BulletReach", triggerGroupId, bezierPath.GetPath(curPathNum));
            EntityManager.getInstance().RemoveEffect(this.Id);
        }
    }
    private float angle_360(Vector3 _from, Vector3 _to)
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

