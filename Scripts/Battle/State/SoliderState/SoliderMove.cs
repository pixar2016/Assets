﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class SoliderMove : StateBase
{
    public SoliderInfo soliderInfo;
    //要移动至附近并攻击的目标
    public CharacterInfo attackInfo;
    public Vector3 curPos;
    public Vector3 targetPos;
    public float speed;
    public SoliderMove(SoliderInfo _soliderInfo)
    {
        soliderInfo = _soliderInfo;
    }

    public void SetParam(StateParam _param)
    {
        if (_param == null)
            return;
        attackInfo = _param.targetInfo;
    }

    public void EnterExcute()
    {
        //curPos = soliderInfo.GetPosition();
        //targetPos = soliderInfo.GetAttackMovePos();
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
            //CharacterInfo attackInfo = soliderInfo.GetTargetInfo();
            //soliderInfo.SetTargetInfo(attackInfo);
            //attackInfo.SetTargetInfo(soliderInfo);
            soliderInfo.ChangeState("attack", new StateParam(attackInfo));
            attackInfo.ChangeState("attack", new StateParam(soliderInfo));
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
