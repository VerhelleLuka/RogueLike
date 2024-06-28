using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
   [SerializeField] private float m_Luck;

   [SerializeField]private float m_Armor;
  
   [SerializeField]private float m_Damage;
   [SerializeField]private float m_PoisonDamage;
    [SerializeField] private float m_Range;

    public float range
    {
        get => m_Range;
        set => m_Range = value;
            }

    [SerializeField] private float m_MovementSpeed;
    public float movementSpeed
    {
        get => m_MovementSpeed;
        set => m_MovementSpeed = value;
    }
    [SerializeField] private float m_AttackSpeed;
    public float attackSpeed
    {
        get => m_AttackSpeed;

        set
        {
            m_AttackSpeed = value;
            StatChanged?.Invoke("AttackSpeed");
        }
    }

    public event Action<string> StatChanged;


}
