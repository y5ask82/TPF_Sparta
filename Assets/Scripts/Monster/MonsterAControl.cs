using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public enum AIState
{
   Searching,
   Following,
}

public class MonsterAControl : MonoBehaviour
{
    [Header("Stats")]
    public float searchSpeed; //Searching ���� �̵� �ӵ�
    public float followSpeed; //�߰��ϰ� Following���� �̵� �ӵ�
    public float attackSpeed; //���ݽ� �̵� �ӵ�

    [Header("AI")]
    private AIState aiState;
    public float detectDistance; //Ž�� �Ÿ�
    public float safeDistance;

    [Header("Combat")]
    public int damage; //������
    public float attackRate; //���� �ӵ�
    private float lastAttackTime;
    public float attackDistance; //���� �Ÿ�

    private float playerDistance; //�÷��̾�� ���Ͱ��� �Ÿ�
    public float searchDistance; //���Ͱ� ��ġ ����̱� ���� �Ÿ�
    public float followDistance; //���Ͱ� �ȷο� ����̱� ���� �Ÿ�


    public float fieldOfView = 120f; //�þ߰�

    private NavMeshAgent agent;
    //private SkinnedMeshRenderer[] meshRenderers; �޽� ������

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        //animator = GetComponentInChildren<Animator>(); �ִϸ��̼�
        //meshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>(); �޽�������
    }

    private void Start()
    {
        SetState(AIState.Searching); //ó�� �����ϸ� ���ʹ� Searching ����.
        agent.isStopped = false;
        GetComponent<NavMeshAgent>().enabled = true;
    }

    private void Update()
    {
        playerDistance = Vector3.Distance(transform.position, PlayerController.instance.transform.position); //�÷��̾� ��ġ ��ǥȭ

        //animator.SetBool("Moving", aiState != AIState.Idle); �ִϸ��̼� ���

        switch (aiState) //�� AI  ������Ʈ�϶� �ش� ������Ʈ ��� Ȱ��ȭ
        {
            case AIState.Searching: SearchUpdate(); break;
            case AIState.Following: FollowUpdate(); break;
        }
    }

    private void SearchUpdate()
    {
        if (playerDistance < searchDistance || !IsPlayerInFieldOfView()) //�÷��̾�� ���Ͱ��� �Ÿ��� search �Ÿ����� �ְų� �Ǵ� �÷��̾ ������ �þ߿� ������
        {
            agent.isStopped = false;
            NavMeshPath path = new NavMeshPath();
            if (agent.CalculatePath(PlayerController.instance.transform.position, path)) //�÷��̾ ���� ��θ� ã�� �̵��ϵ���
            {
                agent.SetDestination(PlayerController.instance.transform.position);
                Debug.Log("��ġ������Ʈ" + PlayerController.instance.transform.position);
            }

            else //��θ� ã�� ���� ��� ���� ���
            {
                Debug.Log("SearchUpdate���� �÷��̾ ���� ��θ� ã�� ���߽��ϴ�.");
            }
        }

        if(playerDistance < followDistance)
        {
            SetState(AIState.Following);
        }
    }

    private void FollowUpdate()
    {
        agent.isStopped = false;
        NavMeshPath path = new NavMeshPath();
        if (agent.CalculatePath(PlayerController.instance.transform.position, path))
        {
            agent.SetDestination(PlayerController.instance.transform.position);
            Debug.Log("�ȷο������Ʈ " + PlayerController.instance.transform.position);
        }
        else
        {
            Debug.Log("FollowUpdate���� �÷��̾ ���� ��θ� ã�� ���߽��ϴ�.");
        }
    }

    bool IsPlayerInFieldOfView() //�÷��̾ �þ߿� �ִ��� Ȯ��.
    {
        Vector3 directionToPlayer = PlayerController.instance.transform.position - transform.position;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);
        return angle < fieldOfView * 0.5f;
    }

    private void SetState(AIState newState)
    {
        aiState = newState;
        switch (aiState)
        {
            case AIState.Searching:
                {
                    agent.speed = searchSpeed;
                    agent.isStopped = false;
                    Debug.Log("��ġ������Ʈ�� ����");
                }
                break;
            case AIState.Following:
                {
                    agent.speed = followSpeed;
                    agent.isStopped = false;
                    Debug.Log("�ȷο������Ʈ�� ����");
                }
                break;
        }
    }
}