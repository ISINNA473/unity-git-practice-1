//using UnityEngine;

//public class enemy : MonoBehaviour
//{

//    float health = 50.0f;
//    public float speed;
//    // Start is called once before the first execution of Update after the MonoBehaviour is created
//    private void TakeDamge(int value)
//    {
//        health -= value;
//        if (health <= 0)
//        {
//            Die();
//        }

//        Debug.Log("Enemy Health:" + health);

//        if (health == 0)
//            Debug.Log("적이 폭파하였습니다");
//    }

//    public float GetHealth()
//    {
//        return health;
//    }

//    void Die()
//    {
//        Destroy(this.gameObject);
//    }

//    public void OnCollisionEnter2D(Collision2D coll)
//    {
//        if (coll.gameObject.CompareTag("Bullet"))
//        {
//            TakeDamge(10);
//            coll.gameObject.SetActive(false);
//        }


//    }
//    public virtual void Move()
//    {

//    }

//    void Start()
//    {

//    }

//    // Update is called once per frame
//    void Update()
//    {


//    }
//}


using UnityEngine;

public class enemy : MonoBehaviour
{
    float health = 50.0f;

    public float speed = 5.0f;

    private void TakeDamge(int value)
    {
        health -= value;

        if (health <= 0)
        {
            Die();
        }

        Debug.Log("Enemy Health:" + health);

        if (health == 0)
            Debug.Log("적이 폭파하였습니다");
    }

    public float GetHealth()
    {
        return health;
    }

    void Die()
    {
        Destroy(this.gameObject);
    }

    public void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Bullet"))
        {
            TakeDamge(10);
            coll.gameObject.SetActive(false);
        }
    }

    public virtual void Move()
    {
        // 아래 방향으로 일정한 속도로 이동
        transform.Translate(
            Vector3.down * speed * Time.deltaTime,
            Space.World
        );
    }

    void Update()
    {
        // 자식 클래스의 Move() 실행
        Move();

        // 카메라 아래쪽 경계 확인
        Camera cam = Camera.main;

        if (cam != null)
        {
            float distance = -cam.transform.position.z;

            float bottomY = cam.ViewportToWorldPoint(
                new Vector3(0, 0, distance)
            ).y;

            // 적기가 화면 아래로 완전히 내려가면 삭제
            if (transform.position.y < bottomY - 1.0f)
            {
                Destroy(gameObject);
            }
        }
    }
}
