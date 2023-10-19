using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class MonsterSpawn : MonoBehaviour
{
    public GameObject MonsterA;
    private GameObject curMonsterA;
    public GameObject MonsterB;
    private GameObject curMonsterB;
    public GameObject MonsterC;
    private GameObject curMonsterC;

    public Vector3[] spawnPoints;

    void Start()
    {
        SpawnMonsterA();
    }

    public void SpawnMonsterA()
    {
        Destroy(curMonsterA);
        curMonsterA = Instantiate(MonsterA);
        int x1 = Random.Range(11, 23);
        int z1 = Random.Range(40, 44);

        curMonsterA.transform.position = new Vector3(x1, 2.0f, z1); //몬스터B의 소환 좌표 설정.
        curMonsterA.GetComponent<NavMeshAgent>().enabled = false; //처음 생성될 때 NavMeshAgent 비활성화
    }

    public void SpawnMonsterB()
    {
        Destroy(curMonsterB);
        curMonsterB = Instantiate(MonsterB);
        int x2 = Random.Range(5, 13);
        int z2 = Random.Range(5, 11);

        curMonsterB.transform.position = new Vector3(x2, 2.0f, z2); //몬스터B의 소환 좌표 설정.
    }

    public void SpawnMonsterC()
    {
        int arrayLength = spawnPoints.Length;
        int randomIndex = Random.Range(0, arrayLength);
        Vector3 spawnPosition = spawnPoints[randomIndex];

        curMonsterC = Instantiate(MonsterC, spawnPosition, Quaternion.identity);
        curMonsterC.GetComponent<NavMeshAgent>().enabled = false; // 처음 생성될 때 NavMeshAgent 비활성화
    }

    public void SecondPhaseSpawn() //첫번째 열쇠를 획득하면 작동.
    {
        if (curMonsterA != null)
        {
            Destroy(curMonsterA);
        }
        SpawnMonsterB();
    }

    public void ThirdPhaseSpawn() //두번째 열쇠를 획득하면 작동.
    {
        if (curMonsterB != null)
        {
            Destroy(curMonsterB);
        }
        SpawnMonsterC();
        //몬스터 C가 움직이지 않도록.
        MonsterCControl monsterController = FindObjectOfType<MonsterCControl>();
        if (monsterController != null)
        {
            monsterController.searchSpeed = 0.0f;
            monsterController.followSpeed = 0.0f;
        }
    }

    public void LastPhaseSpawn() //마지막 열쇠를 획득하면 작동.
    {
        if (curMonsterC != null)
        {
            Destroy(curMonsterC);
        }
        SpawnMonsterA();
        SpawnMonsterB();
        SpawnMonsterC();

        //마지막 페이즈에서 몬스터 ABC 모두 출현, C도 A와 비슷하게 움직이도록
        MonsterCControl monsterController = FindObjectOfType<MonsterCControl>();
        monsterController.gameObject.tag = "Enemy";
        if (monsterController != null)
        {
            monsterController.searchSpeed = 5.0f;
            monsterController.followSpeed = 1.5f;
        }
    }

    public void KillA()
    {
        Destroy(curMonsterA);
    }
    public void KillB()
    {
        Destroy(curMonsterB);
    }

}