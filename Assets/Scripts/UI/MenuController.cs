using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject _spawnPanel;
    [SerializeField] private Button _closeMenuButton;
    [SerializeField] private AnimationButton _animationButton;
    [SerializeField] private GameObject _animationPanel;
    [SerializeField] private List<ColorChangerView> _colorButtons;
    [SerializeField] private GameObject _settingsPanel;

    private List<AnimationButton> _spawnedAnimationButton = new List<AnimationButton>();

    public UnityAction<Color> OnColorSelected;

    private void OnEnable()
    {
        _closeMenuButton.onClick.AddListener(CloseMenu);

        foreach (var button in _colorButtons)
        {
            button.OnColorSelected += ColorChangeIniter;
        }
    }

    private void OnDisable()
    {
        _closeMenuButton.onClick.RemoveListener(CloseMenu);

        foreach (var button in _colorButtons)
        {
            button.OnColorSelected -= ColorChangeIniter;
        }
    }

    public void OpenMenu()
    {
        _settingsPanel.SetActive(true);
        _spawnPanel.SetActive(false);
    }

    public void CloseMenu()
    {
        _settingsPanel.SetActive(false);
        _spawnPanel.SetActive(true);
        ClearPanel();
    }

    public void SpawnAnimationsButton(AnimationSelecter anim)
    {
        if (anim.Animations == null) { return; }
        ClearPanel();

        for (int i = 0; i < anim.Animations.Count; i++)
        {
            AnimationButton button = Instantiate(_animationButton, _animationPanel.transform);
            button.Init(anim, anim.Animations[i]);
            _spawnedAnimationButton.Add(button);
        }
    }

    private void ClearPanel()
    {
        foreach (AnimationButton button in _spawnedAnimationButton)
        {
            Destroy(button.gameObject);
        }
        _spawnedAnimationButton.Clear();
    }

    private void ColorChangeIniter(Color color)
    {
        OnColorSelected.Invoke(color);
    }
}