using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class RoomManager : MonoBehaviour
{
    public static RoomManager Instance;

  
    public RoomEnum startingRoom;

    public Transform playerTransform; // player object which is moved

    private RoomEnum? currentRoom; // currently loaded room

    private bool isTransitioning; // NEW: true while a room change is in progress, stops a door firing twice mid-fade

    public float blackHoldTime = 0.5f; //  time the screen stays black 

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // only 1
        }
    }

    void Start()
    {
        StartCoroutine(LoadInitialRoom()); // load starting room upon play
    }

    
    private IEnumerator LoadInitialRoom()
    {
        yield return LoadRoomAdditive(startingRoom); // additivly loads first scene
        PlacePlayerAtSpawnPoint(FindSpawnPointInLoadedScenes(GetDefaultSpawnPointForRoom(startingRoom))); // place player object at spawn point
    }

    // used by doors to move the player to a new scene
    public void GoToRoom(RoomEnum targetRoom, SpawnPointEnum targetSpawnPoint)
    {
        StartCoroutine(TransitionToRoom(targetRoom, targetSpawnPoint));
    }


    private IEnumerator TransitionToRoom(RoomEnum targetRoom, SpawnPointEnum targetSpawnPoint)
    {
        if (isTransitioning) yield break;
        isTransitioning = true; // doors cant be used

        yield return UIFade.Instance.FadeOut(); // fade to black


        if (currentRoom.HasValue)
        {
            yield return UnloadRoom(currentRoom.Value); // wait for previous scene to unload
        }

        yield return LoadRoomAdditive(targetRoom); // load new scene

        SpawnPoint spawnPoint = FindSpawnPointInLoadedScenes(targetSpawnPoint); // find spawn
        PlacePlayerAtSpawnPoint(spawnPoint); // place at spawn

        yield return null; // wait 1 frame

        yield return new WaitForSecondsRealtime(UIFade.Instance.holdTime); // CHANGED: hold time now comes from the UIFade Inspector field

        yield return UIFade.Instance.FadeIn(); // black to fade out
        isTransitioning = false; // doors can be used again
    }

    private IEnumerator LoadRoomAdditive(RoomEnum room)
    {
        string sceneName = GetSceneName(room); // get starting scene
        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive); // additly load alongside starting scene

        
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName)); // new room = active scene
        currentRoom = room;
    }

    private IEnumerator UnloadRoom(RoomEnum room)
    {
        string sceneName = GetSceneName(room); // get scene 
        yield return SceneManager.UnloadSceneAsync(sceneName); // unload scene
    }

    
    private SpawnPoint FindSpawnPointInLoadedScenes(SpawnPointEnum targetSpawnPoint)
    {
        SpawnPoint[] allSpawnPoints = FindObjectsByType<SpawnPoint>(FindObjectsSortMode.None); // find every spawn point in scene

        foreach (SpawnPoint spawnPoint in allSpawnPoints) // check each spawn
        {
            if (spawnPoint.spawnPointEnum == targetSpawnPoint) // find correct spawn 
            {
                return spawnPoint; // this is spawn point
            }
        }

        Debug.LogError("No SpawnPoint found at " + targetSpawnPoint + ". Player was not moved.");
        return null; // cant find spawn
    }

    private void PlacePlayerAtSpawnPoint(SpawnPoint spawnPoint)
    {
        if (spawnPoint == null) return; // dont move player is no spawn

        
        CharacterController controller = playerTransform.GetComponent<CharacterController>(); // get player controller
        if (controller != null) controller.enabled = false; // disable controller

        playerTransform.SetPositionAndRotation(spawnPoint.transform.position, spawnPoint.transform.rotation); // move player to spawn point

        if (controller != null) controller.enabled = true; // enable character controller
    }

   // scene enum
    private string GetSceneName(RoomEnum room)
    {
        switch (room)
        {
            case RoomEnum.WhiteBox1: return "WhiteBox 1";
            case RoomEnum.WhiteBox2: return "WhiteBox2";
            case RoomEnum.StreetLevelONE: return "StreetLevelONE";
            case RoomEnum.StreetLevelTWO: return "StreetLevelTWO";
            case RoomEnum.Bar: return "Bar";

            default:
                Debug.LogError("No scene name mapped for " + room);
                return null;
        }
    }

    
    private SpawnPointEnum GetDefaultSpawnPointForRoom(RoomEnum room)
    {
        switch (room)
        {
            case RoomEnum.WhiteBox1: return SpawnPointEnum.WhiteBox1Entrance; // sets room default spawn point
            case RoomEnum.WhiteBox2: return SpawnPointEnum.WhiteBox2Entrance;
            default:
                Debug.LogError("No default spawn point mapped for " + room);
                return default;
        }
    }
}