using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManagerScript : MonoBehaviour
{
    private System.Random rnd = new System.Random(); // Create a Random instance

    public List<GameObject> tutoRoomList;
    public List<GameObject> lowGlitchRoomList;
    public GameObject middleGlitchRoom;
    public List<GameObject> highGlitchRoomList;
    public List<GameObject> endgameRoomList;

    public int lowGlitchRoomCount = 3;
    public int highGlitchRoomCount = 3;

    private List<GameObject> roomList = new List<GameObject>();

    private int currentRoomIndex = 0;

    private GameObject currentRoomObj;

    public GameObject CurrentRoomObj
    {
        get { return currentRoomObj; }
    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateRoomList();
        SpawnRoom(0);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void GenerateRoomList()
    {
        foreach (GameObject room in tutoRoomList)
        {
            roomList.Add(room);
        }

        for (int i = 0; i < lowGlitchRoomCount; i++)
        {
            int randomIndex = rnd.Next(lowGlitchRoomList.Count); // Generate a random index
            GameObject selectedRoom = lowGlitchRoomList[randomIndex]; // Get the random GameObject
            lowGlitchRoomList.RemoveAt(randomIndex); // Remove the GameObject from the list
            roomList.Add(selectedRoom);
        }

        roomList.Add(middleGlitchRoom);

        for (int i = 0; i < highGlitchRoomCount; i++)
        {
            int randomIndex = rnd.Next(highGlitchRoomList.Count); // Generate a random index
            GameObject selectedRoom = highGlitchRoomList[randomIndex]; // Get the random GameObject
            highGlitchRoomList.RemoveAt(randomIndex); // Remove the GameObject from the list
            roomList.Add(selectedRoom);
        }

        foreach (GameObject room in endgameRoomList)
        {
            roomList.Add(room);
        }
    }

    private void SpawnRoom(int roomIndex)
    {
        Debug.Log(roomList);
        Debug.Log(currentRoomIndex);
        currentRoomObj = Instantiate(roomList[roomIndex], Vector3.zero, Quaternion.identity);
    }

    public void NextRoom()
    {
        currentRoomIndex += 1;


        if (currentRoomIndex >= roomList.Count)
        {
            currentRoomIndex = 0;
            //Debug.LogError("Room index out of bounds, return to 0");
        }

        Destroy(CurrentRoomObj);
        SpawnRoom(currentRoomIndex);
    }

    public void PreviousRoom()
    {
        currentRoomIndex -= 1;

        Destroy(CurrentRoomObj);
        SpawnRoom(currentRoomIndex);
    }
}
