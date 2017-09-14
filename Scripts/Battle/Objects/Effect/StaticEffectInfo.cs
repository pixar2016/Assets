using System;
using System.Collections.Generic;
using UnityEngine;

public class StaticEffectInfo : EffectInfo
{
    ////特效已产生的时间
    //public float effectTime;
    ////特效播放一次时间
    //public float effectMaxTime;
    ////特效是否循环播放 true-是 false-否
    //public bool loop;
    public Vector3 effPos;
    public StaticEffectInfo(int effectIndexId, int effId, Vector3 _pos)
        : base(effectIndexId, effId)
    {
        effPos = _pos;
        //effectTime = 0;
        //effectMaxTime = 1.0f;
        //loop = effectData._loop == 1 ? true : false;
    }

    public override void Update()
    {
        //if (loop)
        //{
        //    return;
        //}
        //effectTime += Time.deltaTime;
        //if (effectTime >= effectMaxTime)
        //{
        //    effectTime = 0;
        //    EntityManager.getInstance().RemoveEffect(this.Id);
        //}
    }
}

