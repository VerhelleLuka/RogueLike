using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class ChaseEnemy : MonoBehaviour
{
    public float speed = 5f;

    [SerializeField]
    private Health m_Health;


    private void OnEnable()
    {
        m_Health.Died += OnDeath;
    }

    private void OnDisable()
    {
        m_Health.Died -= OnDeath;
    }
    private void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, GameManager.instance.playerTransform.position, speed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, transform.position.y, -1f);
    }

    private void OnDeath()
    {
        Destroy(gameObject);
    }

    
}
