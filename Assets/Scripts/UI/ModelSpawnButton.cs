using UnityEngine;
using UnityEngine.UI;

public class ModelSpawnButton : MonoBehaviour
{
    [SerializeField] private GameObject _obgetToSpawn;
    [SerializeField] private ObjectSpawner _spawner;

    private Button _button;
    private TMPro.TextMeshProUGUI _text;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _text = GetComponentInChildren<TMPro.TextMeshProUGUI>();
        _text.text = _obgetToSpawn.name;
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(Select);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(Select);
    }

    private void Select()
    {
        _spawner.SelectObject(_obgetToSpawn);
    }
}
