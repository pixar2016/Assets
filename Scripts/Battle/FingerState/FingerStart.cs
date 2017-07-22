using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FingerStart : StateBase
{
    public FingerStart()
    {

    }

    public void SetParam(StateParam _param)
    {

    }

    public void EnterExcute()
    {
        FingerGestures.OnFingerDown += OnFingerDown;
    }

    public void OnFingerDown(int fingerIndex, Vector2 fingerPos)
    {
#if IPHONE || ANDROID
        if(EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)){
#else
        if (EventSystem.current.IsPointerOverGameObject()){
#endif
            Debug.Log("点击到UI");
            return;
        }
        GameObject obj = PickObject(fingerPos);
        if (obj != null && obj.GetComponent<ClickInfo>() != null)
        {
            ClickInfo temp = obj.GetComponent<ClickInfo>();
            temp.fingerDown(temp);
            GameManager.getInstance().curClickInfo = temp;
        }
        else
        {
            UiManager.Instance.CloseClickPanels();
            GameManager.getInstance().curClickInfo = null;
        }
    }

    public static GameObject PickObject(Vector2 screenPos)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
            return hit.collider.gameObject;

        return null;
    }

    public void Excute()
    {

    }

    public void ExitExcute()
    {
        FingerGestures.OnFingerDown -= OnFingerDown;
    }
}
