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
        curMonsterA.transform.position = new Vector3(x1, 1.0f, z1); //����B�� ��ȯ ��ǥ ����.
    }

    public void SpawnMonsterB()
    {
        curMonsterB = Instantiate(MonsterB);
        float x2 = Random.Range(5, 13);
        float z2 = Random.Range(5, 11);

        // x�� z ��ǥ�� ¦���� �ݿø�
        x2 = Mathf.Round(x2 / 2) * 2 + 1;
        z2 = Mathf.Round(z2 / 2) * 2 + 1;
        curMonsterB.transform.position = new Vector3(x2, 1f, z2); //����B�� ��ȯ ��ǥ ����.
    }

    public void SpawnMonsterC()
    {
        curMonsterC = Instantiate(MonsterC);
        float x3 = Random.Range(36, 44);
        float z3 = Random.Range(35, 44);

        // x�� z ��ǥ�� ¦���� �ݿø�
        x3 = Mathf.Round(x3 / 2) * 2 + 1;
        z3 = Mathf.Round(z3 / 2) * 2 + 1;
        curMonsterC.transform.position = new Vector3(x3, 1f, z3); //����C�� ���� ��ǥ ����. ����� ��ó, �߾Ӿ������� �����ؾ���.
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
}