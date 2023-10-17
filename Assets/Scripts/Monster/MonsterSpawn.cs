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
        SpawnMonsterB();
        SpawnMonsterC();
        Time.timeScale = 0.0f; //게임 시작후 일시정지, 플레이어가 움직이면 해제되도록 추가할 것.
    }

    public void SpawnMonsterA()
    {
        curMonsterA = Instantiate(MonsterA);
        float x1 = Random.Range(5, 13);
        float z1 = Random.Range(34, 44);

        // x와 z 좌표를 짝수로 반올림
        x1 = Mathf.Round(x1 / 2) * 2 + 1;
        z1 = Mathf.Round(z1 / 2) * 2 + 1;

        curMonsterA.transform.position = new Vector3(x1, 1, z1);
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
        float x3 = Random.Range(5, 44);
        float z3 = Random.Range(5, 44);

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