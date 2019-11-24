using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public GameObject[] rooms; //change to 2d array when more rooms/factions implemented
    public GameObject initialRoom;

    private void Start()
    {
        Invoke("spawnRooms", 1f);
    }

    public void spawnRooms()
    {
        StaticManager.rooms[0] = initialRoom;
        initialRoom.GetComponent<RoomManager>().roomIndex = 0;

        for (int x = 1; x < 4; x++)
        {
            GameObject temp;
            temp = Instantiate(rooms[Random.Range(0, rooms.Length - 1)], new Vector3(initialRoom.transform.position.x, initialRoom.transform.position.y + (64 * x), initialRoom.transform.position.z), Quaternion.identity, this.gameObject.transform);
            temp.GetComponent<RoomManager>().roomIndex = x;
            StaticManager.rooms[x] = temp;
        }
        //instantiate boss room, set it to 5
    }
}
