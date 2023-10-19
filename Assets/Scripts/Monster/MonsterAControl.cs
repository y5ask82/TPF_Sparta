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
    public float searchSpeed; //Searching ���� �̵� �ӵ�
    public float followSpeed; //�߰��ϰ� Following���� �̵� �ӵ�
    public float wanderSpeed;

    [Header("AI")]
    private AIState aiState;

    [Header("Wandering")]
    public float minWanderDistance;
    public float maxWanderDistance;
    public float minWanderWaitTime;
    public float maxWanderWaitTime;

    private float playerDistance; //�÷��̾�� ���Ͱ��� �Ÿ�
    public float searchDistance; //���Ͱ� ��ġ ����̱� ���� �Ÿ�
    public float followDistance; //���Ͱ� �ȷο� ����̱� ���� �Ÿ�

    

    public float fieldOfView = 120f; //�þ߰�
    private NavMeshAgent agent;
    private Animator animator;
    //private SkinnedMeshRenderer[] meshRenderers; �޽� ������

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
        //meshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>(); �޽�������
        _soundManager = SoundManager.instance;
    }

    private void Start()
    {
        SetState(AIState.Searching); //ó�� �����ϸ� ���ʹ� Searching ����.
        GetComponent<NavMeshAgent>().enabled = true;
    }

    private void Update()
    {
        playerDistance = Vector3.Distance(transform.position, PlayerController.instance.transform.position); //�÷��̾� ��ġ ��ǥȭ

        switch (aiState) //�� AI  ������Ʈ�϶� �ش� ������Ʈ ��� Ȱ��ȭ
        {
            case AIState.Searching: SearchUpdate(); break;
            case AIState.Wandering: WanderUpdate(); break;
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