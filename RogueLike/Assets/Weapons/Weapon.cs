using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public enum WeaponType
    {
        Melee,
        Ranged
    }
    public WeaponType weaponType;

    public enum WeaponAttribute
    {
        Poison,
        Explosive,
        Slowing
    }
    public List<WeaponAttribute> weaponAttribute = new List<WeaponAttribute>();

    public float coolDown;

    [SerializeField] private Ammo ammo;
    public GameObject projectilePrefab;

    public string weaponName = "weapon";

    private bool m_Looking = false;

    private PlayerStats m_PlayerStats;

    private float cooldownTimer = 10f;

    private void Awake()
    {
        m_PlayerStats = GetComponentInParent<PlayerStats>();
    }



    private void Start()
    {
        SetAmmo();
        coolDown = coolDown / (coolDown + m_PlayerStats.attackSpeed * 0.01f);
    }

    private void LateUpdate()
    {
        transform.position =new Vector3(transform.position.x, transform.position.y, -1f);
    }
    public void SetAmmo()
    {

    }
    private void Update()
    {

        Debug.Log(GetComponent<BoxCollider2D>().isTrigger);
        int childrenCount = 0;
        if (EnemySpawner.instance)
            childrenCount = EnemySpawner.instance.transform.childCount;
        float distance = 1000;
        Vector3 closestEnemyPos = new Vector3();
        for (int i = 0; i < childrenCount; ++i)
        {
            if (Vector2.Distance(EnemySpawner.instance.transform.GetChild(i).transform.position, transform.position) < distance)
            {
                distance = Vector2.Distance(EnemySpawner.instance.transform.GetChild(i).transform.position, transform.position);
                closestEnemyPos = EnemySpawner.instance.transform.GetChild(i).transform.position;
            }
        }
        if (distance <= ammo.range)
        {
            Vector2 direction = (closestEnemyPos - transform.position).normalized;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
            m_Looking = true;


        }
        else
            m_Looking = false;

        if (m_Looking && weaponType == WeaponType.Ranged)
        {
            if (cooldownTimer >= coolDown)
            {
                cooldownTimer = 0;
                GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
                projectile.GetComponent<Projectile>().ammo = ammo;

            }
            else
            {
                cooldownTimer += Time.deltaTime;
            }
        }

        else if (m_Looking && weaponType == WeaponType.Melee)
        {
            if (cooldownTimer >= coolDown && !m_Attacking)
            {

                m_Attacking = true;
                sequence = DOTween.Sequence();
                sequence.Join(transform.DOLocalMove((Vector2)((closestEnemyPos - transform.position).normalized * (ammo.range + m_PlayerStats.range * 0.01f)), 0.2f * ammo.speed))
                    .OnStepComplete(() => GetComponent<BoxCollider2D>().isTrigger = true);
                sequence.OnStart(() =>  GetComponent<BoxCollider2D>().isTrigger = true);
                    
                sequence.Append(transform.DOLocalMove(new Vector3(0, 0, -1), 0.5f * ammo.speed)).OnStart(() => GetComponent<BoxCollider2D>().isTrigger = false);
                sequence.OnComplete(() => m_Attacking = false);
                sequence.Play();
               
                cooldownTimer = 0;

            }
            else if(!m_Attacking)
            {
                cooldownTimer += Time.deltaTime;
                
            }
        }
    }
    Sequence sequence;
    private bool m_Attacking = false;


    private void OnEnable()
    {
        m_PlayerStats.StatChanged += OnStatChanged;
    }



    private void OnDisable()
    {
        m_PlayerStats.StatChanged -= OnStatChanged;

    }

    private void OnStatChanged(string statName)
    {
        if (statName == "AttackSpeed")
        {
            coolDown = coolDown - coolDown * m_PlayerStats.attackSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            if (collision.gameObject.TryGetComponent<Health>(out var health))
            {
                health.Damage(ammo.damage);
            }
        }
    }
}
