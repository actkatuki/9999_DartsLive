using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionModeSelect : MonoBehaviour
{

    public Button _tutorial, _result, _replay;

    public Color Resetcolor;
    public Color buttoncolor;


    public void TutrialMode()
    {
        if (!SceneLoader.Instance.GetTutorialMode())
        {
            _tutorial.GetComponent<Image>().color = buttoncolor;

            ColorBlock push = _tutorial.colors;
            push.normalColor = buttoncolor;
            push.highlightedColor = buttoncolor;
            _tutorial.colors = push;

        }
        else
        {
            _tutorial.GetComponent<Image>().color = Resetcolor;

            ColorBlock cb = _tutorial.colors;
            cb.normalColor = Resetcolor;
            cb.highlightedColor = Resetcolor;
            _tutorial.colors = cb;

        }
        SceneLoader.Instance.TutrialMode();
    }

    public void ResultMode()
    {
        if (!SceneLoader.Instance.GetResultMode())
        {
            _result.GetComponent<Image>().color = buttoncolor;

            ColorBlock push = _result.colors;
            push.normalColor = buttoncolor;
            push.highlightedColor = buttoncolor;
            _result.colors = push;
        }
        else
        {
            _result.GetComponent<Image>().color = Resetcolor;

            ColorBlock cb = _result.colors;
            cb.normalColor = Resetcolor;
            cb.highlightedColor = Resetcolor;
            _result.colors = cb;
        }
        SceneLoader.Instance.ResultMode();
    }

    public void RePlayMode()
    {
        if (!SceneLoader.Instance.GetReplayMode())
        {
            _replay.GetComponent<Image>().color = buttoncolor;

            ColorBlock push = _replay.colors;
            push.normalColor = buttoncolor;
            push.highlightedColor = buttoncolor;
            _replay.colors = push;
        }
        else
        {
            _replay.GetComponent<Image>().color = Resetcolor;

            ColorBlock cb = _replay.colors;
            cb.normalColor = Resetcolor;
            cb.highlightedColor = Resetcolor;
            _replay.colors = cb;
        }
        SceneLoader.Instance.ReplayMode();

    }


    public void PersonalMode()
    {
        _result.GetComponent<Image>().color = Resetcolor;
        _tutorial.GetComponent<Image>().color = Resetcolor;
        _replay.GetComponent<Image>().color = buttoncolor;

        ColorBlock push = _replay.colors;
        push.normalColor = buttoncolor;
        push.highlightedColor = buttoncolor;

        ColorBlock cb = _replay.colors;
        cb.normalColor = Resetcolor;
        cb.highlightedColor = Resetcolor;

        _replay.colors = push;
        _tutorial.colors = cb;
        _result.colors = cb;
        SceneLoader.Instance.PersonalPlaying();
    }



}
