using System;
using System.Collections.Generic;
using UnityEngine;

public class SoliderMove : StateBase
{
    public SoliderInfo soliderInfo;
    //要移动至附近并攻击的目标
    public CharacterInfo attackInfo;
    public Vector3 curPos;
    public Vector3 targetPos;
    public float speed;
    public SoliderMove(SoliderInfo _soliderInfo)
    {
        soliderInfo = _soliderInfo;
    }

    public void SetParam(StateParam _param)
    {
        if (_param == null)
            return;
        attackInfo = _param.targetInfo;
    }

    public void EnterExcute()
    {
        //curPos = soliderInfo.GetPosition();
        //targetPos = soliderInfo.GetAttackMovePos();
        targetPos = GetAtkPos(attackInfo.GetPosition());
        speed = soliderInfo.GetSpeed();
        soliderInfo.Run(targetPos);
    }

    private Vector3 GetAtkPos(Vector3 _targetPos)
    {
        Vector3 soliderPos = soliderInfo.GetPosition();
        if (soliderPos.x < _targetPos.x)
        {
            _targetPos.x = _targetPos.x - 30;
        }
        else
        {
            _targetPos.x = _targetPos.x + 30;
        }
        return _targetPos;
    }

    public void Excute()
    {
        Vector3 pos = soliderInfo.GetPosition();
        float dis = BattleUtils.Distance2(pos, targetPos);
        if (dis < speed * Time.deltaTime)
        {
            soliderInfo.SetPosition(targetPos.x, targetPos.y, targetPos.z);
            soliderInfo.ChangeState("attack", new StateParam(attackInfo));
        }
        else
        {
            pos = Vector3.MoveTowards(pos, targetPos, Time.deltaTime * speed);
            soliderInfo.SetPosition(pos.x, pos.y, pos.z);
        }
    }

    public void ExitExcute()
    {

    }
}
