using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Ammo : ScriptableObject
{
    public enum AmmoType
    {
        Normal,
        Poison,
    }
    
    public AmmoType type;

    public float range;
    public float speed;
    public int piercing;
    public float damage;

    public float poisonDamage;
    public int poisonTicks;

    public Sprite projectileSprite;

   


}
