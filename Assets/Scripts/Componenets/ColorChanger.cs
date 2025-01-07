using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    private List<Material> _materials = new List<Material>();

    public void Init(List<Material> materials)
    {
        _materials = materials;
    }

    public void ChangeColor(Color color)
    {
        foreach (Material mat in _materials)
        {
            mat.color = color;
        }
    }
}
