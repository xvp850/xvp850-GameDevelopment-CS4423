using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;

public class Plot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;

    public GameObject towerObject;
    public Turret turret;
    private Color startColor;

    [SerializeField] private TextMeshProUGUI errorMessage;

    public Plot main;

    public void Awake() => main  = this;

    private void Start() {
        startColor = sr.color;
        errorMessage.text = "";
    }

    private void OnMouseEnter() {
        sr.color = hoverColor;
    }

    private void OnMouseExit() {
        sr.color = startColor;
    }

    private void OnMouseDown() {
        if(EventSystem.current.IsPointerOverGameObject()) return;
        if(UIManager.main.IsHoveringUI()) return;

        if(towerObject != null) {
            turret.OpenUpgradeUI();
            return;
        }

        Tower towerToBuild = BuildManager.main.GetSelectedTower();
        
        if(towerToBuild.name == "Water Tower I" && EnemySpawner.main.getWaterElementUnlock1() == false) {
            StartCoroutine(ShowMessage("ERROR: You have not unlocked this tower yet (After Wave 6)."));
            return;
        }
        if(towerToBuild.name == "Earth Tower I" && EnemySpawner.main.getEarthElementUnlock1() == false) {
            StartCoroutine(ShowMessage("ERROR: You have not unlocked this tower yet (After Wave 12)."));
            return;
        }
        if(towerToBuild.name == "Fire Tower I" && EnemySpawner.main.getFireElementUnlock1() == false) {
            StartCoroutine(ShowMessage("ERROR: You have not unlocked this tower yet (After Wave 18)."));
            return;
        }
        if(towerToBuild.name == "Air Tower I" && EnemySpawner.main.getAirElementUnlock1() == false) {
            StartCoroutine(ShowMessage("ERROR: You have not unlocked this tower yet (After Wave 24)."));
            return;
        }
        if(towerToBuild.name == "Water Tower II" && EnemySpawner.main.getWaterElementUnlock2() == false) {
            StartCoroutine(ShowMessage("ERROR: You have not unlocked this tower yet (After Wave 30)."));
            return;
        }
        if(towerToBuild.name == "Earth Tower II" && EnemySpawner.main.getEarthElementUnlock2() == false) {
            StartCoroutine(ShowMessage("ERROR: You have not unlocked this tower yet (After Wave 36)."));
            return;
        }
        if(towerToBuild.name == "Fire Tower II" && EnemySpawner.main.getFireElementUnlock2() == false) {
            StartCoroutine(ShowMessage("ERROR: You have not unlocked this tower yet (After Wave 42)."));
            return;
        }
        if(towerToBuild.name == "Air Tower II" && EnemySpawner.main.getAirElementUnlock2() == false) {
            StartCoroutine(ShowMessage("ERROR: You have not unlocked this tower yet (After Wave 48)."));
            return;
        }
        if(towerToBuild.cost > LevelManager.main.currency) {
            //Debug.Log("Can't afford");
            StartCoroutine(ShowMessage("ERROR: You do not have enough currency."));
            return;
        }

        LevelManager.main.SpendCurrency(towerToBuild.cost);

        towerObject = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);
        turret = towerObject.GetComponent<Turret>();
    }

    IEnumerator ShowMessage(string message) {
	    errorMessage.text = message;
	    errorMessage.enabled = true;
	    yield return new WaitForSeconds(3f);
	    errorMessage.enabled = false;
    }
}
