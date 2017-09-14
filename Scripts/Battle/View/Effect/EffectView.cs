using System;
using System.Collections.Generic;
using Hero;
using UnityEngine;
public class EffectView
{
    public ILoadAsset effectAsset;
    public GameObject effectObj;
    public int Id;
    //标记 0-正常 1-添加 2-删除
    public int dirtySign;
    public EffectView()
    {
        dirtySign = 0;
    }

    public void LoadModel()
    {
        effectAsset = GameLoader.Instance.LoadAssetSync("Resources/Prefabs/fly.prefab");
        effectObj = effectAsset.GameObjectAsset;
        InitSortingLayer(effectObj, "Effect");
        InitCom();
    }

    public virtual void InitCom()
    {

    }
    /// <summary>
    /// 初始化动画
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="modelName">动画模型名</param>
    public Animate InitAnimate(GameObject obj, string modelName, float width)
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
        animCom.SetWidth(width);
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

    //设置标记，true-移除 false-添加
    public void SetDirtySign(bool isDirty)
    {
        if (isDirty)
            dirtySign--;
        else
            dirtySign++;
    }

    protected float angle_360(Vector3 _from, Vector3 _to)
    {
        Vector3 cross = Vector3.Cross(_from, _to);
        float angle;
        if (cross.z > 0)
            angle = Vector3.Angle(_from, _to);
        else
            angle = 360 - Vector3.Angle(_from, _to);
        return angle;
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

    public virtual void Update()
    {
    }
}

