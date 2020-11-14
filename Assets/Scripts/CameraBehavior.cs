using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    [SerializeField]
    private Transform m_player;
    //[SerializeField]
    //private Transform m_cursor
    [SerializeField]
    private Vector3 m_offSet;
    [SerializeField]
    private float m_smoothSpeed = 5.0f;
    //[SerializeField]
    //private Camera m_cam;
    [SerializeField]
    private float m_minBoundary;
    [SerializeField]
    private float m_maxBoundary;

    private Camera m_cam;
    private float m_mouseScroll;
    private float m_targetZoom;
    private float m_zoomFactor = 3.0f;
    private float m_zoomLerpSpeed = 10.0f;

    private void Start()
    {
        m_cam = Camera.main;
        m_targetZoom = m_cam.orthographicSize;
    }

    private void Update()
    {
        TrackPlayer();
        ZoomInOut();
    }

    private void TrackPlayer()
    {
        Vector3 desiredPosition = m_player.position + m_offSet;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position,
            desiredPosition, m_smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }

    private void ZoomInOut()
    {
        m_mouseScroll = Input.mouseScrollDelta.y / 10;
        m_targetZoom -= m_mouseScroll * m_zoomFactor;
        m_targetZoom = Mathf.Clamp(m_targetZoom, m_minBoundary, m_maxBoundary);
        m_cam.orthographicSize = Mathf.Lerp(
            m_cam.orthographicSize,
            m_targetZoom, 
            Time.deltaTime * m_zoomLerpSpeed);
    }
}
