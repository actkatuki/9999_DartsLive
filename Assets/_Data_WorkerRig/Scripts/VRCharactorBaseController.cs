using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRCharactorBaseController : MonoBehaviour
{
    public Transform _cameraRig;
    public Transform _head;

    public Transform _bodyDirection;


    public Transform _bodyTr;
    public float _backOffset = 0f;
    [Range(0f, 1f)]
    public float _bodyHeightPos;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //身体の中心を頭から取得
        Vector3 headPos = _head.localPosition;
        _bodyDirection.localPosition = new Vector3(headPos.x, 0f, headPos.z);



        _bodyDirection.localPosition = new Vector3(_cameraRig.InverseTransformPoint(_head.position).x, 0f, _cameraRig.InverseTransformPoint(_head.position).z);

        Vector3 _headForward = Vector3.forward;

        float _headUpDot = Vector3.Dot(_head.forward, Vector3.up);
        if (_headUpDot >= 0f)
        {
            _headForward = Vector3.Lerp(_head.forward, -_head.up, Mathf.Sin(Mathf.Clamp01(_headUpDot) * 0.5f * Mathf.PI));
        }
        else if (_headUpDot < 0f)
        {
            _headForward = Vector3.Lerp(_head.forward, _head.up, Mathf.Sin(Mathf.Abs(_headUpDot) * 0.5f * Mathf.PI));
        }
        Debug.DrawLine(_head.position, _head.position + _headForward, Color.green);

        _bodyDirection.rotation = Quaternion.LookRotation(new Vector3(_headForward.x, 0f, _headForward.z));

        _bodyTr.localPosition = new Vector3(0f, _cameraRig.InverseTransformPoint(_head.position).y - _bodyHeightPos, _backOffset);
        _bodyTr.localRotation = Quaternion.identity;

    }
}
