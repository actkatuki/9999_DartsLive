using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton: MonoBehaviour {


	public Button[] _scene;
	public Button  _pict;
	public bool Playing;
	[Space(10)]

	public Color Resetcolor;
	public Color buttoncolor;

	private bool tutrialmode;
	private bool replaymode;
	private bool resultmode;


	private bool cameraviewmode;
	private bool pictmode;


	void Start () {
		Playing = false;
	}


	public void SceneSelect (int x) {
		foreach(Button sc in _scene) {
			ColorBlock cb = sc.colors;
			cb.normalColor = Resetcolor;
			sc.colors = cb;
		}
		ColorBlock push = _scene[x].colors;
		push.normalColor = buttoncolor;
		_scene[x].colors = push;
	}

	public void SceneStart () {
		Playing = true;
	}

	public void StartContents () {
		SceneLoader.Instance.SetContensStart(true);
	}


		public void PictMode () {

		if(!pictmode) {
            _pict.GetComponent<Image>().color = buttoncolor;
            
            ColorBlock push = _pict.colors;
			push.normalColor = buttoncolor;
			push.highlightedColor = buttoncolor;
			_pict.colors = push;
            
			pictmode = true;
		} else {
            _pict.GetComponent<Image>().color = Resetcolor;
            
            ColorBlock cb = _pict.colors;
			cb.normalColor = Resetcolor;
			cb.highlightedColor = Resetcolor;
			_pict.colors = cb;
            
			pictmode = false;
		}
	}
}
