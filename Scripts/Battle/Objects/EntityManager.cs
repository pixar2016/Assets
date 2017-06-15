using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
//using EventDispatcherSpace;
//管理所有实体，包括怪物，士兵，塔，特效动画等
public class EntityManager {

	private static EntityManager instance = null;
    //用于广播事件
    public MiniEventDispatcher eventDispatcher;
	public static EntityManager getInstance()
	{
		if (instance == null)
		{
			instance = new EntityManager();
		}
		return instance;
	}
    public Dictionary<int, CharacterPrototype> monsterPrototypes;
    public Dictionary<int, CharacterPrototype> soliderPrototypes;
    public Dictionary<int, CharacterPrototype> towerPrototypes;
    public Dictionary<int, MonsterInfo> monsters;
    public Dictionary<int, SoliderInfo> soliders;
    public Dictionary<int, EffectInfo> effects;
    public Dictionary<int, TowerInfo> towers;
    //回收list
    public List<int> monsterDelList;
    public List<int> soliderDelList;
    public List<int> effectDelList;
    public List<int> bulletDelList;
    public List<int> towerDelList;
    //兵种序列ID
    public int monsterIndexId;
    public int soliderIndexId;
    public int effectIndexId;
    public int bulletIndexId;
    public int towerIndexId;
	private EntityManager()
	{
        monsterPrototypes = new Dictionary<int, CharacterPrototype>();
        soliderPrototypes = new Dictionary<int, CharacterPrototype>();
        towerPrototypes = new Dictionary<int, CharacterPrototype>();
        monsters = new Dictionary<int, MonsterInfo>();
        soliders = new Dictionary<int, SoliderInfo>();
        effects = new Dictionary<int, EffectInfo>();
        towers = new Dictionary<int, TowerInfo>();
        eventDispatcher = new MiniEventDispatcher();
        monsterDelList = new List<int>();
        soliderDelList = new List<int>();
        effectDelList = new List<int>();
        bulletDelList = new List<int>();
        towerDelList = new List<int>();
        monsterIndexId = 0;
        soliderIndexId = 0;
        effectIndexId = 0;
        bulletIndexId = 0;
        towerIndexId = 0;
	}

    public MonsterInfo AddMonster(int monsterId, PathInfo pathInfo)
    {
        monsterIndexId += 1;
        if (!monsterPrototypes.ContainsKey(monsterId))
        {
            monsterPrototypes.Add(monsterId, new CharacterPrototype(monsterId, CharacterType.Monster));
        }
        CharacterPrototype proto = monsterPrototypes[monsterId];
        MonsterInfo charInfo = proto.CloneMonster(monsterIndexId, pathInfo);
        monsters.Add(monsterIndexId, charInfo);
        this.eventDispatcher.Broadcast("AddMonster", charInfo);
        return charInfo;
    }

