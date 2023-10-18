using UnityEngine;

public class GreenLightOnEnter : MonoBehaviour
{
    public Light greenLight;

    private void Start()
    {
        greenLight = GetComponentInChildren<Light>();
        greenLight.enabled = false; // 시작할 때 빛은 꺼져 있도록 설정
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 플레이어가 충돌하면
        {
            greenLight.enabled = true; // 초록색 빛을 켜기
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // 플레이어가 충돌하면
        {
            greenLight.enabled = false; // 초록색 빛을 켜기
        }
    }
}
