using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    [SerializeField]
    private float m_speed;
    [SerializeField]
    private GameObject m_destroyEffect;
    [SerializeField]
    private LayerMask m_whatToHit;
    [SerializeField]
    private AudioClip m_soundHitMetal;

    private float m_distance = 0.3f;
    private float m_lifeTime = 1f;

    private void Start()
    {
        Invoke("DestroyProjectile", m_lifeTime);
    }

    private void Update()
    {
        transform.Translate(Vector2.right * m_speed * Time.deltaTime);
        ProjectileHit();
    }

    private void ProjectileHit()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(
                    transform.position,
                    transform.right,
                    m_distance,
                    m_whatToHit
                    );
        if(hitInfo.collider != null)
        {
            if(hitInfo.collider.CompareTag("Platform"))
            {
                Debug.Log("Hit : Platform");
                AudioSource.PlayClipAtPoint(m_soundHitMetal, hitInfo.collider.transform.position);
            }
            DestroyProjectile();
        }
    }

    private void DestroyProjectile()
    {
        //Instantiate(m_destroyEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
