using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniJSON;		// Json

/// <summary>
/// Json response manager.
/// </summary>
public class MemberDataModel
{

    /// <summary>
    /// Desirializes from json.
    /// MemberData型のリストがjsonに入っていると仮定して
    /// </summary>
    /// <returns>The from json.</returns>
    /// <param name="sStrJson">S string json.</param>
    static public List<MemberData> DesirializeFromJson(string sStrJson)
    {
        List<MemberData> ret = new List<MemberData>();
        MemberData tmp = null;

        // JSONデータは最初は配列から始まるので、Deserialize（デコード）した直後にリストへキャスト      
        IList jsonList = (IList)Json.Deserialize(sStrJson);

        // リストの内容はオブジェクトなので、辞書型の変数に一つ一つ代入しながら、処理
        foreach (IDictionary jsonOne in jsonList)
        {
            //新レコード解析開始

            tmp = new MemberData();    //レコード格納用変数を定義

            //該当するキー名が jsonOne に存在するか調べ、存在したら取得して変数に格納する。

            if (jsonOne.Contains("Name"))
            {
                tmp.name = (string)jsonOne["Name"];
            }
            if (jsonOne.Contains("Speed"))
            {
                tmp.speed = (long)jsonOne["Speed"];
            }
            if (jsonOne.Contains("Jump"))
            {
                tmp.jump = (long)jsonOne["Jump"];
            }
            if (jsonOne.Contains("Hp"))
            {
                tmp.HP = (long)jsonOne["Hp"];
            }


            //現レコード解析終了
            ret.Add(tmp);
            tmp = null;
        }
        return ret;
    }
}