using System;
using System.Collections.Generic;
using UnityEngine;

public class TowerIdle : StateBase
{
    public AttackTowerInfo towerInfo;

    public float intervalTime;

    public float curTime;

    public TowerIdle(AttackTowerInfo _towerInfo)
    {
        towerInfo = _towerInfo;
        intervalTime = 1;
        curTime = 0;
    }

    public void SetParam(params object[] args)
    {

    }

    public void EnterExcute()
    {
        //Debug.Log("TowerIdle");
        towerInfo.DoAction("idle");
        CharacterInfo targetInfo = RunAI(towerInfo);
        if (targetInfo != null)
        {
            towerInfo.SetTargetInfo(targetInfo);
            towerInfo.ChangeState("attack", targetInfo);
        }
    }

    public void Attack()
    {
        CharacterInfo targetInfo = RunAI(towerInfo);
        //Debug.Log(towerInfo.WithinRange(targetInfo));
        if (targetInfo != null)
        {
            towerInfo.SetTargetInfo(targetInfo);
            towerInfo.ChangeState("attack", targetInfo);
        }
        else
        {
            towerInfo.ChangeState("idle");
        }
    }

    public CharacterInfo RunAI(TowerInfo towerInfo)
    {
        List<MonsterInfo> monsterList = EntityManager.getInstance().GetMonsterInfo();
        Vector3 towerPos = towerInfo.GetPosition();
        for (int i = 0; i < monsterList.Count; i++)
        {
            MonsterInfo temp = monsterList[i];
            if (!temp.IsDead() && Vector3.Distance(towerPos, temp.GetPosition()) <= 200)
            {
                return temp;
            }
        }
        return null;
    }

    public void Excute()
    {
        curTime += Time.deltaTime;
        if (curTime >= intervalTime)
        {
            curTime = 0;
            Attack();
        }
    }

    public void ExitExcute()
    {

    }
}

