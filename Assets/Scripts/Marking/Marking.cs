using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class Marking : MonoBehaviour
{
    public GameObject player;
    public GameObject[] Markings;
    public LayerMask structureLayer;
    public string SpawnsPoint;
    public MarkingData[] markingDatas;
    private int index;
    void Update()
    {
        Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y, Camera.main.transform.position.z));
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 5, structureLayer))
            {
                float x=0f;
                float z=0f;
                if(player.transform.rotation.eulerAngles.y <90 && player.transform.rotation.eulerAngles.y >=0)
                {
                    z = -0.1f;
                }
                else if (player.transform.rotation.eulerAngles.y < 180&& player.transform.rotation.eulerAngles.y >=90)
                {
                    x = -0.1f;
                }
                else if(player.transform.rotation.eulerAngles.y < 270 && player.transform.rotation.eulerAngles.y >= 180)
                {
                    z = 0.1f;
                }
                else
                {
                    x = 0.1f;
                }
                Vector3 zxOffset = new Vector3(x,0,z);

               GameObject test = Instantiate(Markings[index], hit.point+ zxOffset, hit.transform.rotation);

                MarkingData markingData = new MarkingData();
                markingData.x = test.transform.position.x;
                markingData.y = test.transform.position.y;
                markingData.z = test.transform.position.z;
                markingData.rotation = hit.transform.rotation;
                markingData.MarkingIndex = index;
                string markingDataJson = JsonUtility.ToJson(markingData);
                string path = Path.Combine(Application.dataPath, "MarkingData.json");
                File.AppendAllText(path, markingDataJson + "\n");
            }
        }
        //밑에는 테스트용
        if(Input.GetKeyDown(KeyCode.Keypad1))
        {
            index = 1;
        }
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            index = 0;
        }
    }

    private void Start()
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

}

public class MarkingData
{
    public float x;
    public float y;
    public float z;
    public Quaternion rotation;
    public int MarkingIndex;
}