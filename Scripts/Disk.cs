using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disk : MonoBehaviour
{
    public float scale;
    public Color color;
    public int score;
    public bool goal;

    public void Initialize(float scale, Color color)
    {
        this.scale = scale;
        this.color = color;
        score = 0;
        float newscale = scale * 2;
        Debug.Log("newscale:" + newscale);
        transform.localScale = new Vector3(newscale, 0.05f, newscale);

        Renderer renderer = GetComponent<Renderer>();
        renderer.material.color = color;
    }

    public void setscore(int s)
    {
        score = s;
    }
    void Start()
    {
        ;
    }

    // 当对象被鼠标点击时的处理
    void OnMouseDown()
    {
        goal = true;
        // 将对象设为非活动状态
        this.gameObject.SetActive(false);
        
    }

    // 当对象发生碰撞时的处理
    void OnCollisionEnter(Collision collision)
    {
        // 如果碰撞的对象是地面
        if (collision.gameObject.tag == "ground")
        {
            // 启动协程，延迟一段时间后再将对象设为非活动状态
            StartCoroutine(DisableAfterSeconds(1.0f));
        }
    }

    // 协程，延迟一段时间后将对象设为非活动状态
    IEnumerator DisableAfterSeconds(float seconds)
    {
        // 等待指定的秒数
        yield return new WaitForSeconds(seconds);

        // 将对象设为非活动状态
        this.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (transform.position.y < -10) this.gameObject.SetActive(false);
    }
}
