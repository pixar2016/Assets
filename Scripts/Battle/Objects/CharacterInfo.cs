using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using EventDispatcherSpace;

public enum CharAttr
{
    //血量
    Hp,
    //血量上限
    HpMax,
    //攻击速度
    AttackSpeed,
    //攻击伤害
    AttackDamage,
    //护甲类型
    ArmorType,
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
    public Dictionary<CharAttr, int> attrList;

    //攻击间隔，兵营为出兵间隔
    public float attackTime;
    //用于广播事件
    public MiniEventDispatcher eventDispatcher;


    public CharacterInfo()
    {
        eventDispatcher = new MiniEventDispatcher();
        position = Vector3.zero;
        rotation = Vector3.zero;
        attrList = new Dictionary<CharAttr, int>();
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
    //设置攻击目标等信息
    public virtual void SetAttackInfo(CharacterInfo charInfo)
    {
    }

    public virtual CharacterInfo GetAttackInfo()
    {
        return null;
    }
    //开始一个技能
    public virtual void StartSkill(SkillInfo skillInfo)
    {
        SkillManager.getInstance().StartSkill(skillInfo);
    }
    //开始主动技能或被动技能
    public virtual void StartSkill(int skillId)
    {

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
    //对目标造成普通攻击伤害
    public virtual void Hurt()
    {
    }
    //得到某一个属性值
    public virtual int GetAttr(CharAttr attrName)
    {
        if (attrList.ContainsKey(attrName))
        {
            return attrList[attrName];
        }
        else
        {
            return -1;
        }
    }
    public virtual void ReduceHP(int losehp)
    {
    }

    public virtual void Release()
    {

    }
}
