using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShoot : MonoBehaviour
{
    public float shootSpeed = 12.0f; // 화살속도
    public float shootDelay = 0.25f; // 발사간격
    public GameObject bowPrefab;
    public GameObject arrowPrefab;

    GameObject bowObj;

    bool inAttack = false;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 pos = transform.position; //플레이어 위치
        bowObj = Instantiate(bowPrefab, pos, Quaternion.identity); //생성위치는 플레이어 위치 아이덴티티는 회전이 없는 키워드
        bowObj.transform.SetParent(transform); // 활 프리팹을 플레이어의 피봇에 위치시키겠다.
    }

    // Update is called once per frame
    void Update()
    {
        float bowZ = -1.0f;
        PlayerController player = GetComponent<PlayerController>();
        if (player.angleZ > 30 && player.angleZ < 150)
        {
            // 캐릭터 위로 방향
            bowZ = 1;
        }

        // 활의 회전
        bowObj.transform.rotation = Quaternion.Euler(0,0,player.angleZ); //오일러라고 읽음
        bowObj.transform.position = new Vector3(transform.position.x, transform.position.y, bowZ); // 참고 : position, rotation 값은 x,y,z값을 통채로 넣어줘야 한다. (중요)

        if (Input.GetButtonDown("Fire1"))
        {
            Attack(); 
        }
    }
    public void Attack()
    {
        if (ItemKeeper.hasArrows > 0 && inAttack == false)
        {
            ItemKeeper.hasArrows -= 1; // 화살의 개수가 줄어든다.
            inAttack = true; // 키 누를 때 화살연사방지

            /*발사체 방향 = 플레이어가 바라보는 방향*/
            PlayerController player = GetComponent<PlayerController>();
            float angleZ = player.angleZ;

            /*화살 프리팹 생성*/
            GameObject arrowObj = Instantiate(arrowPrefab, transform.position, Quaternion.Euler(0, 0, angleZ)); 
            // 캐릭터가 바라보는 방향으로 프리팹 생성
            //

            /*각은 알고 있지만 방향값(x와 y)을 알고 싶을 땐, 코사인 사인*/
            float x = Mathf.Cos(angleZ * Mathf.Deg2Rad); // AngleZ에 대한 x값  r * (x/r)과 동일
            float y = Mathf.Sin(angleZ * Mathf.Deg2Rad); // Anglez에 대한 y값

            Vector3 vFly = new Vector3(x, y, 0) * shootSpeed;

            Rigidbody2D rbody = arrowObj.GetComponent<Rigidbody2D>();
            rbody.AddForce(vFly, ForceMode2D.Impulse); // vFly 방향값만큼 즉각으로 발사하겠다.

            Invoke("StopAttack", shootDelay);
        }
    }
    public void StopAttack()
    {
        /*다음 화살 발사를 허락하겠다.*/
        inAttack = false; // invoke의 shootDelay 시간 뒤에 화살이 발사 가능하다는 코드.
    }
}


