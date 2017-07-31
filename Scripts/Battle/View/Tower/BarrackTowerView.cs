﻿using System;
using System.Collections.Generic;
using Hero;
using UnityEngine;

public class BarrackTowerView : TowerView
{
    public Animate towerBase;
    public BarrackTowerView(BarrackTowerInfo towerInfo)
    {
        this.towerInfo = towerInfo;
        this.towerInfo.eventDispatcher.Register("DoAction", DoAction);
    }

    public override void LoadModel()
    {
        towerAsset = GameLoader.Instance.LoadAssetSync("Resources/Prefabs/SoliderTower.prefab");
        towerObj = towerAsset.GameObjectAsset;
        towerObj.transform.position = this.towerInfo.GetPosition();
        //增加点击事件
        AddClickInfo(towerObj, towerInfo.Id);
        //加载塔身图片
        towerBase = InitAnimate(towerObj, towerInfo.towerBase);
        //根据塔基座大小增加碰撞盒
        AddBoxColider(towerObj, 80, 70);
    }

    public override void DoAction(object[] data)
    {
        string actionName = data[0].ToString();
        //Debug.Log("BarrackTowerView  Do Action " + actionName);
        towerBase.startAnimation(actionName);
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

