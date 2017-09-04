using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnControls;


public class CPlayerMove : MonoBehaviour {

    private Transform tr = null;
    private CAnimaitonControl aniCtrl = null;
    private Transform camPivot = null;
    private CCharacterState cState =null;
    private PhotonView pv = null;
    private CharacterController cCtrl = null;

    public float speed;
    public float rotSpeed;
    public float gravity = 20.0f;

    private Vector3 currPos = Vector3.zero;
    private Quaternion currRot = Quaternion.identity;

    private void Awake()
    {
        tr = GetComponent<Transform>();
        aniCtrl = GetComponent<CAnimaitonControl>();
        camPivot = GameObject.Find("CamPivot").GetComponent<Transform>();
        cState = GetComponent<CCharacterState>();
        pv = GetComponent<PhotonView>();
        cCtrl = GetComponent<CharacterController>();

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

            inputVector = tr.TransformDirection(inputVector);
            inputVector *= speed;
            inputVector.y -= gravity * Time.deltaTime;

            cCtrl.Move(inputVector * Time.deltaTime);
        }

        if(inputVector.sqrMagnitude == 0.0f)
        {
            pv.RPC("SetAni", PhotonTargets.All, 1);
        }
    }


    [PunRPC]
    private void SetAni(int index)
    {
        aniCtrl.AnimationCtrl(index);
    }
}
