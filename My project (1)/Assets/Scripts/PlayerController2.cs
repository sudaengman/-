using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    /*속도값*/
    public float speed = 3.0f;
    
    /*좌우*/
    float axisH;
    float axisV;
    
    Rigidbody2D rbody;

    /*애니메이션 부*/
    string downAni = "PlayerDown";
    string upAni = "PlayerUp";
    string leftAni = "PlayerLeft";
    string rightAni = "PlayerRight";

    string oldAnimation;
    string nowAnimation;

    /*각도*/
    public float angleZ = -90f;

    /*움직이고 있는지*/
    bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        oldAnimation = downAni;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving == false)
        {
            axisH = Input.GetAxisRaw("Horizontal");
            axisV = Input.GetAxisRaw("Vertical");
        }

        Vector2 fromPt = transform.position;
        Vector2 toPt = new Vector2(fromPt.x + axisH, fromPt.y + axisV);
        angleZ = GetAngle(fromPt, toPt);


        if (oldAnimation != nowAnimation) 
        { 
            oldAnimation = nowAnimation;
            GetComponent<Animator>().Play(nowAnimation);
        }

        if (angleZ >= -45 && angleZ < 45)
        {
            nowAnimation = rightAni;
        }
        else if (angleZ >= 45 && angleZ < 135)
        {
            nowAnimation = upAni;
        }
        else if (angleZ >= -135 && angleZ < -45)
        {
            nowAnimation = downAni;
        }
        else
        {
            nowAnimation = leftAni;
        }
    }

    private void FixedUpdate()
    {
        rbody.velocity = new Vector2(axisH, axisV) * speed;
    }

    float GetAngle(Vector2 p1, Vector2 p2)
    { 
        float angle = angleZ;

        if (axisH !=0 || axisV !=0) 
        { 
            float dx = p2.x - p1.x;
            float dy = p2.y - p1.y;
            float rad = Mathf.Atan2(dy, dx);

            angle = rad * Mathf.Rad2Deg;
        }
        return angle;
    }
}
