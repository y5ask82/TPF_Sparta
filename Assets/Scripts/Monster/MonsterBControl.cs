using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBControl : MonoBehaviour
{
    // �̵��� ��ǥ ��ġ ��ǥ
    public Vector3 targetPosition;

    // �̵� �ӵ�
    public float moveSpeed = 3.0f;

    // �̵��� �����ϴ� �÷���
    private bool isMoving = true;

    private void Start()
    {

    }
    void Update()
    {
        // isMoving �÷��װ� true�� ���� �̵��� ����
        if (isMoving)
        {
            targetPosition = PlayerController.instance.transform.position;
            // ���� ��ġ���� ��ǥ ��ġ�� �̵��ϴ� ���� ���� ���
            Vector3 moveDirection = (targetPosition - transform.position).normalized;

            // �̵� �ӵ��� Time.deltaTime�� ����Ͽ� �̵����� ����
            Vector3 moveAmount = moveDirection * moveSpeed * Time.deltaTime;

            // ���� �̵� ����
            transform.Translate(moveAmount);

            // ��ǥ ��ġ�� �����ϸ� �̵� ����
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
            //SoundManager.instance.PlaySFX("���������¼Ҹ�");
            GameObject test = Instantiate(Marking.I.Markings[4], PlayerController.instance.transform.position + new Vector3(0, 0.001f, 0), Quaternion.identity);
            Marking.I.SaveMarkingData(test, Quaternion.identity);
            UIManager.Instance.UICoroutine("FadeIn");
        }
    }
}