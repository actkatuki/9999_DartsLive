using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CautionNaration : MonoBehaviour {

	public AudioSource _naration;
    private bool _playend;

 public void NarationPlay() {
		_naration.Play();
        StartCoroutine("NarationPlayCT");
	}


    IEnumerator NarationPlayCT()
    {
        while (_naration.isPlaying)
        {
            yield return null;
        }
        _playend = true;
    }



    public bool GetNarationIsPlaying()
    {
        return _playend;
    }

}
