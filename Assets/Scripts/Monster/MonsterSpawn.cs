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
        //ó�� ���۽� �ð��� ���߰� �ൿ�� �����ϸ� �����̵��� �߰��ϱ�
    }

    public void SpawnMonsterA()
    {
        curMonsterA = Instantiate(MonsterA);
        int x1 = Random.Range(11, 23);
        int z1 = Random.Range(40, 44);

        curMonsterA.transform.position = new Vector3(x1, 1.0f, z1); //����B�� ��ȯ ��ǥ ����.
        curMonsterA.GetComponent<NavMeshAgent>().enabled = false; //ó�� ������ �� NavMeshAgent ��Ȱ��ȭ
    }

    public void SpawnMonsterB()
    {
        curMonsterB = Instantiate(MonsterB);
        int x2 = Random.Range(5, 13);
        int z2 = Random.Range(5, 11);

        curMonsterB.transform.position = new Vector3(x2, 1.0f, z2); //����B�� ��ȯ ��ǥ ����.
        curMonsterB.GetComponent<NavMeshAgent>().enabled = false; //ó�� ������ �� NavMeshAgent ��Ȱ��ȭ
    }

    public void SpawnMonsterC()
    {
        curMonsterC = Instantiate(MonsterC);
        int x3 = Random.Range(36, 44);
        int z3 = Random.Range(35, 44);

        curMonsterC.transform.position = new Vector3(x3, 1.0f, z3); //����C�� ��ȯ ��ǥ ����.
        curMonsterC.GetComponent<NavMeshAgent>().enabled = false; //ó�� ������ �� NavMeshAgent ��Ȱ��ȭ
    }

    public void SecondPhaseSpawn() //ù��° ���踦 ȹ���ϸ� �۵�.
    {
        Destroy(MonsterA);
        SpawnMonsterB();
    }

    public void ThirdPhaseSpawn() //�ι�° ���踦 ȹ���ϸ� �۵�.
    {
        Destroy(MonsterB);
        SpawnMonsterC();
    }

    public void LastPhaseSpawn() //������ ���踦 ȹ���ϸ� �۵�.
    {
        Destroy(MonsterC);
        SpawnMonsterA();
        SpawnMonsterB();
        SpawnMonsterC();
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