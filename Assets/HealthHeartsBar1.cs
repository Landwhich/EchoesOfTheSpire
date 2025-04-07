using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHeartsBar1 : MonoBehaviour
{
    public GameObject heartPrefab;
    public PlayerHealth playerHealth;
    List<HealthHeartBar> hearts = new List<HealthHeartBar>();


    public void DrawHearts()
    {
        ClearHearts();

        float maxHleathRemainder = playerHealth.maxHealth % 2;
        int heartToMake = (int)((playerHealth.maxHealth / 2) + maxHleathRemainder);
        for (int i = 0; i < heartToMake; i++)
        {
            CreateEmptyHeart();
        }

        for (int i = 0; i < hearts.Count; i++)
        {
            int heartStatusRemainder = (int)Mathf.Clamp(playerHealth.curHealth - (i * 2), 0, 2);
                hearts[i].SetHeartImage((HeartStatus)heartStatusRemainder);
        }
    }



    public void CreateEmptyHeart()
    {
        GameObject newHeart = Instantiate(heartPrefab);
        newHeart.transform.SetParent(transform);


        HealthHeartBar heartComponent = newHeart.GetComponent<HealthHeartBar>();
        heartComponent.SetHeartImage(HeartStatus.Empty);
        hearts.Add(heartComponent);
    }


    public void ClearHearts()

    {

        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);

        }

        hearts = new List<HealthHeartBar>();
    }
}