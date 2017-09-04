using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnControls;

public class CBearMove : MonoBehaviour {


    private Transform tr = null;
    private CAnimaitonControl aniCtrl = null;
    private Transform camPivot = null;
    private CCharacterState cState = null;
    private PhotonView pv = null;
    private Rigidbody rigid = null;

    public float speed = 3.0f;
    public float rotSpeed;

    private Vector3 currPos = Vector3.zero;
    private Quaternion currRot = Quaternion.identity;


    private void Awake()
    {
        tr = GetComponent<Transform>();
        aniCtrl = GetComponent<CAnimaitonControl>();
        camPivot = GameObject.Find("CamPivot").GetComponent<Transform>();
        cState = GetComponent<CCharacterState>();
        pv = GetComponent<PhotonView>();
        rigid = GetComponent<Rigidbody>();

        pv.synchronization = ViewSynchronization.UnreliableOnChange;
        pv.ObservedComponents[0] = this;

        currPos = tr.position;
        currRot = tr.rotation;
    }


    private void Start()
    {
        Camera.main.GetComponent<CFollowCamera>().target = camPivot;
    }


    // 동기화 메소드
    private void OnPhotonSerializeView(PhotonStream _stream, PhotonMessageInfo _info)
    {
        if (_stream.isWriting)
        {
            _stream.SendNext(tr.position);
            _stream.SendNext(tr.rotation);
        }
        else
        {
            currPos = (Vector3)_stream.ReceiveNext();
            currRot = (Quaternion)_stream.ReceiveNext();
        }
    }


    private void Update()
    {
        if (pv.isMine && cState.isDie == false)
        {
            Move();
        }
        else if (pv.isMine && cState.isDie == true) { }
        else
        {
            tr.position = Vector3.Lerp(tr.position, currPos, Time.deltaTime * 3.0f);
            tr.rotation = Quaternion.Slerp(tr.rotation, currRot, Time.deltaTime * 3.0f);
        }
    }


    private void Move()
    {
        var inputVector = new Vector3(CnInputManager.GetAxis("Horizontal"), 0,
            CnInputManager.GetAxis("Vertical"));

        if (inputVector.sqrMagnitude > 0.01f) // 움직이면
        {
            tr.Rotate(Vector3.up * inputVector.x * rotSpeed * Time.deltaTime);

            //float index;

            //index = (inputVector.x > 0.35) ? 17 - inputVector.z : (inputVector.x < -0.35) ? 17 + inputVector.z : 18;

            pv.RPC("SetAni", PhotonTargets.All, 18);

            //tr.Translate(Vector3.forward * inputVector.z * speed * Time.deltaTime);

            rigid.velocity = inputVector * speed;

            rigid.position = new Vector3
                (Mathf.Clamp(rigid.position.x, -24.5f, 24.5f),
                tr.position.y,
                Mathf.Clamp(rigid.position.z, -24.5f, 24.5f));
        }

        if (inputVector.sqrMagnitude == 0.0f)
        {
            pv.RPC("SetAni", PhotonTargets.All, 1);
            rigid.velocity = Vector3.zero;
        }

        //tr.Translate(Vector3.down * 9.8f * Time.deltaTime);
    }


    [PunRPC]
    private void SetAni(int index)
    {
        aniCtrl.AnimationCtrl(index);
    }
}
