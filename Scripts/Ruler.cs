using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Ruler
{
    public int v_min;
    public int v_max;
    public int theta_min;
    public int theta_max;
    public List<float> probability;
    public float scale;
    public int round;

    public Ruler(int round)
    {
        this.round = round;
        v_min = 5 + 2 * round;
        v_max = 10 + 2 * round;
        theta_min = 40;
        theta_max = 60;
        scale = 1 - 0.1f * round;

        probability = new List<float>();
        probability.Add(0.4f - round * 0.1f);
        probability.Add(0.3f - round * 0.05f);
        probability.Add(0.2f + round * 0.05f);
        probability.Add(0.1f + round * 0.1f);
    }

    public int num()
    {
        int x = Random.Range(1, 5 + round);
        return x;

    }

    
}