using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCharacterState : MonoBehaviour {

    public int hp = 1;
    public bool isDie = false;

    private PhotonView pv = null;
    private CAnimaitonControl aniCtrl = null;
    private CharacterController cCtrl = null;
    private CapsuleCollider coll = null;


    private void Awake()
    {
        pv = GetComponent<PhotonView>();
        aniCtrl = GetComponent<CAnimaitonControl>();
        cCtrl = GetComponent<CharacterController>();
        coll = GetComponent<CapsuleCollider>();
    }


    // 상태
    public enum State
    {
        Idle,       // 대기
        Move,       // 이동
        Attack,     // 공격
        Die         // 사망
    };
    public State _state;

    // 캐릭터 상태 프로퍼티
    public State state
    {
        get
        {
            return this._state;
        }
        set
        {
            this._state = value;
        }
    }


    // HP 줄이는 메소드
    public void HpDown(int _damage)
    {
        hp -= _damage;

        int _index;

        if (hp <= 0)
        {
            _index = 6;
            pv.RPC("SetDie", PhotonTargets.All);
        }
        else
            _index = 5;

        pv.RPC("SetAnimationEvent", PhotonTargets.All, _index);
    }


    // 애니메이션 동기화
    [PunRPC]
    private void SetAnimationEvent(int _index)
    {
        aniCtrl.AnimationCtrl(_index);
    }


    // Die 상태 동기화
    [PunRPC]
    private void SetDie()
    {
        isDie = true;
        state = State.Die;
        cCtrl.enabled = false;
        coll.isTrigger = true;
    }
}
