using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {
    public Rigidbody rb;
    public int playerID;

    public float speed;     //弾速
    public float accel;     //加速
    public float size;      //弾の大きさ
    public float sizeRising; //弾の巨大化
    public float deleteTime; //生存時間
    public float damage;    //   
    public float mass;      //重量

    public bool useGravity_f;
    private Vector3 velocity = new Vector3(0f, 10f, 0f);
    private Vector3 bulletSize;

    //Photon関連
    PhotonView pView = null;


    // Use this for initialization
    void Start () {
        pView = GetComponent<PhotonView>();

        rb = GetComponent<Rigidbody>();
        bulletSize = transform.localScale * size;
        this.transform.localScale = bulletSize;
    }
	
	// Update is called once per frame
	void Update () {

        if (false == pView.isMine)
        {
            return;
        }
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            rb.velocity = velocity;
            speed = speed + (accel * Time.deltaTime);
            size = size + (sizeRising * Time.deltaTime);
            //rb.AddForce()
            Vector3 BulletMove_h = transform.forward * speed;
            Vector3 BulletMove_v = transform.right * speed;
            Vector3 bulletSize = transform.localScale * size;
            rb.AddForce(BulletMove_h + BulletMove_v);
            
        }*/


        //rb.velocity = velocity;
        speed = speed + (accel * Time.deltaTime);
        accel = accel + 0.1f;
        size = size + (sizeRising * Time.deltaTime);
        //rb.AddForce()
        Vector3 BulletMove_h = transform.forward * speed;
       //Vector3 BulletMove_v = transform.right * speed;
        rb.AddForce(BulletMove_h);
        //Debug.Log(BulletMove_h + BulletMove_v);

        this.transform.localScale = bulletSize + new Vector3(sizeRising, sizeRising, sizeRising);

        deleteTime = deleteTime - Time.deltaTime;
        if (deleteTime < 0)
            Destroy(gameObject);
    
    }


    private void FixedUpdate()
    {
        
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {   //送信処理
            stream.SendNext(damage);    // ダメージ
        }
        else
        {   //受信処理
            damage = (float)stream.ReceiveNext();     // 0:ステータス
            //prams_arrow = (Vector3)stream.ReceiveNext();    // 1:方向
            //item_cnt = (int)stream.ReceiveNext();       // 2:個数
        }
        //※送信する要素と受信する要素の呼び出し順番は合わせる必要がある。
        // ステータス、方向、個数という順番で送信したら
        // ステータス、方向、個数という順番で受信データが届く。
    }

}
