using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ControllerInput: MonoBehaviour {
	public bool _highlight;
	private HighLightSwitcher[] _switch;
	public bool _defaultCondition;
	protected bool _condition;
	protected bool _isInitial = true;
	public bool isInitial
	{
		get
		{
			return _isInitial;
		}
	}

	//--- 接触 ---
	protected Collider _collider;
	protected bool _isLeftEntered = false;
	protected bool _isRightEntered = false;
	private bool _isLeftTouching = false;
	private bool _isRightTouching = false;
	//----------------------------------------

	//--- コントローラー ---
	protected Transform _currentController = null;
	protected bool _isUsingLeft = false;
	protected bool _isUsingRight = false;
	public enum Side {
		Left, Right
	}
	//----------------------------------------

	//--- Transform ---
	protected Transform _target;
	public Transform target
	{
		get
		{
			return _target;
		}
	}


	public virtual void Start () {
		_condition = _defaultCondition;
		_collider = gameObject.GetComponent<Collider>();
		_highlight = true;
		_switch = gameObject.GetComponentsInChildren<HighLightSwitcher>();
	}

	private void OnTriggerEnter (Collider collider) {
		if(_condition) {
			// 左がオブジェクトに触れた
			//if(!_isUsingLeft && !ControllerManager.Instance.leftStay) {
				if(collider == ControllerManager.Instance.leftCollider) {
					if(!ControllerManager.Instance.leftEnter) {
						ForceTriggerEnter(Side.Left);
					} else {
						ControllerManager.Instance.leftTouchingObject = this;
					}
				}
			//}

			// 右がオブジェクトに触れた
			//if(!_isUsingRight && !ControllerManager.Instance.rightStay) {
				if(collider == ControllerManager.Instance.rightCollider) {
					if(!ControllerManager.Instance.rightEnter) {
						ForceTriggerEnter(Side.Right);
					} else {
						ControllerManager.Instance.rightTouchingObject = this;
					}
				}
			//}
		}
	}

	private void OnTriggerExit (Collider collider) {
		// 左がオブジェクトから離れた
		if(collider == ControllerManager.Instance.leftCollider) {
			_isLeftEntered = false;
			_isLeftTouching = false;
			ControllerManager.Instance.leftEnter = null;

			if(ControllerManager.Instance.leftEnter == _collider) {
				if(ControllerManager.Instance.leftTouchingObject == null) {
					ControllerManager.Instance.leftEnter = null;

					if(!ControllerManager.Instance.leftTriggered) {
					}
				} else {
					ControllerManager.Instance.leftTouchingObject.ForceTriggerEnter(Side.Left);
				}
			} else {
				if(ControllerManager.Instance.leftTouchingObject == this) {
					ControllerManager.Instance.leftTouchingObject = null;
				}
			}


			if(!_isRightEntered) {
				SetHighlight(false);
			}
		}

		// 右がオブジェクトから離れた
		if(collider == ControllerManager.Instance.rightCollider) {
			_isRightEntered = false;
			_isRightTouching = false;
			ControllerManager.Instance.rightEnter = null;
			if(ControllerManager.Instance.rightEnter == _collider) {
				if(ControllerManager.Instance.rightTouchingObject == null) {
					ControllerManager.Instance.rightEnter = null;

					if(!ControllerManager.Instance.rightTriggered) {
					}
				} else {
					ControllerManager.Instance.rightTouchingObject.ForceTriggerEnter(Side.Right);
				}
			} else {
				if(ControllerManager.Instance.rightTouchingObject == this) {
					ControllerManager.Instance.rightTouchingObject = null;
				}
			}


			if(!_isLeftEntered) {
				SetHighlight(false);
			}
		}
	}
	public void ForceTriggerEnter (Side side) {
		if(side == Side.Left) {
			_isLeftEntered = true;
			_isLeftTouching = true;
            if (this.GetComponent<Collider>() != ControllerManager.Instance.leftTriggered)
            {
                SetHighlight(true);

            }
               

			ControllerManager.Instance.leftEnter = _collider;
			ControllerManager.Instance.leftTouchingObject = null;
		} else if(side == Side.Right) {
			_isRightEntered = true;
			_isRightTouching = true;
            if (this.GetComponent<Collider>() != ControllerManager.Instance.rightTriggered)
            {
                SetHighlight(true);
            }

            ControllerManager.Instance.rightEnter = _collider;
			ControllerManager.Instance.rightTouchingObject = null;
		}
	}


	public void ForceTriggerExit (Side side) {
		if(side == Side.Left) {
			_isLeftEntered = false;

			ControllerManager.Instance.leftTouchingObject = this;

			if(!_isRightEntered) {
				SetHighlight(false);
			}
		} else if(side == Side.Right) {
			_isRightEntered = false;

			ControllerManager.Instance.rightTouchingObject = this;

			if(!_isLeftEntered) {
				SetHighlight(false);
			}
		}
	}
	//----------------------------------------

	//----------------------------------------
	//	ハイライト状態更新
	//----------------------------------------
	public void SetHighlight (bool isActive) {
		if(isActive) {
			if(_highlight) {
				foreach(HighLightSwitcher _sw in _switch) {
					_sw.Highlight();
				}
			}
		} else {
			if(_highlight) {
				foreach(HighLightSwitcher _sw in _switch) {
					_sw.Highoff();
				}
			}
		}
	}
	//----------------------------------------

	//----------------------------------------
	//	コンディション更新
	//----------------------------------------
	public void SetCondition (bool isActive) {
		_condition = isActive;
	}
	//----------------------------------------



	//----------------------------------------
	//強制的にコントローラーをフリーにする。
	//----------------------------------------
	public void NullController () {
		ControllerManager.Instance.leftTriggered = null;
		ControllerManager.Instance.rightTriggered = null;
		ControllerManager.Instance.leftEnter = null;
		ControllerManager.Instance.rightEnter = null;
	}
	//----------------------------------------

		//----------------------------------------
		//	トリガー操作
		//----------------------------------------
		public void TriggerOn (Side side) {
			if(side == Side.Left) {
				_isLeftEntered = false;
				_isUsingLeft = true;
				_isUsingRight = false;

				_currentController = ControllerManager.Instance.leftController;

				ControllerManager.Instance.leftEnter = null;
			} else if(side == Side.Right) {
				_isRightEntered = false;
				_isUsingLeft = false;
				_isUsingRight = true;

				_currentController = ControllerManager.Instance.rightController;

				ControllerManager.Instance.rightEnter = null;
			}


			if(!_isLeftEntered && !_isRightEntered) {
				SetHighlight(false);
			}
		}


		public void TriggerOff (Side side) {
			if(side == Side.Left) {
				_isUsingLeft = false;

				if(_isLeftTouching) {
					// 「OnTriggerEnter 左がオブジェクトに触れた」と同等
					_isLeftEntered = true;

					SetHighlight(true);

					ControllerManager.Instance.leftEnter = _collider;
				}
			} else if(side == Side.Right) {
				_isUsingRight = false;

				if(_isRightTouching) {
					// 「OnTriggerEnter 右がオブジェクトに触れた」と同等
					_isRightEntered = true;

					SetHighlight(true);

					ControllerManager.Instance.rightEnter = _collider;
				}
			}
		}
		//----------------------------------------


	public void SetHighLightCondition(bool condition) {
		_highlight = condition;
	}


}
