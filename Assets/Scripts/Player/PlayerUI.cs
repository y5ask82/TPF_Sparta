using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Image[] Keys;
    public Image[] FlashGrenade;
    public Image FlashGrenadeEffect;

    public static PlayerUI instance;


    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PlayerGetKey(bool[] hasKeys)
    {
        for(int i=0; i<hasKeys.Length; i++)
        {
            if (hasKeys[i]) 
            {
                Keys[i].gameObject.SetActive(true);
            }
        }
    }

    public void FlashGrenadeEffectOn()
    {
        UpdateFlashGrenadeUI();

        FlashGrenadeEffect.color = new Color(FlashGrenadeEffect.color.r, FlashGrenadeEffect.color.g, FlashGrenadeEffect.color.b, 1f);
        StartCoroutine(Flash());
    }

    public void UpdateFlashGrenadeUI()
    {
        for (int i = 0; i < FlashGrenade.Length; i++)
        {
            if (i < PlayerController.instance.flashGrenadeNum)
            {
                FlashGrenade[i].gameObject.SetActive(true);
            }
            else
            {
                FlashGrenade[i].gameObject.SetActive(false);
            }
        }
    }


    IEnumerator Flash()
    {
        float alpha = 1;

        while (alpha != 0)
        {
            alpha = (alpha < 0) ? 0 : alpha - Time.deltaTime/2;
            FlashGrenadeEffect.color = new Color(FlashGrenadeEffect.color.r, FlashGrenadeEffect.color.g, FlashGrenadeEffect.color.b, alpha);
            yield return null;
        }

        yield break;
    }

}
