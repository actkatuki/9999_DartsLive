using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.GetComponent<AudioSource>().Play();
	}
    void Update()
    {
        var trackedObject = GetComponent<SteamVR_TrackedObject>();
        var device = SteamVR_Controller.Input((int)trackedObject.index);


        if (device.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu))
        {
            SystemManeger.Instance.Reload();
        }

    }
}
