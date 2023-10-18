using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    public AudioClip[] Sounds;
    private void Awake()
    {
        SoundManager.instance.PlaySFX(Sounds[0]);
        SoundManager.instance.PlaySFX(Sounds[1]);
    }
    public void SceneLoadEvent()
    {
        SceneManager.LoadScene("KKHScene");
    }
}
