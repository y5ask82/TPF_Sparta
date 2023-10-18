using UnityEngine;

public class GreenLightOnEnter : MonoBehaviour
{
    private Light greenLight;

    private void Start()
    {
        greenLight = GetComponent<Light>();
        greenLight.enabled = false; // ������ �� ���� ���� �ֵ��� ����
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // �÷��̾ �浹�ϸ�
        {
            greenLight.enabled = true; // �ʷϻ� ���� �ѱ�
        }
    }
}