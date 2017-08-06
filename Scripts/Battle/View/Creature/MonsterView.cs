
using System.Collections.Generic;
using UnityEngine;
using Hero;
public class MonsterView : CharacterView
{
    public MonsterInfo monsterInfo;
    public Animate monsterAnim;

    public MonsterView(MonsterInfo _monsterInfo)
    {
        this.monsterInfo = _monsterInfo;
        this.monsterInfo.charView = this;
        this.monsterInfo.eventDispatcher.Register("DoAction", DoAction);
    }

    public override void LoadModel()
    {
        charAsset = GameLoader.Instance.LoadAssetSync("Resources/Prefabs/fly.prefab");
        charObj = charAsset.GameObjectAsset;
        charObj.name = monsterInfo.charName;
        monsterAnim = InitAnimate(charObj, monsterInfo.charName);
    }
    public void DoAction(object[] data)
    {
        float actionTime = 0;
        if (data.Length > 1)
        {
            actionTime = float.Parse(data[1].ToString());
        }
        monsterAnim.startAnimation(data[0].ToString(), actionTime);
    }
}

