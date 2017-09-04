using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnControls;


public class CTestMove : MonoBehaviour
{

    private Transform tr = null;
    private CAnimaitonControl aniCtrl = null;
    private Transform camPivot = null;
    private CCharacterState cState = null;
    private CharacterController cCtrl = null;

    public float speed;
    public float rotSpeed;


    private void Awake()
    {
        tr = GetComponent<Transform>();
        aniCtrl = GetComponent<CAnimaitonControl>();
        camPivot = GameObject.Find("CamPivot").GetComponent<Transform>();
        cState = GetComponent<CCharacterState>();
        cCtrl = GetComponent<CharacterController>();
    }


    private void Start()
    {
        Camera.main.GetComponent<CFollowCamera>().target = camPivot;
    }


    private void Update()
    {
        if (cState.isDie == false)
        {
            Move();
        }
    }


    private void Move()
    {
        var inputVector = new Vector3(CnInputManager.GetAxis("Horizontal"), 0,
            CnInputManager.GetAxis("Vertical"));

        if (inputVector.sqrMagnitude > 0.01f) // 움직이면
        {
            tr.Rotate(Vector3.up * inputVector.x * rotSpeed * Time.deltaTime);

            SetAni(18);

            tr.Translate(Vector3.forward * speed * inputVector.z, Space.Self);
            tr.Translate(Vector3.right * speed * inputVector.x, Space.Self);
        }

        if (inputVector.sqrMagnitude == 0.0f)
        {
            SetAni(1);
        }
    }


    private void SetAni(int index)
    {
        aniCtrl.AnimationCtrl(index);
    }
}
