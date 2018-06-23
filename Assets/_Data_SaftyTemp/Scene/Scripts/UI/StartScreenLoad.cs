using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScreenLoad : MonoBehaviour
{
    [SerializeField]
    Image _bgImage;

    [SerializeField]
    Image _mask;

    [SerializeField]
    Image _main;

    Color _phantomWhite = new Color(1f, 1f, 1f, 0f);

    private void Start()
    {
        TitleLoadCheck._titleLoaded = true;
    }


    //下記コルーチンをを実行（外から呼ばれる前提）
    public void FadeOutToLoad(float duration)
    {
        StopAllCoroutines();
        StartCoroutine(FadeAndLoadCoroutine(duration));
    }

    //フェードアウトを実行しつつ、UIの画像もアルファで消してく。
    //終わったら1番のシーンをロード。

    IEnumerator FadeAndLoadCoroutine(float duration)
    {

        float _timer = 0f;

        while (_timer < 1f)
        {
            _timer = Mathf.Clamp01(_timer + Time.deltaTime / duration);

            _bgImage.color = Color.Lerp(Color.white, _phantomWhite, _timer);

            yield return new WaitForEndOfFrame();
        }

        _main.gameObject.SetActive(false);
        _mask.gameObject.SetActive(false);


    }
}
