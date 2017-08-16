
using System.Collections.Generic;
using UnityEngine;

public class ConnectEffectInfo : EffectInfo
{
    //特效已产生的时间
    public float effectTime;
    //特效播放一次时间
    public float effectMaxTime;
    //特效是否循环播放 true-是 false-否
    public bool loop;

    public Vector3 startPos;

    public Vector3 endPos;

    public ConnectEffectInfo(int effectIndexId, int effId, Vector3 _startPos, Vector3 _endPos)
        : base(effectIndexId, effId)
    {
        effectTime = 0;
        effectMaxTime = 1.0f;
        loop = effectData._loop == 1 ? true : false;
        startPos = _startPos;
        endPos = _endPos;
    }
    public override void Update()
    {
        if (loop)
        {
            return;
        }
        effectTime += Time.deltaTime;
        if (effectTime >= effectMaxTime)
        {
            effectTime = 0;
            EntityManager.getInstance().RemoveEffect(this.Id);
        }
    }
}

