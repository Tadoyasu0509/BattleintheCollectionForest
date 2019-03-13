using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartWindowManager : MonoBehaviour {

    private const string PHOTON_GAME_VER = "ver1.0";
    private const int PHOTON_SEND_RATE = 30;        //Photonに送るパケット数の設定

    private bool f_Conect = false;

    [SerializeField] private Text roomName;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //---------------------------------------------
    // photon callback

    /// <summary>
    /// event:photonに接続した
    /// </summary>
    public void OnConnectedToPhoton()
    {
        Debug.Log("OnConnectedToPhoton");
    }

    /// <summary>
    /// event:photonが切断した
    /// </summary>
    public void OnDisconnectedFromPhoton()
    {
        Debug.Log("OnDisconnectedFromPhoton");
    }

    /// <summary>
    /// event:接続失敗
    /// </summary>
    public void OnConnectionFail()
    {
        Debug.Log("OnConnectionFail");
    }

    /// <summary>
    /// event:photon接続失敗
    /// </summary>
    /// <param name="parameters">Parameters.</param>
    public void OnFailedToConnectToPhoton(object parameters)
    {
        Debug.Log("OnFailedToConnectToPhoton");
    }

    /// <summary>
    /// event:ロビー入室
    /// </summary>
    public void OnJoinedLobby()
    {
        Debug.Log("OnJoinedLobby");
    }

    /// <summary>
    /// event:ロビー退室
    /// </summary>
    public void OnLeftLobby()
    {
        Debug.Log("OnLeftLobby");
    }

    /// <summary>
    /// Raises the connected to master event.
    /// autoJoinLobby が true 時には OnJoinedLobby が代わりに呼ばれる。
    /// </summary>
    public void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster");
    }

    /// <summary>
    /// event:ルームリストが更新された
    /// </summary>
    public void OnReceivedRoomListUpdate()
    {
        Debug.Log("OnReceivedRoomListUpdate");
    }

    /// <summary>
    /// event:ルーム作成
    /// </summary>
    public void OnCreatedRoom()
    {
        Debug.Log("OnCreatedRoom");
        Debug.Log(string.Format("Name:{0}", PhotonNetwork.room.Name));
    }

    /// <summary>
    /// event:ルーム作成失敗
    /// </summary>
    public void OnPhotonCreateRoomFailed()
    {
        Debug.Log("OnPhotonCreateRoomFailed");
    }

    /// <summary>
    /// event:ルーム入室
    /// </summary>
    public void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");
        Debug.Log(string.Format("Name:{0}", PhotonNetwork.room.Name));
    }

    /// <summary>
    /// event:ルーム入室失敗
    /// </summary>
    /// <param name="cause">Cause.</param>
    public void OnPhotonJoinRoomFailed(object[] cause)
    {
        Debug.Log("OnPhotonJoinRoomFailed");
    }

    /// <summary>
    /// event:ランダム入室失敗
    /// </summary>
    public void OnPhotonRandomJoinFailed()
    {
        Debug.Log("OnPhotonRandomJoinFailed");
    }

    /// <summary>
    /// event:ルーム退室コールバック
    /// </summary>
    public void OnLeftRoom()
    {
        Debug.Log("OnLeftRoom");
    }

    /// <summary>
    /// event:誰かプレイヤーが接続された
    /// </summary>
    /// <param name="player">Player.</param>
    public void OnPhotonPlayerConnected(PhotonPlayer player)
    {
        Debug.Log("OnPhotonPlayerConnected");
    }

    /// <summary>
    /// event:誰かプレイヤーの接続が切れた
    /// </summary>
    /// <param name="player">Player.</param>
    public void OnPhotonPlayerDisconnected(PhotonPlayer player)
    {
        Debug.Log("OnPhotonPlayerDisconnected");
    }

    /// <summary>
    /// event:マスタークライアントが切り替わった
    /// </summary>
    /// <param name="player">Player.</param>
    public void OnMasterClientSwitched(PhotonPlayer player)
    {
        Debug.Log("OnMasterClientSwitched");
    }

    public void Button_ConnectPhoton()
    {

        //Debug.Log("ConnectPhoton:" + f_Conect);

        PhotonNetwork.ConnectUsingSettings(PHOTON_GAME_VER);

        //1秒間に送信するパケット数が決まっているのでそれを変更する。
        PhotonNetwork.sendRate = PHOTON_SEND_RATE;
        PhotonNetwork.sendRateOnSerialize = PHOTON_SEND_RATE;

        //シーン遷移を同期する
        PhotonNetwork.automaticallySyncScene = true;
        f_Conect = true;
        Debug.Log("ConnectPhoton:" + f_Conect);

        /*
        {

            PhotonNetwork.ConnectUsingSettings(PHOTON_GAME_VER);

            //1秒間に送信するパケット数が決まっているのでそれを変更する。
            PhotonNetwork.sendRate = PHOTON_SEND_RATE;
            PhotonNetwork.sendRateOnSerialize = PHOTON_SEND_RATE;

            //シーン遷移を同期する
            PhotonNetwork.automaticallySyncScene = true;
            f_Conect = true;
            Debug.Log("ConnectPhoton:" + f_Conect);


        }
        */

    }

    public void Button_CreatRoom()
    {
        Debug.Log("OnCreatRoom");
        Photon_CreateRoom(roomName.text);

    }

    private void Photon_CreateRoom(string roomName)
    {
        Debug.Log(string.Format("CreateRoom{0}", roomName));


        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 4;
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;

        //room作成
        PhotonNetwork.CreateRoom(roomName);

    }

    //Room入室ボタンを押した際の処理
    public void Button_JoinRoom()
    {
        Debug.Log("OnJoinRoom");
        //Debug.Log(string.Format("Name:{0}", PhotonNetwork.room.Name));
        Photon_JoinRoom(roomName.text);

    }

    //Room入室するための処理
    private void Photon_JoinRoom(string roomName)
    {
        //Debug.Log("JoinRoom");
        Debug.Log(string.Format("Name:{0}", roomName));
        //room入室
        PhotonNetwork.JoinRoom(roomName);


    }

    [PunRPC]
    public void GameStart()
    {
        Debug.Log("GameStart");
        PhotonNetwork.LoadLevel("TestGame");
        //Application.LoadLevel("Test");
    } 



}
