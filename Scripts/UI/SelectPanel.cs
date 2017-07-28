using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//建造选择界面

public class SelectPanel : UIComponent
{
    int panelType;
    private TowerInfo towerInfo;
    private int[] nextlevel;

    public GameObject BuildTowerObj;
    public Button BtnArrowTower;
    public Button BtnMageTower;
    public Button BtnSoliderTower;
    public Button BtnArtileryTower;

    public GameObject UpgradeTowerObj;
    public Button BtnUpgrade;
    public Button BtnSell;

    public GameObject UpgradeSkillObj;
    public Button BtnSkill1;
    public Button BtnSkill2;
    public Button BtnSkill3;

    public GameObject UpgradeFinalTower;
    public Button BtnUpgrade1;
    public Button BtnUpgrade2;
    public Button BtnSell1;
    //public Button button;
    public override void OnInit(object[] data)
    {
        towerInfo = (TowerInfo)data[0];
        nextlevel = towerInfo.towerData._nextlevel;

        this.gameObject.transform.position = Camera.main.WorldToScreenPoint(towerInfo.GetPosition());
        BuildTowerObj = transform.Find("BuildTower").gameObject;
        BtnArrowTower = transform.Find("BuildTower/BtnArrow").GetComponent<Button>();
        BtnMageTower = transform.Find("BuildTower/BtnMage").GetComponent<Button>();
        BtnSoliderTower = transform.Find("BuildTower/BtnSolider").GetComponent<Button>();
        BtnArtileryTower = transform.Find("BuildTower/BtnArtilery").GetComponent<Button>();

        UpgradeTowerObj = transform.Find("UpgradeTower").gameObject;
        BtnUpgrade = transform.Find("UpgradeTower/BtnUpgrade").GetComponent<Button>();
        BtnSell = transform.Find("UpgradeTower/BtnSell").GetComponent<Button>();

        UpgradeSkillObj = transform.Find("UpgradeSkill").gameObject;
        BtnSkill1 = transform.Find("UpgradeSkill/BtnSkill1").GetComponent<Button>();
        BtnSkill2 = transform.Find("UpgradeSkill/BtnSkill2").GetComponent<Button>();
        BtnSkill3 = transform.Find("UpgradeSkill/BtnSkill3").GetComponent<Button>();

        UpgradeFinalTower = transform.Find("UpgradeFinalTower").gameObject;
        //BtnUpgrade1 = transform.Find()
        SetEventListener();

        ShowPanel(towerInfo);
    }
    //1-建造塔  2-升级塔  3-升级塔的技能
    public void ShowPanel(TowerInfo towerInfo)
    {
        int towerType = towerInfo.towerType;
        int towerLevel = towerInfo.towerData._level;
        BuildTowerObj.SetActive(false);
        UpgradeTowerObj.SetActive(false);
        UpgradeSkillObj.SetActive(false);
        UpgradeFinalTower.SetActive(false);
        //升级技能
        if (nextlevel == null)
        {
            UpgradeSkillObj.SetActive(true);
        }
        //升级界面
        else if (nextlevel.Length == 1)
        {
            UpgradeTowerObj.SetActive(true);
        }
        //升级到最终状态
        else if (nextlevel.Length == 2)
        {
            UpgradeFinalTower.SetActive(true);
        }
        //开始建造界面
        else
        {
            BuildTowerObj.SetActive(true);
        }
    }

    public void SetEventListener()
    {
        UIEventListener.Get(BtnArrowTower.gameObject).onClick = OnBtnArrowTowerClick;
        UIEventListener.Get(BtnMageTower.gameObject).onClick = OnBtnMageTowerClick;
        UIEventListener.Get(BtnSoliderTower.gameObject).onClick = OnBtnSoliderTowerClick;
        UIEventListener.Get(BtnArtileryTower.gameObject).onClick = OnBtnArtileryTowerClick;

        UIEventListener.Get(BtnUpgrade.gameObject).onClick = OnBtnUpgradeClick;
        UIEventListener.Get(BtnSell.gameObject).onClick = OnBtnSellClick;

        UIEventListener.Get(BtnSkill1.gameObject).onClick = OnBtnSkill1Click;
        UIEventListener.Get(BtnSkill2.gameObject).onClick = OnBtnSkill2Click;
        UIEventListener.Get(BtnSkill3.gameObject).onClick = OnBtnSkill3Click;
    }

    public void OnBtnArrowTowerClick(GameObject go)
    {
        //towerInfo.ChangeState("constructing", 2);
        if (nextlevel.Length > 1)
        {
            towerInfo.ChangeState("constructing", new StateParam(nextlevel[0]));
            UiManager.Instance.CloseUIById(UIDefine.eSelectPanel);
        }
    }

    public void OnBtnMageTowerClick(GameObject go)
    {
        //towerInfo.ChangeState("constructing", 6);
        if (nextlevel.Length > 1)
        {
            towerInfo.ChangeState("constructing", new StateParam(nextlevel[1]));
            UiManager.Instance.CloseUIById(UIDefine.eSelectPanel);
        }
    }

    public void OnBtnSoliderTowerClick(GameObject go)
    {
        //towerInfo.ChangeState("constructing", 16);
        if (nextlevel.Length > 1)
        {
            towerInfo.ChangeState("constructing", new StateParam(nextlevel[3]));
            UiManager.Instance.CloseUIById(UIDefine.eSelectPanel);
        }
    }

    public void OnBtnArtileryTowerClick(GameObject go)
    {
        //towerInfo.ChangeState("constructing", 11);
        if (nextlevel.Length > 1)
        {
            towerInfo.ChangeState("constructing", new StateParam(nextlevel[2]));
            UiManager.Instance.CloseUIById(UIDefine.eSelectPanel);
        }
    }

    public void OnBtnUpgradeClick(GameObject go)
    {
        Debug.Log("BtnUpgrade");
        //Debug.Log(towerInfo.towerData._nextlevel.Length);
        if (nextlevel.Length == 1)
        {
            towerInfo.ChangeState("constructing", new StateParam(nextlevel[0]));
            UiManager.Instance.CloseUIById(UIDefine.eSelectPanel);
        }
    }

    public void OnBtnSellClick(GameObject go)
    {
        towerInfo.ChangeState("constructing", new StateParam(100));
        UiManager.Instance.CloseUIById(UIDefine.eSelectPanel);
    }

    public void OnBtnSkill1Click(GameObject go)
    {

    }

    public void OnBtnSkill2Click(GameObject go)
    {

    }

    public void OnBtnSkill3Click(GameObject go)
    {

    }

    public override void OnRelease()
    {
        
    }
}

