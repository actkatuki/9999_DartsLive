using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonSet : MonoBehaviour {

    public Button[] _SetButton;
    private bool[] _onoff;

    public Color Resetcolor;
    public Color buttoncolor;



    private void Start()
    {
        _onoff = new bool[_SetButton.Length];
    }

    public void PushImageButton(int x)
    {
        foreach(Button sc in _SetButton)
        {
            sc.GetComponent<Image>().color = Resetcolor;
        }

        _SetButton[x].GetComponent<Image>().color = buttoncolor;
    }


    public void PushButton(int x)
    {
        foreach (Button sc in _SetButton)
        {
            ColorBlock cb = sc.colors;
            cb.normalColor = Resetcolor;
            sc.colors = cb;
        }
        ColorBlock push = _SetButton[x].colors;
        push.normalColor = buttoncolor;
        _SetButton[x].colors = push;
    }

    public void PushOnOff(int x)
    {
        if (!_onoff[x])
        {
            _SetButton[x].GetComponent<Image>().color = buttoncolor;

            ColorBlock push = _SetButton[x].colors;
            push.normalColor = buttoncolor;
            push.highlightedColor = buttoncolor;
            _SetButton[x].colors = push;

            _onoff[x] = true;
        }
        else
        {
            _SetButton[x].GetComponent<Image>().color = Resetcolor;

            ColorBlock cb = _SetButton[x].colors;
            cb.normalColor = Resetcolor;
            cb.highlightedColor = Resetcolor;
            _SetButton[x].colors = cb;

            _onoff[x] = false;
        }
    }



}
