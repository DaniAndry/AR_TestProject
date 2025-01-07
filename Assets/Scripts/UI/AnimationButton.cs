using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class AnimationButton : MonoBehaviour
{
    private Button _button;
    private AnimationSelecter _selecter;
    private TextMeshProUGUI _animationName;
    private string _name;

    public void Init(AnimationSelecter selecter, string name)
    {
        _animationName = GetComponentInChildren<TextMeshProUGUI>();
        _selecter = selecter;
        _animationName.text = name;
        _name = name;
    }

    private void OnEnable()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(Change);
    }

    private void OnDisable()
    {
        _button?.onClick.RemoveListener(Change);
    }

    private void Change()
    {
        _selecter.ChangeAnimation(_name);
    }
}
