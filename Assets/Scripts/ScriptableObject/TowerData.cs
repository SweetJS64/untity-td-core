using Unity.Entities;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ShopObject", fileName = "NewShopObject", order = 51)]
public class TowerData : ScriptableObject
{
    [SerializeField] private string _nameTower;
    [SerializeField] private Sprite _icon;
    [SerializeField] private int _price;



    public Sprite Icon
    {
        get { return _icon; }
        set { _icon = value; }
    }

    public string NameTower
    {
        get { return _nameTower; }
        set { _nameTower = value; }
    }

    public int Price
    {
        get { return _price; }
        set { _price = value; }
    }
}