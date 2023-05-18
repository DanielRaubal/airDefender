using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingProjectile : MonoBehaviour
{
    public List<Building> buildings;
    Building fallOnTo;
    public float speed;


    void Start()
    {
        fallOnTo = buildings[Random.Range(0, buildings.Count)];
    }

    void Update()
    {
        //transform.up = Vector3.MoveTowards(transform.up, fallOnTo.transform.position, 1);

        transform.position = Vector3.MoveTowards(transform.position, fallOnTo.transform.position, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.gameObject.name.Contains("Falling"))
        {
            Main.score += 100;
            Destroy(gameObject);
        }
    }

}
