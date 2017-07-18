using System.Collections;
using System.Collections.Generic;
using Hero;
using UnityEngine;
public class MageTowerView : TowerView
{
    public Animate towerBase;
    public Animate shooter;
    public MageTowerView(AttackTowerInfo towerInfo)
    {
        this.towerInfo = towerInfo;
        this.towerInfo.eventDispatcher.Register("DoAction", DoAction);
    }

    public override void LoadModel()
    {
        towerAsset = GameLoader.Instance.LoadAssetSync("Resources/Prefabs/MageTower.prefab");
        towerObj = towerAsset.GameObjectAsset;
        towerObj.transform.position = this.towerInfo.GetPosition();
        //增加点击事件
        AddClickInfo(towerObj, towerInfo.Id);
        //加载塔身图片
        towerBase = InitAnimate(towerObj, towerInfo.towerBase);
        //加载魔法师
        GameObject shooterObj = towerObj.transform.Find("MageShooter").gameObject;
        shooter = InitAnimate(shooterObj, towerInfo.shooter);
        //根据塔基座大小增加碰撞盒
        //AddBoxColider(towerObj, towerBase.width, towerBase.height);
    }

    public override void DoAction(object[] data)
    {
        string actionName = data[0].ToString();
        towerBase.startAnimation(actionName);
        shooter.startAnimation(actionName);
    }

    public override void FingerDown(ClickInfo curClick)
    {
        if (UiManager.Instance.HasOpenUI(UIDefine.eSelectPanel))
        {
            UiManager.Instance.CloseUIById(UIDefine.eSelectPanel);
        }
        else
        {
            UiManager.Instance.OpenUI(UIDefine.eSelectPanel, towerInfo);
        }
    }

    public override void Release()
    {
        GameLoader.Instance.UnLoadGameObject(towerAsset);
    }

    public override void Update()
    {
        this.towerObj.transform.position = this.towerInfo.GetPosition();
    }
}

