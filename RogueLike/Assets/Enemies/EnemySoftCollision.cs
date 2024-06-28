using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoftCollision : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            if (Vector2.Distance(collision.transform.position, GameManager.instance.playerTransform.position) < Vector2.Distance(transform.position, GameManager.instance.playerTransform.position))
            {
                transform.position += -(collision.transform.position - transform.position).normalized * Time.deltaTime;
                transform.position = new Vector3(transform.position.x, transform.position.y, -1f);
            }
        }
    }
}
