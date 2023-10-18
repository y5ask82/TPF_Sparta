using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetKey : MonoBehaviour
{
    //[SerializeField] GameObject Wall;
    GameObject nearObject;
    bool iDown;
    public GameObject[] keys;
    public GameObject[] walls;
    public bool[] hasKeys;

    private void Update()
    {
        GetInput();
        Interation();
    }
    void GetInput()//키 입력 
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

                PlayerUI.instance.PlayerGetKey(hasKeys);

                Destroy(nearObject);

                if (keyIndex<walls.Length && hasKeys[keyIndex])
                {
                    Destroy(walls[keyIndex]);
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