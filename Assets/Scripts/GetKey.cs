using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetKey : MonoBehaviour
{
    GameObject nearObject;
    [SerializeField] GameObject Timer;
    bool iDown;
    public GameObject[] keys;
    public GameObject[] walls;
    public bool[] hasKeys;

    public int keysToCollect = 3; // 총 획득해야 할 열쇠의 개수
    public int collectedKeys = 0;
    private TimerUI timerUI;

    [SerializeField] AudioClip getKeySFX;

    public GameObject MonsterA;
    public GameObject MonsterB;
    public GameObject MonsterC;


    void Start()
    {
        timerUI = FindObjectOfType<TimerUI>();
        
    }

    private void Update()
    {
        GetInput();
        Interation();
    }

    void GetInput()
    {
        iDown = Input.GetButtonDown("Interation");
    }

    void Interation()
    {
        if (iDown && nearObject != null)
        {
            if (nearObject.tag == "Key")
            {
                Key key = nearObject.GetComponent<Key>();
                int keyIndex = key.value;
                hasKeys[keyIndex] = true;
                collectedKeys++;

                PlayerUI.instance.PlayerGetKey(hasKeys);
                SoundManager.instance.PlaySFXVariable4(getKeySFX, 0.3f);

                Destroy(nearObject);
                MonsterSpawn monsterSpawn = FindObjectOfType<MonsterSpawn>();

                if (keyIndex < walls.Length && hasKeys[keyIndex])
                {
                    Destroy(walls[keyIndex]);
                }
                if(collectedKeys == 1) //키를 획득하고 난 후 수집한 키가 1개면 2번째 스테이지 시작
                {
                    
                    if (monsterSpawn != null)
                    {
                        monsterSpawn.SecondPhaseSpawn();
                    }
                }
                if(collectedKeys == 2) //획득하고 난 후 수집한 키가 2개면 3번째 스테이지 시작
                {
                    
                    if (monsterSpawn != null)
                    {
                        monsterSpawn.ThirdPhaseSpawn();
                    }
                }
                if (collectedKeys == keysToCollect) //획득하고 난 후 수집한 키가 3개면 마지막 스테이지 시작
                {
                    Timer.SetActive(true);
                    
                    if (monsterSpawn != null)
                    {
                        monsterSpawn.LastPhaseSpawn();
                    }
                }
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Key")
        {
            nearObject = other.gameObject;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Key")
        {
            nearObject = null;
        }
    }
}
