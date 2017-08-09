//-----------------------------------------------------
//这段代码是工具生成，不要轻易修改！！！
//-----------------------------------------------------
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;

public class D_Effect
{
	public int _id;
	public string _name;
	public string _model;
	public int _loop;

}
public class J_Effect
{
    private static Dictionary<int, D_Effect> infoDict = new Dictionary<int, D_Effect>();
    private static string tableName = "";
    public static void LoadConfig()
    {
        if (infoDict.Count > 0)
        {
            return;
        }
        tableName = "Effect";
        string infos = FileUtils.LoadFile(Application.dataPath, "Data/Json/Effect.json");
		Init(infos);
    }

    private static void Init(string _info)
    {
        List<object> jsonObjects = MiniJSON.Json.Deserialize(_info) as List<object>;
        for (int i = 0; i < jsonObjects.Count; i++)
        {
            D_Effect info = new D_Effect();
			Dictionary<string, object> jsonObject = jsonObjects[i] as Dictionary<string, object>;
			
			if(jsonObject["id"] != null){
				info._id = int.Parse(jsonObject["id"].ToString());
			}
			else{
				info._id = 0;
			}
			if(jsonObject["name"] != null){
				info._name = jsonObject["name"].ToString();
			}
			else{
				info._name = "";
			}
			if(jsonObject["model"] != null){
				info._model = jsonObject["model"].ToString();
			}
			else{
				info._model = "";
			}
			if(jsonObject["loop"] != null){
				info._loop = int.Parse(jsonObject["loop"].ToString());
			}
			else{
				info._loop = 0;
			}

            infoDict.Add(info._id, info);
        }
        /*
        foreach (KeyValuePair<int, D_Effect> info in infoDict)
        {
            Debug.Log(">>>>>"+info.Value._id+":"+info.Value._name+":"+info.Value._desc+":"+info.Value._point+":"+info.Value._label+":"+info.Value._type+":"+info.Value._number+":"+info.Value._function+":"+info.Value._para+":"+info.Value._reward+":"+"<<<<<\n");
        }
        */
    }

	/// <summary>
    /// 将string拆分为int数组
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static int[] GetIntArray(string data)
    {
        string[] strArray = data.Split('|');
        int strLength = strArray.Length;
        int[] intArray = new int[strLength];
        int result;
        for (int i = 0; i < strLength; i++)
        {
            bool isSuccess = int.TryParse(strArray[i], out result);
            intArray[i] = isSuccess ? result : 0;
        }
        return intArray;
    }

    /// <summary>
    /// 通过key获取数据
    /// </summary>
    /// <param name="_id">字典key</param>
    /// <returns></returns>
    public static D_Effect GetData(int _id)
    {
        D_Effect data = null;
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
    public static List<D_Effect> ToList()
    {
        List<D_Effect> list =  new List<D_Effect>();
        foreach (KeyValuePair<int,D_Effect> info in infoDict)
        {
            list.Add(info.Value);
        }
        return list;
    }

}
