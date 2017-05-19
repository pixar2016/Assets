﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class SoliderMove : StateBase
{
    public SoliderInfo soliderInfo;
    public Vector3 targetPos;
    public float speed;
    public SoliderMove(SoliderInfo _soliderInfo)
    {
        soliderInfo = _soliderInfo;
    }

    public void SetParam(params object[] args)
    {

    }

    public void EnterExcute()
    {
        targetPos = soliderInfo.GetAttackMovePos();
        speed = soliderInfo.GetSpeed();
        soliderInfo.Run(targetPos);
    }

    public void Excute()
    {
        Vector3 pos = soliderInfo.GetPosition();
        float dis = Vector3.Distance(pos, targetPos);
        if (dis < speed * Time.deltaTime)
        {
            soliderInfo.SetPosition(targetPos.x, targetPos.y, targetPos.z);
            CharacterInfo attackInfo = soliderInfo.GetTargetInfo();
            soliderInfo.SetTargetInfo(attackInfo);
            attackInfo.SetTargetInfo(soliderInfo);
            soliderInfo.ChangeState("attack");
            attackInfo.ChangeState("attack");
        }
        else
        {
            pos = Vector3.MoveTowards(pos, targetPos, Time.deltaTime * speed);
            soliderInfo.SetPosition(pos.x, pos.y, pos.z);
        }
    }

    public void ExitExcute()
    {

    }
}