    public SoliderInfo AddSolider(int soliderId)
    {
        soliderIndexId += 1;
        if (!soliderPrototypes.ContainsKey(soliderId))
        {
            soliderPrototypes.Add(soliderId, new CharacterPrototype(soliderId, CharacterType.Solider));
        }
        CharacterPrototype proto = soliderPrototypes[soliderId];
        SoliderInfo charInfo = proto.CloneSolider(soliderIndexId);
        soliders.Add(soliderIndexId, charInfo);
        this.eventDispatcher.Broadcast("AddSolider", charInfo);
        return charInfo;
    }
    //添加静态特效
    public EffectInfo AddStaticEffect(int effectId, int effectType)
    {
        effectIndexId += 1;
        EffectInfo effectInfo = new StaticEffectInfo(effectIndexId, effectId);      
        effects.Add(effectIndexId, effectInfo);
        this.eventDispatcher.Broadcast("AddEffect", effectInfo);
        return effectInfo;
    }
    //添加动态特效
    public EffectInfo AddMoveEffect(int effectId, CharacterInfo charInfo, CharacterInfo targetInfo, float speed, int pathType, int triggerGroupId = 0)
    {
        effectIndexId += 1;
        EffectInfo effectInfo;
        if (pathType == 2)
            effectInfo = new BezierEffectInfo(bulletIndexId, effectId, charInfo, targetInfo, speed, triggerGroupId);
        else
            effectInfo = new StraightEffectInfo(bulletIndexId, effectId, charInfo, targetInfo, speed, triggerGroupId);
        effects.Add(effectIndexId, effectInfo);
        this.eventDispatcher.Broadcast("AddEffect", effectInfo);
        return effectInfo;
    }
    //添加防御塔
    public TowerInfo AddTower(int towerId)
    {
        towerIndexId++;
        if (!towerPrototypes.ContainsKey(towerId))
        {
            towerPrototypes.Add(towerId, new CharacterPrototype(towerId, CharacterType.Tower));
        }
        CharacterPrototype proto = towerPrototypes[towerId];
        TowerInfo towerInfo = proto.CloneTower(towerIndexId);
        towers.Add(towerIndexId, towerInfo);
        this.eventDispatcher.Broadcast("AddTower", towerInfo);
        return towerInfo;
    }
    public void RemoveTower(int towerId)
    {
        foreach (int key in towers.Keys)
        {
            if (towers[key].Id == towerId)
            {
                towerDelList.Add(key);
                this.eventDispatcher.Broadcast("RemoveTower", towerId);
            }
        }
    }
    public void RemoveMonster(int monsterId)
    {
        Debug.Log("RemoveMonster!");
        foreach (int key in monsters.Keys)
        {
            if (monsters[key].Id == monsterId)
            {
                monsterDelList.Add(key);
                this.eventDispatcher.Broadcast("RemoveMonster", monsterId);
            }
        }
    }

    public void RemoveSolider(int soliderId)
    {
        foreach (int key in soliders.Keys)
        {
            if (soliders[key].Id == soliderId)
            {
                soliderDelList.Add(key);
                this.eventDispatcher.Broadcast("RemoveSolider", soliderId);
            }
        }
    }

    public void RemoveEffect(int effectId)
    {
        foreach (int key in effects.Keys)
        {
            if (effects[key].Id == effectId)
            {
                effectDelList.Add(key);
                this.eventDispatcher.Broadcast("RemoveEffect", effectId);
            }
        }
    }

    public void Update()
    {
        foreach (int key in monsters.Keys)
        {
            monsters[key].Update();
        }
        foreach (int key in soliders.Keys)
        {
            soliders[key].Update();
        }
        foreach (int key in effects.Keys)
        {
            effects[key].Update();
        }
        foreach (int key in towers.Keys)
        {
            towers[key].Update();
        }
        CollectDelInfo();
    }
    //回收要删除的实体
    public void CollectDelInfo()
    {
        if (monsterDelList.Count > 0)
        {
            foreach (int indexId in monsterDelList)
            {
                monsters.Remove(indexId);
            }
            monsterDelList.Clear();
        }
        if (soliderDelList.Count > 0)
        {
            foreach (int indexId in soliderDelList)
            {
                soliders.Remove(indexId);
            }
            soliderDelList.Clear();
        }
        if (effectDelList.Count > 0)
        {
            foreach (int indexId in effectDelList)
            {
                effects.Remove(indexId);
            }
            effectDelList.Clear();
        }
        if (towerDelList.Count > 0)
        {
            foreach (int indexId in towerDelList)
            {
                towers.Remove(indexId);
            }
            towerDelList.Clear();
        }
    }

    public void Release()
    {

    }

    public List<SoliderInfo> GetSoliderInfo()
    {
        List<SoliderInfo> list = new List<SoliderInfo>();
        foreach (int key in soliders.Keys)
        {
            list.Add(soliders[key]);
        }
        return list;
    }

    public List<MonsterInfo> GetMonsterInfo()
    {
        List<MonsterInfo> list = new List<MonsterInfo>();
        foreach (int key in monsters.Keys)
        {
            list.Add(monsters[key]);
        }
        return list;
    }
}
