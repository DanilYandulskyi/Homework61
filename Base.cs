using UnityEngine;
using System.Collections.Generic;

public class Base : MonoBehaviour
{
    [SerializeField] private List<Resource> _resources = new List<Resource>();
    [SerializeField] private List<Unit> _units = new List<Unit>();

    [SerializeField] private List<Resource> _collectedResources = new List<Resource>();
    
    [SerializeField] private UI _ui;

    private void Start()
    {
        enabled = true;
        _ui.UpdateText(_collectedResources);
        
        foreach (Unit unit in _units)
        {
            unit.CollectedResource += CollectResource;
        }
    }

    private void OnDisable()
    {
        foreach (Unit unit in _units)
        {
            unit.CollectedResource -= CollectResource;
        }
    }

    private void Update()
    {
        if (_resources.Count > 0)
        {
            for (int i = 0; i < _units.Count; i++)
            {
                if (_units[i].IsStanding)
                {
                    _units[i].StartMovingToResource(_resources[0]);
                    _resources.Remove(_resources[0]);
                    break;
                }
            }
        }
    }   

    public void CollectResource(Resource resource)
    {
        _collectedResources.Add(resource);
        _ui.UpdateText(_collectedResources);
    }

    public void AddResource(Resource resource)
    {
        _resources.Add(resource);
    }
}
