using System;
using System.Collections.Generic;
using UnityEngine;

//子弹基类
public class BulletInfo
{
    //索引ID
    public int Id;
    //子弹ID
    public int bulletId;
    //子弹名称
    public string bulletName;
    //子弹速度
    public float speed;
    //子弹当前位置
    public Vector3 pos;
    //子弹当前角度
    public Vector3 angle;
    //发射子弹的塔或者兵种
    public CharacterInfo charInfo;
    public CharacterInfo targetInfo;
    //发射子弹的触发器Id
    public int triggerGroupId;

    public BulletInfo(int bulletIndexId, int _bulletId, CharacterInfo _charInfo, CharacterInfo _targetInfo, float _speed, int _triggerGroupId)
    {
        Id = bulletIndexId;
        bulletId = _bulletId;
        charInfo = _charInfo;
        targetInfo = _targetInfo;
        bulletName = "arrow";
        speed = _speed;
        pos = charInfo.GetPosition();
        angle = Vector3.zero;
        triggerGroupId = _triggerGroupId;
    }

    public Vector3 GetPosition()
    {
        return pos;
    }

    public Vector3 GetEulerAngles()
    {
        return angle;
    }

    public void SetPosition(float x, float y, float z)
    {
        pos = new Vector3(x, y, z);
    }

    public void Update()
    {
        Vector3 targetPos = targetInfo.GetPosition();
        if (Vector3.Distance(pos, targetPos) < 1)
        {
            this.charInfo.eventDispatcher.Broadcast("BulletReach", triggerGroupId);
            EntityManager.getInstance().RemoveBullet(this.Id);
        }
        else
        {
            angle.z = angle_360(Vector3.left, targetPos - pos);
            pos = Vector3.MoveTowards(pos, targetPos, speed * Time.deltaTime);
        }
    }

    private float angle_360(Vector3 _from, Vector3 _to)
    {
        Vector3 cross = Vector3.Cross(_from, _to);
        float angle;
        if (cross.z > 0)
            angle = Vector3.Angle(_from, _to);
        else
            angle = 360 - Vector3.Angle(_from, _to);
        return angle;
    }
    public void Release()
    {

    }
}

