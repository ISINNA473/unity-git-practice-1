

using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    // 생성할 적기 Prefab

    public float spawnInterval = 2.0f;
    // 적기 생성 간격

    public float firstSpawnDelay = 1.0f;
    // 첫 번째 적기 생성까지 기다리는 시간

    public float spawnY = 1.1f;
    // 화면 위쪽에서 생성할 위치 (1보다 크면 화면 밖에서 생성)

    public float spawnMargin = 0.5f;
    // 화면 좌우 끝에서 생성할 때의 여백

    void Start()
    {
        // Prefab이 연결되어 있는지 확인
        if (enemyPrefab == null)
        {
            Debug.LogError("Enemy Prefab이 연결되지 않았습니다!");
            return;
        }

        // 생성 간격이 0 이하가 되지 않도록 확인
        if (spawnInterval <= 0)
        {
            Debug.LogError("Spawn Interval은 0보다 커야 합니다!");
            return;
        }

        // 첫 번째 적기 생성 후 일정 시간마다 반복
        InvokeRepeating(
            nameof(SpawnEnemy),
            firstSpawnDelay,
            spawnInterval
        );
    }

    void SpawnEnemy()
    {
        // 메인 카메라 가져오기
        Camera cam = Camera.main;

        // 카메라가 존재하는지 확인
        if (cam == null)
        {
            Debug.LogError("Main Camera를 찾을 수 없습니다!");
            return;
        }

        // 카메라와 생성 위치 사이의 거리
        float distance = -cam.transform.position.z;

        // 화면 왼쪽 범위 계산
        float leftX = cam.ViewportToWorldPoint(
            new Vector3(0, 0, distance)
        ).x + spawnMargin;

        // 화면 오른쪽 범위 계산
        float rightX = cam.ViewportToWorldPoint(
            new Vector3(1, 0, distance)
        ).x - spawnMargin;

        // 생성 범위 확인
        if (leftX >= rightX)
        {
            Debug.LogWarning("적기 생성 범위가 너무 좁습니다!");
            return;
        }

        // 무작위 X좌표 결정
        float randomX = Random.Range(leftX, rightX);

        // 화면 위쪽 Y좌표 계산
        float spawnPositionY = cam.ViewportToWorldPoint(
            new Vector3(0, spawnY, distance)
        ).y;

        // 적기 생성 위치 설정
        Vector3 spawnPosition = new Vector3(
            randomX,
            spawnPositionY,
            0
        );

        // Prefab 복제하여 적기 생성
        Instantiate(
            enemyPrefab,
            spawnPosition,
            Quaternion.identity
        );
    }
}
