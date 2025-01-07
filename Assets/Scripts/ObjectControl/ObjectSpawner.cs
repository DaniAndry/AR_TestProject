using UnityEngine;
using System.Collections.Generic;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] Camera _cameraToFace;
    [SerializeField] int m_SpawnOptionIndex;
    [SerializeField] float _viewportPeriphery = 0.15f;
    [SerializeField] private ObjectOnScene _onScene;

    private List<GameObject> m_ObjectPrefabs = new List<GameObject>();
    private bool _applyRandomAngleAtSpawn = true;
    private float _spawnAngleRange = 45f;
    private GameObject _spawnVisualizationPrefab;
    private GameObject _objectToSpawn;

    private void Awake()
    {
        EnsureFacingCamera();
    }

    public Camera cameraToFace
    {
        get
        {
            EnsureFacingCamera();
            return _cameraToFace;
        }
        set => _cameraToFace = value;
    }

    public List<GameObject> objectPrefabs
    {
        get => m_ObjectPrefabs;
        set => m_ObjectPrefabs = value;
    }

    public int spawnOptionIndex
    {
        get => m_SpawnOptionIndex;
        set => m_SpawnOptionIndex = value;
    }

    public void SelectObject(GameObject objectToSpawn)
    {
        _objectToSpawn = objectToSpawn;
    }

    public bool TrySpawnObject(Vector3 spawnPoint, Vector3 spawnNormal)
    {
        if (_objectToSpawn == null) return false;
        
        var inViewMin = _viewportPeriphery;
        var inViewMax = 1f - _viewportPeriphery;
        var pointInViewportSpace = cameraToFace.WorldToViewportPoint(spawnPoint);

        if (pointInViewportSpace.z < 0f ||
            pointInViewportSpace.x > inViewMax ||
            pointInViewportSpace.x < inViewMin ||
            pointInViewportSpace.y > inViewMax ||
            pointInViewportSpace.y < inViewMin)
        {
            return false;
        }

        var newObject = Instantiate(_objectToSpawn);
        _onScene.AddObject(newObject);

        newObject.transform.position = spawnPoint;
        EnsureFacingCamera();

        var facePosition = _cameraToFace.transform.position;
        var forward = facePosition - spawnPoint;

        var projectedForward = Vector3.ProjectOnPlane(forward, spawnNormal);
        newObject.transform.rotation = default;

        if (_applyRandomAngleAtSpawn)
        {
            var randomRotation = UnityEngine.Random.Range(-_spawnAngleRange, _spawnAngleRange);
            newObject.transform.Rotate(Vector3.up, randomRotation);
        }

        if (_spawnVisualizationPrefab != null)
        {
            var visualizationTrans = Instantiate(_spawnVisualizationPrefab).transform;
            visualizationTrans.position = spawnPoint;
            visualizationTrans.rotation = newObject.transform.rotation;
        }

        return true;
    }


    private void EnsureFacingCamera()
    {
        if (_cameraToFace == null)
            _cameraToFace = Camera.main;
    }
}
