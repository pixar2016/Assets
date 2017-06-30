using System;
using System.Collections.Generic;
using UnityEngine;

//兵营
public class BarrackTowerInfo : TowerInfo
{
    public int soliderId;
    public Dictionary<int, SoliderInfo> soliderDict;
    //出兵标记点
    public Vector3 signPos;
    //每个兵种的停留点
    public Vector3[] soliderPos;
    //兵营门开启动画时间
    public float startTime;

    public StateMachine towerStateMachine;
    public BarrackStart barrackStart;
    public BarrackIdle barrackIdle;

    public BarrackTowerInfo(int indexId, int barrackId) 
        : base(indexId, barrackId)
    {
        soliderId = towerData._soliderId;
        towerStateMachine = new StateMachine();
        barrackStart = new BarrackStart(this);
        barrackIdle = new BarrackIdle(this);
        soliderDict = new Dictionary<int, SoliderInfo>();

        startTime = AnimationCache.getInstance().getAnimation(this.towerBase).getMeshAnimation("start").getAnimTime();
    }
    public BarrackTowerInfo(int indexId, CharacterPrototype proto)
        : base(indexId, proto)
    {
        soliderId = towerData._soliderId;
        towerStateMachine = new StateMachine();
        barrackStart = new BarrackStart(this);
        barrackIdle = new BarrackIdle(this);
        soliderDict = new Dictionary<int, SoliderInfo>();

        soliderPos = new Vector3[3];
        startTime = AnimationCache.getInstance().getAnimation(this.towerBase).getMeshAnimation("start").getAnimTime();
    }
    

    public bool ContainsSolider(int indexId)
    {
        return soliderDict.ContainsKey(indexId);
    }

    public SoliderInfo GetSolider(int indexId)
    {
        if (soliderDict.ContainsKey(indexId))
        {
            return soliderDict[indexId];
        }
        return null;
    }

    public void AddSolider(int indexId, SoliderInfo soliderInfo)
    {
        if (!soliderDict.ContainsKey(indexId))
        {
            soliderDict.Add(indexId, soliderInfo);
        }
    }

    public override void SetPosition(float x, float y, float z)
    {
        position = new Vector3(x, y, z);
        SetSignPos(position);
    }

    public void SetSignPos(Vector3 pos)
    {
        //Debug.Log("SetSignPos");
        signPos = pos;
        InitSoliderPos(pos);
    }
    private void InitSoliderPos(Vector3 position)
    {
        soliderPos[0] = new Vector3(signPos.x + 30, signPos.y + 40, signPos.z);
        soliderPos[1] = new Vector3(signPos.x - 30, signPos.y + 40, signPos.z);
        soliderPos[2] = new Vector3(signPos.x, signPos.y + 40, signPos.z);
    }

    public Vector3 GetPosition()
    {
        return position;
    }

    public override void ChangeState(string stateName, StateParam _param = null)
    {
        if (stateName == "start")
        {
            towerStateMachine.ChangeState(barrackStart, _param);
        }
        else if (stateName == "idle")
        {
            towerStateMachine.ChangeState(barrackIdle, _param);
        }
    }

    //是否兵营已经全部出阵
    public bool IsSoliderFull()
    {
        if (soliderDict.Count < 3)
        {
            return false;
        }
        foreach (int key in soliderDict.Keys)
        {
            if (soliderDict[key].IsDead())
            {
                return false;
            }
        }
        return true;
    }

    public override void Update()
    {
        towerStateMachine.Excute();
    }
}

