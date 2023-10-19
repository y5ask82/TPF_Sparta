using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundEvent : MonoBehaviour
{
    PlayerConditions playerConditions;

    AudioClip[] walkFootStepSound;
    AudioClip[] runFootStepSound;
    AudioClip[] walkHeartBeatSound;
    AudioClip[] runHeartBeatSound;

    private float heartBeatTerm;
    private float term = 0;

    private void Awake()
    {
        playerConditions = GetComponentInParent<PlayerConditions>();
        walkFootStepSound = Resources.LoadAll<AudioClip>("PlayerSoundEffect/FootStep_Walk");
        runFootStepSound = Resources.LoadAll<AudioClip>("PlayerSoundEffect/FootStep_Run");
        walkHeartBeatSound = Resources.LoadAll<AudioClip>("PlayerSoundEffect/HeartBeat_Walk");
        runHeartBeatSound = Resources.LoadAll<AudioClip>("PlayerSoundEffect/HeartBeat_Run");
        heartBeatTerm = 1;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        heartBeatTerm = playerConditions.stamina.GetPercentage()+0.5f;
        if(term>=heartBeatTerm)
        {
            SoundManager.instance.PlaySFXVariable1(
                runHeartBeatSound[UnityEngine.Random.Range(0, runHeartBeatSound.Length)],
                1.6f - heartBeatTerm);
            term = 0;
        }

        term += Time.deltaTime;
    }

    void Walk()
    {
        SoundManager.instance.PlaySFXVariable2
            (walkFootStepSound[UnityEngine.Random.Range(0, walkFootStepSound.Length)],
            1.1f - playerConditions.stamina.GetPercentage());
    }
    

    void Run()
    {
        SoundManager.instance.PlaySFXVariable2
            (walkFootStepSound[UnityEngine.Random.Range(0, runFootStepSound.Length)],
            1.1f - playerConditions.stamina.GetPercentage());
    }


    
}
