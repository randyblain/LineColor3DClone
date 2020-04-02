using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionTrigger : MonoBehaviour {
    public UnityEvent OnTriggered = new UnityEvent ();
    // Start is called before the first frame update
    void Start () {

    }

   
    // Update is called once per frame
    void OnTriggerEnter (Collider coll) {
        print (coll.gameObject.name);
        if (coll.gameObject.tag.Equals ("Target")) {
            OnTriggered.Invoke ();
        }

    }
}
