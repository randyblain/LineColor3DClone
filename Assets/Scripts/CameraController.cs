using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public Camera camera;
    public Transform target;
    public Vector3 offsetFromTarget;
    public MegaShapeHelix lineSpline;
    // Start is called before the first frame update
    void Start () {

    }

    // Update is called once per frame
    void Update () {
        transform.position = target.position + offsetFromTarget;
        camera.transform.LookAt (target);
    }

    void AdvanceTarget () {

    }

}
