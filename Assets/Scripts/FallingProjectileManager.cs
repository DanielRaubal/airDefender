using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingProjectileManager : MonoBehaviour
{
    public Vector2 spawnPoints;
    public GameObject fallingProjectile;
    public List<Building> buildings;
    public float time;

    void Start()
    {
        StartCoroutine(spawner());
    }

    IEnumerator spawner()
    {
        while (buildings.Count >0)
        {


            FallingProjectile projectile 
                = Instantiate(fallingProjectile, new Vector3(Random.RandomRange(spawnPoints.x, spawnPoints.y), transform.position.y, 0), fallingProjectile.transform.rotation).GetComponent<FallingProjectile>();

            projectile.buildings = buildings;

            if(Random.Range(0,100) > 90)
            {
                projectile.splitTo = Random.Range(2, 4);
            }

            yield return new WaitForSeconds(time);
        }
      
    }


    void Update()
    {
     
        for (int i = 0; i < buildings.Count; i++)
        {
            if (buildings[i].hp < 1)
            {
                buildings.RemoveAt(i);
            }
        }
    }
}
