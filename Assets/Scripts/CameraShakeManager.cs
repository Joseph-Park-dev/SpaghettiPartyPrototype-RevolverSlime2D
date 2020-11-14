using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeManager : MonoBehaviour
{
    [SerializeField]
    private Animator m_camAnimator;

    public void CamShake(bool t_trigger)
    {
        m_camAnimator.SetBool("CamShake", t_trigger);
    }
}
