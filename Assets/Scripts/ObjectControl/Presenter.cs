using UnityEngine;
using UnityEngine.UI;

public class Presenter : MonoBehaviour
{
    [SerializeField] private MenuController _menuController;

    private ColorChanger _colorChanger;
    private AnimationSelecter _animationSelecter;
    private SelectButton _openButton;


    private void OnEnable()
    {
        _menuController.OnColorSelected += ChangeColor;
    }

    private void OnDisable()
    {
        _menuController.OnColorSelected -= ChangeColor;
    }

    public void Present(GameObject model)
    {
        _openButton = model.GetComponentInChildren<SelectButton>();

        _openButton.OnButtonClick += Open;
    }

    public void Open(CharacterComponentsGetter getter)
    {
        _menuController.OpenMenu();
        _animationSelecter = getter.GetAnimation();
        _colorChanger = getter.GetColorChanger();

        SpawnAnimation();
    }

    private void SpawnAnimation()
    {
        _menuController.SpawnAnimationsButton(_animationSelecter);
    }

    private void ChangeColor(Color color)
    {
        _colorChanger.ChangeColor(color);
    }
}
