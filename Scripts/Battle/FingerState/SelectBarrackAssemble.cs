using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 选择兵营集合点
/// 显示兵营可选择集合点的范围，点击在范围内，出现一个显示2s的旗子，然后退出进入FingerStart状态，点击到范围外，出现一个显示2s的叉，然后继续保留在该状态
/// </summary>
public class SelectBarrackAssemble : StateBase
{

    public BarrackTowerInfo towerInfo;
    public SelectBarrackAssemble()
    {

    }

    //传入barrackInfo
    public void SetParam(StateParam _param)
    {
        if (_param == null)
        {
            return;
        }
        towerInfo = (BarrackTowerInfo)_param.targetInfo;
    }

    public void EnterExcute()
    {
        FingerGestures.OnFingerDown += OnFingerDown;
    }

    public void OnFingerDown(int fingerIndex, Vector2 fingerPos)
    {

    }

    //通过屏幕坐标得到实际坐标
    public Vector3 PickPos(Vector2 screenPos)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            return hit.collider.gameObject.transform.position;
        }
        return Vector3.zero;
    }

    public void Excute()
    {

    }

    public void ExitExcute()
    {
        FingerGestures.OnFingerDown -= OnFingerDown;
    }
}
