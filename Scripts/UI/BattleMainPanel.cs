using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleMainPanel : UIComponent
{
    public override void OnInit(object[] data)
    {
        UIEventSystem.Instance.Register("ShowBuildingProcess", ShowBuildingProcess);
    }

    public void ShowBuildingProcess(object[] param)
    {
        Debug.Log("ShowBuildingProcess");
    }

    public override void OnRelease()
    {
        
    }
}

