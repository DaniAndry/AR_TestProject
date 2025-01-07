using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ColorChangerView : MonoBehaviour
{
    [SerializeField] private Button _button;

    private Color _color;

    public UnityAction<Color> OnColorSelected;

    private void OnEnable()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnButtonClick);
        _color = _button.image.color;
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        OnColorSelected?.Invoke(_color);
    }
}
