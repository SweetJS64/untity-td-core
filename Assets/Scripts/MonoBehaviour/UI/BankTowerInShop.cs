using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class BankTowerInShop : MonoBehaviour
{
    public TowerData TowerData;

    //[SerializeField] private TowerData towerData;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _priceText;
    [SerializeField] private Image _iconSprite;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        _iconSprite.sprite = TowerData.Icon;
        _priceText.text = TowerData.Price.ToString();
        _nameText.text = TowerData.NameTower;
    }
}