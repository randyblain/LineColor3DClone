using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBox : MonoBehaviour {
    // Start is called before the first frame update
    public Camera cam;
    public bool openLid;
    public bool blocked;
    public Transform lid;
    public float openSpeed;
    float lidAngle;
    public List<Rigidbody> coins;
    public bool ejectCoins;
    public float ejectForce = 5.0f;
    public float coinFreezeDelay = 0.5f;
    public UIController uiCtrl;
    public bool collectCoins;
    public bool coinsCollected;
    public Vector3 collPoint;
    public float collSpeed;
    void Start () {
        lidAngle = 180f;
        for (int i = 0; i < coins.Count; i++) {
            coins[i].isKinematic = true;
        }
    }

    // Update is called once per frame
    void Update () {
        if (openLid && !blocked) {
            lidAngle += openSpeed * Time.deltaTime;
            lid.localEulerAngles = new Vector3 (lidAngle, 0f, 0f);
            if (lidAngle > 0f && lidAngle < 10f) {
                openLid = false;
                blocked = true;
                ejectCoins = true;
            }
        }
    }

    void FixedUpdate () {
        if (ejectCoins) {
            for (int i = 0; i < coins.Count; i++) {
                coins[i].isKinematic = false;
                coins[i].AddForce (Vector3.up * ejectForce, ForceMode.Impulse);
            }
            ejectCoins = false;
            StartCoroutine (SetCoinsToKinematic ());
        }
        if (collectCoins) {
            int collected = 0;
            for (int i = 0; i < coins.Count; i++) {
                Vector3 newPos = Vector3.Lerp (coins[i].transform.position, collPoint, collSpeed * Time.deltaTime);
                coins[i].MovePosition (newPos);
                Quaternion q = Quaternion.Euler (new Vector3 (5f, 10f, 15f));
                coins[i].MoveRotation (coins[i].rotation * q);
                Vector3 scl = coins[i].transform.localScale * 0.9f;
                coins[i].transform.localScale = new Vector3 (scl.x, scl.y, scl.z);
                float dist = Vector3.Distance (coins[i].transform.position, collPoint);
                if (dist < 0.1f) {
                    collected++;
                }
            }
            if (collected == coins.Count) {
                for (int i = 0; i < coins.Count; i++) {
                    coins[i].gameObject.SetActive (false);
                }
                uiCtrl.CollectLootAndScore ();
                collectCoins = false;
            }
        }
    }

    IEnumerator SetCoinsToKinematic () {
        yield return new WaitForSeconds (coinFreezeDelay);
        for (int i = 0; i < coins.Count; i++) {
            coins[i].isKinematic = true;
        }
        uiCtrl.ActivateLootCollection ();
    }

    public void CollectCoinsAndScore () {
        if (!blocked) {
            return;
        }
        collPoint = cam.ScreenToWorldPoint (new Vector3 (Screen.width, Screen.height, cam.nearClipPlane));
        collectCoins = true;
    }

    void OnCollisionEnter (Collision collision) {
        if (collision.gameObject.tag.Equals ("Target")) {
            openLid = true;
        }
    }
}
