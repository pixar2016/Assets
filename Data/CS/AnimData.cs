//-----------------------------------------------------
//这段代码是工具生成，不要轻易修改！！！
//-----------------------------------------------------
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;

public class D_AnimData
{
	public int _id;
	public string _modelName;
	public string _animName;
	public string _FrameName;
	public int _startFrame;
	public int _endFrame;
	public float _delta;
	public int _loop;

}
public class J_AnimData
{
    private static Dictionary<int, D_AnimData> infoDict = new Dictionary<int, D_AnimData>();
    private static string tableName = "";
    public static void LoadConfig()
    {
        if (infoDict.Count > 0)
        {
            return;
        }
        tableName = "AnimData";
        string infos = FileUtils.LoadFile(Application.dataPath, "Data/Json/AnimData.json");
		Init(infos);
    }

    private static void Init(string _info)
    {
        List<object> jsonObjects = MiniJSON.Json.Deserialize(_info) as List<object>;
        for (int i = 0; i < jsonObjects.Count; i++)
        {
            D_AnimData info = new D_AnimData();
			Dictionary<string, object> jsonObject = jsonObjects[i] as Dictionary<string, object>;
			
			if(jsonObject["id"] != null){
				info._id = int.Parse(jsonObject["id"].ToString());
			}
			if(jsonObject["modelName"] != null){
				info._modelName = jsonObject["modelName"].ToString();
			}
			if(jsonObject["animName"] != null){
				info._animName = jsonObject["animName"].ToString();
			}
			if(jsonObject["FrameName"] != null){
				info._FrameName = jsonObject["FrameName"].ToString();
			}
			if(jsonObject["startFrame"] != null){
				info._startFrame = int.Parse(jsonObject["startFrame"].ToString());
			}
			if(jsonObject["endFrame"] != null){
				info._endFrame = int.Parse(jsonObject["endFrame"].ToString());
			}
			if(jsonObject["delta"] != null){
				info._delta = float.Parse(jsonObject["delta"].ToString());
			}
			if(jsonObject["loop"] != null){
				info._loop = int.Parse(jsonObject["loop"].ToString());
			}

            infoDict.Add(info._id, info);
        }
        /*
        foreach (KeyValuePair<int, D_AnimData> info in infoDict)
        {
            Debug.Log(">>>>>"+info.Value._id+":"+info.Value._name+":"+info.Value._desc+":"+info.Value._point+":"+info.Value._label+":"+info.Value._type+":"+info.Value._number+":"+info.Value._function+":"+info.Value._para+":"+info.Value._reward+":"+"<<<<<\n");
        }
        */
    }

    /// <summary>
    /// 通过key获取数据
    /// </summary>
    /// <param name="_id">字典key</param>
    /// <returns></returns>
    public static D_AnimData GetData(int _id)
    {
        D_AnimData data = null;
        if (infoDict.ContainsKey(_id))
        {
            data = infoDict[_id];
        }
        else
        {
            Debug.Log(">>>>>table:" + tableName+" id:"+_id+" is null<<<<<\n");
        }
        return data;
    }
    /// <summary>
    /// 获取字典长度
    /// </summary>
    /// <returns></returns>
    public static int GetCount()
    {
        return infoDict.Count;
    }
    /// <summary>
    /// 把字典转换成List
    /// </summary>
    /// <returns></returns>
    public static List<D_AnimData> ToList()
    {
        List<D_AnimData> list =  new List<D_AnimData>();
        foreach (KeyValuePair<int,D_AnimData> info in infoDict)
        {
            list.Add(info.Value);
        }
        return list;
    }

}
