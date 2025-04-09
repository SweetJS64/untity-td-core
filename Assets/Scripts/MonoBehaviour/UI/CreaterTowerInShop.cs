using UnityEngine;

public class CreaterTowerInShop : MonoBehaviour
{
    [SerializeField] private Transform _grid;
    [SerializeField] private GameObject _pref;
    [SerializeField] private TowerData[] _myScriptableObjects;


    private void Start()
    {
        for (int i = 0; i < _myScriptableObjects.Length; i++)
        {
            var node = Instantiate(_pref, _grid);
            node.transform.localScale = Vector3.one;
            node.GetComponent<BankTowerInShop>().TowerData = _myScriptableObjects[i];
        }
    }
}