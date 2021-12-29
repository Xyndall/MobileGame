using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{

    [Header("RoomGeneration")]
    public Transform[] _startingPositions;
    public GameObject[] _rooms; // index 0 = LR, index 1 = DLR, Index 2 = TLR, Index 3 = TLDR

    private int _direction;
    public float _moveAmount;
    private float _timeBtwRoom;
    public float _startTimeBtwRoom = 0.25f;

    [Header("BorderGeneration")]
    public float _minX;
    public float _maxX;
    public float _minY;
    private bool _stopGenertaiion = false;

    public LayerMask RoomType;

    private void Start()
    {
        int randStartingpos = Random.Range(0, _startingPositions.Length);
        transform.position = _startingPositions[randStartingpos].position;
        Instantiate(_rooms[Random.Range(0, _rooms.Length)], transform.position, Quaternion.identity);

        _direction = Random.Range(1, 6);

    }

    private void Update()
    {
        if (_timeBtwRoom <= 0 && _stopGenertaiion == false)
        {
            Move();
            _timeBtwRoom = _startTimeBtwRoom;
        }
        else
        {
            _timeBtwRoom -= Time.deltaTime;
        }

    }

    private void Move()
    {
        if(_direction == 1 || _direction == 2)// move Right
        {

            if(transform.position.x < _maxX) 
            {
                Vector2 newPos = new Vector2(transform.position.x + _moveAmount, transform.position.y);
                transform.position = newPos;

                //random room chosen from the array to spawn
                int rand = Random.Range(0, _rooms.Length);
                Instantiate(_rooms[rand], transform.position, Quaternion.identity);

                //making sure that rooms dont spawn ontop of eachother
                _direction = Random.Range(1, 6);
                if (_direction == 3)
                {
                    _direction = 2;
                }
                if (_direction == 4)
                {
                    _direction = 5;
                }
            }
            else
            {
                _direction = 5;
            }


        }else if(_direction == 3 || _direction == 4) // move left
        {
            if(transform.position.x > _minX)
            {
                Vector2 newPos = new Vector2(transform.position.x - _moveAmount, transform.position.y);
                transform.position = newPos;

                //random room chosen from the array to spawn
                int rand = Random.Range(0, _rooms.Length);
                Instantiate(_rooms[rand], transform.position, Quaternion.identity);

                //making sure that rooms dont spawn ontop of eachother
                _direction = Random.Range(3, 6);
            }
            else
            {
                _direction = 5;
            }

        }else if(_direction == 5)
        {
            if(transform.position.y > _minY)
            {
                Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, RoomType);
                if(roomDetection.GetComponent<RoomType>()._type != 1 && roomDetection.GetComponent<RoomType>()._type != 3)
                {
                    roomDetection.GetComponent<RoomType>().RoomDestruction();

                    int randBottomRoom = Random.Range(1, 4);
                    if(randBottomRoom == 2)
                    {
                        randBottomRoom = 1;
                    }
                    Instantiate(_rooms[randBottomRoom], transform.position, Quaternion.identity);
                }

                Vector2 newPos = new Vector2(transform.position.x, transform.position.y - _moveAmount);
                transform.position = newPos;

                int rand = Random.Range(2, 4);
                Instantiate(_rooms[rand], transform.position, Quaternion.identity);

                _direction = Random.Range(1, 6);
                
            }
            else
            {
                _stopGenertaiion = true;
            }

        }

        
        

    }

}
