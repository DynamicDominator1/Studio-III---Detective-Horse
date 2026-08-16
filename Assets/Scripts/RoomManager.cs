using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class RoomManager : MonoBehaviour
{
    public static RoomManager Instance;

  
    public RoomEnum startingRoom;

    public Transform playerTransform;

    private RoomEnum? currentRoom;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        StartCoroutine(LoadInitialRoom());
    }

    
    private IEnumerator LoadInitialRoom()
    {
        yield return LoadRoomAdditive(startingRoom);
        PlacePlayerAtSpawnPoint(FindSpawnPointInLoadedScenes(GetDefaultSpawnPointForRoom(startingRoom)));
    }

    
    public void GoToRoom(RoomEnum targetRoom, SpawnPointEnum targetSpawnPoint)
    {
        StartCoroutine(TransitionToRoom(targetRoom, targetSpawnPoint));
    }

    private IEnumerator TransitionToRoom(RoomEnum targetRoom, SpawnPointEnum targetSpawnPoint)
    {
        if (currentRoom.HasValue)
        {
            yield return UnloadRoom(currentRoom.Value);
        }

        yield return LoadRoomAdditive(targetRoom);

        SpawnPoint spawnPoint = FindSpawnPointInLoadedScenes(targetSpawnPoint);
        PlacePlayerAtSpawnPoint(spawnPoint);
    }

    private IEnumerator LoadRoomAdditive(RoomEnum room)
    {
        string sceneName = GetSceneName(room);
        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
        currentRoom = room;
    }

    private IEnumerator UnloadRoom(RoomEnum room)
    {
        string sceneName = GetSceneName(room);
        yield return SceneManager.UnloadSceneAsync(sceneName);
    }

    
    private SpawnPoint FindSpawnPointInLoadedScenes(SpawnPointEnum targetSpawnPoint)
    {
        SpawnPoint[] allSpawnPoints = FindObjectsByType<SpawnPoint>(FindObjectsSortMode.None);

        foreach (SpawnPoint spawnPoint in allSpawnPoints)
        {
            if (spawnPoint.spawnPointEnum == targetSpawnPoint)
            {
                return spawnPoint;
            }
        }

        Debug.LogError("No SpawnPoint found matching " + targetSpawnPoint + ". Player was not moved.");
        return null;
    }

    private void PlacePlayerAtSpawnPoint(SpawnPoint spawnPoint)
    {
        if (spawnPoint == null) return;

        
        CharacterController controller = playerTransform.GetComponent<CharacterController>();
        if (controller != null) controller.enabled = false;

        playerTransform.SetPositionAndRotation(spawnPoint.transform.position, spawnPoint.transform.rotation);

        if (controller != null) controller.enabled = true;
    }

   
    private string GetSceneName(RoomEnum room)
    {
        switch (room)
        {
            case RoomEnum.WhiteBox1: return "WhiteBox 1";
            case RoomEnum.WhiteBox2: return "WhiteBox2";
            default:
                Debug.LogError("No scene name mapped for " + room);
                return null;
        }
    }

    
    private SpawnPointEnum GetDefaultSpawnPointForRoom(RoomEnum room)
    {
        switch (room)
        {
            case RoomEnum.WhiteBox1: return SpawnPointEnum.WhiteBox1Entrance;
            case RoomEnum.WhiteBox2: return SpawnPointEnum.WhiteBox2Entrance;
            default:
                Debug.LogError("No default spawn point mapped for " + room);
                return default;
        }
    }
}