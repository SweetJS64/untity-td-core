using TMPro;
using Unity.Entities;
using UnityEngine;

public class ShowWaveLength : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _waveLength;

    private EntityManager _entityManager;
    private Entity _entityStorage;


    void Start()
    {
        _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        _entityStorage = _entityManager.CreateEntityQuery(
            typeof(StorageWaveDataComponent)).GetSingletonEntity();
    }

    void Update()
    {
        _waveLength.text = _entityManager.GetComponentData<StorageWaveDataComponent>(_entityStorage).WaveLength
            .ToString();
    }
}