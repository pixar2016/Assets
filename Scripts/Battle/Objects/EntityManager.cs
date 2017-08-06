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
    //暂时记录增加或者回收的单位
    public Dictionary<int, MonsterInfo> monsterTempList;
    public Dictionary<int, SoliderInfo> soliderTempList;
    public Dictionary<int, EffectInfo> effectTempList;
    public Dictionary<int, TowerInfo> towerTempList;
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
        monsterTempList = new Dictionary<int, MonsterInfo>();
        soliderTempList = new Dictionary<int, SoliderInfo>();
        effectTempList = new Dictionary<int, EffectInfo>();
        towerTempList = new Dictionary<int, TowerInfo>();
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
        //monsters.Add(monsterIndexId, charInfo);
        //标记为“添加”
        charInfo.SetDirtySign(false);
        monsterTempList.Add(monsterIndexId, charInfo);
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
        //soliders.Add(soliderIndexId, charInfo);
        //标记为“添加”
        charInfo.SetDirtySign(false);
        soliderTempList.Add(soliderIndexId, charInfo);
        this.eventDispatcher.Broadcast("AddSolider", charInfo);
        return charInfo;
    }
    //添加静态特效
    public EffectInfo AddStaticEffect(int effectId, Vector3 pos)
    {
        effectIndexId += 1;
        EffectInfo effectInfo = new StaticEffectInfo(effectIndexId, effectId);
        effectInfo.SetPosition(pos);
        //effects.Add(effectIndexId, effectInfo);
        //标记为“添加”
        effectInfo.SetDirtySign(false);
        effectTempList.Add(effectIndexId, effectInfo);
        this.eventDispatcher.Broadcast("AddEffect", effectInfo);
        effectInfo.UpdatePositionToView();
        return effectInfo;
    }
    //添加动态特效
    public EffectInfo AddMoveEffect(int effectId, CharacterInfo charInfo, CharacterInfo targetInfo, float speed, int pathType, int triggerGroupId = 0)
    {
        effectIndexId += 1;
        EffectInfo effectInfo;
        if (pathType == 2)
            effectInfo = new BezierEffectInfo(effectIndexId, effectId, charInfo, targetInfo, speed, triggerGroupId);
        else
            effectInfo = new StraightEffectInfo(effectIndexId, effectId, charInfo, targetInfo, speed, triggerGroupId);
        //effects.Add(effectIndexId, effectInfo);
        //标记为“添加”
        effectInfo.SetDirtySign(false);
        effectTempList.Add(effectIndexId, effectInfo);
        this.eventDispatcher.Broadcast("AddEffect", effectInfo);
        effectInfo.UpdatePositionToView();
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
        //towers.Add(towerIndexId, towerInfo);
        //标记为“添加”
        towerInfo.SetDirtySign(false);
        towerTempList.Add(towerIndexId, towerInfo);
        this.eventDispatcher.Broadcast("AddTower", towerInfo);
        return towerInfo;
    }
    public void RemoveTower(int towerId)
    {
        foreach (int key in towers.Keys)
        {
            if (towers[key].Id == towerId)
            {
                //towerDelList.Add(key);
                if (towerTempList.ContainsKey(key))
                {
                    towerTempList[key].SetDirtySign(true);
                }
                else
                {
                    towerTempList.Add(key, towers[key]);
                    towers[key].SetDirtySign(true);
                }
                this.eventDispatcher.Broadcast("RemoveTower", towerId);
            }
        }
    }
    public void RemoveMonster(int monsterId)
    {
        foreach (int key in monsters.Keys)
        {
            if (monsters[key].Id == monsterId)
            {
                if (monsterTempList.ContainsKey(key))
                {
                    monsterTempList[key].SetDirtySign(true);
                }
                else
                {
                    monsterTempList.Add(key, monsters[key]);
                    monsters[key].SetDirtySign(true);
                }
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
                if (soliderTempList.ContainsKey(key))
                {
                    soliderTempList[key].SetDirtySign(true);
                }
                else
                {
                    soliderTempList.Add(key, soliders[key]);
                    soliders[key].SetDirtySign(true);
                }
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
                if (effectTempList.ContainsKey(key))
                {
                    effectTempList[key].SetDirtySign(true);
                }
                else
                {
                    effectTempList.Add(key, effects[key]);
                    effects[key].SetDirtySign(true);
                }
                
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
        CollectDirtyUnit();
    }
    //处理带有脏标记的单位
    public void CollectDirtyUnit()
    {
        if (monsterTempList.Count > 0)
        {
            foreach (int key in monsterTempList.Keys)
            {
                MonsterInfo monster = monsterTempList[key];
                if (monster.dirtySign > 0)
                {
                    monsters.Add(key, monster);
                }
                else if (monster.dirtySign < 0)
                {
                    monsters.Remove(key);
                }
                monster.dirtySign = 0;
            }
            monsterTempList.Clear();
        }
        if (soliderTempList.Count > 0)
        {
            foreach (int key in soliderTempList.Keys)
            {
                SoliderInfo solider = soliderTempList[key];
                if (solider.dirtySign > 0)
                {
                    soliders.Add(key, solider);
                }
                else if (solider.dirtySign < 0)
                {
                    soliders.Remove(key);
                }
                solider.dirtySign = 0;
            }
            soliderTempList.Clear();
        }
        if (effectTempList.Count > 0)
        {
            foreach (int key in effectTempList.Keys)
            {
                EffectInfo effect = effectTempList[key];
                if (effect.dirtySign > 0)
                {
                    effects.Add(key, effect);
                }
                else if (effect.dirtySign < 0)
                {
                    effects.Remove(key);
                }
                effect.dirtySign = 0;
            }
            effectTempList.Clear();
        }
        if (towerTempList.Count > 0)
        {
            foreach (int key in towerTempList.Keys)
            {
                TowerInfo tower = towerTempList[key];
                if (tower.dirtySign > 0)
                {
                    towers.Add(key, tower);
                }
                else if (tower.dirtySign < 0)
                {
                    towers.Remove(key);
                }
                tower.dirtySign = 0;
            }
            towerTempList.Clear();
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
