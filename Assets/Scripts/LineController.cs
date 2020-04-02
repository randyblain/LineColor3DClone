using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour {
    public Transform target;
    public Rigidbody targetRB;
    public MeshRenderer targetMeshRndr;
    public BoxCollider targetColl;
    public CameraController camCtrl;
    public UIController uiCtrl;
    public bool blockAlphaInput;
    public MegaShapeHelix lineSpline;
    public MegaLoftLayerSimple lineLoft;
    public RigidbodyRotator rbRotator;
    [Range (0f, 0.1f)]
    public float lineSpeed;
    public float tangentDist;
    [Range (0f, 1f)]
    public float alpha;
    public Vector2 touchPos;
    public float screenWidth;
    public float screenHeight;
    public GameObject particlesObj;
    public List<Rigidbody> collisionCubes;
    public float collForce = 0.25f;
    public LootBox lootBox;
    // Start is called before the first frame update
    void Start () {
        screenWidth = Screen.width * 0.5f;
        screenHeight = Screen.height * 0.5f;
        alpha = 0.01f;
        blockAlphaInput = false;
    }

    // Update is called once per frame
    void Update () {
        if (alpha > 0.01f && alpha < 1.0f) {
            uiCtrl.mainPanel.gameObject.SetActive (false);
        }
        if (!blockAlphaInput) {
            targetRB.MovePosition (lineSpline.InterpCurve3D (0, alpha, lineSpline.normalizedInterp));
            float ta = tangentDist / lineSpline.GetCurveLength (0);
            Vector3 pos1 = lineSpline.InterpCurve3D (0, alpha + ta, lineSpline.normalizedInterp);
            Quaternion r = Quaternion.LookRotation (pos1 - target.position);
            if (alpha > 1.001f) {
                lootBox.openLid = true;
                blockAlphaInput = true;
                uiCtrl.mainPanel.gameObject.SetActive (true);
            }
            targetRB.MoveRotation (r);
            lineLoft.pathLength = alpha;
            if (Input.touchCount > 0 || Input.GetMouseButton (0)) {
                alpha += lineSpeed * Time.deltaTime;
                /*Touch touch = Input.GetTouch (0);
                print ("touched");
                if (touch.phase == TouchPhase.Moved) {
                    Vector2 pos = touch.position;
                }*/
            }
        }
    }

    public void SetAlpha () {
        print ("SetAlpha()");
    }

    public void ObstacleCollision () {
        print ("collided with target");
        float pct = alpha * 100f;
        uiCtrl.FailedByObstacleCollision (pct);
        blockAlphaInput = true;
        rbRotator.enabled = false;
        particlesObj.transform.position = target.position;
        particlesObj.transform.rotation = target.rotation;
        targetColl.enabled = false;
        targetMeshRndr.enabled = false;
        particlesObj.SetActive (true);
        StartCoroutine (CollisionEfx ());
    }
    IEnumerator CollisionEfx () {
        yield return null;
        for (int i = 0; i < collisionCubes.Count; i++) {
            collisionCubes[i].AddRelativeForce (Vector3.up * collForce, ForceMode.Impulse);
            collisionCubes[i].AddRelativeTorque ((Vector3.right + Vector3.forward) * collForce, ForceMode.Impulse);
        }
    }
}
