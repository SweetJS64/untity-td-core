using Unity.Entities;
using UnityEngine;

public struct StorageWaveDataComponent : IComponentData
{
    public int WaveLength;
    public int StartWaveLength;
    public bool StopWave;
}