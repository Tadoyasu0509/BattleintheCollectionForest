using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField] private float _playerSpeed = 1f;


    //パラメータ系
    private Rigidbody rb;
    public Rigidbody rb_Item;
    public Rigidbody rb_Bullet;
    public float HP = 100;
    public float Speed = 1;
    public float JumpPower = 10;
    private float h1;
    private float v1;
    private string Item1, Item2, Item3;
    public float deleteTime;

    //アイテム処理
    private int[] ItemStak = new int[3];
    private Vector3 ItemCreat_Pos;  


    //フラグ系
    private bool f_Jump = false;
    private bool f_Attack = true;
    private float time_Stun = 0;    //スタン状態か否か

    //その他

    private GameObject Obj_Item;
    public Transform Obj_UsePos;
    private BulletController code_BulletController;
    private GameObject obj_GameManager;
    private GameManager code_GameManager;
    private GameObject obj_hitEffect;
    private GameObject obj_hitEffect_Pos;


    private bool update = false;

    //public GameObject obj_UI;



    public int PlayerID;

    //Photon関連
    PhotonView pView = null;

    private void Awake()
    {
        pView = GetComponent<PhotonView>();
        gameObject.name = "Player" + pView.owner.ID;
        PlayerID = pView.owner.ID;
    }
    // Use this for initialization
    void Start () {

        

        ItemStak = new int[] { 0, 0, 0 };
        rb = GetComponent< Rigidbody > ();
        Item1 = "Item1";
        Item2 = "Item2";
        Item3 = "Item3";
        obj_GameManager = GameObject.Find("GameManagerAndUI");
        code_GameManager = obj_GameManager.GetComponent<GameManager>();

        

    }

    void OnCollisionEnter(Collision hit)
    {
        //Debug.Log("OnOnCollisionEnter");
        if (hit.gameObject.CompareTag("Ground"))
        { //地に足をつけている？
            f_Jump = true; //地面をけってジャンプ可能
        }
        else
        {
            f_Jump = false; 
        }

        if (hit.gameObject.CompareTag("Item"))
        {
            Set_Item();
        }

    }

    // Update is called once per frame
    void Update ()
    {
        if (update == false)//Start関数中では全てのプレイヤーの変数へ変更が間にあわない。
        {
            HP = DataUpdateMain.Update_HP();
            Speed = DataUpdateMain.Update_Speed();
            JumpPower = DataUpdateMain.Update_Jump();
            Debug.Log("HP:"+HP+",Speed:"+Speed+",Jump:"+JumpPower);
            update = true;
        }

        //Photon実装時、if文で自身のオブジェクトでない場合この処理を抜けるようにする。
        if (false == pView.isMine)
        {
            return;
        }

        if(0 <= time_Stun)
        {
            time_Stun = time_Stun - Time.deltaTime;
            return;
        }



        h1 = Input.GetAxis("Horizontal1");  //横
        v1 = Input.GetAxis("Vertical1");    //縦

        float ang = Mathf.Atan2(h1, v1);
        float deg = ang * Mathf.Rad2Deg;

        if( (h1!=0f)||(v1!=0f))
        {
            this.gameObject.transform.localRotation = Quaternion.Euler(0f, deg, 0f);

            //前進したい
            this.gameObject.transform.Translate(new Vector3(0f, 0f, _playerSpeed));

        }

        if (Input.GetKeyDown(KeyCode.Space) && f_Jump == true)
        { //ジャンプ可能＆ジャンプキーが押された
            Jump();
            f_Jump = false;
        }

        if(Input.GetButtonDown(Item1) && f_Attack == true)
        {
            if (ItemStak[0] != 0)
            {
                ItemCreat_Pos = transform.position + new Vector3(0, 3f, 10f);
                GameObject P_bullet = PhotonNetwork.Instantiate("Bullet", Obj_UsePos.position, Obj_UsePos.rotation, 0);

                ItemUse(P_bullet, ItemStak[0]);

            }


            ItemStak[0] = 0;
        }

        if (Input.GetButtonDown(Item2) && f_Attack == true)
        {
            if (ItemStak[1] != 0)
            {

                GameObject P_bullet = PhotonNetwork.Instantiate("Bullet", Obj_UsePos.position, Obj_UsePos.rotation, 0);

                ItemUse(P_bullet, ItemStak[1]);


            }
            ItemStak[1] = 0;
        }

        if (Input.GetButtonDown(Item3) && f_Attack == true)
        {
            if (ItemStak[2] != 0)
            {

                GameObject P_bullet = PhotonNetwork.Instantiate("Bullet", Obj_UsePos.position, Obj_UsePos.rotation, 0);
                ItemUse(P_bullet, ItemStak[2]);

            }
            ItemStak[2] = 0;
        }

        if(HP <= 0)
        {
            //pView.RPC("forGameManager", PhotonTargets.All);
            code_GameManager.RPCPlayerLost();
            PhotonNetwork.Destroy(gameObject);
        }

    }

    private void FixedUpdate() //物理演算が発生した場合のみ呼び出される(void Update()は全ての可視フレーム)
    {
        Move();
    }

    private void Move()
    {
/*
        Vector3 movement_h = transform.forward * v1 * Speed * Time.deltaTime;
        Vector3 movement_v = transform.right * h1 * Speed * Time.deltaTime;
        rb.MovePosition(rb.position + movement_h + movement_v);
        rb.transform.position.x = movement_v;
        */

    }

    private void Jump()
    {
        f_Jump = false;
        rb.AddForce(0, JumpPower * Speed, 0);
    }


    private void OnTriggerEnter(Collider hit)
    {

        if (hit.gameObject.CompareTag("Bullet"))
        {

            BulletController bcr = hit.gameObject.GetComponent<BulletController>();

            if (bcr.playerID != PlayerID)
            {
                transform.FindChild("CFX_MagicPoof").gameObject.SetActive(true);
                HP = HP - bcr.damage;
            }

        }


    }

    public void Set_Item()
    {
    int Item_num = Random.Range (1, 10);
        for (int i = 0; i <= 2; i++)
        {

            if (ItemStak[i] == 0) //ItemStakに空きがある場合
            {
                ItemStak[i] = Item_num;
                break;
            }

        }
    }

    public void ItemUse(GameObject findBullet, int ItemUse_ItemNum)
    {
        code_BulletController = findBullet.GetComponent<BulletController>();

        code_BulletController.playerID = PlayerID;

        switch (ItemUse_ItemNum)
        {
            case 9:
                code_BulletController.damage = 90;
                code_BulletController.speed = 100;
                code_BulletController.accel = -0.7f;
                code_BulletController.size = 9;
                code_BulletController.sizeRising = 0;
                code_BulletController.mass = 0;
                code_BulletController.deleteTime = 6;
                break;

            case 8:
                code_BulletController.damage = 80;
                code_BulletController.speed = 50;
                code_BulletController.accel = 1f;
                code_BulletController.size = 1f;
                code_BulletController.sizeRising = 1f;
                code_BulletController.mass = 0;
                code_BulletController.deleteTime = 10;
                break;

            case 7:
                code_BulletController.damage = 70;
                code_BulletController.speed = 10;
                code_BulletController.accel = 0;
                code_BulletController.size = 7;
                code_BulletController.sizeRising = 0;
                code_BulletController.mass = 0;
                code_BulletController.deleteTime = 5;
                break;

            case 6:
                code_BulletController.damage = 20;
                code_BulletController.speed = 500f;
                code_BulletController.accel = 0;
                code_BulletController.size = 6;
                code_BulletController.sizeRising = 0;
                code_BulletController.mass = 0;
                code_BulletController.deleteTime = 3;
                break;

            case 5:
                code_BulletController.damage = 50;
                code_BulletController.speed = 50f;
                code_BulletController.accel = 0;
                code_BulletController.size = 10;
                code_BulletController.sizeRising = 0;
                code_BulletController.mass = 0;
                code_BulletController.deleteTime = 5;
                break;
                
            case 4:
                code_BulletController.damage = 40;
                code_BulletController.speed = 100f;
                code_BulletController.accel = 50;
                code_BulletController.size = 10;
                code_BulletController.sizeRising = -1;
                code_BulletController.mass = 0;
                code_BulletController.deleteTime = 7;
                break;

            case 3:
                code_BulletController.damage = 40;
                code_BulletController.speed = 500f;
                code_BulletController.accel = -400f;
                code_BulletController.size = 3;
                code_BulletController.sizeRising = 2;
                code_BulletController.mass = 0;
                code_BulletController.deleteTime = 3;
                break;

            case 2:
                code_BulletController.damage = 40;
                code_BulletController.speed = 200f;
                code_BulletController.accel = 0;
                code_BulletController.size = 5;
                code_BulletController.sizeRising = 0;
                code_BulletController.mass = 0;
                code_BulletController.deleteTime = 3;
                break;

            case 1:
                code_BulletController.damage = 25;
                code_BulletController.speed = 800f;
                code_BulletController.accel = 0;
                code_BulletController.size = 1;
                code_BulletController.sizeRising = 0;
                code_BulletController.mass = 0;
                code_BulletController.deleteTime = 3;

                break;
            case 0:
                print("アイテムを持っておりません");
                break;
            default:
                print("このアイテムの使い方は分からない！");
                break;
        }

    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(HP);
            stream.SendNext(Speed);
            stream.SendNext(JumpPower);
            stream.SendNext(ItemStak);

        }


        else
        {   //受信処理

            HP = (float)stream.ReceiveNext();
            Speed = (float)stream.ReceiveNext();
            JumpPower = (float)stream.ReceiveNext();
            ItemStak = (int[])stream.ReceiveNext();

        }

    }

    public float HPforUI()
    {
        return HP;
    }

    public int[] ItemforUI()
    {
        return ItemStak;
    }

}
