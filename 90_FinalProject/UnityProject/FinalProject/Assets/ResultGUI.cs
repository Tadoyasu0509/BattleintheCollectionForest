using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultGUI : MonoBehaviour {

    private int Winner;

    //public GameObject ResultObj;
    public Text WinnerText;

    //public GameObject WinnerText;

    public GameObject obj_GameManager;
    //private GameManager code_GameManager;

    // Use this for initialization
    void Start () {
        //WinnerText = GetComponent<Text>();
        Winner = GameManager.ForResult();
        Debug.Log("ResultWindow(GUI) : Player" + Winner + " Win!");
        //code_GameManager = obj_GameManager.GetComponent<GameManager>();
        //Winner = code_GameManager.ForResult();
        //WinnerText.text = "Player" + Winner + " Win!";
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log(Winner);
        WinnerText.text = "Player" + Winner + " Win!";
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            //stream.SendNext(HP);
            stream.SendNext(Winner);
        }


        else
        {   //受信処理

            //HP = (float)stream.ReceiveNext();
            Winner = (int)stream.ReceiveNext();
        }

    }
}
