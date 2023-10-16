using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject UIPanel;
    public GameObject Btns;
    public GameObject Setting;
    public GameObject Brightness;
    public Light light;
    public Slider slider;
    public static UIManager Instance;
    private void Awake()
    {
        Instance = this;
        UIPanel.SetActive(false);
    }

    public void PopPanel()
    {
        if(UIPanel.gameObject.activeSelf == false)
        {
            UIPanel.SetActive(true);
            Btns.SetActive(true);
            Setting.SetActive(false);
            Brightness.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            UIPanel.SetActive(false);
            Btns.SetActive(false);
            Setting.SetActive(false);
            Brightness.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }
     
    }
    public void PopSetting()
    {
        Setting.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            PopPanel();
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

        // Map the normalizedValue to the desired X rotation range
        float rotationX = Mathf.Lerp(320, 360, normalizedValue);

        light.transform.rotation = Quaternion.Euler(rotationX, 0, 0);
    }


}
