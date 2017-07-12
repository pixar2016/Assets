
using System;
using System.Collections.Generic;
using UnityEngine;

//战场工具方法 包括战斗双方计算伤害等
public class BattleUtils
{
    /// <summary>
    /// 对单一目标造成物理攻击伤害
    /// </summary>
    /// <param name="atkInfo">攻击方</param>
    /// <param name="defInfo">防守方</param>
    public static void CalcAtkDamage(CharacterInfo atkInfo, CharacterInfo defInfo)
    {
        //Debug.Log("CalcAtkDamage");
        if (atkInfo == null || defInfo == null)
        {
            return;
        }
        int attackDamage = atkInfo.GetAttr(CharAttr.AttackDamage);
        if (attackDamage != -1)
        {
            defInfo.ChangeAttr(CharAttr.Hp, -attackDamage);
            //Debug.Log(defInfo.GetAttr(CharAttr.Hp));
        }
    }
    /// <summary>
    /// 对多个目标造成物理攻击伤害
    /// </summary>
    /// <param name="atkInfo">攻击方</param>
    /// <param name="defList">防守方列表</param>
    public static void CalcAtkDamage(CharacterInfo atkInfo, List<CharacterInfo> defList)
    {

    }
    /// <summary>
    /// 对单一目标造成魔法伤害
    /// </summary>
    /// <param name="atkInfo"></param>
    /// <param name="defInfo"></param>
    public static void CalcMagicDamage(CharacterInfo atkInfo, CharacterInfo defInfo)
    {

    }
    /// <summary>
    /// 对多个目标造成魔法伤害
    /// </summary>
    /// <param name="atkInfo"></param>
    /// <param name="defList"></param>
    public static void CalcMagicDamage(CharacterInfo atkInfo, List<CharacterInfo> defList)
    {

    }
    /// <summary>
    /// 得到攻击者攻击目标时站立的位置
    /// </summary>
    /// <param name="charInfo">攻击者</param>
    /// <param name="targetInfo">攻击目标</param>
    /// <returns></returns>
    public static Vector3 GetAtkPos(CharacterInfo charInfo, CharacterInfo targetInfo)
    {
        return Vector3.zero;
    }

    /// <summary>
    /// 根据攻击者与受攻击者距离排序受攻击者列表
    /// </summary>
    /// <param name="charInfo">攻击者</param>
    /// <param name="atklist">受攻击者</param>
    public static void SortAtkList(CharacterInfo charInfo, List<CharacterInfo> atklist)
    {

    }
}

