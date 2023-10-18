using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{
    public GameObject MonsterA;
    private GameObject curMonsterA;
    public GameObject MonsterB;
    private GameObject curMonsterB;
    public GameObject MonsterC;
    private GameObject curMonsterC;

    void Start()
    {
        SpawnMonsterA();
    }

    public void SpawnMonsterA()
    {
        curMonsterA = Instantiate(MonsterA);
        float x1 = 35.0f;
        float z1 = 35.0f;
        curMonsterA.transform.position = new Vector3(x1, 1.0f, z1); //몬스터B의 소환 좌표 설정.
    }

    public void SpawnMonsterB()
    {
        curMonsterB = Instantiate(MonsterB);
        float x2 = Random.Range(5, 13);
        float z2 = Random.Range(5, 11);

        // x와 z 좌표를 짝수로 반올림
        x2 = Mathf.Round(x2 / 2) * 2 + 1;
        z2 = Mathf.Round(z2 / 2) * 2 + 1;
        curMonsterB.transform.position = new Vector3(x2, 1f, z2); //몬스터B의 소환 좌표 설정.
    }

    public void SpawnMonsterC()
    {
        curMonsterC = Instantiate(MonsterC);
        float x3 = Random.Range(36, 44);
        float z3 = Random.Range(35, 44);

        // x와 z 좌표를 짝수로 반올림
        x3 = Mathf.Round(x3 / 2) * 2 + 1;
        z3 = Mathf.Round(z3 / 2) * 2 + 1;
        curMonsterC.transform.position = new Vector3(x3, 1f, z3); //몬스터C의 랜덤 좌표 설정. 출발점 근처, 중앙안전구역 제외해야함.
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
}