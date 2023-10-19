using UnityEngine;

public class GreenLightOnEnter : MonoBehaviour
{
    public Light greenLight;

    private GetKey getKey;


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
            
            if (getKey.collectedKeys == 0) //������ ���� ������ 1��
            {
                Debug.Log("�Ա�" + getKey.collectedKeys);
            }
            if (getKey.collectedKeys == 1) //������ ���� ������ 2��
            {
                Debug.Log("�Ա�" + getKey.collectedKeys);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // �÷��̾ �浹�ϸ�
        {
            greenLight.enabled = false; // �ʷϻ� ���� ����

            if (getKey.collectedKeys == 0) //������ ���� ������ 1��
            {
                Debug.Log("�ⱸ" + getKey.collectedKeys);
            }
            if (getKey.collectedKeys == 1) //������ ���� ������ 2��
            {
                Debug.Log("�ⱸ" + getKey.collectedKeys);
            }
        }
    }
}