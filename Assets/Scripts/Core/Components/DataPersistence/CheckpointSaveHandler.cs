using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Core.Components.DataPersistence
{
    public static class CheckpointSaveHandler
    {
        public static void SaveCheckpoint(Checkpoint checkpoint)
        {
            PlayerPrefs.SetInt("checkpointID", checkpoint.ID);
        }

        public static int GetLastCheckpointID()
        {
            return PlayerPrefs.GetInt("checkpointID");
        }
    }
}