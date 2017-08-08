using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleMainPanel : UIComponent
{
    public GameObject BtnMagic1;
    public GameObject BtnMagic2;
    public override void OnInit(object[] data)
    {
        BtnMagic1 = transform.Find("LeftDown/Magic1").gameObject;
        BtnMagic2 = transform.Find("LeftDown/Magic2").gameObject;
        UIEventSystem.Instance.Register("ShowBuildingProcess", ShowBuildingProcess);
    }

    public void SetEventListener()
    {
        UIEventListener.Get(BtnMagic1).onClick = OnBtnMagic1Click;
        UIEventListener.Get(BtnMagic2).onClick = OnBtnMagic2Click;
    }

    public void OnBtnMagic1Click(GameObject go)
    {

    }

    public void OnBtnMagic2Click(GameObject go)
    {

    }

    public void ShowBuildingProcess(object[] param)
    {
        Debug.Log("ShowBuildingProcess");
    }

    public override void OnRelease()
    {
        
    }
}

