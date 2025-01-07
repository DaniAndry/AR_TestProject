using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SelectButton : MonoBehaviour
{
    private CharacterComponentsGetter _getter;
    private Button _button;

    public UnityAction<CharacterComponentsGetter> OnButtonClick;

    private void OnEnable()
    {
        _button.onClick.AddListener(Click);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(Click);
    }

    private void Awake()
    {
        _button = GetComponent<Button>();
        _getter = GetComponentInParent<CharacterComponentsGetter>();
    }

    private void Click()
    {
        OnButtonClick?.Invoke(_getter);
    }
}
