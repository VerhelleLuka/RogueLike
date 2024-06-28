using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsManager : MonoBehaviour
{
    static public WeaponsManager instance;
    public GameObject[] weaponSockets;

    public GameObject[] weaponPrefabs;

    void Awake()
    {
        instance = this;
    }

    public void AddWeapon(string weaponName)
    {
        for(int i = 0; i < weaponSockets.Length; i++)
        {
            if (!weaponSockets[i].GetComponentInChildren<Weapon>()) 
            {
                for(int j = 0; j < weaponPrefabs.Length; j++)
                {
                    if(weaponName == weaponPrefabs[j].GetComponent<Weapon>().weaponName)
                    {
                        GameObject weapon = Instantiate(weaponPrefabs[j], weaponSockets[i].transform);
                        //weapon.transform.position = new Vector3(0, 0, -1);
                        break;
                    }

                }
                break;
            }
        }
    }

    public void RemoveWeapon(int socket)
    {
        GameObject.Destroy(weaponSockets[socket].transform.GetChild(0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
