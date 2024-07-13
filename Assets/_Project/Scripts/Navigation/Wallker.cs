using System;
using UnityEngine;

public class Wallker : MonoBehaviour
{
    public bool IsReverse {  get; private set; }
    public int NextPathNode {  get; private set; }

    private Path _path;
    private float _speed;

    public bool IsActive { get; private set; }

    public event Action OnFishish;
    public event Action OnReachDestination;

    [field: SerializeField] public MeshFilter _meshFilter { get; private set; }
    [field: SerializeField] public MeshRenderer _meshRenderer { get; private set; }
    public bool Actve { get; internal set; }

    public void Set(WallkerVisual wallkerVisual, Path path, float speed)
    {
        _meshFilter.mesh = wallkerVisual.Mesh;
        _meshRenderer.materials = wallkerVisual.Materials;

        _speed = speed;
        _path = path;
        NextPathNode = 1;
        IsReverse = false;
        transform.position = path.Points[0];
        IsActive = true;
        // Set Direction
    }

    public void Move(float delta)
    {
        transform.position = Vector3.MoveTowards(
                transform.position,
                _path.Points[NextPathNode],
                delta * _speed
                );
        
        if (Vector3.Distance(transform.position, _path.Points[NextPathNode]) <= 0.1)
        {
            transform.position = _path.Points[NextPathNode];

            if (IsReverse)
            {
                if (NextPathNode > 0)
                {
                    NextPathNode -= 1;
                }
                else
                {
                    IsActive = false;
                    OnReachDestination = null;
                    OnFishish?.Invoke();
                }
            }
            else
            {
                if (NextPathNode < _path.Points.Length - 1)
                {
                    NextPathNode += 1;
                }
                else
                {
                    OnReachDestination?.Invoke();
                    IsReverse = true;
                }
            }
        }
    }
}