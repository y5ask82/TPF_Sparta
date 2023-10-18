using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBControl : MonoBehaviour
{
    // 이동할 목표 위치 좌표
    public Vector3 targetPosition;

    // 이동 속도
    public float moveSpeed = 3.0f;

    // 이동을 시작하는 플래그
    private bool isMoving = true;

    private void Start()
    {

    }
    void Update()
    {
        // isMoving 플래그가 true일 때만 이동을 수행
        if (isMoving)
        {
            targetPosition = PlayerController.instance.transform.position;
            // 현재 위치에서 목표 위치로 이동하는 방향 벡터 계산
            Vector3 moveDirection = (targetPosition - transform.position).normalized;

            // 이동 속도와 Time.deltaTime을 사용하여 이동량을 조절
            Vector3 moveAmount = moveDirection * moveSpeed * Time.deltaTime;

            // 실제 이동 수행
            transform.Translate(moveAmount);

            // 목표 위치에 도달하면 이동 종료
            if (Vector3.Distance(transform.position, targetPosition) < 10.0f)
            {
                moveSpeed = 1.0f;
            }
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                isMoving = false;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            //SoundManager.instance.PlaySFX("죽을때나는소리");
            GameObject test = Instantiate(Marking.I.Markings[4], PlayerController.instance.transform.position + new Vector3(0, 0.001f, 0), Quaternion.identity);
            Marking.I.SaveMarkingData(test, Quaternion.identity);
            UIManager.Instance.UICoroutine("FadeIn");
        }
    }
}