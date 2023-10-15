using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedLight : MonoBehaviour

{
    private bool isWork = false;
    [SerializeField] GameObject PointLight;
    int waitSec;

    void Start()
    {
        StartCoroutine(Blink());
    }

    IEnumerator Blink()
    {
        while (true)
        {
            Debug.Log("while문 시작");

            waitSec = Random.Range(5, 10);

            if (isWork == false)
            {
                PointLight.SetActive(true);
                isWork = true;

                Debug.Log("켜졌다");

            }
            else
            {
                PointLight.SetActive(false);
                isWork = false;
                Debug.Log("꺼졌다");

            }
            Debug.Log("if문 끝");
            yield return new WaitForSeconds(waitSec * 0.1f);

        }

    }

}
