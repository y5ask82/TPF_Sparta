using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashTrap : MonoBehaviour
{
    [SerializeField] private Light flashLight;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //flashLight.gameObject.SetActive(false);
            bool active = !flashLight.enabled;
            flashLight.enabled = false;
        }
    }
}
