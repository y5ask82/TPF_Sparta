using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class Marking : MonoBehaviour
{
    public GameObject player;
    public GameObject[] Markings;
    public MarkingData[] markingDatas;
    [HideInInspector]
    public int index;
    public static Marking I;
  

    private void Awake()
    {
        I = this;
    }
    private void Start()
    {
        LoadMarkingData();
    }

    public void LoadMarkingData() //���� ���۽� Start�������� �ҷ������� ����� ��ũ�� �ε���
    {
        string path = Path.Combine(Application.dataPath, "MarkingData.json");
        string[] jsonDataLines = File.ReadAllLines(path);
        markingDatas = new MarkingData[jsonDataLines.Length];
        for (int i = 0; i < jsonDataLines.Length; i++)
        {
            markingDatas[i] = JsonUtility.FromJson<MarkingData>(jsonDataLines[i]);
            Instantiate(Markings[markingDatas[i].MarkingIndex], new Vector3(markingDatas[i].x, markingDatas[i].y, markingDatas[i].z), markingDatas[i].rotation);
        }
    }
    public void RemoveMarkingData(GameObject obj) //���Ÿ� ���� ������� �Լ��� PlayerController���� ȣ��Ǹ� ����ĳ��Ʈ�� ����Ͽ� Ray�� �浹�� ������Ʈ�� �����ϸ� ���ÿ� json���� ������Ʈ��
    {
        MarkingData[] currentMarkingDatas = ReLoadMarkingData();
        List<MarkingData> updatedMarkingDatas = new List<MarkingData>();

        foreach (var data in currentMarkingDatas)
        {
            if (data.x != obj.transform.position.x || data.y != obj.transform.position.y || data.z != obj.transform.position.z)
            {
                updatedMarkingDatas.Add(data);
            }
        }
        string path = Path.Combine(Application.dataPath, "MarkingData.json");
        File.WriteAllText(path, "");
        foreach (var data in updatedMarkingDatas)
        {
            string markingDataJson = JsonUtility.ToJson(data);
            File.AppendAllText(path, markingDataJson + "\n");
        }
    }

    public MarkingData[] ReLoadMarkingData() //RemoveMarkingData ������ ����ϱ����� json������ ���� �迭�� ����������
    {
        string path = Path.Combine(Application.dataPath, "MarkingData.json");
        if (File.Exists(path))
        {
            string[] jsonDataLines = File.ReadAllLines(path);
            markingDatas = new MarkingData[jsonDataLines.Length];
            for (int i = 0; i < jsonDataLines.Length; i++)
            {
                markingDatas[i] = JsonUtility.FromJson<MarkingData>(jsonDataLines[i]);
                
            }
            return markingDatas;
        }
        else
        {
            return new MarkingData[0];
        }
    }
    public void SaveMarkingData(GameObject obj,Quaternion qua) //hit�� ������Ʈ ����
    {
        MarkingData markingData = new MarkingData();
        markingData.x = obj.transform.position.x;
        markingData.y = obj.transform.position.y;
        markingData.z = obj.transform.position.z;
        markingData.rotation = qua;
        markingData.MarkingIndex = index;
        string markingDataJson = JsonUtility.ToJson(markingData);
        string path = Path.Combine(Application.dataPath, "MarkingData.json");
        File.AppendAllText(path, markingDataJson + "\n");
    }

    public void IndexChange()
    {
        float wheelInput = Input.GetAxis("Mouse ScrollWheel");
        if (wheelInput > 0)
        {
            if (index == 3)
            {
                index = 0;
            }
            else
                index++;
        }
        else if (wheelInput < 0)
        {
            if (index == 0)
            {
                index = 3;
            }
            else
                index--;
        }
    }
}
   
public class MarkingData
{
    public float x;
    public float y;
    public float z;
    public Quaternion rotation;
    public int MarkingIndex;
}