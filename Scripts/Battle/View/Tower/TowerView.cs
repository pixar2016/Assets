using System;
using System.Collections.Generic;
using UnityEngine;

public class TowerView
{
    public TowerInfo towerInfo;
    public ILoadAsset towerAsset;
    public GameObject towerObj;
    public TowerView()
    {
    }

    //被点击
    public virtual void FingerDown(ClickInfo curClick)
    {
        ClickInfo preClick = GameManager.getInstance().curClickInfo;
        //如果上次点击 点中可交互物体，并且类型相同，Id不同，即点中其他塔，立刻关闭UI
        if (preClick != null && preClick.clickType == curClick.clickType && preClick.Id != curClick.Id)
        {
            UiManager.Instance.CloseUIById(UIDefine.eSelectPanel);
            UiManager.Instance.OpenUI(UIDefine.eSelectPanel, towerInfo);
        }
        else if (UiManager.Instance.HasOpenUI(UIDefine.eSelectPanel))
        {
            UiManager.Instance.CloseUIById(UIDefine.eSelectPanel);
        }
        else
        {
            UiManager.Instance.OpenUI(UIDefine.eSelectPanel, towerInfo);
        }
    }

    public virtual void SetPosition(Vector3 _pos)
    {
        towerObj.transform.position = _pos;
    }

    public virtual void LoadModel()
    {

    }

    public virtual void DoAction(object[] data)
    {

    }
    /// <summary>
    /// 增加点击事件，并挂上信息ClickInfo组件
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="towerId"></param>
    public virtual void AddClickInfo(GameObject obj, int towerId)
    {
        ClickInfo clickInfo;
        if (obj.GetComponent<ClickInfo>() == null)
        {
            clickInfo = obj.AddComponent<ClickInfo>();
        }
        else
        {
            clickInfo = obj.GetComponent<ClickInfo>();
        }
        clickInfo.OnInit(ClickType.Tower, towerId, FingerDown);
    }
    /// <summary>
    /// 给GameObject增加碰撞盒
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    public virtual void AddBoxColider(GameObject obj, float width, float height)
    {
        BoxCollider collider;
        if (obj.GetComponent<BoxCollider>() != null)
        {
            collider = obj.GetComponent<BoxCollider>();
        }
        else
        {
            collider = obj.AddComponent<BoxCollider>();
        }
        collider.size = new Vector3(width, height, 0.2f);
    }
    /// <summary>
    /// 初始化动画
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="modelName">动画模型名</param>
    public virtual Animate InitAnimate(GameObject obj, string modelName)
    {
        Animate animCom;
        if (obj.GetComponent<Animate>() != null)
        {
            animCom = obj.GetComponent<Animate>();
        }
        else
        {
            animCom = obj.AddComponent<Animate>();
        }
        animCom.OnInit(modelName);
        animCom.startAnimation("idle");
        return animCom;
    }
    /// <summary>
    /// 初始化图片
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="modelName"></param>
    public virtual SpriteImage InitSpriteImage(GameObject obj, string modelName)
    {
        SpriteImage imageCom;
        if (obj.GetComponent<SpriteImage>() != null)
        {
            imageCom = obj.GetComponent<SpriteImage>();
        }
        else
        {
            imageCom = obj.AddComponent<SpriteImage>();
        }
        imageCom.OnInit(modelName);
        return imageCom;
    }
    /// <summary>
    /// 设置meshrenderer组件中所在的层名字和层ID，用于深度控制
    /// </summary>
    /// <param name="obj">包含meshrenderer的GameObject</param>
    /// <param name="layerName">层名字sortingLayerName</param>
    /// <param name="layerId">在该层的sortingOrder</param>
    public virtual void InitSortingLayer(GameObject obj, string layerName, int layerId = 0)
    {
        MeshRenderer render = obj.GetComponent<MeshRenderer>();
        if (render == null)
        {
            Debug.Log("error:No MeshRenderer Component");
        }
        render.sortingLayerName = layerName;
        render.sortingOrder = layerId;
    }

    public virtual Vector3 GetBulletPos()
    {
        return towerInfo.GetPosition();
    }

    public virtual void Update()
    {

    }

    public virtual void Release()
    {

    }
}

