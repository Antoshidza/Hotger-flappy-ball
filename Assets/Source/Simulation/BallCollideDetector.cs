using System;
using UnityEngine;

namespace Core
{
    public class BallCollideDetector : MonoBehaviour
    {
        public event Action OnCollide;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            OnCollide?.Invoke();
        }
    }
}