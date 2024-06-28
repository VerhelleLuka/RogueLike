using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Transform playgroundTransform;

    public Transform playerTransform;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        WeaponsManager.instance.AddWeapon("Rock");
    }

}
