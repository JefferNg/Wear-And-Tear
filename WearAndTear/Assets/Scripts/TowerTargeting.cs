using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TowerTargeting : MonoBehaviour
{
    [SerializeField] private float targetRange = 3f;
    [SerializeField] private Transform rotatePoint;
    [SerializeField] private float rotationSpeed = 200f;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float bps = 1f;

    private Transform target;
    private float fireDelay;

    private void Update()
    {
        if (target == null || !ValidTarget())
        {
            FindTarget();
            return;
        }

        RotateToTarget();

        if (!CheckTargetInRange())
        {
            target = null;
        }
        else
        {
            fireDelay += Time.deltaTime;

            if (fireDelay >= 1f / bps)
            {
                Shoot();
                fireDelay = 0f;
            }
        }
    }

    private bool ValidTarget()
    {
        if (target == null) return false;

        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetRange, (Vector2)transform.position, 0f, enemyMask);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.transform == target) return true;
        }
        return false;
    }

    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetRange, (Vector2)transform.position, 0f, enemyMask);

        if (hits.Length > 0)
        {
            target = hits[0].transform;
        }
        
    }

    private void RotateToTarget()
    {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg + 90f;
        
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        rotatePoint.rotation = Quaternion.RotateTowards(rotatePoint.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private bool CheckTargetInRange()
    {
        return Vector2.Distance(target.position, transform.position) <= targetRange;
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        BulletBehavior bscript = bullet.GetComponent<BulletBehavior>();
        bscript.SetTarget(target, 6); // value of enemy layer
    }

    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetRange);
    }
}
