using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatTextScript : MonoBehaviour
{
    public float score;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<TextMesh>();
    }

    private void Update()
    {
        if(score>0)
        {
            StartCoroutine(scaler());
            sr.text = $"+{score}";

            if (score < 0)
            {
                sr.text = $"BUILDING DESTROYED";
            }
            score = 0;
        }
    }

    TextMesh sr;

    IEnumerator scaler()
    {

        yield return new WaitForSeconds(0.1f);
        Color color;
        ColorUtility.TryParseHtmlString("#61001d", out color);
        sr.color = color;
        yield return new WaitForSeconds(0.07f);
        ColorUtility.TryParseHtmlString("#b9937c", out color);
        sr.color = color;

        yield return new WaitForSeconds(0.2f);



    }
}
