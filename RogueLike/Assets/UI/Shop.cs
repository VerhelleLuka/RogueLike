using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private List<GameObject> m_Weapons = new List<GameObject>();
    [SerializeField] private List<GameObject> m_ShopItemSlots = new List<GameObject>();
    public void RefreshShop()
    {
        List<int> takenItems = new List<int>();
        int currentItem = 0;
        for (int i = 0; i < m_ShopItemSlots.Count; ++i)
        {
            while (!takenItems.Contains(i))
                currentItem = Random.Range(0, m_Weapons.Count);

            takenItems.Add(currentItem);

            m_ShopItemSlots[i].transform.Find("ItemName").GetComponent<TMPro.TextMeshProUGUI>().text = m_Weapons[currentItem].GetComponent<Weapon>().weaponName;
            m_ShopItemSlots[i].transform.Find("ItemDescription").GetComponent<TMPro.TextMeshProUGUI>().text = m_Weapons[currentItem].GetComponent<Weapon>().weaponDescription;



        }
    }

    public void OnBuy()
    {

    }
}
