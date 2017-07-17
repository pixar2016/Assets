using System;
using System.Collections.Generic;
using UnityEngine;

public class StraightEffectInfo : EffectInfo
{
    public float speed;
    public CharacterInfo charInfo;
    public CharacterInfo targetInfo;
    public int triggerGroupId;
    public StraightEffectInfo(int _effectIndexId, int _effId, CharacterInfo _charInfo, CharacterInfo _targetInfo, float _speed, int _triggerGroupId)
        : base(_effectIndexId, _effId)
    {
        this.charInfo = _charInfo;
        this.targetInfo = _targetInfo;
        this.speed = _speed;
        pos = charInfo.GetPosition();
        angle = Vector3.zero;
        triggerGroupId = _triggerGroupId;
    }

    public override void Update()
    {
        Vector3 targetPos = targetInfo.GetPosition();
        //Debug.Log(Vector3.Distance(pos, targetPos));
        if (BattleUtils.Distance2(pos, targetPos) < 10)
        {
            Debug.Log("BulletReach");
            this.charInfo.eventDispatcher.Broadcast("BulletReach", triggerGroupId, targetPos, targetInfo);
            EntityManager.getInstance().RemoveEffect(this.Id);
        }
        else
        {
            angle.z = angle_360(Vector3.left, targetPos - pos);
            pos = Vector3.MoveTowards(pos, targetPos, speed * Time.deltaTime);
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

