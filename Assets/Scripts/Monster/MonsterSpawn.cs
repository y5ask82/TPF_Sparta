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

    private int keyamount;

    void Start()
    {
        SpawnMonsterA();
        //처음 시작시 시간을 멈추고 행동을 시작하면 움직이도록 추가하기
    }

    public void SpawnMonsterA()
    {
        curMonsterA = Instantiate(MonsterA);
        int x1 = Random.Range(11, 23);
        int z1 = Random.Range(40, 44);

        curMonsterA.transform.position = new Vector3(x1, 1.0f, z1); //몬스터B의 소환 좌표 설정.
        curMonsterA.GetComponent<NavMeshAgent>().enabled = false; //처음 생성될 때 NavMeshAgent 비활성화
    }

    public void SpawnMonsterB()
    {
        curMonsterB = Instantiate(MonsterB);
        int x2 = Random.Range(5, 13);
        int z2 = Random.Range(5, 11);

        curMonsterB.transform.position = new Vector3(x2, 1.0f, z2); //몬스터B의 소환 좌표 설정.
        curMonsterB.GetComponent<NavMeshAgent>().enabled = false; //처음 생성될 때 NavMeshAgent 비활성화
    }

    public void SpawnMonsterC()
    {
        curMonsterC = Instantiate(MonsterC);
        int x3 = Random.Range(36, 44);
        int z3 = Random.Range(35, 44);

        curMonsterC.transform.position = new Vector3(x3, 1.0f, z3); //몬스터C의 소환 좌표 설정.
        curMonsterC.GetComponent<NavMeshAgent>().enabled = false; //처음 생성될 때 NavMeshAgent 비활성화
    }

    public void SecondPhaseSpawn() //첫번째 열쇠를 획득하면 작동.
    {
        Destroy(MonsterA);
        SpawnMonsterB();
    }

    public void ThirdPhaseSpawn() //두번째 열쇠를 획득하면 작동.
    {
        Destroy(MonsterB);
        SpawnMonsterC();
    }

    public void LastPhaseSpawn() //마지막 열쇠를 획득하면 작동.
    {
        Destroy(MonsterC);
        SpawnMonsterA();
        SpawnMonsterB();
        SpawnMonsterC();
    }

    public void SafeHouseMonsterStop(Collider objec) //안전지대 들어가는 시스템 추가.
    {
        if(keyamount < 3) //보유한 열쇠 개수가 3보다 적을 때.
        {
            if (objec.CompareTag("Player")) //플레이어가 안전지대에 들어가면 몬스터 비활성화.
            {
                curMonsterA.GetComponent<MonsterAControl>().enabled = false;
                curMonsterB.GetComponent<MonsterAControl>().enabled = false;
                curMonsterC.GetComponent<MonsterAControl>().enabled = false;
            }
        }
    }
}