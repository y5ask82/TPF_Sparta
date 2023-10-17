using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public enum Type { Key}
    public Type type;
    public int value;
    private void Update()
    {
        transform.Rotate(Vector3.up * 10 * Time.deltaTime);
    }
}
