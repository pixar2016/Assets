using System;
using System.Collections.Generic;
using UnityEngine;

public class TowerConstructing : StateBase
{
    public AttackTowerInfo towerInfo;

    public int changeTowerId;

    public float interval;

    public float curTime;

    public TowerConstructing(AttackTowerInfo _towerInfo)
    {
        towerInfo = _towerInfo;
        interval = 1;
        curTime = 0;
    }

    public void SetParam(StateParam _param)
    {
        if (_param == null)
        {
            return;
        }
        changeTowerId = _param.towerId;
    }

    public void EnterExcute()
    {
        curTime = 0;
    }

    public void Excute()
    {
        curTime += Time.deltaTime;
        if (curTime > 1f)
        {
            TowerInfo changeTower = EntityManager.getInstance().AddTower(changeTowerId);
            Vector3 pos = towerInfo.GetPosition();
            changeTower.SetPosition(pos);
            changeTower.ChangeState("idle");
            EntityManager.getInstance().RemoveTower(towerInfo.Id);
        }
    }

    public void ExitExcute()
    {

    }
}

