using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class MonsterCControl : MonoBehaviour
{
    [Header("Stats")]
    public float searchSpeed; //Searching 중의 이동 속도
    public float followSpeed; //발견하고 Following중의 이동 속도

    [Header("AI")]
    private AIState aiState;

    private float playerDistance; //플레이어와 몬스터간의 거리
    public float searchDistance; //몬스터
    public float followDistance; //몬스터가 팔로우 모드이기 위한 거리

    public float fieldOfView = 120f; //시야각

    public NavMeshAgent agent;
    //private SkinnedMeshRenderer[] meshRenderers; 메쉬 렌더링

    private GetKey getKey; //GetKey 스크립트 불러오기

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        PlayerController.instance.targetMonsterC = this;
        //animator = GetComponentInChildren<Animator>(); 애니메이션
        //meshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>(); 메쉬렌더링
    }

    private void Start()
    {
        SetState(AIState.Searching); //처음 시작하면 몬스터는 Searching 상태.
        agent.isStopped = false;
        GetComponent<NavMeshAgent>().enabled = true;
        gameObject.tag = "Key";
    }

    private void Update()
    {
        playerDistance = Vector3.Distance(transform.position, PlayerController.instance.transform.position); //플레이어 위치 좌표화

        //animator.SetBool("Moving", aiState != AIState.Idle); 애니메이션 기능

        switch (aiState) //각 AI  스테이트일때 해당 업데이트 기능 활성화
        {
            case AIState.Searching: SearchUpdate(); break;
            case AIState.Following: FollowUpdate(); break;
        }

        if(agent.isStopped == true) //
        {
            gameObject.tag = "Enemy";
        }
    }

    private void SearchUpdate()
    {
        if (playerDistance < searchDistance || !IsPlayerInFieldOfView()) //플레이어와 몬스터간의 거리가 search 거리보다 멀거나 또는 플레이어가 몬스터의 시야에 없으면
        {
            //agent.isStopped = false;
            NavMeshPath path = new NavMeshPath();
            if (agent.CalculatePath(PlayerController.instance.transform.position, path)) //플레이어를 향한 경로를 찾고 이동하도록
            {
                agent.SetDestination(PlayerController.instance.transform.position);
            }
        }

        if(playerDistance < followDistance)
        {
            SetState(AIState.Following);
        }
    }

    private void FollowUpdate()
    {
        //agent.isStopped = false;
        NavMeshPath path = new NavMeshPath();
        if (agent.CalculatePath(PlayerController.instance.transform.position, path))
        {
            agent.SetDestination(PlayerController.instance.transform.position);
        }
    }

    bool IsPlayerInFieldOfView() //플레이어가 시야에 있는지 확인.
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
                }
                break;
            case AIState.Following:
                {
                    agent.speed = followSpeed;
                    agent.isStopped = false;
                }
                break;
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