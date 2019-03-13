using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public int ID;
    public float PlayerHP;
    public Slider sliderHP;

    public GameObject Obj_TargetPlayer;

    public PlayerController target;

    public Sprite[] ItemImages;
    public Image[] Pocket;

    public int itemNumber;

    // Use this for initialization
    void Start () {
        PlayerHP = 100;

        ItemImages = Resources.LoadAll<Sprite>("Images/Items/");

    }
	
	// Update is called once per frame
	void Update () {

        /* 変更前
        target = GameObject.Find("Player" + ID).GetComponent<PlayerController>();

        PlayerHP = target.HPforUI();
        sliderHP.value = target.HPforUI();
        ItemUpdate();
        */
        GameObject playerGameObject = GameObject.Find("Player" + ID);

        if (playerGameObject != null)
        {
            target = playerGameObject.GetComponent<PlayerController>();
            PlayerHP = target.HPforUI();
            sliderHP.value = target.HPforUI();
        }

        ItemUpdate();


    }

    public void SliderUpdate()
    {
        sliderHP.value = PlayerHP;
    }

    public void ItemUpdate()
    {
        //Debug.Log("ItemUpdate()");

        int[] Items = target.ItemforUI();
        for (int i = 0;i < 3 ;i++)
        {
            itemNumber = Items[i];
            Sprite getItemImage = ItemImages[itemNumber];
            Pocket[i].sprite = getItemImage;
            
        }
        //Debug.Log("Items:"+Items[0]+","+Items[1]+","+ Items[2]);
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //Debug.Log("OPSV_UIManager");
        if (stream.isWriting)
        {
            //stream.SendNext(HP);
            /*
            stream.SendNext(Pocket[0].sprite);
            stream.SendNext(Pocket[1].sprite);
            stream.SendNext(Pocket[2].sprite);
            */
            stream.SendNext(itemNumber);
        }


        else
        {   //受信処理

            //HP = (float)stream.ReceiveNext();
            /*
            Pocket[0].sprite = (Sprite)stream.ReceiveNext();
            Pocket[1].sprite = (Sprite)stream.ReceiveNext();
            Pocket[2].sprite = (Sprite)stream.ReceiveNext();
            */
            itemNumber = (int)stream.ReceiveNext();
        }

    }
}
