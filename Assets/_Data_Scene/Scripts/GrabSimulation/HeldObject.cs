using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class HeldObject : ControllerInput
{
    public bool _useGripEvents;
    public UnityEvent _GripEvents;
    public bool _useReleaseEvents;
    public UnityEvent _ReleaseEvents;
    public Hand _con;

    public bool _grab;
    public bool _release;



    void Update()
    {
        if (_useGripEvents)
        {
            if (_grab)
            {
                Debug.Log(gameObject.name + "grab");
                _grab = false;
                _GripEvents.Invoke();
            }
        }
        if (_useReleaseEvents)
        {
            if (_release)
            {
                Debug.Log(gameObject.name + "release");
                _release = false;
                _ReleaseEvents.Invoke();
            }
        }
    }





}
