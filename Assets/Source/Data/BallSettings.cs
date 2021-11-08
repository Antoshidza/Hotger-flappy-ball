using UnityEngine;

[System.Serializable]
public struct BallSettings
{
    [SerializeField]
    public float jumpVelocity;
    [SerializeField]
    public float maxVerticalVelocity;
    [SerializeField]
    public float verticalStartAcceleration;
}
