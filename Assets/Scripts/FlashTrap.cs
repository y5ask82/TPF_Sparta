using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Timeline.TimelinePlaybackControls;
using UnityEngine.InputSystem;

public class FlashTrap : MonoBehaviour
{
    [SerializeField] private Light flashLight;

    private void OnTriggerEnter(Collider other)
    {
        flashLight.gameObject.SetActive(false);
    }
}
