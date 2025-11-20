using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class BallMove : MonoBehaviour
{
    Rigidbody rb;
    Vector3 moveVec = Vector3.zero;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        // 무작위 속도 적용 & 충돌 전 이동 속도값 저장
        moveVec = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized * 3;
        rb.velocity = moveVec;
        Debug.Log(rb.velocity);
    }

    public BallData GetBallData()
    {
        return new BallData(transform.position, rb.velocity);
    }
    public void SetBallData(BallData data)
    {
        transform.position = data.pos;
        moveVec = data.vel;
        rb.velocity = moveVec;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 collisionWallNormal = collision.contacts[0].normal;
        Vector3 moveSource = moveVec;
        Debug.Log(rb.velocity);
        float len = Vector3.Dot(collisionWallNormal, moveSource);
        Vector3 reflectVec = moveSource - (len * 2 * collisionWallNormal);

        // 계산 후 결과 적용
        moveVec = reflectVec.normalized * 3;
        rb.velocity = moveVec;
    }
}
