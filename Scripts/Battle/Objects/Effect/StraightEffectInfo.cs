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
        charInfo = _charInfo;
        targetInfo = _targetInfo;
        speed = _speed;
        triggerGroupId = _triggerGroupId;
    }

    public void EndShow(Vector3 targetPos, CharacterInfo targetInfo)
    {
        this.charInfo.eventDispatcher.Broadcast("BulletReach", triggerGroupId, targetPos, targetInfo);
        EntityManager.getInstance().RemoveEffect(this.Id);
    }

}

