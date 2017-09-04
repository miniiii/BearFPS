using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerDamage : MonoBehaviour {

    private PhotonView pv = null;
    private CCharacterState cState = null;


    private void Awake()
    {
        pv = GetComponent<PhotonView>();
        cState = GetComponent<CCharacterState>();
    }


    public void Damage(int _damage)
    {
        if(!cState.isDie)
            cState.HpDown(_damage);
    }
}
