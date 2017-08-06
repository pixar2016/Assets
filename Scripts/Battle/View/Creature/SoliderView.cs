
using System.Collections.Generic;
using UnityEngine;
using Hero;

public class SoliderView : CharacterView
{
    public SoliderInfo soliderInfo;
    public Animate soliderAnim;

    public SoliderView(SoliderInfo _soliderInfo)
    {
        this.soliderInfo = _soliderInfo;
        this.soliderInfo.charView = this;
        this.soliderInfo.eventDispatcher.Register("DoAction", DoAction);
    }

    public override void LoadModel()
    {
        charAsset = GameLoader.Instance.LoadAssetSync("Resources/Prefabs/fly.prefab");
        charObj = charAsset.GameObjectAsset;
        charObj.name = soliderInfo.charName;
        soliderAnim = InitAnimate(charObj, soliderInfo.charName);
    }

    public void DoAction(object[] data)
    {
        float actionTime = 0;
        if (data.Length > 1)
        {
            actionTime = float.Parse(data[1].ToString());
        }
        soliderAnim.startAnimation(data[0].ToString(), actionTime);
    }
}

