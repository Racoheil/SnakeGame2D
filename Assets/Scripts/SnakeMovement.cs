using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class SnakeMovement : MonoBehaviour
{
    private Vector2 _direction = Vector2.right;

    [SerializeField]private float step=0.5f;

    [SerializeField] private GameObject _snakeTail;

    private List<Transform> _segments;

    private void Start()
    {
        _segments = new List<Transform>();
        _segments.Add(this.transform);
        Debug.Log("Count of segments = "+_segments.Count);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _direction = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _direction = Vector2.right;
        }
    }
    private void FixedUpdate()
    {
        Vector3 pointB = new Vector3(Mathf.Round(this.transform.position.x + _direction.x), Mathf.Round(this.transform.position.y + _direction.y), 0.0f);
        MoveSnake(pointB);
    }
    private void MoveSnake(Vector3 pointB)
    {
        if (_segments.Count < 2)
        {
            _segments[0].DOMove(pointB, step);
        }
        
        else if (_segments.Count > 1)
        {
                MoveSnake(0, pointB);
        }
        
       
    }
    private void MoveSnake(int i, Vector3 pointB)
    {
        if (i < _segments.Count)
        {
            _segments[i].DOMove(pointB, step)
                             .OnComplete(() => MoveSnake(i + 1, _segments[i].position));
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Food")
        {
            Debug.Log("Food!");
            CreateNewSegment();
        }
    }
    
    private void CreateNewSegment()
    {
        GameObject snakeTail = Instantiate(_snakeTail);
        _segments.Add(snakeTail.transform);
    }

}
