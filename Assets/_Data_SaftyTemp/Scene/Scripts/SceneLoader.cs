using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class SceneLoader: MonoBehaviour {

	public static SceneLoader Instance = null;

    private bool _resultmode;
    private bool _replaymode;
    private bool _tutrialmode;
    private bool _tutrialplaying;

    /// <summary>
    /// ///////////システムシーンのID
    /// </summary>
    [Range(3, 5)]
	private static int SceneID = 3;
    private static int OptionID = 0;

	private static int SceneSave = 0;

	private bool ContentsStart;

	private void Awake () {
		if(Instance == null) {
			Instance = this;
		} else {
			Destroy(this.gameObject);
		}
#if UNITY_EDITOR
		SetSceneSave();

#endif


#if !UNITY_EDITOR
		SceneManager.LoadScene(1, LoadSceneMode.Additive);
		SceneManager.LoadScene(2, LoadSceneMode.Additive);
        
#endif
	}



	public void Reload () {

		if(SceneSave > 2) {

                SceneManager.UnloadSceneAsync(SceneSave);
            
		}
            SceneManager.LoadScene(SceneID, LoadSceneMode.Additive);
        

        SetSceneSave();
		SetContensStart(true);
	}

	public void SetSceneID (int i) {
		SceneID = i;
	}

    public void LoadSystem () {
		if(SceneSave > 2) {
            if (_tutrialplaying)
            {
                SceneManager.UnloadSceneAsync(SceneSave+1);
            }
            else
            {
                SceneManager.UnloadSceneAsync(SceneSave);
            }
		}

        /////////////////////////////////チュートリアルシーンは対応シーンの次のIDになるようにする
        if (_tutrialmode)
        {
            SetTutrialMode(true);
            SceneManager.LoadScene(SceneID + 1, LoadSceneMode.Additive);
        }
        else
        {
            SceneManager.LoadScene(SceneID, LoadSceneMode.Additive);
        }

        SetSceneSave();
	}


    public void TutorialEnd()
    {
        
        SetTutorial(false);

        if (SceneSave > 2)
        {
            SceneManager.UnloadSceneAsync(SceneSave + 1);
        }
        SceneManager.LoadScene(SceneID, LoadSceneMode.Additive);

        SetSceneSave();
        SetContensStart(true);
    }


    /// <summary>
    /// //////////////////////////選択中のSceneIDのGetSet
    /// </summary>
    /// <returns></returns>
    public int GetSceneID () {
		return SceneID;
	}

	public void SetSceneSave () {
		SceneSave = SceneID;
	}

    /// <summary>
    /// //////////////////////////プレイは一回だけ。Enter連打回避
    /// </summary>
    /// <param name="ready"></param>

	public void SetContensStart (bool ready) {
		ContentsStart = ready;
	}

	public bool GetContentsStart () {
		return ContentsStart;
	}


	/// <summary>
	/// //////////////////////////モードの確認////////////////////////////
	/// </summary>
	public void ReplayMode () {
		_replaymode = !_replaymode;
	}

	public bool GetReplayMode () {
		return _replaymode;
	}

	public void ResultMode () {
		_resultmode = !_resultmode;
	}

	public bool GetResultMode () {
		return _resultmode;
	}

	public void TutrialMode () {

        if (!_tutrialmode)
        {
            _tutrialmode = true;
        }
        else
        {
            _tutrialmode = false;
        }
	}

    public bool GetTutorialMode()
    {
        return _tutrialmode;
    }

    public void SetTutorial(bool mode)
    {
        _tutrialmode = mode;
    }
    
	public bool GetTutrialPlaying () {
		return _tutrialplaying;
	}
    public void SetTutrialMode(bool playing)
    {
         _tutrialplaying = playing;
    }
    /////////////////////////////////////////////////////////////////////////


    public void PersonalPlaying()
    {
        _replaymode = true;
        _tutrialmode = false;
        _resultmode = false;
    }

}
