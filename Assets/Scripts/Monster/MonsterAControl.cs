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


}