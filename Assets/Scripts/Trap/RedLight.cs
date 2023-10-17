using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedLight : MonoBehaviour
{
    private bool isWork = false;
    public AudioSource audioSource;
    public AudioClip audioClip;
    [SerializeField] private AudioClip[] clip;
    [SerializeField] Light PointLight;
    [SerializeField] GameObject Point;
    Coroutine test;
    int waitSec;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Player")
        {
            audioSource.Play();
            Point.SetActive(true);
            //test=StartCoroutine(nameof(Blink));//????????
            StartCoroutine(nameof(Blink));
            Invoke(nameof(Off), 2f);
            //StopCoroutine(test);///????????
        }
    }
    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.tag == "Player")
    //    {
    //        Point.SetActive(false);
    //        StopCoroutine(Blink());
    //    }
    //}
    IEnumerator Blink()
    {
        while (true)
        {
            waitSec = Random.Range(5, 10);
            if (isWork == false)
            {
                PointLight.enabled = true;
                isWork = true;
            }
            else
            {
                PointLight.enabled=false;
                isWork = false;
            }
            yield return new WaitForSeconds(waitSec * 0.1f);
        }
    }

    public void Play()
    {
        int r = Random.Range(0, 4);
        audioSource.clip = clip[0];
        audioSource.Play();
    }

    public void Off()
    {
        Point.SetActive(false);
        StopCoroutine(nameof(Blink));
    }

}
