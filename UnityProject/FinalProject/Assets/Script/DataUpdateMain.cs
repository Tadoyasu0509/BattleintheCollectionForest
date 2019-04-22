/// <summary>
/// Manager main.
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;	//Text
using MiniJSON;		// Json
using System;		// File
using System.IO;	// File
using System.Text;	// File


public class DataUpdateMain : MonoBehaviour
{
    public static float speed = 2;
    public static float jumpPower = 2000;
    public static int Hp = 100;

    public Text DisplayField;
    private float[] arrayBox = new float[3];

    List<MemberData> memberList = null;

    /// <summary>
    /// Start this instance.
    /// </summary>
    void Start()
    {

    }

    /// <summary>
    /// Update this instance.
    /// </summary>
    void Update()
    {
        
    }

    /// <summary>
    /// Raises the click clear display event.
    /// </summary>
    public void OnClickClearDisplay()
    {

        DisplayField.text = " ";

    }

    /// <summary>
    /// Raises the click get json from www event.
    /// </summary>
    public void OnClickGetJsonFromWww()
    {

        DisplayField.text = "wait...";
        GetJsonFromWww();

    }

    /// <summary>
    /// Raises the click show member list
    /// </summary>
    public void OnClickShowMemberList()
    {
        string sStrOutput = "";

        if (null == memberList)
        {
            sStrOutput = "no list !";
        }
        else
        {
            //リストの内容を表示
            foreach (MemberData memberOne in memberList)
            {
                speed = memberOne.speed;
                jumpPower = memberOne.jump;
                Hp = (int)memberOne.HP;

                arrayBox = new float[]{speed, jumpPower, Hp };

                sStrOutput += string.Format("name:{0} Speed:{1} JumpPower:{2}  HP:{3} \n", memberOne.name, memberOne.speed, memberOne.jump, memberOne.HP);
                DisplayField.text = "speed:" + speed + "\n jump:" + jumpPower + "\n HP:" + Hp;
                WritteCSV();
            }
        }

        DisplayField.text = "speed:"+speed+"\n jump:"+jumpPower+"\n HP:"+Hp;


    }


    /// <summary>
    /// Gets the json from www.
    /// </summary>
    private void GetJsonFromWww()
    {

        // APIが設置してあるURLパス
        string sTgtURL = "http://localhost/characterdataapi/intcharacters/getMessages";


        // API を呼んだ際に想定されるレスポンス
        // [{"name":"\u3072\u3068\u308a\u3081","age":123,"hobby":"\u30b4\u30eb\u30d5"},{"name":"\u3075\u305f\u308a\u3081","age":25,"hobby":"walk"},{"name":"\u3055\u3093\u306b\u3093\u3081","age":77,"hobby":"\u5c71"}]
        //

        // Wwwを利用して json データ取得をリクエストする
        StartCoroutine(
            DownloadJson(
                sTgtURL,                // APIのリクエストURL
                CallbackWwwSuccess,     // APIコールが成功した際に呼ばれる関数を指定
                CallbackWwwFailed       // APIコールが失敗した際に呼ばれる関数を指定
            )
        );

    }
    /// <summary>
    /// Callbacks the www success.
    /// </summary>
    /// <param name="response">Response.</param>
    private void CallbackWwwSuccess(string response)
    {

        //Json の内容を MemberData型のリストとしてデコードする。
        memberList = MemberDataModel.DesirializeFromJson(response);

        //memberList ここにデコードされたメンバーリストが格納される。

        DisplayField.text = "Success!";
        OnClickShowMemberList();

    }
    /// <summary>
    /// Callbacks the www failed.
    /// </summary>
    private void CallbackWwwFailed()
    {

        // jsonデータ取得に失敗した
        DisplayField.text = "Www Failed";

    }

    /// <summary>
    /// Downloads the json.
    /// </summary>
    /// <returns>The json.</returns>
    /// <param name="sTgtURL">S tgt UR.</param>
    /// <param name="cbkSuccess">Cbk success.</param>
    /// <param name="cbkFailed">Cbk failed.</param>
    private IEnumerator DownloadJson(string sTgtURL, Action<string> cbkSuccess = null, Action cbkFailed = null)
    {

        // WWWを利用してリクエストを送る
        WWW www = new WWW(sTgtURL);

        // WWWレスポンス待ち
        yield return StartCoroutine(ResponceCheckForTimeOutWWW(www, 5.0f));

        if (www.error != null)
        {
            //レスポンスエラーの場合
            Debug.LogError(www.error);
            if (null != cbkFailed)
            {
                cbkFailed();
            }
        }
        else
        if (www.isDone)
        {
            // リクエスト成功の場合
            Debug.Log(string.Format("Success:{0}", www.text));
            if (null != cbkSuccess)
            {
                cbkSuccess(www.text);
            }
        }



    }
    /// <summary>
    /// Responces the check for time out WWW.
    /// </summary>
    /// <returns>The check for time out WWW.</returns>
    /// <param name="www">Www.</param>
    /// <param name="timeout">Timeout.</param>
    private IEnumerator ResponceCheckForTimeOutWWW(WWW www, float timeout)
    {
        float requestTime = Time.time;

        while (!www.isDone)
        {
            if (Time.time - requestTime < timeout)
            {
                yield return null;
            }
            else
            {
                Debug.LogWarning("TimeOut"); //タイムアウト
                break;
            }
        }
        yield return null;
    }

    private void WritteCSV()
    {
        string CSVFilePass = Application.dataPath + @"CSV\DBData.csv";

        using (StreamWriter write = new StreamWriter(CSVFilePass))
        {
            write.WriteLine(arrayBox);
        }
            
    }

    public static float Update_Speed()
    {
        return speed;
    }

    public static float Update_Jump()
    {
        return jumpPower;
    }

    public static int Update_HP()
    {
        return Hp;
    }
}
