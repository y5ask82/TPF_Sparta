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
    private int collectedKeys = 0;
    private TimerUI timerUI;

    [SerializeField] AudioClip getKeySFX;

    private MonsterSpawn monsterSpawnscript;
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

                if (keyIndex < walls.Length && hasKeys[keyIndex])
                {
                    Destroy(walls[keyIndex]);
                }
                if(collectedKeys == 1)
                {
                    MonsterSpawn monsterSpawn = FindObjectOfType<MonsterSpawn>();
                    if (monsterSpawn != null)
                    {
                        monsterSpawn.SecondPhaseSpawn();
                    }
                }
                if(collectedKeys == 2)
                {
                    MonsterSpawn monsterSpawn = FindObjectOfType<MonsterSpawn>();
                    if (monsterSpawn != null)
                    {
                        monsterSpawn.ThirdPhaseSpawn();
                        Debug.Log("세번째 스테이지 시작");
                    }
                }
                if (collectedKeys == keysToCollect)
                {
                    Timer.SetActive(true);
                    MonsterSpawn monsterSpawn = FindObjectOfType<MonsterSpawn>();
                    if (monsterSpawn != null)
                    {
                        monsterSpawn.LastPhaseSpawn();
                        Debug.Log("마지막 스테이지 시작");
                    }
                    //마지막 라운드 안전지대 제거하는거 추가
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
