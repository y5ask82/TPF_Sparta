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
   Wandering
}

public class MonsterAControl : MonoBehaviour
{
    [Header("Stats")]
    public float searchSpeed; //Searching 중의 이동 속도
    public float followSpeed; //발견하고 Following중의 이동 속도
    public float wanderSpeed;

    [Header("AI")]
    private AIState aiState;

    [Header("Wandering")]
    public float minWanderDistance;
    public float maxWanderDistance;
    public float minWanderWaitTime;
    public float maxWanderWaitTime;

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
        GetComponent<NavMeshAgent>().enabled = true;
    }

    private void Update()
    {
        playerDistance = Vector3.Distance(transform.position, PlayerController.instance.transform.position); //플레이어 위치 좌표화

        switch (aiState) //각 AI  스테이트일때 해당 업데이트 기능 활성화
        {
            case AIState.Searching: SearchUpdate(); break;
            case AIState.Wandering: WanderUpdate(); break;
            case AIState.Following: FollowUpdate(); break;
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
            }
        }
        if(playerDistance < followDistance)
        {
            SetState(AIState.Wandering);
        }
    }

    private void FollowUpdate()
    {
        agent.isStopped = false;
        NavMeshPath path = new NavMeshPath();
        agent.CalculatePath(PlayerController.instance.transform.position, path);
        agent.SetDestination(PlayerController.instance.transform.position);
        if (playerDistance > followDistance)
        {
            _soundManager.StopBGM();
            SetState(AIState.Searching);
        }
    }

    private void WanderUpdate()
    {
        if (IsPlayerInFieldOfView())
        {
            
            SetState(AIState.Following);
        }
        if(playerDistance > followDistance + 5.0f)
        {
            SetState(AIState.Searching);
        }
        else
        {
            Invoke("WanderToNewLocation", Random.Range(minWanderWaitTime, maxWanderWaitTime));
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
                }
                break;
            case AIState.Wandering:
                {
                    agent.speed = wanderSpeed;
                }
                break;
            case AIState.Following:
                {
                    agent.speed = followSpeed;
                }
                break;
        }
    }

    void WanderToNewLocation()
    {
        agent.SetDestination(GetWanderLocation());
    }


    Vector3 GetWanderLocation()
    {
        NavMeshHit hit;

        NavMesh.SamplePosition(transform.position + (Random.onUnitSphere * Random.Range(minWanderDistance, maxWanderDistance)), out hit, maxWanderDistance, NavMesh.AllAreas);

        int i = 0;
        while (Vector3.Distance(transform.position, hit.position) < followDistance)
        {
            NavMesh.SamplePosition(transform.position + (Random.onUnitSphere * Random.Range(minWanderDistance, maxWanderDistance)), out hit, maxWanderDistance, NavMesh.AllAreas);
            i++;
            if (i == 30)
                break;
        }

        return hit.position;
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
        Debug.Log("stop");
    }
}