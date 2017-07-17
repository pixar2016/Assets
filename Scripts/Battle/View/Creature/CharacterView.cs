using UnityEngine;
using System.Collections;
using Hero;
//using EventDispatcherSpace;

public class CharacterView {

    public CharacterInfo charInfo;
    public ILoadAsset charAsset;
    public GameObject charObj;
    public Animate charAnim;

    public CharacterView()
    {

    }
    public CharacterView(CharacterInfo charInfo)
    {
        this.charInfo = charInfo;
        this.charInfo.eventDispatcher.Register("DoAction", DoAction);
    }

    public void LoadModel()
    {
        charAsset = GameLoader.Instance.LoadAssetSync("Resources/Prefabs/fly.prefab");
        charObj = charAsset.GameObjectAsset;
        charObj.name = charInfo.charName;
        if (charObj.GetComponent<Animate>() != null)
        {
            charAnim = charObj.GetComponent<Animate>();
        }
        else
        {
            charAnim = charObj.AddComponent<Animate>();
        }
        charAnim.OnInit(AnimationCache.getInstance().getAnimation(charInfo.charName));
        charAnim.startAnimation("idle");
        MeshRenderer render = charObj.GetComponent<MeshRenderer>();
        //render.sortingLayerName = "Creature";
        //Debug.Log(render.sortingOrder);
        //render.sortingOrder = this.charInfo.Id;
    }

    public void DoAction(object[] data)
    {
        //Debug.Log("View DoAction" + data[0].ToString());
        float actionTime = 0;
        if (data.Length > 1)
        {
            actionTime = float.Parse(data[1].ToString());
        }
        charAnim.startAnimation(data[0].ToString(), actionTime);
    }

    public void Release()
    {
        GameLoader.Instance.UnLoadGameObject(charAsset);
    }
    public void Update()
    {
        charObj.transform.position = charInfo.GetPosition();
        charObj.transform.eulerAngles = charInfo.GetRotation();
    }
}
