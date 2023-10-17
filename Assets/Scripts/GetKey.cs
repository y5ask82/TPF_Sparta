using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetKey : MonoBehaviour
{

    GameObject nearObject;
    bool iDown;
    public GameObject[] keys;
    public bool[] hasKeys;

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

                PlayerUI.instance.PlayerGetKey(hasKeys);

                Destroy(nearObject);
            }
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Key")
        {
            nearObject = other.gameObject;
        }
        Debug.Log("hi");
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Key")
        {
            nearObject = null;
        }
    }


}
