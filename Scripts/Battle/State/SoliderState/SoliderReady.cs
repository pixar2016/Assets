using System;
using System.Collections.Generic;
using UnityEngine;

public class SoliderReady : StateBase
{
    public SoliderInfo soliderInfo;
    public Vector3 curPos;
    public Vector3 targetPos;
    public float speed;
    public SoliderReady(SoliderInfo _soliderInfo)
    {
        soliderInfo = _soliderInfo;
    }

    public void SetParam(StateParam _param)
    {

    }

    public void EnterExcute()
    {
        curPos = soliderInfo.GetPosition();
        targetPos = soliderInfo.GetBarrackPos();
        speed = soliderInfo.GetSpeed();
        //Debug.Log(curPos);
        //Debug.Log(targetPos);
        soliderInfo.Run(targetPos);
    }

    public void Excute()
    {
        Vector3 pos = soliderInfo.GetPosition();
        float dis = BattleUtils.Distance2(pos, targetPos);
        if (dis < 10 * Time.deltaTime)
        {
            soliderInfo.SetPosition(targetPos.x, targetPos.y, targetPos.z);
            soliderInfo.ChangeState("idle");
        }
        else
        {
            pos = Vector3.MoveTowards(pos, targetPos, Time.deltaTime * 10);
            soliderInfo.SetPosition(pos.x, pos.y, pos.z);
        }
    }

    public void ExitExcute()
    {

    }
}

