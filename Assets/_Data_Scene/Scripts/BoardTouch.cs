using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardTouch : MonoBehaviour {
    private Vector3 _Hitpos;





    private void OnCollisionEnter(Collision collision)
    {
        _Hitpos = collision.contacts[0].point;

        if(collision.collider.tag == "Darts")
        {

            gameObject.GetComponent<AudioSource>().Play();

            collision.transform.position = _Hitpos;
            collision.gameObject.GetComponent<Rigidbody>().useGravity = false;
            collision.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
