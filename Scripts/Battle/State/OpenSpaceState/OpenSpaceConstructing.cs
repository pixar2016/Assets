using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenSpaceConstructing : StateBase
{
    public OpenSpaceInfo openSpaceInfo;
    public float curTime;
    public Vector3 constructPos;
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
        constructPos = (Vector3)args[1];
    }

    public void EnterExcute()
    {
        Debug.Log("EnterSpaceConstructing");
        //开始播放建造中动画
        curTime = 0;
    }

    public void Excute()
    {
        curTime += Time.deltaTime;
        if (curTime > 0.1f)
        {
            //Debug.Log(changeTowerId);
            TowerInfo towerInfo = EntityManager.getInstance().AddTower(changeTowerId);
            //towerInfo.ChangeState("idle");
            //towerInfo.SetPosition(constructPos.x, constructPos.y, constructPos.z);
            //EntityManager.getInstance().RemoveTower(openSpaceInfo.Id);
        }
    }

    public void ExitExcute()
    {

    }
}

