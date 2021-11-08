using UnityEngine;
using System.Collections.Generic;
using Zenject;

public class ObstacleController : IObstacleController
{
    private List<Transform> _obstacles  = new List<Transform>();
    private Vector2 _bounds             = new Vector2(0f, 10f);

    private IGameObjectPool _obstaclePool;

    [Inject]
    private void Initialize(IGameObjectPool obstaclePool)
    {
        _obstaclePool = obstaclePool;
    }

    public void SetHorizontalBounds(float left, float right)
    {
#if UNITY_EDITOR
        if(left > right)
            throw new System.Exception($"left bound can't be greater then right: left {left} > right {right}");
#endif
        _bounds = new Vector2(left, right);
    }

    public void SetObstacle(GameObject obstaclePrefab)
    {
        _obstaclePool.SetGameObjectToPopulate(obstaclePrefab);
    }

    public void Spawn(Vector2 position)
    {
        var obstacleTransform = _obstaclePool.Get().transform;
        obstacleTransform.position = position;
        _obstacles.Add(obstacleTransform);
    }

    public void Update(float delta)
    {
        var counter = 0;
        while(counter < _obstacles.Count)
        {
            var obstacleTransform = _obstacles[counter];

            obstacleTransform.Translate(delta, 0f, 0f);

            if(obstacleTransform.position.x < _bounds.x /*|| obstacleTransform.position.x > _bounds.y*/) //ignore right condition
            {
                _obstacles.RemoveAt(counter);

                //destroy / pool obstacle
                _obstaclePool.Release(obstacleTransform.gameObject);
            }
            else
                //we want to increase counter only if obstacle haven't been deleted
                counter++;
        }
    }

    public void Reset()
    {
        _obstaclePool.Release(_obstacles);
        _obstacles.Clear();
    }
}
