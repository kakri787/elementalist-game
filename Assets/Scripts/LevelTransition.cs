using System.Numerics;
using Unity.Cinemachine;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class LevelTransition : MonoBehaviour
{
    [SerializeField] private PolygonCollider2D levelBoundary;
    [SerializeField] private Direction direction;
    [SerializeField] private float playerPosAdjustment;
    CinemachineConfiner2D confiner;

    enum Direction {LEFT, RIGHT}

    void Awake()
    {
        confiner = FindFirstObjectByType<CinemachineConfiner2D>();
    }

    private void UpdatePlayerPos(GameObject player)
    {
        UnityEngine.Vector3 newPos = player.transform.position;

        switch (direction)
        {
            case Direction.LEFT:
                newPos.x -= playerPosAdjustment;
                break;
            case Direction.RIGHT:
                newPos.x += playerPosAdjustment;
                break;
        }

        player.transform.position = newPos;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            confiner.BoundingShape2D = levelBoundary;
            UpdatePlayerPos(collision.gameObject);
        }
    }
}
