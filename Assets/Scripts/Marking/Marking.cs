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

    public void LoadMarkingData() //게임 시작시 Start구문에서 불러와지며 저장된 마크들 로딩함
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
    public void RemoveMarkingData(GameObject obj) //제거를 위해 만들어진 함수로 PlayerController에서 호출되며 레이캐스트를 사용하여 Ray에 충돌된 오브젝트를 제거하며 동시에 json구문 업데이트함
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

    public MarkingData[] ReLoadMarkingData() //RemoveMarkingData 구문을 사용하기위해 json구문을 따로 배열로 가지고있음
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
    public void SaveMarkingData(GameObject obj,Quaternion qua) //hit된 오브젝트 저장
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