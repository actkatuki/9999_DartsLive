using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand: MonoBehaviour {
	private GameObject heldObject;
	Rigidbody simulator;

	private bool _hold;
    [SerializeField]
	private Hand _otherhand;

	void Start () {
		simulator = new GameObject().AddComponent<Rigidbody>();
		simulator.name = "simulator";
		simulator.transform.parent = transform.parent;
	}

	void Update () {
		if(!heldObject) {
			if(this.name == "Controller (left)") {
				ControllerManager.Instance.leftcondition = true;

			}
			if(this.name == "Controller (right)") {
				ControllerManager.Instance.rightcondition = true;

			}
		}
		if(heldObject) {
			simulator.velocity = (transform.position - simulator.position) * 50f;

			if(this.name == "Controller (left)") {
				if(ControllerManager.Instance.leftUp) {


					heldObject.transform.parent = null;
					heldObject.GetComponent<Rigidbody>().isKinematic = false;
					heldObject.GetComponent<Rigidbody>().velocity = simulator.velocity;
					heldObject.GetComponent<HeldObject>()._con = null;
					heldObject.GetComponent<HeldObject>()._release = true;
					heldObject.GetComponent<HeldObject>().SetHighLightCondition(true);


					heldObject = null;
				}
			} else if(this.name == "Controller (right)") {
				if(ControllerManager.Instance.rightUp) {

					heldObject.transform.parent = null;
					heldObject.GetComponent<Rigidbody>().isKinematic = false;
					heldObject.GetComponent<Rigidbody>().velocity = simulator.velocity;
					heldObject.GetComponent<HeldObject>()._con = null;
					heldObject.GetComponent<HeldObject>()._release = true;
					heldObject.GetComponent<HeldObject>().SetHighLightCondition(true);

					heldObject = null;
				}
			}

		} else {

			if(this.name == "Controller (left)") {
				if(ControllerManager.Instance.leftEnter != null) {
					if(ControllerManager.Instance.leftEnter == ControllerManager.Instance.leftTriggered) {
						if(ControllerManager.Instance.leftDown) {
							if(heldObject == null && ControllerManager.Instance.leftEnter.GetComponent<HeldObject>()) {
								_hold = true;
								heldObject = ControllerManager.Instance.leftTriggered.gameObject;
								heldObject.GetComponent<HeldObject>().SetHighlight(false);
                                heldObject.transform.parent = transform;
                                heldObject.GetComponent<Rigidbody>().isKinematic = true;
                                heldObject.GetComponent<HeldObject>()._con = this;
                                heldObject.GetComponent<HeldObject>()._grab = true;

                                if (heldObject == _otherhand.heldObject) {
									_otherhand.heldObject = null;
								}

                                /////////////////////ポジション指定の小物///////////////////////////////////////
                                if (heldObject.GetComponent<HoldPosition>())
                                {
                                    heldObject.GetComponent<HoldPosition>().HoldPos();

                                    heldObject.GetComponent<HeldObject>().SetHighlight(false);

                                }
                                ////////////////////////////////////////////////////////////////////////////////

                                if (this.name == "Controller (left)") {
									ControllerManager.Instance.leftcondition = false;
								}
								if(this.name == "Controller (right)") {
									ControllerManager.Instance.rightcondition = false;
								}
                                
							}
						}
					}
				}
			} else if(this.name == "Controller (right)") {
				if(ControllerManager.Instance.rightEnter != null) {
					if(ControllerManager.Instance.rightEnter == ControllerManager.Instance.rightTriggered) {
						if(ControllerManager.Instance.rightDown) {
							if(heldObject == null && ControllerManager.Instance.rightEnter.GetComponent<HeldObject>()) {
								_hold = true;
								heldObject = ControllerManager.Instance.rightTriggered.gameObject;
								heldObject.GetComponent<HeldObject>().SetHighlight(false);

                                heldObject.transform.parent = transform;
                                heldObject.GetComponent<Rigidbody>().isKinematic = true;
                                heldObject.GetComponent<HeldObject>()._con = this;
                                heldObject.GetComponent<HeldObject>()._grab = true;
                                if (heldObject == _otherhand.heldObject) {
									_otherhand.heldObject = null;
								}


                                /////////////////////ポジション指定の小物///////////////////////////////////////
                                if (heldObject.GetComponent<HoldPosition>())
                                {
                                    heldObject.GetComponent<HoldPosition>().HoldPos();

                                    heldObject.GetComponent<HeldObject>().SetHighlight(false);

                                }
                                ////////////////////////////////////////////////////////////////////////////////

                                if (this.name == "Controller (left)") {
									ControllerManager.Instance.leftcondition = false;
								}
								if(this.name == "Controller (right)") {
									ControllerManager.Instance.rightcondition = false;
								}
                                
							}
						}
					}
				}
			}
		}
	}
}
