using UnityEngine;
using System.Collections;
//using EventDispatcherSpace;

<<<<<<< HEAD
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
    //攻击时间
    AttackTime,
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

=======
>>>>>>> origin/master
public class CharacterInfo
{
    //序列ID
    public int Id;
    public int charId;
    public string charName;
    public Vector3 position;
    public Vector3 rotation;
    public string actionName;
<<<<<<< HEAD
    //key-CharAttr value-attrValue
    public Dictionary<int, int> attrList;
    //用于广播事件
    public MiniEventDispatcher eventDispatcher;
    //兵种模板
    public CharacterPrototype charProto;
    public CharacterInfo()
    {
        eventDispatcher = new MiniEventDispatcher();
        position = Vector3.zero;
        rotation = Vector3.zero;
        attrList = new Dictionary<int, int>();
=======

    //攻击间隔，兵营为出兵间隔
    public float attackTime;
    //用于广播事件
    public MiniEventDispatcher eventDispatcher;
    

    public CharacterInfo()
    {
        eventDispatcher = new MiniEventDispatcher();
        position = new Vector3(0, 0, 0);
        rotation = new Vector3(0, 0, 0);
>>>>>>> origin/master
    }

    public virtual void SetPosition(float x, float y, float z)
    {
        position = new Vector3(x, y, z);
    }

    public void SetPosition(Vector3 _pos)
    {
        position = _pos;
    }

    public Vector3 GetPosition()
    {
        return position;
    }

    public void SetRotation(float x, float y, float z)
    {
        rotation = new Vector3(x, y, z);
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

<<<<<<< HEAD
    //开始一个技能
    public void StartSkill(SkillInfo skillInfo)
=======
    //开始普通攻击
    public virtual void StartAttack()
>>>>>>> origin/master
    {

    }

<<<<<<< HEAD
    public virtual void Run(Vector3 targetPos)
=======
    }
    //向上走
    public virtual void RunUp()
>>>>>>> origin/master
    {
        DoAction("run1");
    }
<<<<<<< HEAD

    //得到某一个属性基础值
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
    //得到某一个属性的最终值
    public virtual int GetFinalAttr(CharAttr attrName)
=======
    //向下走
    public virtual void RunDown()
    {
        DoAction("run1");
    }
    //向右走
    public virtual void RunRight()
    {
        DoAction("run1");
    }
    //向左走
    public virtual void RunLeft()
    {
        DoAction("run1");
    }
    //对目标造成普通攻击伤害
    public virtual void Hurt()
    {
    }

    public virtual void ReduceHP(int losehp)
>>>>>>> origin/master
    {
        switch (attrName)
        {
            case CharAttr.Hp:
                return GetAttr(CharAttr.Hp) * (1 + GetAttr(CharAttr.HpPer));
            case CharAttr.HpMax:
                return GetAttr(CharAttr.HpMax) * (1 + GetAttr(CharAttr.HpMaxPer));
            case CharAttr.AttackTime:
                return 1 / (GetAttr(CharAttr.AttackSpeed) * (1 + GetAttr(CharAttr.AttackSpeedPer)));
            case CharAttr.AttackSpeed:
                return GetAttr(CharAttr.AttackSpeed) * (1 + GetAttr(CharAttr.AttackSpeedPer));
            case CharAttr.AttackDamage:
                return GetAttr(CharAttr.AttackDamage) * (1 + GetAttr(CharAttr.AttackDamagePer));
            case CharAttr.Speed:
                return GetAttr(CharAttr.Speed) * (1 + GetAttr(CharAttr.SpeedPer));
            default:
                break;
        }
        return GetAttr(attrName);
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
