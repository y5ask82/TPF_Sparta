using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashGrenade : MonoBehaviour
{
    private SphereCollider _sphereCollider;
    public bool Flashing = false;

    private void Awake()
    {
        _sphereCollider = GetComponent<SphereCollider>();
        _sphereCollider.radius = 0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {

        }
    }


    public void Flash()
    {
        if (!Flashing)
        {
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
