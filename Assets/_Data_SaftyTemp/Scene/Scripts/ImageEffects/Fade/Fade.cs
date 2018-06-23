using System;
using UnityEngine;
using UnityStandardAssets.ImageEffects;




[ExecuteInEditMode]

public class Fade : ImageEffectBase {

	public static Fade Instance = null;

	public Color _defaultColor;

	[Range(0, 1)]
	public float _amount;

	public bool _withAudioVolume;


	private enum State {
		Stationary, In, Out
	}
	private State _state = State.Stationary;

	private Color _color;
	private float _from;
	private float _to;
	private float _duration;
	private float _timeCount = 0;
    
	    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
	}


	public new void Start () {
		_color = _defaultColor;
	}


	void Update () {
		if(_state != State.Stationary) {
			_timeCount += Time.deltaTime / _duration;

			if(_timeCount < 1) {
				_amount = Mathf.Lerp(_from, _to, _timeCount);

				if(_withAudioVolume) {
					AudioListener.volume = 1 - _amount;
				}
			} else {
				_amount = _to;

				if(_withAudioVolume){
					AudioListener.volume = 1 - _to;
				}
					
				_state = State.Stationary;
			}
		}
	}


	void OnRenderImage (RenderTexture source, RenderTexture destination) {
		if(Application.isPlaying) {
			material.SetColor("_Color", _color);
		} else {
			material.SetColor("_Color", _defaultColor);
		}
		material.SetFloat("_Amount", _amount);

		Graphics.Blit (source, destination, material);
	}



	public void FadeIn (float duration) {
		_from = 1;
		_to = 0;
		_duration = duration;
		_color = _defaultColor;

		_timeCount = 0;
		_state = State.In;
	}

	public void FadeIn (float duration, Color color) {
		_from = 1;
		_to = 0;
		_duration = duration;
		_color = color;

		_timeCount = 0;
		_state = State.In;
	}


	public void FadeOut (float duration) {
		_from = 0;
		_to = 1;
		_duration = duration;
		_color = _defaultColor;

		_timeCount = 0;
		_state = State.Out;
	}

	public void FadeOut (float duration, Color color) {
		_from = 0;
		_to = 1;
		_duration = duration;
		_color = color;

		_timeCount = 0;
		_state = State.Out;
	}
}