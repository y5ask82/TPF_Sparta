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
    public float searchSpeed; //Searching ���� �̵� �ӵ�
    public float followSpeed; //�߰��ϰ� Following���� �̵� �ӵ�


}