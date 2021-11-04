using UnityEngine;

[System.Serializable]
public struct BallSettings
{
    [SerializeField]
    public GameObject prefab;
    [SerializeField]
    public float maxUpVelocity;
    [SerializeField]
    public float upAcceleration;
    [SerializeField]
    public float forwardVelocity;
}
