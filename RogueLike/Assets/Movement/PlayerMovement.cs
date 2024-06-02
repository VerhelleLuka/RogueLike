using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private float m_HorizontalMovement = 0f;
    private float m_VerticalMovement = 0f;

    private PlayerStats stats;

    private float m_xBounds = 19.5f;
    private float m_yBounds = 9.5f;

    public Transform playgroundTransform;

    private void Start()
    {
        stats = GetComponent<PlayerStats>();

        //0.5f = temporary player size
        m_xBounds = (playgroundTransform.localScale.x / 2f) - 0.5f;
        m_yBounds = (playgroundTransform.localScale.y / 2f) - 0.5f;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(m_HorizontalMovement * stats.movementSpeed, m_VerticalMovement * stats.movementSpeed, 0f) * Time.deltaTime;
        if (transform.position.x > m_xBounds || transform.position.x < -m_xBounds)
        {
            if (transform.position.x < 0)
                transform.position = new Vector3(-m_xBounds, transform.position.y, 0);
            else
                transform.position = new Vector3(m_xBounds, transform.position.y, 0);
        }
        if (transform.position.y > m_yBounds || transform.position.y < -m_yBounds)
        {
            if (transform.position.y < 0)
                transform.position = new Vector3(transform.position.x, -m_yBounds, 0);
            else
                transform.position = new Vector3(transform.position.x, m_yBounds, 0);
        }
    }

    

    public void Move(InputAction.CallbackContext context)
    {
        m_HorizontalMovement = context.ReadValue<Vector2>().x;
        m_VerticalMovement = context.ReadValue<Vector2>().y;
    }
}
