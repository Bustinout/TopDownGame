using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class RoomManager : MonoBehaviour
{
    
    public int roomIndex;

    public int BonusObjective;
    /*
    1 - take x amount of damage
    2 - take no damage
    3 - kill streak (no long interval between kills)
    4 - kill all before timer
    5 - kill stationary mob before timer
    6 - kill special escaping mob
    7 - channel object before timer
    */
    public float BonusObjectiveCount;
    public float BonusObjectiveRequred;
    public int enemiesOnScreen;
    public float timeLeft;
    public float totalTime; //this doesn't change, is there for updating the timer slider.
    public float killStreakTimeLeft;
    public float damageTaken; //increased by character health script when damaged

    public GameObject EnemySpawnPoint;
    public GameObject PlayerSpawnPoint;
    public GameObject topwall;
    public GameObject botwall;
    public GameObject leftwall;
    public GameObject rightwall;

    public bool BonusFailed;
    public bool BonusCompleted;
    private bool zoneComplete;

    public GameObject portalPrefab;
    public GameObject bossPortalPrefab;
    public GameObject portal;
    //private powerUp reward; //pickupable item

    // Start is called before the first frame update
    void Start()
    {
        //BonusObjective = Random.Range(1, 7);
        BonusObjective = 4;

        if (roomIndex == 0)
        {
            Invoke("onPlayerEnter", 0.01f);
        }

        if (roomIndex == 3)
        {
            portal = Instantiate(bossPortalPrefab, transform);
            portal.SetActive(false);
        }
        else
        {
            portal = Instantiate(portalPrefab, transform);
            portal.SetActive(false);
        }
        

        //choose 
        //spawn stuff related to bonus obj
        //Invoke("initializeRoom", 0.01f);
    }

    public void initializeRoom()
    {
        StaticManager.objectiveTracker.refresh();
        BonusObjectiveCount = 0;
        enemiesOnScreen = 0;
        damageTaken = 0;
        BonusFailed = false;
        BonusCompleted = false;
        
        switch (BonusObjective)
        {
            case 1: //take x damage
                SpawnEnemy();
                BonusObjectiveRequred = 50; //change to portion of player hp
                timeLeft = -1;
                break;
            case 2:
                SpawnEnemy();
                timeLeft = -1;
                break;
            case 3:
                SpawnEnemy();
                timeLeft = -1;
                //on kill in health script of enemy, check if subObjective is 3, then call chain kill function
                break;
            case 4:
                SpawnEnemy();
                timeLeft = 60;
                StaticManager.objectiveTracker.timerEnabled = true; //move to all cases
                break;
            case 5:
                SpawnEnemy();
                BonusObjectiveRequred = 3;
                timeLeft = 30;
                break;
            case 6:
                SpawnEnemy();
                BonusObjectiveRequred = 1;
                timeLeft = 30;
                break;
            case 7:
                SpawnEnemy();
                BonusObjectiveRequred = 3;
                timeLeft = 60;
                break;
            default:
                Debug.Log("Invalid Subobjective Case");
                break;
        }
        totalTime = timeLeft;
        StaticManager.objectiveTracker.updateText();
        initialized = true;
    }

    public void checkObjective() //check if objective is met
    {
        if (!BonusFailed && !BonusCompleted)
        {
            if (timeLeft == -1)
            {
                switch (BonusObjective)
                {
                    case 1: //take x damage
                        if (damageTaken > BonusObjectiveCount)
                        {
                            completeBonusObjective();
                        }
                        break;
                    case 2: //take no damage
                        if (damageTaken > 0)
                        {
                            failBonusObjective();
                        }
                        break;
                    case 3: //kill streak
                        killStreakTimeLeft -= Time.deltaTime;
                        if (killMade)
                        {
                            if (killStreakTimeLeft <= 0)
                            {
                                failBonusObjective();
                            }
                        }
                        break;
                    default:
                        Debug.Log("BonusObj - " + BonusObjective + " (Invalid Subobjective Case)");
                        break;
                }
            }
            else
            {
                timeLeft -= Time.deltaTime;
                StaticManager.objectiveTracker.updateTimer();
                if (timeLeft <= 0)
                {
                    failBonusObjective();
                }
                else
                {
                    switch (BonusObjective)
                    { //case 4 is completed at endRoom()
                        case 5:
                            if (BonusObjectiveCount >= BonusObjectiveRequred)
                            {
                                completeBonusObjective();
                            }
                            break;
                        case 6:
                            if (BonusObjectiveCount >= BonusObjectiveRequred)
                            {
                                completeBonusObjective();
                            }
                            break;
                        case 7:
                            if (BonusObjectiveCount >= BonusObjectiveRequred)
                            {
                                completeBonusObjective();
                            }
                            break;
                    }
                }
            }
            
        }

        if (enemiesOnScreen == 0)
        {
            switch (BonusObjective)
            {
                case 1:
                    if (damageTaken < BonusObjectiveRequred)
                    {
                        failBonusObjective();
                    }
                    break;
                case 2:
                    if (damageTaken == 0)
                    {
                        completeBonusObjective();
                    }
                    break;
                case 3:
                    if (killStreakTimeLeft > 0)
                    {
                        completeBonusObjective();
                    }
                    break;
                case 4:
                    if (timeLeft > 0)
                    {
                        completeBonusObjective();
                    }
                    break;
                default:

                    break;
            }

            if (!BonusCompleted && !BonusFailed)
            {
                Debug.Log("[BUG] BonusObjective " + BonusObjective + " neither completed nor finished.");
            }
            endRoom();
        }
    }

    public void completeBonusObjective()
    {
        BonusCompleted = true;
        StaticManager.objectiveTracker.updateText();
        //animation/sound
        //drop reward
    }

    public void failBonusObjective()
    {
        BonusFailed = true;
        StaticManager.objectiveTracker.updateText();
        //animation/sound
    }

    public float killStreakWindow = 5f;
    private bool killMade;
    public void chainKill() //invoked in health script of enemy when subobjective is 3.
    {
        if (!BonusFailed)
        {
            if (!killMade)
            {
                killMade = true;
            }
            killStreakTimeLeft = killStreakWindow;
        }
    }

    public void moveSpawner()
    {
        EnemySpawnPoint.transform.position = new Vector3(UnityEngine.Random.Range(leftwall.transform.position.x + 5f, rightwall.transform.position.x - 5f), UnityEngine.Random.Range(botwall.transform.position.y + 5f, topwall.transform.position.y - 5), 0);
    }

    protected virtual void SpawnEnemy()
    {
        for (int x = 0; x < 5; x++)
        {
            moveSpawner();
            Instantiate(StaticManager.levelManager.EnemyPrefabs[UnityEngine.Random.Range(0, StaticManager.levelManager.EnemyPrefabs.Length)], EnemySpawnPoint.transform.position, Quaternion.identity, StaticManager.levelManager.Enemies.transform);
            enemiesOnScreen++;
        }
        
    }

    public void endRoom()
    {
        zoneComplete = true;
        spawnPortal();
        //spawn portal (teleports on touch to next rooms initial) - location depends on players location from wall (cant always be center of stage ocs if player stands there auto tp without reward)
        //change text to enter protal
    }

    public void spawnPortal()
    {

        //spawns in a radius around player
        float radius = 5;
        float angle; //1.6 is 90 clockwise

        if (Vector3.Distance(StaticManager.player.transform.position, leftwall.transform.position) > Vector3.Distance(StaticManager.player.transform.position, rightwall.transform.position)) //spawn left
        {
            if (Vector3.Distance(StaticManager.player.transform.position, topwall.transform.position) > Vector3.Distance(StaticManager.player.transform.position, botwall.transform.position))
            {
                //topleft
                angle = Random.Range(4.8f, 6f);
            }
            else
            {
                angle = Random.Range(3.2f, 4.8f);
            }
        }
        else //spawn right
        {
            if (Vector3.Distance(StaticManager.player.transform.position, topwall.transform.position) > Vector3.Distance(StaticManager.player.transform.position, botwall.transform.position))
            {
                angle = Random.Range(0f, 1.6f);
            }
            else
            {
                angle = Random.Range(1.6f, 3.2f);
            }
        }

        Vector3 offset = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle)) * radius;
        portal.transform.position = StaticManager.player.transform.position + offset;
        portal.transform.position = new Vector3(portal.transform.position.x, portal.transform.position.y, -5);
        portal.SetActive(true);

        spawnItem();
    }

    public void spawnItem()
    {
        //Instantiate(StaticManager.prefabmanager.randomItem(), portal.transform.position, Quaternion.identity);
        Instantiate(StaticManager.prefabmanager.randomItem(), portal.transform.position, Quaternion.identity);
    }

    public GameObject playerSpawn;
    public GameObject followCamera;
    public void teleport()
    {
        if (StaticManager.player.GetComponent<Status>().dashing)
        {
            //if eggman... otherwise other script
            StaticManager.player.GetComponent<Dash>().dashIntoPortal();
        }
        StaticManager.player.GetComponent<PlayerSpawnAnimation>().disableStuff(true);
        StaticManager.player.GetComponent<PlayerSpawnAnimation>().hideModel(true);
        StaticManager.player.GetComponent<Status>().teleporting = true;
        StaticManager.player.GetComponent<Status>().enableReticle(false);
        followCamera.transform.position = StaticManager.player.transform.position;
        StaticManager.camera.m_Follow = followCamera.transform;
        StaticManager.player.transform.position = new Vector3(StaticManager.rooms[roomIndex + 1].GetComponent<RoomManager>().playerSpawn.transform.position.x, StaticManager.rooms[roomIndex + 1].GetComponent<RoomManager>().playerSpawn.transform.position.y, 0);

        StaticManager.rooms[roomIndex + 1].GetComponent<RoomManager>().onPlayerEnter();

        StartCoroutine("moveCamera");
    }

    public void bossPortal()
    {
        SceneManager.LoadScene("DemoEnd");
    }


    IEnumerator moveCamera()
    {
        while (followCamera.transform.position != StaticManager.player.transform.position)
        {
            followCamera.transform.position = Vector3.MoveTowards(followCamera.transform.position, StaticManager.player.transform.position, 1f);
            yield return new WaitForSeconds(0.01f);
        }
        StaticManager.camera.m_Follow = StaticManager.player.GetComponent<Character>().CameraTarget.transform;
        StaticManager.player.GetComponent<Status>().enableReticle(true);
        StaticManager.player.GetComponent<PlayerSpawnAnimation>().startAnimation();
    }

    //this should happen either start of game, or when player teleports to this room
    public void onPlayerEnter() //new function replacing trigger 
    {
        StaticManager.currentRoom = this;
        StaticManager.levelManager.InitialSpawnPoint = PlayerSpawnPoint;
        initializeRoom();
        StaticManager.zonetext.startZoneText(SaveLoad.current.zoneName + " - " + (roomIndex + 1));
    }


    private bool initialized;
    // Update is called once per frame
    void Update()
    {
        if (initialized)
        {
            if (StaticManager.currentRoom.roomIndex == roomIndex)
            {
                if (!zoneComplete)
                {
                    checkObjective();
                }
            }
        }

    }
}
