using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using LitJson;
using Hero;
using CoreLib.Timer;
public class Test1 : MonoBehaviour {

    Animate animate;

    MonsterInfo charInfo;
    SoliderInfo soliderInfo;
    List<SoliderInfo> soliderList;

    CTimerSystem m_TimerSystem = null;

    public TowerInfo tower;
    EffectInfo baseEffect;
    public MonsterInfo monster;
	// Use this for initialization
    void Awake()
    {
        GameLoader loader = GameObject.Find("GameLoader").GetComponent<GameLoader>();
        loader.Initialize();
    }
	void Start () {
        J_Map.LoadConfig();
        J_Creature.LoadConfig();
        J_SkillEvent.LoadConfig();
        J_Skill.LoadConfig();
        J_Tower.LoadConfig();
        J_AnimData.LoadConfig();
        J_Effect.LoadConfig();
        J_ModelResource.LoadConfig();

        EntityManager.getInstance();
        EntityViewManager.getInstance();
        
        //DataPreLoader.getInstance().LoadData("Monsters");
        //根据图片信息txt将图片里帧信息分离出来
        //SpriteFrameCache.getInstance().addSpriteFrameFromFile("Resources/Config/monster1.txt");
        SpriteFrameCache.getInstance().addSpriteFrameFromFile("Resources/Config/Helper.txt");
        SpriteFrameCache.getInstance().addSpriteFrameFromFile("Resources/Config/Monster1.txt");
        SpriteFrameCache.getInstance().addSpriteFrameFromFile("Resources/Config/Monster2.txt");
        SpriteFrameCache.getInstance().addSpriteFrameFromFile("Resources/Config/Solider1.txt");
        SpriteFrameCache.getInstance().addSpriteFrameFromFile("Resources/Config/Tower1.txt");
        SpriteFrameCache.getInstance().addSpriteFrameFromFile("Resources/Config/TowerShooter.txt");
        AnimationCache animCache = AnimationCache.getInstance();

        List<D_AnimData> animList = J_AnimData.ToList();
        int count = animList.Count;
        for (int i = 0; i < count; i++)
        {
            D_AnimData animData = animList[i];
            bool loop = (animData._loop == 1?true:false);
            //无动作动画
            if (animData._animName == "")
            {
                animCache.addAnimation(
                    animCache.createAnimation(animData._FrameName, animData._startFrame, animData._endFrame, animData._delta, loop, animData._xoffset, animData._yoffset),
                    animData._modelName
                );
            }
            else
            {
                animCache.addAnimation(
                    animCache.createAnimation(animData._FrameName, animData._startFrame, animData._endFrame, animData._delta, loop, animData._xoffset, animData._yoffset),
                    animData._modelName,
                    animData._animName
                );
            }
            
        }

        GameObject uiroot = GameObject.Find("Canvas").gameObject;
        UiManager.Instance.Init(uiroot);

        PathLoader pathloader = new PathLoader();
        pathloader.LoadPath("level1");
        PathInfo path = pathloader.GetPath("1");
        //path.PrintAllPoint();

        
        //charInfo.ChangeState("move");
        BattleFingerEvent.getInstance().ChangeState("start");
        //tower = EntityManager.getInstance().AddTower(100);
        //tower.SetPosition(0, 0, 0);
        //tower.ChangeState("idle");
        //CharacterInfo charInfo = EntityManager.getInstance().AddSolider(50001);
        //charInfo.SetPosition(0, 0, 0);
        //monster = EntityManager.getInstance().AddMonster(10001, path);
        //monster.SetPosition(0, 0, 0);
        //tower = EntityManager.getInstance().AddTower(11);
        //tower.SetPosition(0, 0, 0);
        //tower.ChangeState("idle");
        //tower = EntityManager.getInstance().AddTower(12);
        //tower.SetPosition(0, 0, 0);
        //tower.ChangeState("idle");
        //tower = EntityManager.getInstance().AddTower(13);
        //tower.SetPosition(0, 0, 0);
        //tower.ChangeState("idle");
        tower = EntityManager.getInstance().AddTower(15);
        tower.SetPosition(0, 0, 0);
        tower.ChangeState("idle");
        //tower = EntityManager.getInstance().AddTower(15);
        //tower.SetPosition(0, 0, 0);
        //tower.ChangeState("idle");

        baseEffect = EntityManager.getInstance().AddStaticEffect(17, Vector3.zero);


        //GameManager.getInstance().LoadLevel(1);
        //GameManager.getInstance().StartGame();

        monster = EntityManager.getInstance().AddMonster(10001, path);
        monster.SetPosition(100, 5, 0);
        //monster = EntityManager.getInstance().AddMonster(10001, path);
        //monster.SetPosition(250, 0, 0);
        //monster.SetPosition(116, -100, 0);
        //monster.ChangeState("move");

        //CharacterInfo charInfo = EntityManager.getInstance().AddSolider(50001);
        //charInfo.SetPosition(-180, -150, 0);
        //charInfo.DoAction("idle");
        //charInfo = EntityManager.getInstance().AddSolider(50001);
        //charInfo.SetPosition(-170, -150, 0);
        //charInfo.DoAction("idle");
    }

    public void StartAI()
    {
        Debug.Log("StartAI");
        if (null == m_TimerSystem)
        {
            m_TimerSystem = new CTimerSystem();
            m_TimerSystem.Create();

        }
        m_TimerSystem.CreateTimer(1, 1000, UpdateMonsterAI);
    }

    public void UpdateMonsterAI(uint nTimeID)
    {
        Debug.Log("UpdateMonsterAI");
        foreach (SoliderInfo solider in soliderList)
        {
            if (!solider.IsDead())
            {
                solider.RunAI();
            }
        }
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.W))
        {
            charInfo.DoAction("stuck_start");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            charInfo.DoAction("stuck_end");
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            charInfo.ChangeState("attack");
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            charInfo.ChangeState("move");
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            monster.SetPosition(100, 0, 0);
        }
        EntityManager.getInstance().Update();
        EntityViewManager.getInstance().Update();
        TriggerManager.getInstance().Update(Time.deltaTime);
        if (null != m_TimerSystem)
        {
            m_TimerSystem.UpdateTimer();
        }
        GameManager.getInstance().Update();
//        animate.OnUpdate(Time.deltaTime);
	}
}
