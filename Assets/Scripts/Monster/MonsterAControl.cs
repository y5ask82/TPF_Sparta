using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.HID;
using UnityEngine.SceneManagement;

public enum AIState
{
   Searching,
   Following,
}

public class MonsterAControl : MonoBehaviour
{
    [Header("Stats")]
    public float searchSpeed; //Searching 중의 이동 속도
    public float followSpeed; //발견하고 Following중의 이동 속도

    [Header("AI")]
    private AIState aiState;

    private float playerDistance; //플레이어와 몬스터간의 거리
    public float searchDistance; //몬스터가 서치 모드이기 위한 거리
    public float followDistance; //몬스터가 팔로우 모드이기 위한 거리


    public float fieldOfView = 120f; //시야각
    private NavMeshAgent agent;
    private Animator animator;
    //private SkinnedMeshRenderer[] meshRenderers; 메쉬 렌더링

    [SerializeField] AudioClip detectPlayerSFX;
    [SerializeField] AudioClip detectPlayerBGM;
    [SerializeField] LayerMask playerLayer;
    private float detectCoolTime = 15f;
    RaycastHit hit;
    SoundManager _soundManager;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        //meshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>(); 메쉬렌더링
        _soundManager = SoundManager.instance;
    }

    private void Start()
    {
        SetState(AIState.Searching); //처음 시작하면 몬스터는 Searching 상태.
        agent.isStopped = false;
        GetComponent<NavMeshAgent>().enabled = true;
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

        if (Physics.Raycast(transform.position - new Vector3(0,0,0), transform.forward, out hit, 15f))
        {
            
            if (detectCoolTime == 15f && hit.transform.tag == "Player")
            {

                _soundManager.PlayBGM();
                _soundManager.PlaySFXVariable3(detectPlayerSFX, 0.4f);
                detectCoolTime -= Time.deltaTime;
            }

            Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.red);
        }
        if (detectCoolTime <= 0)
            detectCoolTime = 15f;
        else if (detectCoolTime != 15f)
            detectCoolTime -= Time.deltaTime;
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
        }
        if (playerDistance > followDistance)
        {
            _soundManager.StopBGM();
            SetState(AIState.Searching);
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
        if(collision.transform.tag == "Player")
        {
            animator.SetTrigger("Attack");
            GameObject test = Instantiate(Marking.I.Markings[4], PlayerController.instance.transform.position+new Vector3 (0,0.001f,0),Quaternion.identity);
            Marking.I.SaveMarkingData(test, Quaternion.identity);
            UIManager.Instance.UICoroutine("FadeIn");
        }
    }

    private void OnDisable()
    {
        _soundManager.StopBGM();
    }
}