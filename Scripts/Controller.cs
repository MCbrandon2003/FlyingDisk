using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Controller : MonoBehaviour
{

    DiskFactory factory;
    public BasePoint basePoint;
    List<Disk> LDisk;
    public static int score;
    int round;
    public static bool gameover;
    // the first scripts
    void Awake()
    {
        factory = DiskFactory.Instance;
        factory.Initialize();
        LDisk = new List<Disk>(); // 创建一个空的列表来存储 Disk
    }

    public void Pause()
    {
        throw new System.NotImplementedException();
    }

    public void Resume()
    {
        throw new System.NotImplementedException();
    }

    // Use this for initialization
    void Start()
    {
        ;
    }

    // Update is called once per frame
    void Update()
    {   if(gameover)
        { return;
        }
        if(LDisk.Count == 0)
        {
            //give advice first
            round += 1;
            Debug.Log("Round " + round);
            Ruler r = new Ruler(round);
            int N = r.num();
            for (int i = 0; i < N; i++)
            {
                Disk d = factory.GetDisk(r);
                LDisk.Add(d); // 将得到的 Disk 添加到列表中

                for (int j = 0; j < 4; j++)
                {
                    Debug.Log(d.color);
                    if (d.color == basePoint.colorPoints[j].baseColor)
                    {
                        Rigidbody rb = d.GetComponent<Rigidbody>();
                        Debug.Log("x:" + d.transform.localScale.x);
                        float score = basePoint.colorPoints[j].baseScore + rb.velocity.magnitude + (2 - d.transform.localScale.x) * 10;
                        Debug.Log("Score: " + (int)score);
                        d.setscore((int)score);

                    }
                }
            }
        }
        //give advice first
        for (int i = LDisk.Count - 1; i >= 0; i--)
        {
            Disk disk = LDisk[i];
            if (!disk.gameObject.activeSelf)
            {
                if (disk.goal)
                {
                    score += disk.score; // 将得分加到静态变量 score 上
                    Debug.Log("当前得分：" + score);
                }
                LDisk.RemoveAt(i); // 从列表中移除该 Disk
            }
        }
        if(LDisk.Count == 0 && round == 6)
        {
            gameover = true;
        }
    }
}
