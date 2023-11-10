using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskFactory : MonoBehaviour
{
    List<GameObject> freeDisk;
    List<GameObject> usedDisk;
    private static DiskFactory instance;
    private DiskFactory() {}
    private void Awake()
    {
        ;
    }

    public void Initialize()
    {
        freeDisk = new List<GameObject>();
        usedDisk = new List<GameObject>();
    }
    public void FreeDisk()
    {
        for (int i = usedDisk.Count - 1; i >= 0; i--)
        {
            GameObject diskObject = usedDisk[i];
            if (!diskObject.activeSelf)
            {
                diskObject.SetActive(true); // 取消禁用
                usedDisk.RemoveAt(i); // 从 usedDisk 中移除
                freeDisk.Add(diskObject); // 放入 freeDisk 中
            }
        }
    }

    public static DiskFactory Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new DiskFactory();
            }
            return instance;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    Vector3 GetRandomLocation()
    {
        float x = Random.Range(-3f, 3f);
        float y = 1f;
        float z = -5f;

        return new Vector3(x, y, z);
    }

    Color GetRandomColor(List<float> prob)
    {
        float randomValue = Random.Range(0f, 1f);
        float cumulativeProb = 0f;

        for (int i = 0; i < prob.Count; i++)
        {
            cumulativeProb += prob[i];

            if (randomValue <= cumulativeProb)
            {
                return GetColorByIndex(i);
            }
        }

        return Color.white;
    }

    Color GetColorByIndex(int index)
    {
        switch (index)
        {
            case 0:
                return new Color(1.0f, 0.5f, 0f);
            case 1:
                return Color.red;
            case 2:
                return new Color(0f, 0f, 0.5f); // 深蓝色
            case 3:
                return Color.black;
            default:
                return Color.white;
        }
    }

    public Disk GetDisk(Ruler r)
    {   
        FreeDisk(); // 在获取新的 Disk 前调用 Free 函数

        float angle = Random.Range(r.theta_min, r.theta_max) * Mathf.Deg2Rad;  // 转换为弧度

        Quaternion rotation = Quaternion.Euler(new Vector3(-angle / Mathf.Deg2Rad, 0, 0));
        Vector3 vec = GetRandomLocation();
        Color diskColor = GetRandomColor(r.probability);
        Rigidbody rb;
        Disk diskData;

        // 检查 freeDisk 中是否有可用的对象
        if (freeDisk.Count > 0)
        {
            GameObject diskObject = freeDisk[0];
            freeDisk.RemoveAt(0); // 从 freeDisk 中移除
            usedDisk.Add(diskObject); // 放入 usedDisk 中
            diskData = diskObject.GetComponent<Disk>();
            rb = diskObject.GetComponent<Rigidbody>();
  
            diskData.transform.rotation = rotation;
            diskData.transform.position = vec;
            rb.useGravity = true;
            return diskData;
        }
        else
        {
            GameObject newdiskObject = Instantiate<GameObject>(
                         Resources.Load<GameObject>("prefabs/Disk"),
                         vec, rotation);
            // 添加刚体组件，并设置重力
            rb = newdiskObject.AddComponent<Rigidbody>();
            rb.useGravity = true;

            diskData = newdiskObject.AddComponent<Disk>();
        }

        Debug.Log("r.scale:" + r.scale);
        diskData.Initialize(r.scale, diskColor);

        // 生成随机速度
        float velocity = Random.Range(r.v_min, r.v_max);
        

        // 计算初始速度的x和y分量
        float vz = velocity * Mathf.Cos(angle);
        float vy = velocity * Mathf.Sin(angle);

        // 设置刚体的初速度
        rb.velocity = new Vector3(0, vy, vz);
        return diskData;
        
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
