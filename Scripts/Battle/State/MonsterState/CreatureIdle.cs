using System;
using System.Collections.Generic;
using UnityEngine;

public class CreatureIdle : StateBase
{
    public MonsterInfo monsterInfo;
    public string name;
    public List<CharacterInfo> atkList;
    public CreatureIdle(MonsterInfo _monsterInfo)
    {
        name = "CreatureIdle";
        monsterInfo = _monsterInfo;
        atkList = monsterInfo.atkList;
    }

    public void SetParam(StateParam _param)
    {
        if (_param == null)
        {
            return;
        }
        atkList.Add(_param.targetInfo);
    }

    public void EnterExcute()
    {
        //Debug.Log("CreatureIdle EnterExcute");
        monsterInfo.DoAction("idle");
    }

    public void Excute()
    {
        int count = atkList.Count;
        if (count == 1)
        {
            monsterInfo.ChangeState("attack", new StateParam(atkList[0]));
            atkList.Clear();
        }
        else if (count > 1)
        {
            monsterInfo.ChangeState("attack", new StateParam(atkList[0]));
            atkList.RemoveAt(0);
        }
    }

    public void ExitExcute()
    {

    }
}

