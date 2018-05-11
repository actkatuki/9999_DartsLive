using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIOpener : MonoBehaviour
{

    private Animator _UIopen;
    void Start()
    {
        _UIopen = gameObject.GetComponent<Animator>();
    }

    public void UIOpen()
    {
        _UIopen.SetBool("Open", true);
        _UIopen.SetBool("Close", false);
    }

    public void UIClose()
    {
        _UIopen.SetBool("Open", false);
        _UIopen.SetBool("Close", true);
    }
}
