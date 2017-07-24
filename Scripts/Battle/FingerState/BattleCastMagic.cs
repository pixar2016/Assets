using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 释放魔法状态
/// 全图选择一个位置，按下释放魔法，然后退出进入FingerStart状态
/// </summary>
public class BattleCastMagic : StateBase
{
    public SkillInfo skillInfo;
    public BattleCastMagic()
    {

    }

    public void SetParam(StateParam _param)
    {
        if (_param == null)
        {
            return;
        }
        skillInfo = (SkillInfo)_param.skillInfo;
    }

    public void EnterExcute()
    {
        FingerGestures.OnFingerDown += OnFingerDown;
    }

    public void OnFingerDown(int fingerIndex, Vector2 fingerPos)
    {

    }

    public void Excute()
    {

    }

    public void ExitExcute()
    {
        FingerGestures.OnFingerDown -= OnFingerDown;
    }
}

