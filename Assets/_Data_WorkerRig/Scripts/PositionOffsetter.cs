using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PositionOffsetter : MonoBehaviour
{

    public Transform _camRig, _head;
    [SerializeField]
    [Range(0f, 1f)]
    private float m_fader;
    public float _fader
    {
        get { return m_fader; }
        set
        {
            m_fader = value;
            if (_withY)
            {
                if (!_isCurveMode)
                {
                    _camRig.localPosition = Vector3.Lerp(Vector3.zero, new Vector3(-_head.localPosition.x, -_head.localPosition.y, -_head.localPosition.z), value);
                }
                else if (_isCurveMode)
                {
                    _camRig.localPosition = Vector3.Lerp(Vector3.zero, new Vector3(-_head.localPosition.x, -_head.localPosition.y, -_head.localPosition.z), _fadingCurve.Evaluate(value));
                }
            }
            else if (!_withY)
            {
                if (!_isCurveMode)
                {
                    _camRig.localPosition = Vector3.Lerp(Vector3.zero, new Vector3(-_head.localPosition.x, 0f, -_head.localPosition.z), value);
                }
                else if (_isCurveMode)
                {
                    _camRig.localPosition = Vector3.Lerp(Vector3.zero, new Vector3(-_head.localPosition.x, 0f, -_head.localPosition.z), _fadingCurve.Evaluate(value));
                }
            }
        }
    }

    public Coroutine _crt;
    public bool _withY, _always;

    public bool _isCurveMode = false;
    public AnimationCurve _fadingCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1f, 1f));

    public static PositionOffsetter _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != null)
        {
            Destroy(this.gameObject);
        }
    }

    public static PositionOffsetter GetInstance()
    {
        return _instance;
    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }


    // Use this for initialization
    void Start()
    {

        if (_camRig == null)
        {
            _camRig = GameObject.Find("[CameraRig]").transform;
        }
        if (_head == null)
        {
            _head = Camera.main.transform;
        }

        if (_head.parent != _camRig)
        {
            _head = _head.parent;
        }
        _fader = 0f;
    }

    private void Update()
    {
        if (_always)
        {
            _fader = m_fader;
        }
    }

    public void SetOffset(float duration)
    {
        if (_crt != null)
        {
            StopCoroutine(_crt);
        }
        StartCoroutine(AdjustTimer(true, duration));
    }

    public void UnsetOffset(float duration)
    {
        if (_crt != null)
        {
            StopCoroutine(_crt);
        }
        StartCoroutine(AdjustTimer(false, duration));
    }

    public void ResetOffset()
    {
        _fader = 0f;
    }

    IEnumerator AdjustTimer(bool bl, float _dur)
    {
        float _timer = 0f;

        while (_timer < 1f)
        {
            _timer = Mathf.Clamp01(_timer + Time.deltaTime / _dur);

            //Main Function
            if (bl)
            {
                _fader = _timer;
            }
            else if (!bl)
            {
                _fader = 1f - _timer;
            }

            yield return new WaitForEndOfFrame();
        }

        yield break;
    }

    public void SetYMode(bool bl)
    {
        _withY = bl;
    }

    public void SetAlways(bool bl)
    {
        _always = bl;
    }

}
