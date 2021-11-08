using UnityEngine;
using System.Collections.Generic;

namespace Core
{
    public class GameObjectPool : IGameObjectPool
    {
        private GameObject _prefab;
        private Queue<GameObject> _pool = new Queue<GameObject>();

        public GameObject Get()
        {
#if UNITY_EDITOR
            if(_prefab == null)
                throw new System.Exception("Given prefab is null, before Get() you should use SetGameObjectToPopulate(GameObject)");
#endif
            if(_pool.Count != 0)
            {
                var gameObject = _pool.Dequeue();
                gameObject.SetActive(true);
#if UNITY_EDITOR
                gameObject.hideFlags = HideFlags.None;
#endif
                return gameObject;
            }
            return GameObject.Instantiate(_prefab);
        }
        public void Release(GameObject obj)
        {
            _pool.Enqueue(obj);
            obj.SetActive(false);
#if UNITY_EDITOR
            obj.hideFlags = HideFlags.HideInHierarchy;
#endif
        }
        public void Release(IEnumerable<GameObject> objects)
        {
            var enumerator = objects.GetEnumerator();
            while(enumerator.MoveNext())
                Release(enumerator.Current);
        }
        public void Release<T>(IEnumerable<T> components)
            where T : Component
        {
            var enumerator = components.GetEnumerator();
            while(enumerator.MoveNext())
                Release(enumerator.Current.gameObject);
        }
        public void SetGameObjectToPopulate(GameObject gameObject)
        {
            _prefab = gameObject;
        }
        public void Clear()
        {
            while(_pool.Count != 0)
                GameObject.Destroy(_pool.Dequeue());
        }
    }
}