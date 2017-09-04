using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CAIMove : MonoBehaviour {

    private Transform tr = null;
    private NavMeshAgent nvAgent = null;
    private CCharacterState cState = null;
    private CAnimaitonControl aniCtrl = null;
    private PhotonView pv = null;
    private CharacterController cCtrl = null;

    private Vector3 point = Vector3.zero;
    private Vector3 prevPos = Vector3.zero;

    public float gravity = 20.0f;
    private Vector3 move = Vector3.zero;

    private Vector3 currPos = Vector3.zero;
    private Quaternion currRot = Quaternion.identity;


    private void Awake()
    {
        tr = GetComponent<Transform>();
        nvAgent = GetComponent<NavMeshAgent>();
        cState = GetComponent<CCharacterState>();
        aniCtrl = GetComponent<CAnimaitonControl>();
        pv = GetComponent<PhotonView>();
        cCtrl = GetComponent<CharacterController>();

        pv.synchronization = ViewSynchronization.UnreliableOnChange;
        pv.ObservedComponents[0] = this;

        cState.state = CCharacterState.State.Move;

        currPos = tr.position;
        currRot = tr.rotation;
    }


    private void Update()
    {
        // 내가 살았으면
        if(pv.isMine && cState.isDie == false) { }
        // 내가 죽었으면
        else if(pv.isMine && cState.isDie == true)
        {
            nvAgent.destination = tr.position;
        }
        else
        {
            tr.position = Vector3.Lerp(tr.position, currPos, Time.deltaTime * 3.0f);
            tr.rotation = Quaternion.Slerp(tr.rotation, currRot, Time.deltaTime * 3.0f);
        }
    }


    private void Start () {

        StartCoroutine("CheckAIState");
        StartCoroutine("AIAction");
	}


    // 
    private IEnumerator CheckAIState()
    {
        while(!cState.isDie)
        {
            float _posX = Random.Range(-23.0f, 23.0f);
            float _posY = Random.Range(-32.0f, 14.0f);
            point = new Vector3(_posX, tr.position.y, _posY);    // 랜덤 위치 잡음

            int delay = 30;


            while(!(nvAgent.stoppingDistance > Vector3.Distance(point, tr.position)))    // 목적지에 다다르지 않았으면
            {
                cState.state = (cState.isDie) ? 
                    CCharacterState.State.Die : CCharacterState.State.Move;

                delay--;

                if (delay == 0)
                    break;

                yield return new WaitForSeconds(0.2f);
            }

            cState.state = CCharacterState.State.Idle;
            float _time = Random.Range(1.0f, 5.0f);

            yield return new WaitForSeconds(_time);
        }
    }


    private IEnumerator AIAction()
    {
        while (!cState.isDie)
        {
            switch (cState.state)
            {

                case CCharacterState.State.Idle:
                    if (pv.isMine)
                    {
                        nvAgent.isStopped = true;    // 멈춤
                        pv.RPC("SetAni", PhotonTargets.All, 1);
                        move.y -= gravity * Time.deltaTime;
                        cCtrl.Move(move * Time.deltaTime);
                    }
                    break;

                case CCharacterState.State.Move:

                    float _delta_X = tr.position.x - prevPos.x;
                    float _delta_Z = tr.position.z - prevPos.z;

                    if(0 != _delta_X)
                    {
                        tr.Rotate(Vector3.up * _delta_X * nvAgent.angularSpeed * Time.deltaTime);

                        prevPos = tr.position;
                    }

                    if(0.1f > Mathf.Abs(_delta_Z))
                    {
                        pv.RPC("SetAni", PhotonTargets.All, 1);

                        prevPos = tr.position;
                    }

                    if (pv.isMine)
                    {
                        nvAgent.destination = point;    // 목적지 설정
                        nvAgent.isStopped = false;      // 출발

                        pv.RPC("SetAni", PhotonTargets.All, 18);

                        move.y -= gravity * Time.deltaTime;
                        cCtrl.Move(move * Time.deltaTime);
                    }

                    break;
            }
            yield return null;
        }
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


    // 애니메이션 동기화
    [PunRPC]
    private void SetAni(int index)
    {
        aniCtrl.AnimationCtrl(index);
    }
}
