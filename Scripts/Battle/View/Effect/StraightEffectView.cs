
using System.Collections.Generic;
using UnityEngine;

public class StraightEffectView : EffectView
{
    public StraightEffectInfo effectInfo;

    public CharacterInfo charInfo;
    public CharacterInfo targetInfo;
    public float speed;
    public Vector3 angle;
    public StraightEffectView(StraightEffectInfo _effectInfo)
    {
        Id = _effectInfo.Id;
        effectInfo = _effectInfo;
        charInfo = _effectInfo.charInfo;
        targetInfo = _effectInfo.targetInfo;
        speed = _effectInfo.speed;
        angle = Vector3.zero;
    }

    public override void InitCom()
    {
        InitAnimate(effectObj, effectInfo.effectName, effectInfo.GetWidth());
        InitPos();
    }

    private void InitPos()
    {
        effectObj.transform.position = charInfo.GetBulletPos();
        effectObj.transform.eulerAngles = angle;
    }

    public override void Update()
    {
        Vector3 targetPos = targetInfo.GetPosition();
        Vector3 pos = effectObj.transform.position;
        if (BattleUtils.Distance2(pos, targetPos) < 10)
        {
            effectInfo.EndShow(pos, targetInfo);
        }
        else
        {
            angle.z = angle_360(Vector3.left, targetPos - pos);
            pos = Vector3.MoveTowards(pos, targetPos, speed * Time.deltaTime);
            effectObj.transform.position = pos;
            effectObj.transform.eulerAngles = angle;
        }
    }
}
