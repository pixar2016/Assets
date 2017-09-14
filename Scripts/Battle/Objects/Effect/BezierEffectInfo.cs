using System;
using System.Collections.Generic;
using UnityEngine;

public class BezierEffectInfo : EffectInfo
{
    CharacterInfo charInfo;
    public Vector3 startPos;
    public Vector3 endPos;
    public float speed;
    public int triggerGroupId;

    public BezierEffectInfo(int effectIndexId, int effId, CharacterInfo _charInfo, CharacterInfo _targetInfo, float _speed, int _triggerGroupId)
        : base(effectIndexId, effId)
    {
        charInfo = _charInfo;
        speed = _speed;
        triggerGroupId = _triggerGroupId;
        startPos = _charInfo.GetBulletPos();
        endPos = _targetInfo.GetPosition();
    }

    public void EndShow(Vector3 endPos)
    {
        this.charInfo.eventDispatcher.Broadcast("BulletReach", triggerGroupId, endPos);
        EntityManager.getInstance().RemoveEffect(this.Id);
    }
}

