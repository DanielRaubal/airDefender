using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Main : MonoBehaviour
{
    public GameObject pointer,projectile,gun,gunPosition;
    [Header("ANITMATOR's")]
    public Animator gunAnimator;
    [Header("SCORE MANAGER")]
    public Text scoreUi;
    public float scoreTimer;
    public static int score =0;
    int scoreShow;
    AudioSource audio;


    void Start()
    {
        Application.targetFrameRate = 300;
        audio = GetComponent<AudioSource>();



        StartCoroutine(scoreUpdate());
        startY = gun.transform.localPosition.y;
    }

    IEnumerator scoreUpdate()
    {
        Debug.Log(score);
        while (true)
        {
            if (score > 0)
            {
                int localScore = score;
                for (int i = 0; i < localScore; i += 10)
                {
                    scoreShow += i;
                    //Debug.Log(scoreShow);
                    yield return new WaitForSeconds(scoreTimer);
                    //scoreUi.text = scoreShow.ToString();
                    scoreUi.text = string.Format("{0}", scoreShow.ToString("D8"));
         


                }
                score -= localScore;
            }
            yield return new WaitForSeconds(0.5f);

        }




    }

    bool shot;
    public float shotY = -0.1f, speed = 0.1f;


    Projectile createdProjectile;


    public GameObject arrow;

    float startY;

    void Update()
    {
        gunAnimator.SetBool("shot", false);

        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pointer.transform.position = new Vector3(worldPosition.x, worldPosition.y, 0);


        if (createdProjectile)
        {
            createdProjectile = null;
            gun.transform.up = Vector3.MoveTowards(transform.up, pointer.transform.localPosition, 1);

        }

    

        if (Input.GetMouseButtonDown(0))
        {
            gunAnimator.SetBool("shot", true);
            arrow.SetActive(false);



            /*
  Vector3 current = gun.transform.forward;
  Vector3 to = pointer.transform.position - gun.transform.position;
  float speed = 12 * Time.deltaTime; 
  gun.transform.forward = Vector3.RotateTowards(current, to, speed, speed);
  */

            CreateProjectile();
        }
      

        //scoreUi.text = score.ToString();
    }

    public void CreateProjectile()
    {
        audio.Play();

        createdProjectile = Instantiate(projectile, gunPosition.transform.position, gun.transform.localRotation).GetComponent<Projectile>();

        createdProjectile.target = pointer.transform.position;
    }
}
