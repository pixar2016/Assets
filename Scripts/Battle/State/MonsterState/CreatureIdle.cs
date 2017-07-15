using System;
using System.Collections.Generic;
using UnityEngine;

public class CreatureIdle : StateBase
{
    public MonsterInfo monsterInfo;
    public string name;
    public CharacterInfo atkInfo;
    public CreatureIdle(MonsterInfo _monsterInfo)
    {
        name = "CreatureIdle";
        monsterInfo = _monsterInfo;
    }

    public void SetParam(StateParam _param)
    {
        if (_param == null)
        {
            return;
        }
        monsterInfo.SetAtkInfo(_param.targetInfo);
    }

    public void EnterExcute()
    {
        //Debug.Log("CreatureIdle EnterExcute");
        monsterInfo.DoAction("idle");
        atkInfo = monsterInfo.GetAtkInfo();
    }

    public void Excute()
    {
        if (atkInfo == null)
        {
            return;
        }
        if (Vector3.Distance(atkInfo.GetPosition(), monsterInfo.GetPosition()) < 20)
        {
            monsterInfo.ChangeState("attack", new StateParam(atkInfo));
        }
    }

    

    public void ExitExcute()
    {

    }
}

