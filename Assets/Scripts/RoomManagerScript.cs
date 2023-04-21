using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManagerScript : MonoBehaviour
{
    public GameObject[] roomList;

    private GameObject currentRoomIndex;

    private GameObject currentRoomObj;

    public GameObject CurrentRoomObj
    {
        get { return currentRoomObj; }
    }


    // Start is called before the first frame update
    void Start()
    {
        SpawnRoom(0);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void SpawnRoom(int roomIndex)
    {
        currentRoomObj = Instantiate(roomList[roomIndex], Vector3.zero, Quaternion.identity);
    }
}
