using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvents: MonoBehaviour {

	public bool _defaultcondition;
	private bool _condition;



	public Collider[] _target;
	public UnityEvent _EnterEvents;

	public bool _endless;

    public UnityEvent _ExitEvents;

    private Collider _myself;

	private void Start () {
		_condition = _defaultcondition;
		_myself = gameObject.GetComponent<Collider>();
	}

	private void OnTriggerEnter (Collider other) {
		if(_condition) {

            
            foreach (Collider col in _target) {
				if(other == col) {
                    Debug.Log("T_Event"+col.name);

                    _EnterEvents.Invoke();
                    
                    if (!_endless) {
                        Setcondition(false);
                    }
				}
			}
		}

	}

    	private void OnTriggerExit (Collider other) {
		if(_condition) {

            
            foreach (Collider col in _target) {
				if(other == col) {
                    Debug.Log("T_Event"+col.name);

                    _ExitEvents.Invoke();
                    
                    if (!_endless) {
                        Setcondition(false);
                    }
				}
			}
		}

	}

	public void Setcondition (bool isActive) {
		_condition = isActive;
	}


}
