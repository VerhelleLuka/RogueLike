using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Ammo ammo;
    public Vector3 initialPos = Vector3.zero;

    private int m_Piercing;
    void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, -1);
        initialPos = transform.position;

        m_Piercing = ammo.piercing;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * ammo.speed * Time.deltaTime);
        if (Vector3.Distance(initialPos, transform.position) > ammo.range)
        {
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            if (collision.gameObject.TryGetComponent<Health>(out var health))
            {
                health.Damage(ammo.damage);
                health.ammo = ammo;
                --m_Piercing;
                CheckPierce();
            }
        }
    }

    private void CheckPierce()
    {
        if (m_Piercing <= 0)
            Destroy(gameObject);
    }

}
