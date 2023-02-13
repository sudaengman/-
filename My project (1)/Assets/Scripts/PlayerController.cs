using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /*�̼�*/
    public float speed = 3.0f;

    /*�ִϸ��̼� �̸�(���� ��)*/

    public string upAni = "PlayerUp";
    public string downAni = "PlayerDown";
    public string leftAni = "PlayerLeft";
    public string rightAni = "PlayerRight";
    public string deadAni = "PlayerDead";

    /* �ִϸ��̼� ���� */
    string nowAnimation = "";
    string oldAnimation = "";

    /* ĳ���� ���� */
    float axisH; // (x= -1 or 0 or 1)
    float axisV; // (y= -1 or 0 or 1)
    public float angleZ = -90.0f;

    Rigidbody2D rBody;
    bool isMoving = false; // �̵������� üũ

    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        nowAnimation = downAni; // �⺻ �ִϸ��̼�
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving == false)
        {
            axisH = Input.GetAxisRaw("Horizontal");
            axisV = Input.GetAxisRaw("Vertical");
        }


        /*�÷��̾��� �ǽð� ������*/
        Vector2 fromPt = this.transform.position; // �÷��̾��� �ǽð� ������ġ
        Vector2 toPt = new Vector2(fromPt.x + axisH, fromPt.y + axisV); //�÷��̾��� �ǽð� �ٲ���ġ
        Debug.Log($"X: {toPt.x} Y: {toPt.y}"); // �����
        angleZ = GetAngle(fromPt, toPt); // ����ġ�� �ٲ� ��ġ�� ������ ������ֱ�
        


        /*�÷��̾� �ִϸ��̼� �ǽð� �۵�����
        *             --  upAni  --
        *             
        *       |     135   90   45     | 
        *   LeftAni   180  �߽�   0  RightAni
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

        if (nowAnimation != oldAnimation) // �õ尡 �Է°� �ٸ��ٸ�
        { 
            oldAnimation = nowAnimation; // �õ忡 ���찪�� �ְ�
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
        float angle = angleZ; //��������(angleZ)
        if (axisH != 0 || axisV != 0) // ����Ű�� �� �� �ϳ� ���� Ȥ�� �¿� �Է��� �Ǿ� ���� ��,
        {
            float dx = p2.x - p1.x; //������ x�� ����
            float dy = p2.y - p1.y; //������ y�� ����

            float rad = Mathf.Atan2(dy, dx); // rad = dy/dx(ź��Ʈ��)  => �ޱ��� ���ϴ� �Լ�
            angle = rad * Mathf.Rad2Deg;
        }

        return angle;
    }
}
