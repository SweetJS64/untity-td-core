using Unity.Entities;
using UnityEngine;

public class StartFirstLevel : MonoBehaviour
{
    private EntityManager _entityManager;
    private EntityQuery _queryStorage;
    private bool _ready;

    private void Start()
    {
        _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        _queryStorage = _entityManager.CreateEntityQuery(typeof(StorageEntitiesComponent));
    }

    private void Update()
    {
        if (!_ready)
        {
            int count = _queryStorage.CalculateEntityCount();
            if (count > 0)
            {
                _ready = true;
            }
        }
    }

    public void StartLevel()
    {
        if (!_ready) return;

        var entityStorage = _queryStorage.GetSingletonEntity();
        var statusStart = new StorageStatusLevelComponent { Start = true };
        _entityManager.SetComponentData(entityStorage, statusStart);
    }
}