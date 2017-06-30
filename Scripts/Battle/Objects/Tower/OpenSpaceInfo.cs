using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//空地类，可以建设所有塔
public class OpenSpaceInfo : TowerInfo
{
    public StateMachine stateMachine;
    public OpenSpaceConstructing openSpaceConstructing;

    public OpenSpaceInfo(int indexId, int spaceId)
        :base(indexId, spaceId)
    {
        this.towerData = J_Tower.GetData(spaceId);
        stateMachine = new StateMachine();
        openSpaceConstructing = new OpenSpaceConstructing(this);
    }

    public OpenSpaceInfo(int indexId, CharacterPrototype proto)
        :base(indexId, proto)
    {
        this.towerData = J_Tower.GetData(proto.charId);
        stateMachine = new StateMachine();
        openSpaceConstructing = new OpenSpaceConstructing(this);
    }

    public override void ChangeState(string stateName, StateParam _param = null)
    {
        if (stateName == "constructing")
        {
            stateMachine.ChangeState(openSpaceConstructing, _param);
        }
    }

    public override void Update()
    {
        stateMachine.Excute();
    }
}

