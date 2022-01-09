using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    public GameObject characterSpawn;


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
    public bool _stopGenertaiion = false;

    public LayerMask RoomType;

    private int _downCounter;

    Vector3 charPos;

    private void Start()
    {
        int randStartingpos = Random.Range(0, _startingPositions.Length);
        transform.position = _startingPositions[randStartingpos].position;
        Instantiate(_rooms[Random.Range(0, _rooms.Length)], transform.position, Quaternion.identity);

        charPos = transform.position;
        StartCoroutine(Spawn());

        _direction = Random.Range(1, 6);

    }


    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(2);
        Instantiate(characterSpawn, charPos, Quaternion.identity);
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
            

            if (transform.position.x < _maxX) 
            {
                _downCounter = 0;
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
            {  // forces the room generation to go down
                _direction = 5;
            }


        }else if(_direction == 3 || _direction == 4) // move left
        {
            

            if(transform.position.x > _minX)
            {
                _downCounter = 0;
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

        }else if(_direction == 5) // move down
        {
            
            _downCounter++;
            Debug.Log(_downCounter + "");
            if (transform.position.y > _minY)
            {
                Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, RoomType);
                if(roomDetection.GetComponent<RoomType>()._type != 1 && roomDetection.GetComponent<RoomType>()._type != 3)
                {

                    if (_downCounter >= 2) //if moves down twice in a row makes it so the second room will have all 4 openings TLDR
                    {
                        roomDetection.GetComponent<RoomType>().RoomDestruction();
                        Instantiate(_rooms[3], transform.position, Quaternion.identity);
                    }
                    else
                    {
                        roomDetection.GetComponent<RoomType>().RoomDestruction();

                        int randBottomRoom = Random.Range(1, 4);
                        if (randBottomRoom == 2)
                        {
                            randBottomRoom = 1;
                        }
                        Instantiate(_rooms[randBottomRoom], transform.position, Quaternion.identity);
                    }

                    
                }

                Vector2 newPos = new Vector2(transform.position.x, transform.position.y - _moveAmount);
                transform.position = newPos;

                int rand = Random.Range(2, 4);
                Instantiate(_rooms[rand], transform.position, Quaternion.identity);

                _direction = Random.Range(1, 6);
                
            }
            else
            {   // stops the level generation
                _stopGenertaiion = true;
            }

        }

        
        

    }

}
