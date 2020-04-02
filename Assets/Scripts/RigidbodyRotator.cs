using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyRotator : MonoBehaviour {
    public Rigidbody rb;
    public float speed;
    // Start is called before the first frame update
    void Start () {

    }

    // Update is called once per frame
    void Update () {
        Quaternion q = rb.rotation * Quaternion.Euler (Vector3.up * speed * Time.deltaTime);
        rb.MoveRotation (q);
    }
}
