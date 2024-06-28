using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public int maxHp;
    private int m_Hp;

    private int m_PoisonTicks;
    private Ammo m_Ammo;
    public Ammo ammo
    {
        get => m_Ammo;
        set
        {
            m_Ammo = value;
            if(m_Ammo.poisonDamage > 0)
            {
                m_PoisonTicks = ammo.poisonTicks;
                StartCoroutine(ApplyPoisonDamage());
            }
        }
    }

    public int hp
    {
        get =>  m_Hp;

        private set
        {
            var isDamage = value < m_Hp;
            m_Hp = Mathf.Clamp(value, 0, maxHp);
            if(isDamage)
            {
                Damaged?.Invoke(hp);
            }
            else
            {
                Healed?.Invoke(hp);
            }

            if(m_Hp <= 0)
            {
                Died?.Invoke();
            }

        }
    }

    private void Awake()
    {
        m_Hp = maxHp;
    }

    public void Damage(float damage) => hp -= (int)damage;

    public void HealFull() => hp = maxHp;

    public void SetHp(int amount) => hp = amount;

    public event Action<float> Healed;
    public event Action<float> Damaged;
    public event Action Died;

    public void Heal(int amount)
    {
        hp += amount;
        if(hp > maxHp)
            hp = maxHp;
    }

    private IEnumerator ApplyPoisonDamage()
    {
        while(m_PoisonTicks > 0)
        {
            hp -= (int)ammo.poisonDamage;
            --m_PoisonTicks;
            yield return new WaitForSeconds(0.7f);
        }
    }


}
