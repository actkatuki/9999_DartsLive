using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageSelect : MonoBehaviour {

    public GameObject[] Panel;

    void OnEnable()
    {
        Invoke("PositionReset",1f);
    }

    public void PanelSelect(int x)
    {
        foreach(GameObject pa in Panel)
        {
            pa.SetActive(false);
        }

        Panel[x].SetActive(true);
    }
}
