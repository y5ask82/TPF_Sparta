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
            // �ʱ� ��ġ�� (0, 0)���� ����
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





