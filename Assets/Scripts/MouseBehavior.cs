using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseBehavior : MonoBehaviour
{
    private SpriteRenderer rend;
    [SerializeField]
    private Sprite shootingCursor;
    [SerializeField]
    private Sprite normalCursor;

    private void Start()
    {
        Cursor.visible = false;
        rend = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = cursorPos;
        if (Input.GetMouseButtonDown(0))
        {
            rend.sprite = shootingCursor;
        }
        else if(Input.GetMouseButtonUp(0))
        {
            rend.sprite = normalCursor;
        }
    }
}
