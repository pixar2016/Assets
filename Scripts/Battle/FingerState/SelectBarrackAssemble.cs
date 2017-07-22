using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
