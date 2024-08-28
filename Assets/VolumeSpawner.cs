using UnityEngine;
using UnityEngine.AI; // Necesario para trabajar con NavMesh

public class VolumeSpawner : MonoBehaviour
{
    [Header("Volume Settings")]
    public Vector3 volumeSize = new Vector3(10f, 10f, 10f); // Tamaño del volumen

    [Header("Spawning Settings")]
    public GameObject prefab; // Prefab a instanciar
    public float spawnDuration = 5f; // Duración en segundos durante la cual se hará el spawn
    public int spawnCount = 10; // Cantidad total de prefabs a instanciar

    private float timeElapsed = 0f;
    private int spawnedPrefabs = 0;

    void OnDrawGizmos()
    {
        // Dibuja el volumen en la escena
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, volumeSize);
    }

    void Start()
    {
        // Inicia el proceso de spawn
        StartCoroutine(SpawnPrefabs());
    }

    private System.Collections.IEnumerator SpawnPrefabs()
    {
        float timeInterval = spawnDuration / spawnCount;

        while (spawnedPrefabs < spawnCount)
        {
            SpawnPrefab();

            spawnedPrefabs++;
            yield return new WaitForSeconds(timeInterval);
        }
    }

    private void SpawnPrefab()
    {
        // Genera una posición aleatoria dentro del volumen
        Vector3 randomPosition = new Vector3(
            UnityEngine.Random.Range(-volumeSize.x / 2, volumeSize.x / 2),
            0f, // Dejamos Y en 0 para ajustar luego la altura con NavMesh
            UnityEngine.Random.Range(-volumeSize.z / 2, volumeSize.z / 2)
        );

        randomPosition += transform.position;

        // Definimos un punto en el NavMesh cercano a nuestra posición aleatoria
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPosition, out hit, volumeSize.y, NavMesh.AllAreas))
        {
            // Usamos la altura del NavMesh para posicionar el prefab
            Vector3 spawnPosition = hit.position;

            // Instancia el prefab en la posición calculada sobre el NavMesh
            Instantiate(prefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            UnityEngine.Debug.LogWarning("No se encontró una posición válida en el NavMesh para spawnear el prefab.");
        }
    }
}
