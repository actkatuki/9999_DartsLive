using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationNumber : MonoBehaviour {


    public Animator[] _Replayanimator;



    private void Awake()
    {
        _Replayanimator = gameObject.GetComponentsInChildren<Animator>();
        foreach(Animator _third in _Replayanimator)
        {
            _third.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            PlayAnimationID(1);
        }
    }





    public void PlayAnimationID(int ID)
    {
        _Replayanimator[ID].gameObject.SetActive(true);
        _Replayanimator[ID].GetComponent<CautionNaration>().NarationPlay();
        StartCoroutine("PlayBack", ID);
    }

    IEnumerator PlayBack(int ID)
    {

        while (!_Replayanimator[ID].GetComponent<CautionNaration>().GetNarationIsPlaying())
        {
            yield return null;
        }

        _Replayanimator[ID].SetTrigger("Start");
    }



}
