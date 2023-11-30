using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class Turret : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;
    [SerializeField] private GameObject upgradeUI;
    [SerializeField] private Button upgradeButton;

    [Header("Attribute")]
    [SerializeField] private float targetingRange = 3f;
    [SerializeField] private float rotationSpeed = 200f;
    [SerializeField] private float bulletsPerSecond = 4f;
    [SerializeField] private int baseUpgradeCost = 100;

    [Header("Audio")]
    [SerializeField] private AudioSource waterAudio;
    [SerializeField] private AudioSource sellAudio;

    private float bulletsPerSecondBase;
    private float targetingRangeBase;

    private Transform target;
    private float timeUntilFire;

    private int level = 1;

    //Optimization Pattern Object Pool
    List<GameObject> pool;

    private void Start() {
        pool = new List<GameObject>();
        bulletsPerSecondBase = bulletsPerSecond;
        targetingRangeBase = targetingRange;

        upgradeButton.onClick.AddListener(UpgradeTurret);
    }

    private void Update() {
        if(target == null) {
            FindTarget();
            return;
        }
        RotateTowardsTarget();
        if(!CheckTargetIsRange()) {
            target = null;
        } else {
            //shoot
            timeUntilFire += Time.deltaTime;
            if(timeUntilFire >= 1f / bulletsPerSecond) {
                Shoot();
                timeUntilFire = 0f;
            }
        }
    }

    private void Shoot() {
        //Object Pooling
        GameObject bulletObject;
        if(pool.Count > 100) {
            pool.RemoveAt(0);
            bulletObject = pool[0];
            bulletObject.transform.position = firingPoint.position;
        } else {
            bulletObject = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        }
        pool.Add(bulletObject);

        WaterBullet bulletScript = bulletObject.GetComponent<WaterBullet>();
        bulletScript.SetTarget(target);
        //GetComponent<AudioSource>().Play();
        waterAudio.Play();
        //Destroy(bulletObject.gameObject, 5.0f);
    }

    private void FindTarget() {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)
        transform.position, 0f, enemyMask);

        if(hits.Length > 0) {
            target = hits[0].transform;
        }
    }

    private bool CheckTargetIsRange() {
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }

    private void RotateTowardsTarget() {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - 
        transform.position.x) * Mathf.Rad2Deg - 90f;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, 
        targetRotation, rotationSpeed * Time.deltaTime);
    }

    public void OpenUpgradeUI() {
        upgradeUI.SetActive(true);
    }

    public void CloseUpgradeUI() {
        upgradeUI.SetActive(false);
        UIManager.main.SetHoveringState(false);
    }

    public void UpgradeTurret() {
        //Changed to sell, kept change to avoid breaking stuff
        //if(CalculateCost() > LevelManager.main.currency) return;
        Tower towerToSell = BuildManager.main.GetSelectedTower();

        if(towerToSell != null) {
        //LevelManager.main.SpendCurrency(CalculateCost());
            LevelManager.main.IncreaseCurrency(towerToSell.cost);
            sellAudio.Play();
            Destroy(gameObject);

        //level++;
        //bulletsPerSecond = CalculateBulletsPerSecond();
        //targetingRange = CalculateRange();
            //Destroy(Plot.main.towerObject.gameObject);
        }
        CloseUpgradeUI();
        //Debug.Log("New BPS: " + bulletsPerSecond);
        //Debug.Log("New range: " + targetingRange);
        //Debug.Log("New cost: " + CalculateCost());
    }

    private int CalculateCost() {
        return Mathf.RoundToInt(baseUpgradeCost * Mathf.Pow(level, 0.8f));
    }

    private float CalculateBulletsPerSecond() {
        return bulletsPerSecondBase * Mathf.Pow(level, 0.6f);
    }

    private float CalculateRange() {
        return targetingRangeBase * Mathf.Pow(level, 0.4f);
    }

    #if UNITY_EDITOR
    private void OnDrawGizmosSelected() {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }
    #endif
}
