﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectButton : MonoBehaviour {
    public Camera eventCamera;
    public GameObject definedButton;
    public UnityEvent OnTouch = new UnityEvent ();

    // Use this for initialization
    void Start () {
        definedButton = this.gameObject;
    }

    // Update is called once per frame
    void Update () {
        var ray = eventCamera.ScreenPointToRay (Input.mousePosition);
        RaycastHit Hit;

        if (Input.GetMouseButtonDown (0)) {
            if (Physics.Raycast (ray, out Hit) && Hit.collider.gameObject == gameObject) {
               // Debug.Log ("Button Clicked");
                OnTouch.Invoke ();
            }
        }
    }

    public void ClickFromScript() {
        OnTouch.Invoke ();
    }
}
