using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiliarioBehavior : MonoBehaviour
{
    [SerializeField] private GameObject amaksa;

    private GameObject centerEyeAnchor;
    private Light flare;

    // Start is called before the first frame update
    void Start()
    {
        centerEyeAnchor = GameObject.Find("CenterEyeAnchor");
        flare = gameObject.transform.Find("FlarePosition").GetComponent<Light>();
        flare.color = Color.black;        
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, centerEyeAnchor.transform.position) < 20f)
        {
            StartCoroutine(LerpIncreaseFlareColor(Color.white, 1));
        }
        if (flare == null)
        {
            StopAllCoroutines();
        }
    }

    IEnumerator LerpIncreaseFlareColor(Color endValue, float duration)
    {
        float time = 0;
        Color startValue = flare.color;

        while (time < duration)
        {
            flare.color = Color.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        flare.color = endValue;

        StartCoroutine(LerpDecreaseFlareColor(Color.black, 1));
    }

    IEnumerator LerpDecreaseFlareColor(Color endValue, float duration)
    {
        float time = 0;
        Color startValue = flare.color;

        while (time < duration)
        {
            flare.color = Color.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        flare.color = endValue;

        StartCoroutine(LerpIncreaseFlareColor(Color.white, 2));
    }
}
