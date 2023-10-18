using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyinitialization : MonoBehaviour
{
    class Monster
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Monster()
        {
            // 초기 위치를 (0, 0)으로 설정
            X = 0;
            Y = 0;
        }

        public void SetPosition(int newX, int newY)
        {
            X = newX;
            Y = newY;
        }
    }

}





