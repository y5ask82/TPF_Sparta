using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject Explain;
    public void Onclick()
    {
        SceneManager.LoadScene("Intro");
    }

    public void OnClickExplain()
    {
        if(!Explain.activeSelf)
        Explain.SetActive(true);
        else Explain.SetActive(false);
    }


}
