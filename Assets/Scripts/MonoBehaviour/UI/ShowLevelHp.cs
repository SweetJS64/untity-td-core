using TMPro;
using Unity.Entities;
using UnityEngine;
public class ShowLevelHp : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _levelHpText;

    private EntityManager _entityManager;
    private Entity _entityStorage;


    void Start()
    {
        _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        _entityStorage = _entityManager.CreateEntityQuery(
            typeof(StorageLevelHpComponent)).GetSingletonEntity();
    }

    void Update()
    {
        _levelHpText.text = _entityManager.GetComponentData<StorageLevelHpComponent>(_entityStorage).LevelHp.ToString();
    }
}
