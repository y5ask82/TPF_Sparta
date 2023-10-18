using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetKey : MonoBehaviour
{
    GameObject nearObject;
    [SerializeField] GameObject Timer;
    bool iDown;
    public GameObject[] keys;
    public GameObject[] walls;
    public bool[] hasKeys;

    public int keysToCollect = 3; // 총 획득해야 할 열쇠의 개수
    private int collectedKeys = 0;
    private TimerUI timerUI;

    void Start()
    {
        timerUI = FindObjectOfType<TimerUI>();
    }

    private void Update()
    {
        GetInput();
        Interation();
    }

    void GetInput()
    {
        iDown = Input.GetButtonDown("Interation");
    }

    void Interation()
    {
        if (iDown && nearObject != null)
        {
            if (nearObject.tag == "Key")
            {
                Key key = nearObject.GetComponent<Key>();
                int keyIndex = key.value;
                hasKeys[keyIndex] = true;
                collectedKeys++;

                PlayerUI.instance.PlayerGetKey(hasKeys);

                Destroy(nearObject);

                if (keyIndex < walls.Length && hasKeys[keyIndex])
                {
                    Destroy(walls[keyIndex]);
                }

                if (collectedKeys == keysToCollect)
                {
                    Timer.SetActive(true);
                }
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Key")
        {
            nearObject = other.gameObject;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Key")
        {
            nearObject = null;
        }
    }
}
