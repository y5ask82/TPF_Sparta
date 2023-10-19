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

    public Vector3[] spawnPoints;

    void Start()
    {
        SpawnMonsterA();
    }

    public void SpawnMonsterA()
    {
        curMonsterA = Instantiate(MonsterA);
        int x1 = Random.Range(11, 23);
        int z1 = Random.Range(40, 44);

        curMonsterA.transform.position = new Vector3(x1, 2.0f, z1); //����B�� ��ȯ ��ǥ ����.
        curMonsterA.GetComponent<NavMeshAgent>().enabled = false; //ó�� ������ �� NavMeshAgent ��Ȱ��ȭ
    }

    public void SpawnMonsterB()
    {
        curMonsterB = Instantiate(MonsterB);
        int x2 = Random.Range(5, 13);
        int z2 = Random.Range(5, 11);

        curMonsterB.transform.position = new Vector3(x2, 2.0f, z2); //����B�� ��ȯ ��ǥ ����.
    }

    public void SpawnMonsterC()
    {
        int arrayLength = spawnPoints.Length;
        int randomIndex = Random.Range(0, arrayLength);
        Vector3 spawnPosition = spawnPoints[randomIndex];

        curMonsterC = Instantiate(MonsterC, spawnPosition, Quaternion.identity);
        curMonsterC.GetComponent<NavMeshAgent>().enabled = false; // ó�� ������ �� NavMeshAgent ��Ȱ��ȭ
    }

    public void SecondPhaseSpawn() //ù��° ���踦 ȹ���ϸ� �۵�.
    {
        if (curMonsterA != null)
        {
            Destroy(curMonsterA);
        }
        SpawnMonsterB();
    }

    public void ThirdPhaseSpawn() //�ι�° ���踦 ȹ���ϸ� �۵�.
    {
        if (curMonsterB != null)
        {
            Destroy(curMonsterB);
        }
        SpawnMonsterC();
        //���� C�� �������� �ʵ���.
        MonsterCControl monsterController = FindObjectOfType<MonsterCControl>();
        if (monsterController != null)
        {
            monsterController.searchSpeed = 0.0f;
            monsterController.followSpeed = 0.0f;
        }
    }

    public void LastPhaseSpawn() //������ ���踦 ȹ���ϸ� �۵�.
    {
        if (curMonsterC != null)
        {
            Destroy(curMonsterC);
        }
        SpawnMonsterA();
        SpawnMonsterB();
        SpawnMonsterC();
        //������ ������� ���� ABC ��� ����, C�� A�� ����ϰ� �����̵���
        MonsterCControl monsterController = FindObjectOfType<MonsterCControl>();
        if (monsterController != null)
        {
            monsterController.searchSpeed = 5.0f;
            monsterController.followSpeed = 1.5f;
        }
    }

    public void SafeHouseMonsterStop(Collider objec) //�������� ���� �ý��� �߰�.
    {
        if(keyamount < 3) //������ ���� ������ 3���� ���� ��.
        {
            if (objec.CompareTag("Player")) //�÷��̾ �������뿡 ���� ���� ��Ȱ��ȭ.
            {
                curMonsterA.GetComponent<MonsterAControl>().enabled = false;
                curMonsterB.GetComponent<MonsterAControl>().enabled = false;
                curMonsterC.GetComponent<MonsterAControl>().enabled = false;
            }
        }
    }
}