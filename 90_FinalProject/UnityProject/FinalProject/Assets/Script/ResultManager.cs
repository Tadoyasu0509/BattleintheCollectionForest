using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{

    [SerializeField] private PhotonView _photonView;

    private int Winner;

    //public GameObject ResultObj;
    public Text WinnerText;

    //public GameObject WinnerText;

    public GameObject obj_GameManager;
    private GameManager code_GameManager;


    private void Start()
    {
        Winner = GameManager.ForResult();
        Debug.Log("ResultWindow : Player" + Winner + " Win!");
        //code_GameManager = obj_GameManager.GetComponent<GameManager>();
        //Winner = code_GameManager.ForResult();
        WinnerText.text = "Player"+Winner+" Win!";
    }

    public void SelectRestartButton()
    {
        _photonView.RPC("GoTonextGame", PhotonTargets.MasterClient);
    }

    [PunRPC]
    public void GoTonextGame()
    {
        PhotonNetwork.LoadLevel("TestGame");
    }
}
