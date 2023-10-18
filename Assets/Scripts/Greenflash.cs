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
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // �÷��̾ �浹�ϸ�
        {
            greenLight.enabled = false; // �ʷϻ� ���� �ѱ�
        }
    }
}
