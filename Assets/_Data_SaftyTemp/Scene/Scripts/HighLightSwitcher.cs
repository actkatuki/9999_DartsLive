using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HighLightSwitcher: MonoBehaviour {
	[HideInInspector]
	public List<MeshRenderer> _mesh = new List<MeshRenderer>();

	private bool _condition;
	private bool attention;

	void Awake () {
		_condition = true;
		_mesh = gameObject.GetComponentsInChildren<MeshRenderer>().ToList();
	}


	void Start () {

		List<HighLightSwitcher> _test = gameObject.GetComponentsInChildren<HighLightSwitcher>().Where(x => x != this).ToList();

		List<MeshRenderer> _childMesh = new List<MeshRenderer>();
		foreach(HighLightSwitcher _ts in _test) {
			_childMesh = _childMesh.Union(_ts._mesh).ToList();
		}

		_mesh = _mesh.Except(_childMesh).ToList();
	}

	void Update () {
		if(!_condition) {
			Highoff();
		}
	}


	public virtual void Highlight () {
		if(_condition) {
			foreach(MeshRenderer mat in _mesh) {
				mat.material.SetFloat("_Highlight", 1f);
			}
		}
	}


	public virtual void Highoff () {
		foreach(MeshRenderer mat in _mesh) {
			mat.material.SetFloat("_Highlight", 0f);
		}
	}

	public virtual void HighGradation () {
		if(_condition) {
			StopAllCoroutines();
			StartCoroutine("HighGraCT");
		}
	}

	public virtual void HighGraOff () {
		attention = false;
	}

	IEnumerator HighGraCT () {
		float timer = 0;
		attention = true;
		while(attention) {

			timer += Time.deltaTime;
			foreach(MeshRenderer mat in _mesh) {
				mat.material.SetFloat("_Highlight", Mathf.PingPong(timer, 1f));
			}
			yield return new WaitForEndOfFrame();
		}
	}

	public void SetCondition (bool high) {
		_condition = high;
	}

}
