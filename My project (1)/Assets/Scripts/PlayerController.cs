using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /*이속*/
    public float speed = 3.0f;

    /*애니메이션 이름(만든 것)*/

    public string upAni = "PlayerUp";
    public string downAni = "PlayerDown";
    public string leftAni = "PlayerLeft";
    public string rightAni = "PlayerRight";
    public string deadAni = "PlayerDead";

    /* 애니메이션 상태 */
    string nowAnimation = "";
    string oldAnimation = "";

    /* 캐릭터 방향 */
    float axisH; // (x= -1 or 0 or 1)
    float axisV; // (y= -1 or 0 or 1)
    public float angleZ = -90.0f;

    Rigidbody2D rBody;
    bool isMoving = false; // 이동중인지 체크

    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        nowAnimation = downAni; // 기본 애니메이션
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving == false)
        {
            axisH = Input.GetAxisRaw("Horizontal");
            axisV = Input.GetAxisRaw("Vertical");
        }


        /*플레이어의 실시간 각도값*/
        Vector2 fromPt = this.transform.position; // 플레이어의 실시간 현재위치
        Vector2 toPt = new Vector2(fromPt.x + axisH, fromPt.y + axisV); //플레이어의 실시간 바뀐위치
        Debug.Log($"X: {toPt.x} Y: {toPt.y}"); // 디버깅
        angleZ = GetAngle(fromPt, toPt); // 현위치와 바뀐 위치의 각도값 만들어주기
        


        /*플레이어 애니메이션 실시간 작동로직
        *             --  upAni  --
        *             
        *       |     135   90   45     | 
        *   LeftAni   180  중심   0  RightAni
        *       |    -135  -90  -45     | 
        *       
        *             -- downAni --
        */

        if (angleZ >= -45 && angleZ < 45)
        {
            nowAnimation = rightAni;
        }
        else if (angleZ >= 45 && angleZ <= 135)
        {
            nowAnimation = upAni;
        }
        else if (angleZ >= -135 && angleZ <= 45)
        {
            nowAnimation = downAni;
        }
        else 
        {
            nowAnimation = leftAni;
        }

        if (nowAnimation != oldAnimation) // 올드가 입력값 다르다면
        { 
            oldAnimation = nowAnimation; // 올드에 나우값을 넣고
            GetComponent<Animator>().Play(nowAnimation);
        }

        //rBody.velocity = new Vector2(axisH, axisV) * speed;
    }

    private void FixedUpdate()
    {
        rBody.velocity = new Vector2(axisH, axisV) * speed;
    }

    float GetAngle(Vector2 p1, Vector2 p2)
    {
        float angle = angleZ; //이전각도(angleZ)
        if (axisH != 0 || axisV != 0) // 방향키가 둘 중 하나 상하 혹은 좌우 입력이 되어 있을 때,
        {
            float dx = p2.x - p1.x; //벡터의 x값 차이
            float dy = p2.y - p1.y; //벡터의 y값 차이

            float rad = Mathf.Atan2(dy, dx); // rad = dy/dx(탄젠트값)  => 앵글을 구하는 함수
            angle = rad * Mathf.Rad2Deg;
        }

        return angle;
    }
}
