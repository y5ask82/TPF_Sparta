using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Color = UnityEngine.Color;

public class UIManager : MonoBehaviour
{
    public GameObject UIPanel;
    public GameObject Btns;
    public GameObject Setting;
    public GameObject Brightness;
    public GameObject Sound;
    public Light light;
    public Slider slider;
    public Slider Backslider;
    public Slider Effectslider;
    public static UIManager Instance;
    public RawImage Ghost;
    public RawImage DeadFadeinout;
    public RawImage DeadGhost;
    private bool IsUI;
    private Color color;
    private GameObject player;
    public AudioClip DeathSound1;
    public AudioClip DeathSound2;
    [HideInInspector]
    public bool IsDead;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        else if (Instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        UIPanel.SetActive(false);
        color = Ghost.GetComponent<RawImage>().color;
    }
    private void Start()
    {
        Backslider.value = SoundManager.instance.GetBGMVolume();
        Effectslider.value = SoundManager.instance.GetSFXVolume();
    }
    public void PopPanel()
    {
        if(UIPanel.gameObject.activeSelf == false)
        {
            IsUI = true;
            Ghost.gameObject.SetActive(true);
            UIPanel.SetActive(true);
            Btns.SetActive(true);
            Setting.SetActive(false);
            Brightness.SetActive(false);
            Sound.SetActive(false);
            Time.timeScale = .0f;
            PlayerController.instance.canLook = false;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            IsUI = false;
            Ghost.gameObject.SetActive(false);
            UIPanel.SetActive(false);
            Btns.SetActive(false);
            Setting.SetActive(false);
            Brightness.SetActive(false);
            Sound.SetActive(false);
            Time.timeScale = 1f;
            PlayerController.instance.canLook = true;
            Cursor.lockState = CursorLockMode.Locked;
        }
     
    }
    public void PopSetting()
    {
        Setting.SetActive(true);
    }

    private void Update()
    {
        if(IsUI)
        {
         color.a += 0.00001f;
        Ghost.GetComponent<RawImage>().color = color;
        }
    }

    public void OnClickCountinue()
    {
        PopPanel();
    }
    public void OnClickSetting()
    {
        if (Setting.gameObject.activeSelf == false)
        {
            Btns.SetActive(false);
            Setting.SetActive(true);
        }
        else if(Setting.gameObject.activeSelf == true)
        {
            Btns.SetActive(true);
            Setting.SetActive(false);
        }
      
    }
    public void OnClickBright()
    {
        Setting.SetActive(false);
        Sound.SetActive(false);
        Brightness.SetActive(true);
    }
    public void OnClickExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void SetBright()
    {
        float normalizedValue = slider.normalizedValue;
        float rotationX = Mathf.Lerp(320, 360, normalizedValue);
        light.transform.rotation = Quaternion.Euler(rotationX, 0, 0);
    }

    public void OnClickSound()
    {
        Setting.SetActive(false);
        Brightness.SetActive(false);
        Sound.SetActive(true);
    }
    public void SetbackSound()
    {
        float normalizedValue = Backslider.value;
        float val = Mathf.Lerp(0, 1, normalizedValue);
        SoundManager.instance.SetBGMVolume(val);
    }
    public void SeteffectSound()
    {
        float normalizedValue = Effectslider.value;
        float val = Mathf.Lerp(0, 1, normalizedValue);
        SoundManager.instance.SetSFXVolume(val);
    }
    
    public void UICoroutine(string name)
    {
        StartCoroutine(name);
    }
   public IEnumerator FadeIn()
    {
        SoundManager.instance.PlaySFX(DeathSound1);
        SoundManager.instance.PlaySFX(DeathSound2);
        yield return new WaitForSecondsRealtime(0f);
        float curTime = 0f;
        float FadeTime = 1f;
        Color c = DeadFadeinout.GetComponent<RawImage>().color;
        Color c2 = DeadGhost.GetComponent<RawImage>().color;
        while (curTime < FadeTime)
        {
            c.a = Mathf.Lerp(0f, 1f, curTime / FadeTime);
            c2.a = Mathf.Lerp(1f, 0f, curTime / FadeTime);
            DeadGhost.GetComponent<RawImage>().color= c2;
            DeadFadeinout.GetComponent<RawImage>().color = c;
            yield return 0;
            curTime += Time.deltaTime;
        }
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("Restart");
        StartCoroutine(FadeOut());

    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        float curTime = 0f;
        float FadeTime = 1f;
        Color c = DeadFadeinout.GetComponent<RawImage>().color;
        Color c2 = DeadGhost.GetComponent<RawImage>().color;
        while (curTime < FadeTime)
        {
            c.a = Mathf.Lerp(1f, 0f, curTime / FadeTime);
            DeadFadeinout.GetComponent<RawImage>().color = c;
            yield return 0;
            curTime += Time.deltaTime;
        }
    }
}
