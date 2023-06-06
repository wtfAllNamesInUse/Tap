using UnityEngine;

public class PositionProvider : MonoBehaviour
{
    public Transform PlayerStart => playerStart;
    public Transform EnemyStart => enemyStart;

    [SerializeField]
    private Transform playerStart;

    [SerializeField]
    private Transform enemyStart;
}
