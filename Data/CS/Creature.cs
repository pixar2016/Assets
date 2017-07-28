//-----------------------------------------------------
//这段代码是工具生成，不要轻易修改！！！
//-----------------------------------------------------
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;

public class D_Creature
{
	public int _id;
	public string _name;
	public string _modelName;
	public int _hp;
	public int _attackSpeed;
	public int _attackDamage;
	public int _defenceType;
	public int _attackRange;
	public int _attackId;
	public int _speed;
	public string _description;
	public int _attackType;
	public int _skill1;
	public int _skill2;
	public int _skill3;
	public int _skill4;

}
public class J_Creature
{
    private static Dictionary<int, D_Creature> infoDict = new Dictionary<int, D_Creature>();
    private static string tableName = "";
    public static void LoadConfig()
    {
        if (infoDict.Count > 0)
        {
            return;
        }
        tableName = "Creature";
        string infos = FileUtils.LoadFile(Application.dataPath, "Data/Json/Creature.json");
		Init(infos);
    }

    private static void Init(string _info)
    {
        List<object> jsonObjects = MiniJSON.Json.Deserialize(_info) as List<object>;
        for (int i = 0; i < jsonObjects.Count; i++)
        {
            D_Creature info = new D_Creature();
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
			if(jsonObject["modelName"] != null){
				info._modelName = jsonObject["modelName"].ToString();
			}
			else{
				info._modelName = "";
			}
			if(jsonObject["hp"] != null){
				info._hp = int.Parse(jsonObject["hp"].ToString());
			}
			else{
				info._hp = 0;
			}
			if(jsonObject["attackSpeed"] != null){
				info._attackSpeed = int.Parse(jsonObject["attackSpeed"].ToString());
			}
			else{
				info._attackSpeed = 0;
			}
			if(jsonObject["attackDamage"] != null){
				info._attackDamage = int.Parse(jsonObject["attackDamage"].ToString());
			}
			else{
				info._attackDamage = 0;
			}
			if(jsonObject["defenceType"] != null){
				info._defenceType = int.Parse(jsonObject["defenceType"].ToString());
			}
			else{
				info._defenceType = 0;
			}
			if(jsonObject["attackRange"] != null){
				info._attackRange = int.Parse(jsonObject["attackRange"].ToString());
			}
			else{
				info._attackRange = 0;
			}
			if(jsonObject["attackId"] != null){
				info._attackId = int.Parse(jsonObject["attackId"].ToString());
			}
			else{
				info._attackId = 0;
			}
			if(jsonObject["speed"] != null){
				info._speed = int.Parse(jsonObject["speed"].ToString());
			}
			else{
				info._speed = 0;
			}
			if(jsonObject["description"] != null){
				info._description = jsonObject["description"].ToString();
			}
			else{
				info._description = "";
			}
			if(jsonObject["attackType"] != null){
				info._attackType = int.Parse(jsonObject["attackType"].ToString());
			}
			else{
				info._attackType = 0;
			}
			if(jsonObject["skill1"] != null){
				info._skill1 = int.Parse(jsonObject["skill1"].ToString());
			}
			else{
				info._skill1 = 0;
			}
			if(jsonObject["skill2"] != null){
				info._skill2 = int.Parse(jsonObject["skill2"].ToString());
			}
			else{
				info._skill2 = 0;
			}
			if(jsonObject["skill3"] != null){
				info._skill3 = int.Parse(jsonObject["skill3"].ToString());
			}
			else{
				info._skill3 = 0;
			}
			if(jsonObject["skill4"] != null){
				info._skill4 = int.Parse(jsonObject["skill4"].ToString());
			}
			else{
				info._skill4 = 0;
			}

            infoDict.Add(info._id, info);
        }
        /*
        foreach (KeyValuePair<int, D_Creature> info in infoDict)
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
    public static D_Creature GetData(int _id)
    {
        D_Creature data = null;
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
    public static List<D_Creature> ToList()
    {
        List<D_Creature> list =  new List<D_Creature>();
        foreach (KeyValuePair<int,D_Creature> info in infoDict)
        {
            list.Add(info.Value);
        }
        return list;
    }

}
