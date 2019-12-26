using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelGenerator : MonoBehaviour {

    public GameObject layoutRoom;
    public Color startColor, endColor;
    
    public int distanceToEnd;

    public Transform generatorPoint;

    public enum Direction {
        Up,
        Right,
        Down,
        Left
    };

    public Direction selectedDirection;

    public float xOffset = 18f, yOffset = 10f;

    public LayerMask roomLayerMask;

    private GameObject _endRoom;
    
    private List<GameObject> _layoutRoomObjects = new List<GameObject>();

    public RoomPrefabs rooms;
    
    private List<GameObject> _generatedOutlines = new List<GameObject>();

    public RoomCenter centerStart, centerEnd;
    public RoomCenter[] potentialCenters;
    
    // Start is called before the first frame update
    void Start() {
        
        // Draw the map
        Instantiate(layoutRoom, generatorPoint.position, generatorPoint.rotation).GetComponent<SpriteRenderer>().color = startColor;

        selectedDirection = (Direction)Random.Range(0, 4);
        
        MoveGenerationPoint();

        for (int i = 0; i < distanceToEnd; i++) {
            
            GameObject newRoom = Instantiate(layoutRoom, generatorPoint.position, generatorPoint.rotation);
            
            _layoutRoomObjects.Add(newRoom);
            
            if (i + 1 == distanceToEnd) {
                newRoom.GetComponent<SpriteRenderer>().color = endColor;
                _layoutRoomObjects.RemoveAt(_layoutRoomObjects.Count - 1);
                _endRoom = newRoom;
            }
            
            selectedDirection = (Direction)Random.Range(0, 4);
            MoveGenerationPoint();
            
            // Prevent Overlap, use circle to check overlap status with the collider2d 
            while (Physics2D.OverlapCircle(generatorPoint.position, .2f, roomLayerMask)) {
                MoveGenerationPoint();
            }
        }
        
        
        // Create room outlines
        CreateRoomOutline(Vector3.zero);

        foreach (GameObject room in _layoutRoomObjects) {
            CreateRoomOutline(room.transform.position);
        }
        
        CreateRoomOutline(_endRoom.transform.position);
        
        
        // Create room centers
        foreach (GameObject outline in _generatedOutlines) {

            bool centerSetted = false;
            
            if (outline.transform.position == Vector3.zero) {
                Instantiate(centerStart, outline.transform.position, transform.rotation).theRoom = outline.GetComponent<Room>();
                centerSetted = true;
            }

            if (outline.transform.position == _endRoom.transform.position) {
                Instantiate(centerEnd, outline.transform.position, transform.rotation).theRoom = outline.GetComponent<Room>();
                centerSetted = true;
            }

            if (!centerSetted) {
                int centerSelect = Random.Range(0, potentialCenters.Length);

                Instantiate(potentialCenters[centerSelect], outline.transform.position, transform.rotation).theRoom =
                    outline.GetComponent<Room>();
            }
        }
    }

    // Update is called once per frame
    void Update() {
        
        // Only for debug use, R only works in Unity Editor
        #if UNITY_EDITOR
            if (Input.GetKey(KeyCode.R)) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        #endif 
    }

    public void MoveGenerationPoint() {

        switch (selectedDirection) {
            case Direction.Up:
                generatorPoint.position += new Vector3(0f, yOffset,0f);
                break;
            case Direction.Down:
                generatorPoint.position -= new Vector3(0f, yOffset, 0f);
                break;
            case Direction.Left:
                generatorPoint.position -= new Vector3(xOffset, 0f,0f);
                break;
            case Direction.Right:
                generatorPoint.position += new Vector3(xOffset, 0f,0f);
                break;
        }
    }


    public void CreateRoomOutline(Vector3 roomPosition) {

        bool roomUp = Physics2D.OverlapCircle(roomPosition + new Vector3(0f, yOffset, 0f), .2f, roomLayerMask);
        bool roomDown = Physics2D.OverlapCircle(roomPosition - new Vector3(0f, yOffset, 0f), .2f, roomLayerMask);
        bool roomLeft = Physics2D.OverlapCircle(roomPosition - new Vector3(xOffset, 0f, 0f), .2f, roomLayerMask);
        bool roomRight = Physics2D.OverlapCircle(roomPosition + new Vector3(xOffset, 0f, 0f), .2f, roomLayerMask);

        int directionCount = 0;
        
        if (roomUp) {
            directionCount++;
        }

        if (roomDown) {
            directionCount++;
        }

        if (roomLeft) {
            directionCount++;
        }

        if (roomRight) {
            directionCount++;
        }

        switch (directionCount) {
            case 0:
                Debug.LogError("??????? No room");
                break;
            case 1:

                if (roomUp) {
                    _generatedOutlines.Add(Instantiate(rooms.singleUp, roomPosition, transform.rotation));
                } else if (roomDown) {
                    _generatedOutlines.Add(Instantiate(rooms.singleDown, roomPosition, transform.rotation));
                } else if (roomLeft) {
                    _generatedOutlines.Add(Instantiate(rooms.singleLeft, roomPosition, transform.rotation));
                } else if (roomRight) {
                    _generatedOutlines.Add(Instantiate(rooms.singleRight, roomPosition, transform.rotation));
                }
                
                break;
            
            case 2:

                if (roomLeft && roomRight) {
                    _generatedOutlines.Add(Instantiate(rooms.doubleLeftRight, roomPosition, transform.rotation));
                } else if (roomUp && roomDown) {
                    _generatedOutlines.Add(Instantiate(rooms.doubleUpDown, roomPosition, transform.rotation));
                } else if (roomUp && roomRight) { 
                    _generatedOutlines.Add(Instantiate(rooms.doubleUpRight, roomPosition, transform.rotation));
                } else if (roomDown && roomRight) {
                    _generatedOutlines.Add(Instantiate(rooms.doubleDownRight, roomPosition, transform.rotation));
                } else if (roomDown && roomLeft) {
                    _generatedOutlines.Add(Instantiate(rooms.doubleDownLeft, roomPosition, transform.rotation));
                } else if (roomUp && roomLeft) {
                    _generatedOutlines.Add(Instantiate(rooms.doubleUpLeft, roomPosition, transform.rotation));
                }

                break;
            
            case 3:

                if (roomUp && roomDown && roomRight) {
                    _generatedOutlines.Add(Instantiate(rooms.tripleUpDownRight, roomPosition, transform.rotation));
                } else if (roomDown && roomLeft && roomRight) {
                    _generatedOutlines.Add(Instantiate(rooms.tripleDownLeftRight, roomPosition, transform.rotation));
                } else if (roomUp && roomDown && roomLeft) {
                    _generatedOutlines.Add(Instantiate(rooms.tripleUpDownLeft, roomPosition, transform.rotation));
                } else if (roomUp && roomLeft && roomRight) {
                    _generatedOutlines.Add(Instantiate(rooms.tripleUpLeftRight, roomPosition, transform.rotation));
                }
                
                break;
            
            case 4:

                if (roomUp && roomDown && roomLeft && roomRight) {
                    _generatedOutlines.Add(Instantiate(rooms.quadrupleUpDownLeftRight, roomPosition, transform.rotation));
                }
                
                break;
        }

    }
}

[System.Serializable]
public class RoomPrefabs {
    
    public GameObject singleUp, singleDown, singleRight, singleLeft, 
         doubleLeftRight, doubleUpDown, doubleUpRight, doubleDownRight, doubleDownLeft, doubleUpLeft,
         tripleUpDownRight, tripleDownLeftRight, tripleUpDownLeft, tripleUpLeftRight, 
         quadrupleUpDownLeftRight;
    
}
