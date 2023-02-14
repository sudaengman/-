using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public float deleteTime = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, deleteTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*화살이 날라갔는데 닿은 적에게 화살이 꽂힌 채 남아있는 연출*/
        transform.SetParent(collision.transform);

        /*충돌하면 어떤 물리엔진도 적용시키지 않겠다.*/
        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().simulated = false;
    }
}
