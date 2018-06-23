using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoveOnPath : MonoBehaviour {
    public Transform _piv;
    //public Spline _path;

        // SuperSpline入れたら外して

    public AnimationCurve _easer;
    public float _duration = 1f;
    public RotFromClamped[] _rfc;

    public UnityEvent _endFunc;

    [SerializeField][Range(0f, 1f)]
    private float m_path;    
    public float _phase {
        get
        {
            return m_path;
        }
        set
        {
            if (m_path < 1f && value >= 1f) {
                _endFunc.Invoke();
            }
            m_path = value;
            //_piv.position = _path.GetPositionOnSpline(_easer.Evaluate( m_path));
            if (_rfc.Length > 0) {
                for (int i= 0;i < _rfc.Length; i++) { 
                    _rfc[i]._phase = value;
                }
            }
        }
    }

    

	// Use this for initialization
	void Start () {
        _phase = 0f;
	}

    private void Update () {
#if UNITY_EDITOR
        _phase = m_path;

        if (Input.GetKeyDown(KeyCode.T)) {
            Launch();
        }

#endif
    }

    public void Launch () {
        StartCoroutine(MoveTimer(_duration));
    }


    IEnumerator MoveTimer ( float _dur ) {
        float _timer = 0f;

        while (_timer < 1f) {
            _timer = Mathf.Clamp01(_timer + Time.deltaTime / _dur);

            //Main Function
            _phase = _timer;

            yield return new WaitForEndOfFrame();            
        }
        
        yield break;
    }


}
