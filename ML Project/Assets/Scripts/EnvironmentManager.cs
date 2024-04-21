using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    [SerializeField] private Transform agent1;
    [SerializeField] private Transform agent2;

    [SerializeField] private Transform obstacle;

    [SerializeField] private Transform goal1;
    [SerializeField] private Transform goal2;

    private int count;

    public void ResetEnvironment()
    {
        obstacle.localPosition = new Vector3(Random.Range(16f, -16f), 0.5f, 0);

        if (Random.Range(0, 1) > 0.5f)
        {
            goal1.localPosition = new Vector3(Random.Range(-3f, -16f), 0.5f, 16);
            goal2.localPosition = new Vector3(Random.Range(3f, 16f), 0.5f, 16);
        }
        else
        {
            goal2.localPosition = new Vector3(Random.Range(-3f, -16f), 0.5f, 16);
            goal1.localPosition = new Vector3(Random.Range(3f, 16f), 0.5f, 16);
        }

        if (Random.Range(0, 1) > 0.5f)
        {
            agent1.localPosition = new Vector3(Random.Range(-3f, -16f), 0.5f, -16);
            agent2.localPosition = new Vector3(Random.Range(3f, 16f), 0.5f, -16);
        }
        else
        {
            agent2.localPosition = new Vector3(Random.Range(-3f, -16f), 0.5f, -16);
            agent1.localPosition = new Vector3(Random.Range(3f, 16f), 0.5f, -16);
        }
    }

    public void FinishedRun()
    {
        count++;
        if(count == 2)
        {
            ResetEnvironment();
            count = 0;
        }
    }
}
