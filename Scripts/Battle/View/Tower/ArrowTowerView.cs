using System;
using System.Collections.Generic;
using Hero;
using UnityEngine;

public class ArrowTowerView : TowerView
{
    public SpriteImage towerBase;
    public Animate shooter1;
    public Animate shooter2;

    private Vector3 bulletPos1;
    private Vector3 bulletPos2;
    //轮到哪个弓手
    public int shooterNum;
    public ArrowTowerView(AttackTowerInfo towerInfo)
    {
        this.towerInfo = towerInfo;
        this.towerInfo.towerView = this;
        this.towerInfo.eventDispatcher.Register("DoAction", DoAction);
        this.shooterNum = 1;
    }

    public override void LoadModel()
    {
        string modelPath = "Resources/" + J_ModelResource.GetData(towerInfo.towerData._modelId)._modelPath;
        towerAsset = GameLoader.Instance.LoadAssetSync(modelPath);
        towerObj = towerAsset.GameObjectAsset;
        towerObj.transform.position = this.towerInfo.GetPosition();
        //增加点击事件
        AddClickInfo(towerObj, towerInfo.Id);
        //加载塔身图片
        GameObject towerBaseObj = towerObj.transform.Find("ArrowTowerBase").gameObject;
        towerBase = InitSpriteImage(towerBaseObj, towerInfo.towerBase);
        //加载射手1
        GameObject shooterObj1 = towerObj.transform.Find("ArrowShooter1").gameObject;
        shooter1 = InitAnimate(shooterObj1, towerInfo.shooter);
        //加载射手2
        GameObject shooterObj2 = towerObj.transform.Find("ArrowShooter2").gameObject;
        shooter2 = InitAnimate(shooterObj2, towerInfo.shooter);

        bulletPos1 = towerObj.transform.Find("BulletPos1").position;
        bulletPos2 = towerObj.transform.Find("BulletPos2").position;
        //根据塔基座大小增加碰撞盒
        //AddBoxColider(towerObj, towerBase.width, towerBase.height);
    }

    public override void DoAction(object[] data)
    {
        string actionName = data[0].ToString();
        if (actionName == "attack")
        {
            Attack();
        }
        else
        {
            shooter1.startAnimation(actionName);
            shooter2.startAnimation(actionName);
        }
    }

    public override Vector3 GetBulletPos()
    {
        return bulletPos1;
    }

    private void Attack()
    {
        if (shooterNum == 1)
        {
            shooterNum = 2;
            shooter1.startAnimation("attack");
            shooter2.startAnimation("idle");
        }
        else
        {
            shooterNum = 1;
            shooter1.startAnimation("idle");
            shooter2.startAnimation("attack");
        }
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

