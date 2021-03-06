﻿using System;
using System.Collections.Generic;
using UnityEngine;

//兵营刷新兵种状态
public class BarrackStart : StateBase
{
    public BarrackTowerInfo barrackInfo;
    float curTime;
    public BarrackStart(BarrackTowerInfo _barrackInfo)
    {
        barrackInfo = _barrackInfo;
        curTime = 0;
    }

    public void SetParam(StateParam _param)
    {

    }

    public void EnterExcute()
    {
        Debug.Log("BarrackInfo DoAction Start");
        barrackInfo.DoAction("start");
        curTime = 0;
        for (int i = 0; i < 3; i++)
        {
            //如果兵营中已存在该Index的兵种
            if (barrackInfo.ContainsSolider(i))
            {
                SoliderInfo solider = barrackInfo.GetSolider(i);
                //如果该兵种已死亡
                if (solider.IsDead())
                {
                    solider.Reset();
                    solider.ChangeState("ready");
                }
            }
            //如果兵营不存在该Index兵种
            else
            {
                SoliderInfo solider = EntityManager.getInstance().AddSolider(barrackInfo.soliderId);
                solider.SetBarrackPos(barrackInfo.soliderPos[i], barrackInfo.GetPosition());
                solider.ChangeState("ready");
                barrackInfo.AddSolider(i, solider);
            }
        }
    }

    public void Excute()
    {
        curTime += Time.deltaTime;
        //动画播放结束，转入空闲状态
        if (curTime >= barrackInfo.startTime)
        {
            curTime = 0;
            barrackInfo.ChangeState("idle");
        }
    }

    public void ExitExcute()
    {

    }
}

