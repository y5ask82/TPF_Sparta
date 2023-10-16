using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedLight : MonoBehaviour
{
    private bool isWork = false;
    [SerializeField] Light PointLight;
    //[SerializeField] GameObject PointLight;
    int waitSec;

    void Start()
    {
        StartCoroutine(Blink());
    }
    IEnumerator Blink()
    {
        while (true)
        {
            waitSec = Random.Range(5, 10);
            if (isWork == false)
            {
                PointLight.enabled = true;
                //PointLight.SetActive(true);
                isWork = true;
            }
            else
            {
                PointLight.enabled=false;
                //PointLight.SetActive(false);
                isWork = false;
            }
            yield return new WaitForSeconds(waitSec * 0.1f);
        }
    }
}
