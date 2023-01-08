using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : Singleton<CheckpointManager> {

    [SerializeField] List<Checkpoint> checkpoints;
    [SerializeField] Transform startPosition;
    [SerializeField] string checkpointKey = "CheckpointKey";

    public Vector3 GetLastPosition() {
        int valueKey = PlayerPrefs.GetInt(checkpointKey, 0);
        Debug.Log(valueKey);
        if (valueKey > 0) {
            return GetCheckpointPosition(valueKey);
        }
        return startPosition.position;
    }

    Vector3 GetCheckpointPosition(int valueKey) {
        var checkpoint = checkpoints.Find(i => i.valueKey == valueKey);
        return checkpoint.transform.position;
    }
    
}
