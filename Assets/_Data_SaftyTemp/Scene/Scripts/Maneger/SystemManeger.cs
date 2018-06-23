
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using UnityEngine.Events;


public class SystemManeger : MonoBehaviour {

	public static SystemManeger Instance = null;

	//--- Enter時コールバック ---
	public bool _useDoCallback = false;
	public UnityEvent _doCallback;
	//----------------------------------------

	public bool StartOK;

	//--- Start時コールバック ---
	public bool _useStartCallback = false;
	public UnityEvent _StartCallback;
	//----------------------------------------



	private void Awake () {
		if(Instance == null) {
			Instance = this;
		} else {
			Destroy(this.gameObject);
		}
	}

    /// <summary>
    /// ////////////チュートリアルの時はリロードでシーンIDをマイナスする。
    /// </summary>
	private void Start () {

		if(SceneLoader.Instance.GetTutorialMode()) {
			SceneLoader.Instance.SetTutrialMode(true);
		} else {
			SceneLoader.Instance.SetTutrialMode(false);
		}
	}


    /// <summary>
    /// /////////////キーイベント（EnterとEsc）スタートとリスタート
    /// </summary>
	private void Update () {
		if(Input.GetKeyDown(KeyCode.Return)) {
            StartContents();
        }

        
		if(Input.GetKeyDown(KeyCode.Escape)) {
			if(SceneLoader.Instance.GetTutrialPlaying()) {
				TutorialEnd();
			} else {
				Reload();
			}
		}
	}


    /// <summary>
    /// ///////////コンテンツのスタート
    /// ///////////イベントを２グループ呼ぶ
    /// </summary>
    public void StartContents()
    {
        if (SceneLoader.Instance.GetContentsStart())
        {
            SceneLoader.Instance.SetContensStart(false);
            StartCoroutine("StartCT");

        }

    }


	IEnumerator StartCT () {
		if(_useDoCallback) {
			_doCallback.Invoke();
		}


		while(!StartOK) {
			yield return null;
		}


		if(_useStartCallback) {
			_StartCallback.Invoke();
		}
	}

    /// <summary>
    /// ///////////エンターで始めた後のインターバル
    /// </summary>
	public void TryOK () {
        StartOK = true;
	}


    /// <summary>
    /// ///////////コンテンツの終了
    /// </summary>
	public void Endiong () {
		StartCoroutine("EndingCT");
	}

	IEnumerator EndingCT () {

		Fade.Instance.FadeOut(3f);
		yield return new WaitForSeconds(3f);

		SceneLoader.Instance.Reload();
	}


    /// <summary>
    /// ////////////チュートリアルから本編への移動
    /// </summary>
    /// 
	public void TutorialEnd () {
		StartCoroutine("TutorialEndCT");
	}

	IEnumerator TutorialEndCT () {

		Fade.Instance.FadeOut(3f);
		yield return new WaitForSeconds(3f);

		SceneLoader.Instance.TutorialEnd();
	}


    /// <summary>
    /// ////////////シーンリロード
    /// </summary>
    public void Reload()
    {
        SceneLoader.Instance.Reload();
        SceneLoader.Instance.SetContensStart(true);
    }
}

