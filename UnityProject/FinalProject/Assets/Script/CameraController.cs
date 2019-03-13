using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Camera obj_Camera; //カメラ登録
    public Transform[] TargetPlayers; //カメラの向きを決めるため、全プレイヤーの座標を取得
    public float obj_DampTime = 0.2f;


    private Vector3 obj_desiredPosition; //移動・ズーム時におけるカメラ基本位置
    private Vector3 obj_moveVelocity;

    private void Awake()
    {
        obj_Camera = GetComponentInChildren<Camera>();

    }

    private void FixedUpdate()
    {
        //C_Move();
        C_Zoom();
    }

    private void C_Move()
    {
        FindAveragePosition();
        for (int i = 0; i < TargetPlayers.Length; i++)
        {
            Debug.Log("プレイヤー数："+TargetPlayers.Length+"座標："+TargetPlayers[i].position);
        }
        transform.position = Vector3.SmoothDamp(transform.position, obj_desiredPosition, ref obj_moveVelocity, obj_DampTime);
    }

    private void C_Zoom()
    {
        //後で書く
    }

    private void FindAveragePosition()
    {
        Vector3 avaragePos = new Vector3();
        int targetCount = 0;

        for(int i = 0; i < TargetPlayers.Length; i++)
        {
            if (!TargetPlayers[i].gameObject.activeSelf)
                continue;
            avaragePos += TargetPlayers[i].position;
            targetCount++;

        }

        if (targetCount > 0)
            avaragePos /= targetCount;

        avaragePos.y = transform.position.y;

        obj_desiredPosition = avaragePos;
    }

    public void SetStartPositionAndSize()
    {
        FindAveragePosition();

        transform.position = obj_desiredPosition;

        //ズーム実装時Tanks!内の以下にあたるコードを記述
        //m_Camera.orthographicSize = FindRequiredSize();

    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
