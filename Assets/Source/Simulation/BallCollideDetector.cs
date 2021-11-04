using System;
using UnityEngine;

public class BallCollideDetector : MonoBehaviour
{
    public event Action OnCollide;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnCollide?.Invoke();
    }
}
