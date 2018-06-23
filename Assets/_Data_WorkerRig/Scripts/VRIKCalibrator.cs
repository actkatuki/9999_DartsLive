using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(VRIKScaler))]
class VRIKCalibrator: MonoBehaviour {
    [SerializeField, Range(0.5f, 2.5f)]
    float modelEyeHeight = 1.5f;
    [SerializeField]
    Transform hmd;

    VRIKScaler scaler;


    public static VRIKCalibrator _instance;

    private void Awake () {
        if (_instance == null) {
            _instance = this;
        } else if (_instance != null) {
            Destroy(this.gameObject);
        }
        scaler = GetComponent<VRIKScaler>();
        if (hmd == null) {
            hmd = Camera.main.transform;
        }
        
    }

    public static VRIKCalibrator GetInstance () {
        return _instance;
    }

    private void OnDestroy () {
        if (_instance == this) {
            _instance = null;
        }
    }

    [ContextMenu("Calibrate")]
    public void Calibrate () {
        /*if (hmd.localPosition == Vector3.zero) {
            hmd = hmd.parent;
        }*/
        scaler.scale = hmd.localPosition.y / modelEyeHeight;
    }

    void OnDrawGizmosSelected () {
        Gizmos.DrawWireCube(transform.position + Vector3.up * (modelEyeHeight / 2f), new Vector3(0.2f, modelEyeHeight, 0.2f));
    }

    private void Update () {

        if (Input.GetKeyDown(KeyCode.Space)) {
            Calibrate();
        }
    
    }
}
