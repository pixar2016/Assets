using System;
using System.Collections.Generic;

public class BarrackConstructing : StateBase
{
    public BarrackTowerInfo towerInfo;

    public int changeTowerId;

    public BarrackConstructing(BarrackTowerInfo _towerInfo)
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
        EntityManager.getInstance().AddStaticEffect(21, towerInfo.GetPosition());
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

