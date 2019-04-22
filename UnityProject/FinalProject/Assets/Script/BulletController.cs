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
    public float damage;    //ダメージ   
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

        speed = speed + (accel * Time.deltaTime);
        accel = accel + 0.1f;
        size = size + (sizeRising * Time.deltaTime);
        Vector3 BulletMove_h = transform.forward * speed;
        rb.AddForce(BulletMove_h);

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
            damage = (float)stream.ReceiveNext(); 

        }

    }

}
