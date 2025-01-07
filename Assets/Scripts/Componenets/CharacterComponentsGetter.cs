using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AnimationSelecter), typeof(ColorChanger))]
public class CharacterComponentsGetter : MonoBehaviour
{
    private AnimationSelecter _selecter;
    private ColorChanger _colorChanger;
    private Button _openButton;

    private List<Material> _materials;
    private List<string> _animationClipsName;

    private Renderer[] _renderers;
    private Animation _animation;

    private void Awake()
    {
        _selecter = GetComponent<AnimationSelecter>();
        _colorChanger = GetComponent<ColorChanger>();
        _materials = new List<Material>();
        _renderers = GetComponentsInChildren<Renderer>();
        _animation = GetComponent<Animation>();
        _openButton = GetComponentInChildren<Button>();

        TakeMaterials();
        _colorChanger.Init(_materials);

        if (_animation != null)
            TakeAnimations();
        _selecter.TakeAnimation(_animationClipsName, _animation);
    }

    private void TakeMaterials()
    {
        foreach (var renderer in _renderers)
        {
            foreach (Material material in renderer.materials)
            {
                if (!_materials.Contains(material))
                {
                    _materials.Add(material);
                }
            }
        }
    }

    private void TakeAnimations()
    {
        _animationClipsName = new List<string>();

        foreach (AnimationState anim in _animation)
        {
            _animationClipsName.Add(anim.name);
        }
    }

    public ColorChanger GetColorChanger()
    {
        return _colorChanger;
    }

    public AnimationSelecter GetAnimation()
    {
        return _selecter;
    }

    public Button GetOpenButton()
    {
        return _openButton;
    }
}

