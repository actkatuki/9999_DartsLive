using UnityEngine;
using System.Collections;

public class FootSuspender: MonoBehaviour {

    public float controllerHeight = 0.15f, footHieght = 0.1067085f;
    public Transform controllerTr, locator;
    public SteamVR_RenderModel _model;


    // Use this for initialization
    void Start () {
       // Fit();
    }

    // Update is called once per frame
    void Update () {

        if (Input.GetKeyDown(KeyCode.Space)) {

            Fit();
           
        }

        //locator.rotation = Quaternion.LookRotation(new Vector3(-locator.forward.y,0f,locator.forward.z));
        
    }

    void Fit () {
        controllerHeight = controllerTr.localPosition.y;
        string renderName = _model.renderModelName;

        if (renderName.IndexOf("vr_controller_vive_1_5") > -1) {
            locator.localPosition = new Vector3(locator.localPosition.x, locator.localPosition.y, -(controllerHeight - (footHieght)));
            locator.localRotation = Quaternion.identity;
            Debug.Log("Controller");
        } else if (renderName.IndexOf("{htc}vr_tracker_vive_1_0") > -1) {
            locator.localPosition = new Vector3(locator.localPosition.x, -(controllerHeight - (footHieght)), locator.localPosition.z);
            locator.localRotation = Quaternion.Euler(new Vector3(-90f, 0f, 0f));
            Debug.Log("Tracker");
        }
        
    }

}
