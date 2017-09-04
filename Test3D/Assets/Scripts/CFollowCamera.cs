using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CFollowCamera : MonoBehaviour {

    [HideInInspector]
    public Transform target;

    private Transform tr = null;


    private void Awake()
    {
        tr = GetComponent<Transform>();
    }


    private void LateUpdate()
    {
        if (target == null) return;

        tr.position = Vector3.Lerp(tr.position, target.position, 0.9f);
        tr.rotation = Quaternion.Slerp(tr.rotation, target.rotation, 0.9f);
    }
}
