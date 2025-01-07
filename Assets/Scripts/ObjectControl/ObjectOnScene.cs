using System.Collections.Generic;
using UnityEngine;

public class ObjectOnScene : MonoBehaviour
{
    public List<GameObject> _objects;
    private Presenter _presenter;
    private SelectButton _selectButton;


    private void Awake()
    {
        _presenter = GetComponent<Presenter>();
    }

    public void AddObject(GameObject gameObject)
    {
        _objects.Add(gameObject);

        _selectButton = gameObject.GetComponentInChildren<SelectButton>();
        GetPresenter(gameObject);
    }

    private void GetPresenter(GameObject model)
    {
        _presenter.Present(model);
    }
}
