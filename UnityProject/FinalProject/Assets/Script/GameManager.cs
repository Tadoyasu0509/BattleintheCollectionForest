using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject obj_Player;
    public GameObject obj_Item;
    private GameObject obj_Winner;
    public PlayerController code_PlayerController;
    private Vector3 Vec_Start = new Vector3(0f, 1f, 0f);
    private Quaternion Qua_Start = new Quaternion(0f,0f,0f,0f);
    private int Item_create = 10;

    public string playerName;

    public bool endFlag = false;
    public static int Winner = 0;

    public int ActivePlayer;

    PhotonView pView = null;

    // Use this for initialization
    private void Awake()
    {
        pView = GetComponent<PhotonView>();
    }

    void Start () {
        StartCreator();
        ActivePlayer = PhotonNetwork.playerList.Length;
    }

    // Update is called once per frame
    void Update () {

        int Item_count = GameObject.FindGameObjectsWithTag("Item").Length;

        if (Item_count < Item_create)
        {
            GameObject P_item = PhotonNetwork.Instantiate
                ("Item", new Vector3
                (
                    Random.Range(-50f, 50f), 
                    20f, 
                    Random.Range(-50, 50)
                ),
                Qua_Start, 0);
        }

        if(PhotonNetwork.isMasterClient == false)
        {
            return;
        }
        if(ActivePlayer <= 1)
        {
            PhotonNetwork.LoadLevel("Result");

            obj_Winner = GameObject.FindGameObjectWithTag("Player");
            code_PlayerController = obj_Winner.GetComponent<PlayerController>();
            Winner = code_PlayerController.PlayerID;

        }


    }

    void StartCreator()
    {
        GameObject P_obj = PhotonNetwork.Instantiate("Witch", Vec_Start, Quaternion.identity, 0);
        PhotonView photonView = P_obj.GetComponent<PhotonView>();
        Debug.LogWarning(photonView.owner.ID);

        endFlag = false;

        switch (photonView.owner.ID)
        {
            case 1:
                P_obj.transform.position = new Vector3(10f, 10f, 0f);
                break;
            case 2:
                P_obj.transform.position = new Vector3(0f, 10f, 10f);
                break;
            case 3:
                P_obj.transform.position = new Vector3(-10f, 10f, 0f);
                break;
            case 4:
                P_obj.transform.position = new Vector3(0f, 10f, -10f);
                break;
            default:
                P_obj.transform.position = new Vector3(0f, 1f, 0f);
                break;

        }
    }

    public static int ForResult()
    {
        return Winner;
    }

    public void ReStartButton()
    {
        PhotonNetwork.LoadLevel("TestGame");
    }

    /*
    [PunRPC]
    public void GoToResult()
    {
        PhotonNetwork.LoadLevel("Result");
    }
    */

    public void RPCPlayerLost()
    {
        pView.RPC("PlayerLost", PhotonTargets.All);
    }

    [PunRPC]
    public void PlayerLost()
    {
        ActivePlayer--;
        Debug.Log("PlayerLost");
    }

}
