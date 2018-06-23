using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grubber: MonoBehaviour {

    private Transform _selfTr;
    public SteamVR_TrackedController viveCon;
    public Animator _selfHandAnim, _vrikBodyAnim;
    [SerializeField]
    private bool _grubbing, _triggered, _gripped;
    [SerializeField]
    private bool isRight;

    private void Awake () {
        if (viveCon == null) {
            viveCon = GetComponent<SteamVR_TrackedController>();
        }
        viveCon.Gripped += ( sender, e ) => Grip();
        viveCon.Ungripped += ( sender, e ) => UnGrip();
        viveCon.TriggerClicked += ( sender, e ) => Trigger();
        viveCon.TriggerUnclicked += ( sender, e ) => UnTrigger();

    }

    // Use this for initialization
    void Start () {

    }

    void Grip () {

        CheckAndDoGrub();
        _gripped = true;
    }

    void UnGrip () {
        _gripped = false;
        CheckAndDoUnGrub();
    }

    void Trigger () {

        CheckAndDoGrub();
        _triggered = true;
    }

    void UnTrigger () {
        _triggered = false;
        CheckAndDoUnGrub();
    }

    void CheckAndDoGrub () {
        if (!_triggered && !_gripped) {
            _grubbing = true;
            _selfHandAnim.SetBool("Gripped", true);

            if (_vrikBodyAnim != null) {
                if (!isRight) {
                    _vrikBodyAnim.SetBool("LeftGrip", true);
                } else if (isRight) {
                    _vrikBodyAnim.SetBool("RightGrip", true);
                }
            }

        }
    }

    void CheckAndDoUnGrub () {
        if (!_triggered && !_gripped) {
            _grubbing = false;
            _selfHandAnim.SetBool("Gripped", false);
            if (_vrikBodyAnim != null) {
                if (!isRight) {
                    _vrikBodyAnim.SetBool("LeftGrip", false);
                } else if (isRight) {
                    _vrikBodyAnim.SetBool("RightGrip", false);
                }
            }
        }
    }
}