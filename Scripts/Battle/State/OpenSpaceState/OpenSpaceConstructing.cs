using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenSpaceConstructing : StateBase
{
    public OpenSpaceInfo openSpaceInfo;
    public int changeTowerId;
    public float curTime;
    public EffectInfo baseEffect;
    public OpenSpaceConstructing(OpenSpaceInfo _openSpaceInfo)
    {
        openSpaceInfo = _openSpaceInfo;
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
        //进入第一次创建过程，需要显示底座
        baseEffect = EntityManager.getInstance().AddStaticEffect(12, openSpaceInfo.GetPosition());
        openSpaceInfo.DoAction("hide");
    }

    public void Excute()
    {
        curTime += Time.deltaTime;
        if (curTime > 1)
        {
            EntityManager.getInstance().RemoveEffect(baseEffect.Id);
            TowerInfo changeTower = EntityManager.getInstance().AddTower(changeTowerId);
            Vector3 pos = openSpaceInfo.GetPosition();
            changeTower.SetPosition(pos.x, pos.y, pos.z);
            changeTower.ChangeState("idle");
            //加入灰尘特效
            EntityManager.getInstance().AddStaticEffect(20, pos);
            EntityManager.getInstance().RemoveTower(openSpaceInfo.Id);
        }
    }

    public void ExitExcute()
    {

    }
}

