using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShoot : MonoBehaviour
{
    [SerializeField]
    private float m_gunDamage;
    [SerializeField]
    private float m_chargeUpSec;
    [SerializeField]
    private Transform m_gunOrigin;
    [SerializeField]
    private LayerMask m_whatToHit;
    [SerializeField]
    private GameObject m_projectile;
    [SerializeField]
    private float m_startFireRate;
    private CameraShakeManager m_cameraShake;

    [SerializeField]
    private AudioClip m_soundFire;
    [SerializeField]
    private AudioClip m_soundReload;

    private int m_magSize = 15;
    private int m_magLeft = 15;
    private float m_fireRate;
    private float m_distance = 100f;

    public bool m_isFiring = false;

    private void Start()
    {
        m_cameraShake = GameObject.FindGameObjectWithTag("CameraShake").
            GetComponent<CameraShakeManager>();
    }

    private void Update()
    {
        Aim();
        Shoot();
        m_cameraShake.CamShake(m_isFiring);
        Reload();
    }

    private void Aim()
    {
        Vector2 difference =
            Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotateZ);
    }

    private void Shoot()
    {
        if (m_fireRate <= 0 && m_magLeft > 0)
        {
            if (Input.GetMouseButton(0))
            {
                Instantiate(m_projectile, transform.position, transform.rotation);
                m_isFiring = true;

                AudioSource.PlayClipAtPoint(m_soundFire, transform.position);

                m_fireRate = m_startFireRate;
                --m_magLeft;
            }
        }
        else
        {
            m_isFiring = false;
            m_fireRate -= Time.deltaTime;
        }
    }

    private void Reload()
    {
        if(Input.GetMouseButtonDown(1) && m_magLeft < m_magSize)
        {
            ++m_magLeft;
            AudioSource.PlayClipAtPoint(m_soundReload, transform.position);
        }
    }
}
