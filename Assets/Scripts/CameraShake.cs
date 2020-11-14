using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField]
    private Transform m_shakeObject;

    private float m_shakeDuration;
    private float m_shakeMagnitude = 0.7f;
    private float m_dampingSpeed = 1.0f;

    private Vector3 m_initialPos;

    private void OnEnable()
    {
        m_initialPos = m_shakeObject.localPosition;
    }

    private void Update()
    {
        if(m_shakeDuration > 0)
        {
            m_shakeObject.localPosition = m_initialPos +
                Random.insideUnitSphere * m_shakeMagnitude;
            m_shakeDuration -= Time.deltaTime * m_dampingSpeed;
        }
        else
        {
            m_shakeDuration = 0f;
            m_shakeObject.localPosition = m_initialPos;
        }
    }

    public void TriggerShake()
    {
        m_shakeDuration = 1.0f;
    }
}
