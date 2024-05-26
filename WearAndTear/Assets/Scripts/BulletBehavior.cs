using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private int dmg = 1;

    private Transform target;
    private int targetLayer;

    private void Update()
    {
        if (transform.position.x < -10f || transform.position.x > 10f || transform.position.y < -7f || transform.position.y > 7f)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (!target) return;

        if (target.gameObject.layer != targetLayer)
        {
            target = null;
            return;
        }

        Vector2 direction = (target.position - transform.position).normalized;

        rb.velocity = direction * speed;
    }

    public void SetTarget(Transform _target, int _targetLayer)
    {
        target = _target;
        targetLayer = _targetLayer;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        other.gameObject.GetComponent<EnemyHealth>().TakeDamage(dmg);
        Destroy(gameObject);
    }

}
