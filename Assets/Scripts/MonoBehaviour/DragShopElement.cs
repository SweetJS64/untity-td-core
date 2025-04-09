using Unity.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public class DragShopElement : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Entity _entityStorage;
    private Entity _entitySimpleTower;
    private Entity _entityFireTower;
    private EntityManager _entityManager;
    private float3 _posSpawn;
    private BlobAssetStore _blobAssetStore;
    private int _coins;

    [SerializeField] private GameObject _towerImgPrefab;
    private GameObject _greenSquare;
    private GameObject _redSquare;

    private Vector2[,] _arrayGridPosElements;
    private bool[,] _arrayGridBool;

    private Vector2 _posActive;
    private int _indexBoolI;
    private int _indexBoolJ;
    private int _towerPrice;

    private GridTowerControl _gridTowerControl;

    void Start()
    {
        _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        _gridTowerControl = gameObject.GetComponentInParent<GridTowerControl>();
        _arrayGridPosElements = _gridTowerControl.GetPosValueInGrid();
        _arrayGridBool = _gridTowerControl.GetBoolValueInGrid();
        
        _entityStorage = _entityManager.CreateEntityQuery(
            typeof(StorageEntitiesComponent)).GetSingletonEntity();
        _entitySimpleTower = _entityManager.GetComponentData<StorageEntitiesComponent>(_entityStorage).SimpleTowerPrefab;
        _entityFireTower = _entityManager.GetComponentData<StorageEntitiesComponent>(_entityStorage).FireTowerPrefab;
    }

    private void Update()
    {
        _towerImgPrefab.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
            Input.mousePosition.y, 1));
        
        //releasing the grid when the level is restarted
        var statusLevel = _entityManager.GetComponentData<StorageStatusLevelComponent>(_entityStorage).Stop;
        if (statusLevel)
        {
            for (int i = 0; i < _arrayGridPosElements.GetLength(0); i++)
            {
                for (int j = 0; j < _arrayGridPosElements.GetLength(1); j++)
                {
                    _gridTowerControl.SetBoolValueInGrid(i, j, true);
                }
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _greenSquare = gameObject.GetComponentInParent<GridTowerControl>().GreenSquare;
        _redSquare = gameObject.GetComponentInParent<GridTowerControl>().RedSquare;
        _arrayGridBool = _gridTowerControl.GetBoolValueInGrid();
        _towerPrice = GetComponent<BankTowerInShop>().TowerData.Price;
        _coins = _entityManager.GetComponentData<StorageCoinsComponent>(_entityStorage).Coins;
        if (_coins >= _towerPrice)
        {
            var towerIcon = GetComponent<BankTowerInShop>().TowerData.Icon;
            _towerImgPrefab.GetComponent<SpriteRenderer>().sprite = towerIcon;
            Instantiate(_towerImgPrefab);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_coins < _towerPrice) return;

        Vector2 dragPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var roundHit = new Vector2(Mathf.Round(dragPoint.x), Mathf.Round(dragPoint.y));

        for (int i = 0; i < _arrayGridPosElements.GetLength(0); i++)
        {
            for (int j = 0; j < _arrayGridPosElements.GetLength(1); j++)
            {
                if (_arrayGridPosElements[i, j] == roundHit && _arrayGridBool[i, j])
                {
                    _posActive = _arrayGridPosElements[i, j];
                    _greenSquare.transform.position = new Vector3(_posActive.x, _posActive.y, 0);
                    _indexBoolI = i;
                    _indexBoolJ = j;
                }

                if (_arrayGridPosElements[i, j] == roundHit && !_arrayGridBool[i, j])
                {
                    _posActive = _arrayGridPosElements[i, j];
                    _redSquare.transform.position = new Vector3(_posActive.x, _posActive.y, 0);
                }
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_coins < _towerPrice) return;
        _greenSquare.transform.position = new Vector3(100, 100, 0);
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
        
        if (hit.collider != null)
        {
            _posSpawn = new float3(_posActive.x, _posActive.y, 0);

            var setSpawnTowerPosition = new LocalTransform
                { Position = _posSpawn, Scale = 0.5f };

            var towerData = GetComponent<BankTowerInShop>().TowerData;
            if (towerData.NameTower == "SimpleTower")
            {
                _entityManager.SetComponentData(_entitySimpleTower, setSpawnTowerPosition);
                _entityManager.Instantiate(_entitySimpleTower);
            }

            if (towerData.NameTower == "FireTower")
            {
                _entityManager.SetComponentData(_entityFireTower, setSpawnTowerPosition);
                _entityManager.Instantiate(_entityFireTower);
            }

            var storageComponent = new StorageCoinsComponent { Coins = _coins - _towerPrice };
            _entityManager.SetComponentData(_entityStorage, storageComponent);

            _arrayGridBool = _gridTowerControl.SetBoolValueInGrid(_indexBoolI, _indexBoolJ, false);
        }
    }
}