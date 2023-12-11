using UnityEngine;

[CreateAssetMenu(fileName = "MoveStats", menuName = "Stats/Move", order = 0)]
public class MoveStats : ScriptableObject
{
    [SerializeField] private float _speed = 10;
    public float speed => _speed;
    [SerializeField][Range(0, 1)] private float _acceleration = 0.2f;
    public float acceleration => _acceleration;
    [SerializeField][Range(0, 1)] private float _deceleration = 0.2f;
    public float deceleration => _deceleration;
}