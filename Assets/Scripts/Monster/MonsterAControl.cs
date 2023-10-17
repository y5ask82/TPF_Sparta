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
   Attacking
}

public class MonsterAControl : MonoBehaviour
{
    [Header("Stats")]
    public float searchSpeed; //Searching 중의 이동 속도
    public float followSpeed; //발견하고 Following중의 이동 속도
    public float attackSpeed; //공격시 이동 속도

    [Header("AI")]
    private AIState aiState;
    public float detectDistance; //탐지 거리
    public float safeDistance;

    [Header("Combat")]
    public int damage; //데미지
    public float attackRate; //공격 속도
    private float lastAttackTime;
    public float attackDistance; //공격 거리

    private float playerDistance; //플레이어와 몬스터간의 거리
    public float searchDistance; //몬스터가 서치 모드이기 위한 거리
    public float followDistance; //몬스터가 팔로우 모드이기 위한 거리


    public float fieldOfView = 120f; //시야각

    private NavMeshAgent agent;
    //private SkinnedMeshRenderer[] meshRenderers; 메쉬 렌더링

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        //animator = GetComponentInChildren<Animator>(); 애니메이션
        //meshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>(); 메쉬렌더링
    }

    private void Start()
    {
        SetState(AIState.Searching); //처음 시작하면 몬스터는 Searching 상태.
        agent.isStopped = false;
    }

    private void Update()
    {
        playerDistance = Vector3.Distance(transform.position, PlayerController.instance.transform.position); //플레이어 위치 좌표화

        //animator.SetBool("Moving", aiState != AIState.Idle); 애니메이션 기능

        switch (aiState) //각 AI  스테이트일때 해당 업데이트 기능 활성화
        {
            case AIState.Searching: SearchUpdate(); break;
            case AIState.Following: FollowUpdate(); break;
            case AIState.Attacking: AttackUpdate(); break;
        }

    }

    private void SearchUpdate()
    {
        if (playerDistance < searchDistance || !IsPlayerInFieldOfView()) //플레이어와 몬스터간의 거리가 search 거리보다 멀거나 또는 플레이어가 몬스터의 시야에 없으면
        {
            agent.isStopped = false;
            NavMeshPath path = new NavMeshPath();
            if (agent.CalculatePath(PlayerController.instance.transform.position, path)) //플레이어를 향한 경로를 찾고 이동하도록
            {
                agent.SetDestination(PlayerController.instance.transform.position);
                Debug.Log("서치업데이트" + PlayerController.instance.transform.position);
            }
            else //경로를 찾지 못할 경우 에러 출력
            {
                Debug.Log("SearchUpdate에서 플레이어를 향한 경로를 찾지 못했습니다.");
            }
        }
        else //플레이어와 몬스터간의 거리가 search 거리보다 가까워지고 몬스터의 시야에 보이면
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
            Debug.Log("팔로우업데이트 " + PlayerController.instance.transform.position);
        }
        else
        {
            Debug.Log("FollowUpdate에서 플레이어를 향한 경로를 찾지 못했습니다.");
        }
    }

    private void AttackUpdate()
    {
        if (playerDistance > attackDistance || !IsPlayerInFieldOfView()) //플레이어와의 거리가 공격거리보다 멀면
        {
            agent.isStopped = false;
            NavMeshPath path = new NavMeshPath();
            if (agent.CalculatePath(PlayerController.instance.transform.position, path)) //플레이어를 향한 경로를 찾고 이동하도록
            {
                agent.SetDestination(PlayerController.instance.transform.position);
                Debug.Log("어택업데이트" + PlayerController.instance.transform.position);
            }
            else //경로를 찾지 못할 경우 에러 출력
            {
                Debug.Log("AttackUpdate에서 플레이어를 향한 경로를 찾지 못했습니다.");
            }
        }
        else //플레이어와의 거리가 공격거리 이내가 되면 공격
        {
            agent.isStopped = true;
            if (Time.time - lastAttackTime > attackRate) //공격속도와 마지막 공격까지의 시간 계산.
            {
                lastAttackTime = Time.time;
                PlayerController.instance.GetComponent<IDamagable>().TakePhysicalDamage(damage);
                //animator.speed = 1;
                //animator.SetTrigger("Attack"); 애니메이션 일시 제거
            }
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
                    Debug.Log("서치업데이트로 변경");
                }
                break;
            case AIState.Following:
                {
                    agent.speed = followSpeed;
                    agent.isStopped = false;
                    Debug.Log("팔로우업데이트로 변경");
                }
                break;

            case AIState.Attacking:
                {
                    agent.speed = attackSpeed;
                    agent.isStopped = false;
                    Debug.Log("어택업데이트로 변경");
                }
                break;
        }
    }
}