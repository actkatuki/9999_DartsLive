using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandFader : MonoBehaviour
{

    public Material _handMat, _bodyMat, _extraMat;

    public GameObject[] _goes;
    [Range(0f, 1f)]
    public float _alpha;
    public bool _defaultIsOff;
    // Use this for initialization
    void Start()
    {
        if (!_defaultIsOff)
        {
            _alpha = 1f;
        }
        else
        {
            _alpha = 0f;
            foreach (GameObject go in _goes)
            {
                go.SetActive(false);
            }
        }
        _handMat.SetFloat("_Alpha", _alpha);
        if (_bodyMat != null)
        {
            _bodyMat.SetFloat("_Alpha", _alpha);
        }
        if (_extraMat != null)
        {
            _extraMat.SetFloat("_Alpha", _alpha);
        }
    }

    IEnumerator FadeTimer(float _dur, bool bl)
    {
        float _timer = 0f;

        while (_timer < 1f)
        {
            _timer = Mathf.Clamp01(_timer + Time.deltaTime / _dur);

            //Main Function

            _alpha = (bl) ? _timer : 1f - _timer;

            _handMat.SetFloat("_Alpha", _alpha);
            if (_bodyMat != null)
            {
                _bodyMat.SetFloat("_Alpha", _alpha);
            }
            if (_extraMat != null)
            {
                _extraMat.SetFloat("_Alpha", _alpha);
            }

            if (_alpha.Equals(0f))
            {
                if (_goes[0].activeInHierarchy)
                {
                    foreach (GameObject go in _goes)
                    {
                        go.SetActive(false);
                    }
                }
            }
            else
            {
                if (!_goes[0].activeInHierarchy)
                {
                    foreach (GameObject go in _goes)
                    {
                        go.SetActive(true);
                    }
                }
            }

            yield return new WaitForEndOfFrame();
        }

        yield break;
    }

    public void SetHands(float duration)
    {
        StartCoroutine(FadeTimer(duration, true));
    }

    public void EraseHands(float duration)
    {
        StartCoroutine(FadeTimer(duration, false));
    }

}
