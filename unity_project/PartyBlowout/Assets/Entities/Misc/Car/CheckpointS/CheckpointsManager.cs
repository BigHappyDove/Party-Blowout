using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointsManager : MonoBehaviour
{
    public Dictionary<int, CheckpointSingle> checkpointSingles;

    void Awake()
    {
        checkpointSingles = new Dictionary<int, CheckpointSingle>();
        for(int i = 0; i < transform.childCount; i++)
        {
            CheckpointSingle c = transform.GetChild(i).GetComponent<CheckpointSingle>();
            if (c != null)
                checkpointSingles.Add(c.id, c);
        }
    }
}
