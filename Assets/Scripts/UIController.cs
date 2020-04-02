using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {
    public GameObject levelFailPanel;
    public TMP_Text failTxt;
    public TMP_Text pctCompleteTxt;
    public string failMsg;
    public float failPanelDelay = 1f;
    public Button mainPanel;
    public int sceneIdx;
    public string sceneName;
    public int score;
    public int levelCompleteScore = 50;
    public TMP_Text scoreTxt;
    public TMP_Text tapToCollectTxt;
    public bool coinsCollected;
    // Start is called before the first frame update
    void Start () {
        coinsCollected = false;
        tapToCollectTxt.gameObject.SetActive (false);
        levelFailPanel.SetActive (false);
        mainPanel.gameObject.SetActive (true);
        Scene scene = SceneManager.GetActiveScene ();
        sceneIdx = scene.buildIndex;
        sceneName = scene.name;
    }

    // Update is called once per frame
    public void FailedByObstacleCollision (float pct) {
        failTxt.text = failMsg;
        pctCompleteTxt.text = pct.ToString ("f2") + "% Completed";
        StartCoroutine (ActivateFailPanel ());
    }

    IEnumerator ActivateFailPanel () {
        yield return new WaitForSeconds (failPanelDelay);
        levelFailPanel.SetActive (true);
    }

    public void ActivateLootCollection () {
        mainPanel.gameObject.SetActive (true);
        tapToCollectTxt.gameObject.SetActive (true);
    }

    public void CollectLootAndScore () {
        if (coinsCollected) { return; }
        score += levelCompleteScore;
        scoreTxt.text = score.ToString ();
        coinsCollected = true;
    }

    public void ReloadLevel () {
        SceneManager.LoadScene (sceneName);
    }

}
