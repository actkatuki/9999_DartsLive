using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeLagEvents : MonoBehaviour {

    public float time;
    private float remenbertime;
    public UnityEvent _CallBack;


     void Awake()
    {
        remenbertime = time;
        gameObject.SetActive(false);
    }
    

    private void OnEnable()
    {
        StartCoroutine("Counting");
    }

    IEnumerator Counting()
    {
        
        while (time > 0f)
        {
            time -= Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }
        Debug.Log(gameObject.name + "   TimeCount");
        _CallBack.Invoke();
        gameObject.SetActive(false);
        time = remenbertime;
    }

}
