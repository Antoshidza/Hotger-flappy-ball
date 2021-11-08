using System.Collections.Generic;
using UnityEngine;

public interface IGameObjectPool
{
    void SetGameObjectToPopulate(GameObject gameObject);
    GameObject Get();
    void Release(GameObject obj);
    void Release(IEnumerable<GameObject> objects);
    void Release<T>(IEnumerable<T> components) where T : Component;
    void Clear();
}
