using TMPro;
using UnityEngine;
using Unity.Entities;

public class ShowCoins : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _coinsText;

    private EntityManager _entityManager;
    private Entity _entityStorage;


    void Start()
    {
        _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        _entityStorage = _entityManager.CreateEntityQuery(
            typeof(StorageCoinsComponent)).GetSingletonEntity();
    }

    void Update()
    {
        _coinsText.text = _entityManager.GetComponentData<StorageCoinsComponent>(_entityStorage).Coins.ToString();
    }
}