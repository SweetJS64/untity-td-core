using Unity.Entities;
using UnityEngine;

public struct StorageStatusLevelComponent : IComponentData
{
    public bool Victory;
    public bool Lose;
    public bool Start;
    public bool Stop;
    public bool Reset;
}