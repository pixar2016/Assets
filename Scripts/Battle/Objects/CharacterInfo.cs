using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using EventDispatcherSpace;

public enum CharAttr
{
    //血量
    Hp,
    //血量增加百分比
    HpPer,
    //血量上限
    HpMax,
    //血量上限增加百分比
    HpMaxPer,
    //攻击速度
    AttackSpeed,
    //攻击速度增加百分比
    AttackSpeedPer,
    //攻击伤害
    AttackDamage,
    //攻击伤害增加百分比
    AttackDamagePer,
    //护甲类型
    ArmorType,
    //速度
    Speed,
    //速度增加百分比
    SpeedPer
}

public class CharacterInfo
{
    //序列ID
    public int Id;
    public int charId;
    public string charName;
    public Vector3 position;
    public Vector3 rotation;
    public string actionName;
    //key-CharAttr value-attrValue
    public Dictionary<int, int> attrList;

    //攻击间隔，兵营为出兵间隔
    public float attackTime;
    //用于广播事件
    public MiniEventDispatcher eventDispatcher;


    public CharacterInfo()
    {
        eventDispatcher = new MiniEventDispatcher();
        position = Vector3.zero;
        rotation = Vector3.zero;
        attrList = new Dictionary<int, int>();
    }

    public void SetPosition(float x, float y, float z)
    {
        position.x = x;
        position.y = y;
        position.z = z;
    }

    public Vector3 GetPosition()
    {
        return position;
    }

    public void SetRotation(float x, float y, float z)
    {
        rotation.x = x;
        rotation.y = y;
        rotation.z = z;
    }

    public Vector3 GetRotation()
    {
        return rotation;
    }

    public virtual bool IsDead()
    {
        return false;
    }

    public void DoAction(string actionName)
    {
        //Debug.Log("Dispatching: SampleEvent " + sampleEvent);
        this.actionName = actionName;
        this.eventDispatcher.Broadcast("DoAction", actionName);
    }

    public virtual void ChangeState(string _state, params object[] args)
    {

    }

    public virtual void SetState(string _state)
    {

    }

    //用作设置目标，方便状态机转换
    public virtual void SetTargetInfo(CharacterInfo charInfo)
    {

    }
    //得到目标
    public virtual CharacterInfo GetTargetInfo()
    {
        return null;
    }

    //开始一个技能
    public virtual void StartSkill(SkillInfo skillInfo)
    {
        SkillManager.getInstance().StartSkill(skillInfo);
    }

    public virtual void Run(Vector3 targetPos)
    {
        Vector3 curPos = this.GetPosition();
        if (targetPos.y > curPos.y && Mathf.Abs(targetPos.y - curPos.y) > Mathf.Abs(targetPos.x - curPos.x))
        {
            DoAction("run1");
        }
        else if (targetPos.x >= curPos.x)
        {
            DoAction("run1");
        }
        else
        {
            DoAction("run1");
        }
    }

    //得到某一个属性值
    public virtual int GetAttr(CharAttr attrName)
    {
        int temp = (int)attrName;
        if (attrList.ContainsKey(temp))
        {
            return attrList[temp];
        }
        else
        {
            return -1;
        }
    }
    //改变某个属性
    public virtual bool ChangeAttr(CharAttr attrName, int changeNum)
    {
        int temp = (int)attrName;
        if (attrList.ContainsKey(temp))
        {
            attrList[temp] += changeNum;
            return true;
        }
        else
        {
            return false;
        }
    }

    //设置某个属性值
    public void SetAttr(CharAttr attrName, int attrNum)
    {
        int temp = (int)attrName;
        if (attrList.ContainsKey(temp))
        {
            attrList[temp] = attrNum;
        }
        else
        {
            attrList.Add(temp, attrNum);
        }
    }

    public virtual void Release()
    {

    }
}
