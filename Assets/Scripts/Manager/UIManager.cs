using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Color = UnityEngine.Color;

public class UIManager : MonoBehaviour
{
    public GameObject UIPanel;
    public GameObject Btns;
    public GameObject Setting;
    public GameObject Brightness;
    public Light light;
    public Slider slider;
    public static UIManager Instance;
    public RawImage Ghost;
    private bool IsUI;
    private Color color;
    private GameObject player;

    private void Awake()
    {
        Instance = this;
        UIPanel.SetActive(false);
        player = GameObject.FindWithTag("Player");
        color = Ghost.GetComponent<RawImage>().color;
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
            Time.timeScale = .0f;
            player.GetComponent<PlayerController>().canLook = false;
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
            Time.timeScale = 1f;
            player.GetComponent<PlayerController>().canLook = true;
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
        Brightness.SetActive(true);
    }
    public void OnClickExit()
    {
        Debug.Log("������~");
    }

    public void SetBright()
    {
        float normalizedValue = slider.normalizedValue;
        float rotationX = Mathf.Lerp(320, 360, normalizedValue);
        light.transform.rotation = Quaternion.Euler(rotationX, 0, 0);
    }


}
