using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBGM : MonoBehaviour
{
    [SerializeField] AudioClip baseBGM;

    // Start is called before the first frame update
    void Start()
    {
        SoundManager.instance.PlayBaseBGM(baseBGM);
    }

}
