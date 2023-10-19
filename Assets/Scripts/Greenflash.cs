using UnityEngine;

public class GreenLightOnEnter : MonoBehaviour
{
    public Light greenLight;




    private void Start()
    {
        greenLight = GetComponentInChildren<Light>();
        greenLight.enabled = false; // ������ �� ���� ���� �ֵ��� ����
    }

    private void OnTriggerEnter(Collider other)
    {
       

        if (other.CompareTag("Player")) // �÷��̾ �浹�ϸ�
        {
            greenLight.enabled = true; // �ʷϻ� ���� �ѱ�
            MonsterSpawn monsterSpawn = FindObjectOfType<MonsterSpawn>();
            GetKey getKey = FindObjectOfType<GetKey>();
            if (getKey.collectedKeys == 0) //������ ���� ������ 1��
            {
               monsterSpawn.KillA();

            }
            if (getKey.collectedKeys == 1) //������ ���� ������ 2��
            {
                monsterSpawn.KillB();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
        if (other.CompareTag("Player")) // �÷��̾ �浹�ϸ�
        {
            greenLight.enabled = false; // �ʷϻ� ���� ����
            MonsterSpawn monsterSpawn = FindObjectOfType<MonsterSpawn>();
            GetKey getKey = FindObjectOfType<GetKey>();
            if (getKey.collectedKeys == 0) //������ ���� ������ 1��
            {
                monsterSpawn.SpawnMonsterA();
            }
            if (getKey.collectedKeys == 1) //������ ���� ������ 2��
            {
                monsterSpawn.SpawnMonsterB();
            }
        }
    }
}