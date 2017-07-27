using System;
using System.Collections.Generic;
using UnityEngine;

//塔升级状态，并在完成时播放尘土特效
public class TowerConstructing : StateBase
{
    public AttackTowerInfo towerInfo;

    public int changeTowerId;

    public TowerConstructing(AttackTowerInfo _towerInfo)
    {
        towerInfo = _towerInfo;
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
        //加入灰尘特效
        EntityManager.getInstance().AddStaticEffect(20, towerInfo.GetPosition());
        TowerInfo tower = EntityManager.getInstance().AddTower(changeTowerId);
        tower.SetPosition(towerInfo.GetPosition());
        tower.ChangeState("idle");
        EntityManager.getInstance().RemoveTower(towerInfo.Id);
    }

    public void Excute()
    {
    }

    public void ExitExcute()
    {

    }
}

