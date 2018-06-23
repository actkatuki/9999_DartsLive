using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupFader : MonoBehaviour
{

    public Material[] _mats;

    public GameObject[] _goes;
    [Range(0f, 1f)]
    public float _alpha;

    // Use this for initialization
    void Start()
    {
        _alpha = 1f;

        for (int i = 0; i < _mats.Length; i++)
        {
            _mats[i].SetFloat("_Alpha", _alpha);
            _goes[i].SetActive(true);
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


            for (int i = 0; i < _mats.Length; i++)
            {
                _mats[i].SetFloat("_Alpha", _alpha);

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

    public void SetAppear(float duration)
    {
        StartCoroutine(FadeTimer(duration, true));
    }

    public void SetErase(float duration)
    {
        StartCoroutine(FadeTimer(duration, false));
    }

}
