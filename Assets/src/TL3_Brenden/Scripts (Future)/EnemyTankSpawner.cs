using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyTankSpawner : MonoBehaviour
{
    private PlaceTile placeTileScript;
    private TankManager tankManager;

    public int numberOfEnemiesToSpawn = 0;

    void Start()
    {
        placeTileScript = FindObjectOfType<PlaceTile>();
        tankManager = FindObjectOfType<TankManager>();

        if (tankManager == null)
        {
            Debug.LogError("EnemyTankSpawner: No TankManager found in the scene!");
            return;
        }

        if (placeTileScript == null)
        {
            Debug.LogError("PlaceTile script not found in the scene!");
        }
    }

    public void SpawnAllEnemies()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        switch (currentSceneName)
        {
            case "Level1": numberOfEnemiesToSpawn = 1; break;
            case "Level2": numberOfEnemiesToSpawn = 1; break;
            case "Level3": numberOfEnemiesToSpawn = 1; break;
            case "Level4": numberOfEnemiesToSpawn = 1; break;
            case "Level5": numberOfEnemiesToSpawn = 2; break;
            case "Level6": numberOfEnemiesToSpawn = 2; break;
            case "Level7": numberOfEnemiesToSpawn = 2; break;
            case "Level8": numberOfEnemiesToSpawn = 1; break;
            case "Level9": numberOfEnemiesToSpawn = 1; break;
            case "Level10": numberOfEnemiesToSpawn = 3; break;
            case "Level666": numberOfEnemiesToSpawn = 4; break;
            case "LevelEaster": numberOfEnemiesToSpawn = 5; break;
        }
        for (int i = 0; i < numberOfEnemiesToSpawn; i++)
        {
            SpawnEnemyTankWithRandomPosition();
        }
    }

    public void SpawnEnemyTankWithRandomPosition()
    {
        if (placeTileScript.Grid == null || placeTileScript.Grid.GetLength(0) == 0)
        {
            Debug.LogError("Grid is not populated properly.");
            return;
        }

        int width = placeTileScript.width;
        int height = placeTileScript.height;

        Vector3 enemyPos;
        bool validPosition = false;
        int attemptLimit = 100;
        int attempts = 0;

        do
        {
            int randX = Random.Range(0, width);
            int randY = Random.Range((height / 2) + (height / 3), height);
            enemyPos = placeTileScript.Grid[randX, randY];
            enemyPos.z = -1f;

            Collider2D tileCollider = Physics2D.OverlapPoint(enemyPos);
            if (tileCollider != null)
            {
                Tiles tile = tileCollider.GetComponent<Tiles>();
                if (tile != null && !(tile is EarthTerrain))
                {
                    validPosition = true;
                    SpawnEnemyTank(enemyPos);
                }
            }
            attempts++;
        } while (!validPosition && attempts < attemptLimit);

        if (attempts >= attemptLimit)
        {
            Debug.LogWarning("EnemyTankSpawner: Failed to find a valid spawn position after many attempts.");
        }
    }

    public void SpawnEnemyTank(Vector3 spawnLocation)
    {
        if (tankManager == null)
        {
            Debug.LogError("EnemyTankSpawner: No TankManager assigned!");
            return;
        }

        GameObject tankPrefab = tankManager.GetTankForCurrentScene();
        if (tankPrefab == null)
        {
            Debug.LogError("EnemyTankSpawner: No tank prefab returned from TankManager!");
            return;
        }

        GameObject spawnedTank = Instantiate(tankPrefab, spawnLocation, Quaternion.identity);
        TankType tank = spawnedTank.GetComponent<TankType>();

        if (tank != null)
        {
            tank.Initialize();
            tank.UpdateTankLocation(spawnLocation);
        }
        else
        {
            Debug.LogError("Spawned object does not have a TankType component!");
        }
    }
}

