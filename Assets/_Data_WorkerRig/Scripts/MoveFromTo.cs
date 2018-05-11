using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoveFromTo : MonoBehaviour
{

    public Transform _piv;

    public Transform _fromTr, _toTr;
    public Vector3 _fromVec, _toVec;
    public bool _localSpace, _updatePos;

    public enum AnchorIs
    {
        None,
        From,
        To
    }

    public AnchorIs _anchorMode;

    public AnimationCurve _easerX, _easerY, _easerZ, _easerRotate;
    public float _duration = 1f;
    public RotFromClamped[] _rfc;
    public MoveFromTo[] _mft;

    public UnityEvent _endFunc;
    public string[] _Tip;
    public UnityEvent _startfunc;
    public string[] _Tip2;


    [SerializeField]

    private float m_phase;
    public float _phase
    {
        get
        {
            return m_phase;
        }
        set
        {
            if (m_phase < 1f && value >= 1f)
            {
                _endFunc.Invoke();
            }
            if (_fromTr != null && _fromVec == Vector3.zero)
            {
                if (!_localSpace)
                {
                    _fromVec = _fromTr.position;
                }
            }
            if (_toTr != null && _toVec == Vector3.zero)
            {
                if (!_localSpace)
                {
                    _toVec = _toTr.position;
                }
            }

            if (_fromTr != null && _localSpace && _anchorMode != AnchorIs.None)
            {
                if (_anchorMode == AnchorIs.To)
                {
                    _fromVec = _toTr.InverseTransformPoint(_fromTr.position);
                }
                else if (_anchorMode == AnchorIs.From)
                {
                    _fromVec = Vector3.zero;
                }
            }

            if (_toTr != null && _localSpace && _anchorMode != AnchorIs.None)
            {
                if (_anchorMode == AnchorIs.To)
                {
                    _toVec = Vector3.zero;
                }
                else if (_anchorMode == AnchorIs.From)
                {
                    _toVec = _fromTr.InverseTransformPoint(_toTr.position);
                }
            }




            if (m_phase != value)
            {

                m_phase = value;
                float _posX = Mathf.LerpUnclamped(_fromVec.x, _toVec.x, _easerX.Evaluate(value));
                float _posY = Mathf.LerpUnclamped(_fromVec.y, _toVec.y, _easerY.Evaluate(value));
                float _posZ = Mathf.LerpUnclamped(_fromVec.z, _toVec.z, _easerZ.Evaluate(value));

                Vector3 _pos = new Vector3(_posX, _posY, _posZ);

                if (_localSpace)
                {
                    _piv.localPosition = _pos;
                }
                else
                {
                    _piv.position = _pos;
                }
                if (_rfc.Length > 0)
                {
                    for (int i = 0; i < _rfc.Length; i++)
                    {
                        _rfc[i]._phase = _easerRotate.Evaluate(value);
                    }
                }
                if (_mft.Length > 0)
                {
                    for (int i = 0; i < _mft.Length; i++)
                    {
                        _mft[i]._phase = value;
                    }
                }


            }

        }
    }


    public bool _isDebugMode;



    // Use this for initialization
    void Start()
    {
        m_phase = 0f;
        _phase = 0f;

        _isDebugMode = false;
    }

    private void Update()
    {
#if UNITY_EDITOR

        _phase = m_phase;

        if (Input.GetKeyDown(KeyCode.T) && _isDebugMode)
        {
            Launch();
        }

#endif

        if (_updatePos)
        {
            if (_phase <= 0f)
            {
                if (_fromTr != null && _localSpace && _anchorMode != AnchorIs.None)
                {
                    if (_anchorMode == AnchorIs.To)
                    {
                        _fromVec = _toTr.InverseTransformPoint(_fromTr.position);
                        _piv.localPosition = _fromVec;
                    }
                }



            }
            else if (_phase >= 1f)
            {

                if (_toTr != null && _localSpace && _anchorMode != AnchorIs.None)
                {
                    if (_anchorMode == AnchorIs.From)
                    {
                        _toVec = _fromTr.InverseTransformPoint(_toTr.position);
                        _piv.localPosition = _toVec;
                    }
                }
            }

            if (_phase == 0f)
            {
                if (_fromTr != null && _toTr != null && !_localSpace)
                {
                    _fromVec = _fromTr.position;
                    _toVec = _toTr.position;
                }
            }

        }
    }

    public void Launch()
    {
        StartCoroutine(MoveTimer(_duration));
        _startfunc.Invoke();
        Debug.Log("Launch :" + gameObject.name);
    }


    IEnumerator MoveTimer(float _dur)
    {
        float _timer = 0f;

        while (_timer < 1f)
        {
            _timer = Mathf.Clamp01(_timer + Time.deltaTime / _dur);

            //Main Function
            _phase = _timer;

            yield return new WaitForEndOfFrame();
        }

        if (_timer >= 1f)
        {
            _endFunc.Invoke();
        }

        yield break;
    }

    public void RefleshPos()
    {

        if (!_localSpace)
        {
            _fromVec = _fromTr.position;
        }

        if (!_localSpace)
        {
            _toVec = _toTr.position;
        }


        if (_fromTr != null && _localSpace && _anchorMode != AnchorIs.None)
        {
            if (_anchorMode == AnchorIs.To)
            {
                _fromVec = _toTr.InverseTransformPoint(_fromTr.position);
            }
            else if (_anchorMode == AnchorIs.From)
            {
                _fromVec = Vector3.zero;
            }
        }

        if (_toTr != null && _localSpace && _anchorMode != AnchorIs.None)
        {
            if (_anchorMode == AnchorIs.To)
            {
                _toVec = Vector3.zero;
            }
            else if (_anchorMode == AnchorIs.From)
            {
                _toVec = _fromTr.InverseTransformPoint(_toTr.position);
            }
        }
    }

}
