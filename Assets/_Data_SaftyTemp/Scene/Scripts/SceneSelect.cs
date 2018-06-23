
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSelect : MonoBehaviour
{

    public int _defaultscene;

    private bool _loading;
#if UNITY_EDITOR


    void Awake()
    {
        SceneLoader.Instance.SetSceneID(_defaultscene);
        SceneLoader.Instance.SetSceneSave();
    }
#endif

    /// <summary>
    /// ///////////シーンローダーの中継
    /// </summary>
    /// <param name="i"></param>

    public void SceneChange(int i)
    {
        SceneLoader.Instance.SetSceneID(i);

    }
    public void SceneLoad()
    {
        Invoke("Load", 0.5f);
    }

    public void AppExit()
    {
        Application.Quit();
    }

    public void ReMenu()
    {
        _loading = false;
    }

    private void Load()
    {
        if (!_loading)
        {
            SceneLoader.Instance.LoadSystem();
            SceneLoader.Instance.SetSceneSave();
            _loading = true;
        }
    }
}
