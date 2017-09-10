using System.Collections;
using System.Collections.Generic;
using Hero;
using UnityEngine;

public class ArtilleryTowerView : TowerView
{
    public Animate towerBase;
    private Vector3 bulletPos;
    public ArtilleryTowerView(AttackTowerInfo towerInfo)
    {
        this.towerInfo = towerInfo;
        this.towerInfo.towerView = this;
        this.towerInfo.eventDispatcher.Register("DoAction", DoAction);
    }

    public override void LoadModel()
    {
        string modelPath = "Resources/" + J_ModelResource.GetData(towerInfo.towerData._modelId)._modelPath;
        towerAsset = GameLoader.Instance.LoadAssetSync(modelPath);
        towerObj = towerAsset.GameObjectAsset;
        towerObj.transform.position = this.towerInfo.GetPosition();
        bulletPos = towerObj.transform.Find("BulletPos1").position;
        //增加点击事件
        AddClickInfo(towerObj, towerInfo.Id);
        //加载塔身图片
        towerBase = InitAnimate(towerObj, towerInfo.towerBase);

        //根据塔基座大小增加碰撞盒
        //AddBoxColider(towerObj, 80, 70);
    }

    public override void DoAction(object[] data)
    {
        string actionName = data[0].ToString();
        towerBase.startAnimation(actionName);
    }

    public override Vector3 GetBulletPos()
    {
        return bulletPos;
    }

    public override void Release()
    {
        GameLoader.Instance.UnLoadGameObject(towerAsset);
    }

    public override void Update()
    {
        //this.towerObj.transform.position = this.towerInfo.GetPosition();
    }
}

