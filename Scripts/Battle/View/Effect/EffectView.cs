using System;
using System.Collections.Generic;
using Hero;
using UnityEngine;
public class EffectView
{
    public EffectInfo effectInfo;
    public ILoadAsset effectAsset;
    public GameObject effectObj;
    public EffectView(EffectInfo effectInfo)
    {
        this.effectInfo = effectInfo;
        this.effectInfo.effectView = this;
    }

    public void LoadModel()
    {
        effectAsset = GameLoader.Instance.LoadAssetSync("Resources/Prefabs/fly.prefab");
        effectObj = effectAsset.GameObjectAsset;
        InitAnimate(effectObj, effectInfo.effectName);
        InitSortingLayer(effectObj, "Effect");
    }
    /// <summary>
    /// 初始化动画
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="modelName">动画模型名</param>
    public Animate InitAnimate(GameObject obj, string modelName)
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
        animCom.startAnimation();
        return animCom;
    }

    /// <summary>
    /// 设置meshrenderer组件中所在的层名字和层ID，用于深度控制
    /// </summary>
    /// <param name="obj">包含meshrenderer的GameObject</param>
    /// <param name="layerName">层名字sortingLayerName</param>
    /// <param name="layerId">在该层的sortingOrder</param>
    public void InitSortingLayer(GameObject obj, string layerName, int layerId = 0)
    {
        MeshRenderer render = obj.GetComponent<MeshRenderer>();
        if (render == null)
        {
            Debug.Log("error:No MeshRenderer Component");
        }
        render.sortingLayerName = layerName;
        render.sortingOrder = layerId;
    }

    public void SetPosition(Vector3 _pos)
    {
        effectObj.transform.position = _pos;
    }

    public void SetRotation(Vector3 _rot)
    {
        effectObj.transform.eulerAngles = _rot;
    }

    public void Release()
    {
        GameLoader.Instance.UnLoadGameObject(effectAsset);
    }

    public void Update()
    {
        //effectObj.transform.position = effectInfo.GetPosition();
        //effectObj.transform.eulerAngles = effectInfo.GetEulerAngles();
    }
}

