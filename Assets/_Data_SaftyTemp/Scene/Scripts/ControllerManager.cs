
using UnityEngine;
using System;
using System.Collections;


public class ControllerManager: MonoBehaviour {

	public static ControllerManager Instance = null;

	public SteamVR_TrackedController R_con, L_con;
	private bool R_click, L_click, L_unpre, R_unpre;
	private bool R_trigger, L_trigger;

	public Transform rightController, leftController;
	public Vector3 R_clickpoint, L_clickpoint;

	public Collider leftCollider;
	public Collider rightCollider;

	public Collider leftEnter;
	public Collider rightEnter;
	public Collider leftTriggered;
	public Collider rightTriggered;

	public bool leftDown;
	public bool leftStay;
	public bool leftUp;
	public bool leftcondition;

	public bool rightDown;
	public bool rightStay;
	public bool rightUp;
	public bool rightcondition;
	private float _timer;

	//--- 接触 ---

	//private bool _isLeftTouching = false;
	//private bool _isRightTouching = false;
	//----------------------------------------
	public ControllerInput leftEnterObject;
	public ControllerInput rightEnterObject;

	public ControllerInput leftTouchingObject;
	public ControllerInput rightTouchingObject;



	private void Awake () {
		if(Instance == null) {
			Instance = this;
		} else {
			Destroy(this.gameObject);
		}
		leftcondition = true;
		rightcondition = true;
		R_con.TriggerClicked += (sender, e) => TriggerPress(0);
		L_con.TriggerClicked += (sender, e) => TriggerPress(1);
		R_con.TriggerUnclicked += (sender, e) => TriggerUnPress(0);
		L_con.TriggerUnclicked += (sender, e) => TriggerUnPress(1);

	}


	public void TriggerPress (int c) {
		if(c == 0) {
			R_clickpoint = R_con.transform.position;
			R_click = true;
		} else if(c == 1) {
			L_clickpoint = L_con.transform.position;
			L_click = true;
		}
	}



	private void Update () {
		if(L_click) {
			L_trigger = true;
			L_click = false;
		}
		if(!leftTriggered) {
			if(leftUp) {
				leftUp = false;
			}
		}

		if(L_trigger) {
			if(leftEnter && !leftTriggered) {
				leftTriggered = leftEnter;

				leftDown = true;
			} else {
				if(leftDown) {
					leftDown = false;
				}

				if(!leftStay) {
					leftStay = true;
				}
			}
		}
		if(L_unpre) {
			leftUp = true;
			L_unpre = false;
		}

		if(R_click) {
			R_trigger = true;
			R_click = false;
		}
		if(!rightTriggered) {
			if(rightUp) {
				rightUp = false;
			}
		}

		if(R_trigger) {
			if(rightEnter && !rightTriggered) {
				rightTriggered = rightEnter;

				rightDown = true;
			} else {
				if(rightDown) {
					rightDown = false;
				}

				if(!rightStay) {
					rightStay = true;
				}
			}
		}
		if(R_unpre) {
			rightUp = true;
			R_unpre = false;
		}
	}
	public void TriggerUnPress (int c) {

		if(c == 0) {
			R_unpre = true;
			rightStay = false;
			rightTriggered = null;
			R_trigger = false;
		} else if(c == 1) {
			L_unpre = true;
			leftStay = false;
			leftTriggered = null;
			L_trigger = false;
		}
	}

	//バイブレーションコルーチン
	public void Vibration (Collider con) {
		StartCoroutine("Viveration", con);
	}

	 IEnumerator Viveration (Collider con) {
		while(_timer < 10) {

			_timer += Time.deltaTime;
			if(con == rightCollider) {
				var trackobj = R_con.GetComponent<SteamVR_TrackedObject>();
				var device = SteamVR_Controller.Input((int)trackobj.index);
				device.TriggerHapticPulse(2000);

			}
			if(con == leftCollider) {
				if(!leftTriggered) {
					var trackobj = L_con.GetComponent<SteamVR_TrackedObject>();
					var device = SteamVR_Controller.Input((int)trackobj.index);
					device.TriggerHapticPulse(2000);
				}
			}
		}
		yield return null;
		_timer = 0f;
	}

	public void SetCondition (bool isTouch, bool isLeft) {
		if(isLeft) {
			leftcondition = isTouch;
		} else {
			rightcondition = isTouch;
		}

	}
	public bool CheckCondition (bool isLeft) {
		if(isLeft) {
			return leftcondition;
		} else {
			return rightcondition;
		}
	}
}

