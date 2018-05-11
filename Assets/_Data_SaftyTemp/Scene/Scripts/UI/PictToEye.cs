using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PictToEye : MonoBehaviour {

    private Image _background;

    private void Awake()
    {
        _background = gameObject.GetComponent<Image>();
    }
}
