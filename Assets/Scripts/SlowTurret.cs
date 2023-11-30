using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SlowTurret : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LayerMask enemyMask;

    [Header("Attribute")]
    [SerializeField] private float targetingRange = 2f;
    [SerializeField] private float attackPerSecond = 0.25f;
    [SerializeField] private float freezeTime = 1f;

    private float timeUntilFire;

    private void Update() {
            //shoot
        timeUntilFire += Time.deltaTime;
        if(timeUntilFire >= 1f / attackPerSecond) {
            FreezeEnemies();
            timeUntilFire = 0f;
        }
    }

    private void FreezeEnemies() {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)
        transform.position, 0f, enemyMask);

        if(hits.Length > 0) {
            for(int i =0; i < hits.Length; i++) {
                RaycastHit2D hit = hits[i];
                EnemyMovement enemeyMovement = hit.transform.GetComponent<EnemyMovement>();
                enemeyMovement.UpdateSpeed(0.5f);

                StartCoroutine(ResetEnemySpeed(enemeyMovement));
            }
        }
    }

    private IEnumerator ResetEnemySpeed(EnemyMovement enemeyMovement) {
        yield return new WaitForSeconds(freezeTime);

        enemeyMovement.ResetSpeed();
    }

    #if UNITY_EDITOR
    private void OnDrawGizmosSelected() {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }
    #endif
}
