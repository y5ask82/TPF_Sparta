using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlashGrenade : MonoBehaviour
{
    private SphereCollider _sphereCollider;
    public bool Flashing = false;

    public AudioClip flashGrenadeClip;

    private void Awake()
    {
        _sphereCollider = GetComponent<SphereCollider>();
        _sphereCollider.radius = 0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            other.gameObject.SetActive(false);
            StartCoroutine(RespawnMonster(other.gameObject));
        }
    }

    IEnumerator RespawnMonster(GameObject go)
    {
        yield return new WaitForSeconds(5f);
        go.SetActive(true);

        yield break;
    }


    public void Flash()
    {
        if (!Flashing)
        {
            SoundManager.instance.PlaySFXVariable4(flashGrenadeClip, 0.1f);
            Flashing = true;
            _sphereCollider.radius = 3.5f;
            Invoke("Shut", 2f);
        }
    }

    private void Shut()
    {
        Flashing = false;
        _sphereCollider.radius = 0f;
    }


}
