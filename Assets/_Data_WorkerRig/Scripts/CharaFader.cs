using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaFader : MonoBehaviour
{

    //public GameObject _charaRoot;
    public Renderer[] _bodys, _items;

    public MaterialPropertyBlock _block, _headBlock;


    public static CharaFader _instance;
    // Use this for initialization


    private Vector3[] _fakePos;
    private Quaternion[] _fakeRot;

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
        if (_block == null)
        {
            _block = new MaterialPropertyBlock();
        }
        if (_headBlock == null)
        {
            _headBlock = new MaterialPropertyBlock();
        }

        // _vrik.enabled = false;
    }

    public static CharaFader GetInstance()
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

    void Start()
    {


        ResetSystem();

    }

    public void ResetSystem()
    {

        if (_block == null)
        {
            _block = new MaterialPropertyBlock();
        }
        if (_headBlock == null)
        {
            _headBlock = new MaterialPropertyBlock();
        }


        Switch(false, false);

    }



    public void Switch(bool _bd, bool _itm)
    {


        if (_bd)
        {
            _block.SetFloat("_Alpha", 1f);
            foreach (Renderer rnd in _bodys)
            {
                rnd.enabled = true;
                rnd.SetPropertyBlock(_block);
            }
        }
        else if (!_bd)
        {
            _block.SetFloat("_Alpha", 0f);
            foreach (Renderer rnd in _bodys)
            {
                rnd.SetPropertyBlock(_block);
                rnd.enabled = false;
            }
        }

        if (_itm)
        {
            _headBlock.SetFloat("_Alpha", 1f);
            foreach (Renderer rnd in _items)
            {
                rnd.enabled = true;
                rnd.SetPropertyBlock(_block);
            }
        }
        else if (!_itm)
        {
            _headBlock.SetFloat("_Alpha", 0f);
            foreach (Renderer rnd in _items)
            {
                rnd.SetPropertyBlock(_block);
                rnd.enabled = false;
            }
        }

    }

    public void AllErase()
    {
        Switch(false, false);
    }

    public void SetBody(float _dur)
    {
        StartCoroutine(StartFade(true, true, _dur));
    }

    public void EraseBody(float _dur)
    {
        StartCoroutine(StartFade(false, false, _dur));
    }

    IEnumerator StartFade(bool bl, bool withItem, float _fadetime)
    { //キャラクターのレンダラーをフェードイン、フェードアウトするスクリプト。With ○○ は、一緒に変化してほしいもの

        float _timer = 0f;

        if (bl)
        {



            foreach (Renderer rnd in _bodys)
            {
                rnd.enabled = true;
            }

            if (withItem)
            {
                foreach (Renderer rnd in _items)
                {
                    rnd.enabled = true;
                }
            }
        }




        while (_timer < 1f)
        {
            _timer = Mathf.Clamp01(_timer + (Time.deltaTime / _fadetime));

            if (bl)
            {
                _block.SetFloat("_Alpha", _timer);
            }
            else if (!bl)
            {
                _block.SetFloat("_Alpha", 1f - _timer);
            }
            foreach (Renderer rnd in _bodys)
            {
                rnd.SetPropertyBlock(_block);
            }

            if (withItem)
            {
                if (bl)
                {
                    _headBlock.SetFloat("_Alpha", _timer);
                }
                else if (!bl)
                {
                    _headBlock.SetFloat("_Alpha", 1f - _timer);
                }
                foreach (Renderer rnd in _items)
                {
                    rnd.SetPropertyBlock(_headBlock);
                }
            }


            yield return new WaitForEndOfFrame();
        }

        if (!bl)
        {



            foreach (Renderer rnd in _bodys)
            {
                rnd.enabled = false;
            }
            if (withItem)
            {
                foreach (Renderer rnd in _items)
                {
                    rnd.enabled = false;
                }
            }
        }

        yield break;
    }



}
