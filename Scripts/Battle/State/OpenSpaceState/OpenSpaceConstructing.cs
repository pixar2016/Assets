﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenSpaceConstructing : StateBase
{
    public OpenSpaceInfo openSpaceInfo;
    public float curTime;
    public int changeTowerId;
    public OpenSpaceConstructing(OpenSpaceInfo _openSpaceInfo)
    {
        openSpaceInfo = _openSpaceInfo;
    }

    public void SetParam(params object[] args)
    {
        if (args == null || args.Length < 1)
        {
            return;
        }
        changeTowerId = (int)args[0];
    }

    public void EnterExcute()
    {
        //开始播放建造中动画
        curTime = 0;
    }

    public void Excute()
    {
        curTime += Time.deltaTime;
        if (curTime > 1f)
        {
            TowerInfo changeTower = EntityManager.getInstance().AddTower(changeTowerId);
            Vector3 pos = openSpaceInfo.GetPosition();
            changeTower.SetPosition(pos.x, pos.y, pos.z);
            changeTower.ChangeState("idle");
            EntityManager.getInstance().RemoveTower(openSpaceInfo.Id);
        }
    }

    public void ExitExcute()
    {

    }
}

