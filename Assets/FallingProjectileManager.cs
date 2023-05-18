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
        while (true)
        {
            FallingProjectile projectile 
                = Instantiate(fallingProjectile, new Vector3(Random.RandomRange(spawnPoints.x, spawnPoints.y), transform.position.y, 0), fallingProjectile.transform.rotation).GetComponent<FallingProjectile>();

            projectile.buildings = buildings;
            yield return new WaitForSeconds(time);
        }
      
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
