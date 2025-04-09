using UnityEngine;
using UnityEngine.UI;

public class ShopWindowController : MonoBehaviour
{
    private Animator _anim;
    private Image _img;
    private bool _isOpened;
    [SerializeField] private Sprite _iconOpen;
    [SerializeField] private Sprite _iconClose;
    [SerializeField] private GameObject _panel;

    private void Start()
    {
        _anim = GetComponent<Animator>();
        _img = GetComponent<Image>();
    }

    public void ControlShopPanel()
    {
        if (!_isOpened)
        {
            _isOpened = true;
            _anim.SetBool("IsOpened", true);
            _img.sprite = _iconClose;
            _panel.GetComponent<Animator>().SetBool("IsOpened", true);
        }
        else
        {
            _isOpened = false;
            _anim.SetBool("IsOpened", false);
            _img.sprite = _iconOpen;
            _panel.GetComponent<Animator>().SetBool("IsOpened", false);
        }
    }
}