using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public static class StaticManager
{
    //static manager for game stage only

    public static LevelManager levelManager;
    public static GameObject player;
    public static ObjectPool ObjectPool;
    public static GameObject SpecialEffectsParent;
    public static SpellsPanel spellsPanel;
    public static DialogueBox dialogueBox;
    public static ObjectiveTracker objectiveTracker;
    public static GameObject [] rooms = new GameObject[5];
    public static CinemachineVirtualCamera camera;
    public static stunBar stunbar;
    public static PrefabManager prefabmanager;
    public static HealthBar healthbar;
    public static inventoryDisplay inventorydisplay;
    public static customCursor customcursor;
    public static MusicManager musicmanager;
    public static SoundManager soundmanager;
    public static zoneText zonetext;
    public static SceneFader sceneFader;

    public static RoomManager currentRoom; //set when player enters room
    //public static GameObject[] roomSpawnPoints = new GameObject[5];

    public static bool cursorMode;

    public static void matchVolumeSettings() //called when changes to volume are made
    {
        musicmanager.matchSettings();
        soundmanager.matchSettings();
    }

    public static void addObjectToPool() //add 1 to pool count, destroy inactive ones if too many.
    {
        ObjectPool.addObject();
    }
}
