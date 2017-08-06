using UnityEngine;
using System.Collections;
using Hero;
//using EventDispatcherSpace;

public class CharacterView {

    //public CharacterInfo charInfo;
    public ILoadAsset charAsset;
    public GameObject charObj;
    //public Animate charAnim;

    public CharacterView()
    {

    }
    //public CharacterView(CharacterInfo charInfo)
    //{
    //    this.charInfo = charInfo;
    //    this.charInfo.eventDispatcher.Register("DoAction", DoAction);
    //}

    public virtual void LoadModel()
    {
        //charAsset = GameLoader.Instance.LoadAssetSync("Resources/Prefabs/fly.prefab");
        //charObj = charAsset.GameObjectAsset;
        //charObj.name = charInfo.charName;
        //charAnim = InitAnimate(charObj, charInfo.charName);
    }

    //public void DoAction(object[] data)
    //{
    //    //Debug.Log("View DoAction" + data[0].ToString());
    //    float actionTime = 0;
    //    if (data.Length > 1)
    //    {
    //        actionTime = float.Parse(data[1].ToString());
    //    }
    //    charAnim.startAnimation(data[0].ToString(), actionTime);
    //}

    public virtual void SetPosition(Vector3 _pos)
    {
        charObj.transform.position = _pos;
    }

    public virtual void SetRotation(Vector3 _rot)
    {
        charObj.transform.eulerAngles = _rot;
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
        animCom.startAnimation("idle");
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

    public void Release()
    {
        GameLoader.Instance.UnLoadGameObject(charAsset);
    }
    public void Update()
    {
        //charObj.transform.position = charInfo.GetPosition();
        //charObj.transform.eulerAngles = charInfo.GetRotation();
    }
}
