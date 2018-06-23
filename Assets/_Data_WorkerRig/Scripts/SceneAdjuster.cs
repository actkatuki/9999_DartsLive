using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;
using UnityEngine.SceneManagement;

public static class AdjustedOnce
{
    public static bool _adjusted = false;
}

public class SceneAdjuster : MonoBehaviour
{
    private Transform _selfTr;
    public Transform _camrig;
    [SerializeField]
    private float _adjustX, _adjustZ;
    public float _adjustDistance = 0.1f;
    [Range(0, 3)]
    public int _flip;

    private Vector3 _offset;

    [SerializeField]
    [Range(0f, 1f)]
    private float m_phase;
    public float _phase
    {
        get { return m_phase; }
        set
        {
            m_phase = value;
            UpdatePos();
        }
    }

    [Space(6f)]
    public bool _renderMode;

    [Header("AutoAdjust")]
    public bool _autoAdjustable;
    public Transform _head;

    public bool _isCurveMode = false;
    public AnimationCurve _fadingCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1f, 1f));

    // Use this for initialization
    void Start()
    {

        _selfTr = transform;

        _adjustX = PlayerPrefs.GetFloat("_adX", 0f);
        _adjustZ = PlayerPrefs.GetFloat("_adZ", 0f);
        int _flipper = PlayerPrefs.GetInt("_flip", 0);

        switch (_flipper)
        {
            case 0:
                _flip = 0;
                break;

            case 1:
                _flip = 1;
                break;

            case 2:
                _flip = 2;
                break;

            case 3:
                _flip = 3;
                break;

            default:
                _flip = 0;
                break;
        }

        _camrig.localRotation = Quaternion.Euler(new Vector3(0f, 90f * _flip, 0f));

        if (!_renderMode)
        {

            _offset = new Vector3(_adjustX, 0f, _adjustZ);

        }
        else if (_renderMode)
        {
            _offset = Vector3.zero;
        }
        _phase = 0f;

    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _adjustZ += _adjustDistance;
            PlayerPrefs.SetFloat("_adZ", _adjustZ);
            _offset = new Vector3(_adjustX, 0f, _adjustZ);
            UpdatePos();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _adjustZ -= _adjustDistance;
            PlayerPrefs.SetFloat("_adZ", _adjustZ);
            _offset = new Vector3(_adjustX, 0f, _adjustZ);
            UpdatePos();

        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _adjustX -= _adjustDistance;
            PlayerPrefs.SetFloat("_adX", _adjustX);
            _offset = new Vector3(_adjustX, 0f, _adjustZ);
            UpdatePos();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _adjustX += _adjustDistance;
            PlayerPrefs.SetFloat("_adX", _adjustX);
            _offset = new Vector3(_adjustX, 0f, _adjustZ);
            UpdatePos();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            _flip++;
            if (_flip > 3)
            {
                _flip = 0;
            }
            PlayerPrefs.SetInt("_flip", _flip);

            _camrig.localRotation = Quaternion.Euler(new Vector3(0f, 90f * _flip, 0f));




        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_autoAdjustable && !AdjustedOnce._adjusted)
            {
                AutoAdjust();
                AdjustedOnce._adjusted = true;
            }

        }

        if (Input.GetKeyDown(KeyCode.Home))
        {
            ResetPosition();
        }

#if UNITY_EDITOR
        // _phase = m_phase;
#endif


    }

    public void Up()
    {
        _adjustZ += _adjustDistance;
        PlayerPrefs.SetFloat("_adZ", _adjustZ);
        _offset = new Vector3(_adjustX, 0f, _adjustZ);
        UpdatePos();
    }

    public void Down()
    {
        _adjustZ -= _adjustDistance;
        PlayerPrefs.SetFloat("_adZ", _adjustZ);
        _offset = new Vector3(_adjustX, 0f, _adjustZ);
        UpdatePos();
    }
    public void Left()
    {
        _adjustX -= _adjustDistance;
        PlayerPrefs.SetFloat("_adX", _adjustX);
        _offset = new Vector3(_adjustX, 0f, _adjustZ);
        UpdatePos();
    }

    public void Right()
    {
        _adjustX += _adjustDistance;
        PlayerPrefs.SetFloat("_adX", _adjustX);
        _offset = new Vector3(_adjustX, 0f, _adjustZ);
        UpdatePos();
    }

    public void ResetPosition()
    {
        _adjustZ = 0f;
        _adjustX = 0f;
        PlayerPrefs.SetFloat("_adZ", 0f);
        PlayerPrefs.SetFloat("_adX", 0f);
        _offset = Vector3.zero;
        UpdatePos();

    }

    public void AutoAdjust()
    {
        Vector3 _gap = _camrig.localRotation * _head.localPosition;
        Debug.Log(_gap);
        _adjustX = -_gap.x;
        _adjustZ = -_gap.z;
        PlayerPrefs.SetFloat("_adZ", _adjustZ);
        PlayerPrefs.SetFloat("_adX", _adjustX);
        _offset = new Vector3(_adjustX, 0f, _adjustZ);
        UpdatePos();

        Debug.Log("Auto Adjust");
    }


    void UpdatePos()
    {

        if (!_renderMode)
        {
            if (!_isCurveMode)
            {
                _camrig.localPosition = Vector3.Lerp(_offset, Vector3.zero, m_phase);
            }
            else if (_isCurveMode)
            {
                _camrig.localPosition = Vector3.Lerp(_offset, Vector3.zero, _fadingCurve.Evaluate(m_phase));
            }
        }
        else if (_renderMode)
        {
            _camrig.localPosition = Vector3.zero;
        }

    }

    public void ResetOffset()
    {
        _phase = 0f;
    }

    public void StartFade(float tm)
    {
        StartCoroutine(FadingUnoffset(tm));
    }

    IEnumerator FadingUnoffset(float _dur)
    {
        float _timer = 0f;

        while (_timer < 1f)
        {
            _timer = Mathf.Clamp01(_timer + Time.deltaTime / _dur);
            _phase = _timer;
            yield return new WaitForEndOfFrame();
        }

        yield break;
    }


}
