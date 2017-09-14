
using System.Collections.Generic;
using UnityEngine;

public class BezierEffectView : EffectView
{
    public BezierEffectInfo bezierEffectInfo;
    public int curPathNum;
    public Bezier bezierPath;
    public Vector3 angle;
    public BezierEffectView(BezierEffectInfo _effectInfo)
    {
        Id = _effectInfo.Id;
        bezierEffectInfo = _effectInfo;
        curPathNum = 0;
        angle = Vector3.zero;
        Vector3 startPos = _effectInfo.startPos;
        Vector3 endPos = _effectInfo.endPos;
        int fps = (int)(60 * BattleUtils.Distance2(startPos, endPos) / _effectInfo.speed);
        bezierPath = new Bezier();
        bezierPath.AddPath(startPos, endPos, fps);
    }

    public override void InitCom()
    {
        InitAnimate(effectObj, bezierEffectInfo.effectName, bezierEffectInfo.GetWidth());
        InitPos();
    }

    private void InitPos()
    {
        Vector3 _pos = bezierPath.GetPath(curPathNum);
        Vector3 _targetPos = bezierPath.GetPath(curPathNum + 1);
        effectObj.transform.position = _pos;
        angle.z = angle_360(Vector3.left, _targetPos - _pos);
        effectObj.transform.eulerAngles = angle;
    }

    public override void Update()
    {
        if (curPathNum < bezierPath.GetCount() - 1)
        {
            Vector3 _pos = bezierPath.GetPath(curPathNum);
            Vector3 _targetPos = bezierPath.GetPath(curPathNum + 1);
            angle.z = angle_360(Vector3.left, _targetPos - _pos);
            effectObj.transform.eulerAngles = angle;
            effectObj.transform.position = _targetPos;
            curPathNum++;
        }
        else
        {
            bezierEffectInfo.EndShow(bezierPath.GetPath(curPathNum));
        }
    }
}

