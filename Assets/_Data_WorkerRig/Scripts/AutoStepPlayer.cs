using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AutoStepPlayer: MonoBehaviour {

    public MoveOnPath _mop;
    private float _cashed;

    public float [] _checks;

    public AudioSource _audio;

    // Use this for initialization
    void Start () {
        _cashed = 0f;


    }

    private void Update () {
        if (_mop._phase > 0f && _mop._phase < 1f) {
            Check();
        }
    }

    public void Check () {
        float fl = _mop._easer.Evaluate( _mop._phase)+0.03f;
        for (int i = 0; i < _checks.Length; i++) {
            
            if (_cashed < _checks [i] && fl >= _checks [i]) {
                
                    _audio.Play();

                    Debug.Log("Check?");
                
            }
        }
        _cashed = fl;

    }


}
