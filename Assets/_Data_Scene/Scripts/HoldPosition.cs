using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldPosition : MonoBehaviour {


    [SerializeField]
    private Vector3 _Pos,_Rot;


public void HoldPos() {
        Debug.Log("PropsPosition");
        transform.localPosition =_Pos;
        transform.localRotation = Quaternion.Euler(_Rot);
        gameObject.GetComponent<HeldObject>().SetHighLightCondition(false);
    }
}
