

using UnityEngine;

public class EnemyA : enemy
{
    public float horizontalRange = 1.5f;
    // 좌우로 움직이는 최대 거리

    public float horizontalFrequency = 1.5f;
    // 좌우 움직임의 빠르기

    public float screenMargin = 0.5f;
    // 화면 양쪽 끝에서 유지할 여백

    float startX;
    // 적기가 처음 생성된 X축 위치

    float randomOffset;
    // 적기마다 다른 움직임을 만들기 위한 무작위 값

    float leftLimit;
    float rightLimit;
    // 카메라 화면의 좌우 경계


    void Start()
    {
        // 적기가 생성된 위치 저장
        startX = transform.position.x;

        // 적기마다 다른 좌우 움직임 생성
        randomOffset = Random.Range(0f, 6.28f);

        // 메인 카메라 가져오기
        Camera cam = Camera.main;

        // 카메라와 적기 사이의 거리
        float distance = -cam.transform.position.z;

        // 화면 왼쪽 경계 계산
        leftLimit = cam.ViewportToWorldPoint(
            new Vector3(0, 0, distance)
        ).x + screenMargin;

        // 화면 오른쪽 경계 계산
        rightLimit = cam.ViewportToWorldPoint(
            new Vector3(1, 0, distance)
        ).x - screenMargin;
    }


    public override void Move()
    {
        // 부모 클래스의 아래 방향 이동 실행
        base.Move();

        // Mathf.Sin()으로 자연스러운 좌우 움직임 계산
        float x = startX + Mathf.Sin(
            Time.time * horizontalFrequency + randomOffset
        ) * horizontalRange;

        // 현재 적기의 위치 저장
        Vector3 pos = transform.position;

        // 좌우 이동 위치 적용
        pos.x = x;

        // 화면 밖으로 나가지 않도록 좌우 위치 제한
        pos.x = Mathf.Clamp(pos.x, leftLimit, rightLimit);

        // 최종 위치 적용
        transform.position = pos;
    }
}
