using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShoot : MonoBehaviour
{
    public float shootSpeed = 12.0f; // ȭ��ӵ�
    public float shootDelay = 0.25f; // �߻簣��
    public GameObject bowPrefab;
    public GameObject arrowPrefab;

    GameObject bowObj;

    bool inAttack = false;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 pos = transform.position; //�÷��̾� ��ġ
        bowObj = Instantiate(bowPrefab, pos, Quaternion.identity); //������ġ�� �÷��̾� ��ġ ���̵�ƼƼ�� ȸ���� ���� Ű����
        bowObj.transform.SetParent(transform); // Ȱ �������� �÷��̾��� �Ǻ��� ��ġ��Ű�ڴ�.
    }

    // Update is called once per frame
    void Update()
    {
        float bowZ = -1.0f;
        PlayerController player = GetComponent<PlayerController>();
        if (player.angleZ > 30 && player.angleZ < 150)
        {
            // ĳ���� ���� ����
            bowZ = 1;
        }

        // Ȱ�� ȸ��
        bowObj.transform.rotation = Quaternion.Euler(0,0,player.angleZ); //���Ϸ���� ����
        bowObj.transform.position = new Vector3(transform.position.x, transform.position.y, bowZ); // ���� : position, rotation ���� x,y,z���� ��ä�� �־���� �Ѵ�. (�߿�)

        if (Input.GetButtonDown("Fire1"))
        {
            Attack(); 
        }
    }
    public void Attack()
    {
        if (ItemKeeper.hasArrows > 0 && inAttack == false)
        {
            ItemKeeper.hasArrows -= 1; // ȭ���� ������ �پ���.
            inAttack = true; // Ű ���� �� ȭ�쿬�����

            /*�߻�ü ���� = �÷��̾ �ٶ󺸴� ����*/
            PlayerController player = GetComponent<PlayerController>();
            float angleZ = player.angleZ;

            /*ȭ�� ������ ����*/
            GameObject arrowObj = Instantiate(arrowPrefab, transform.position, Quaternion.Euler(0, 0, angleZ)); 
            // ĳ���Ͱ� �ٶ󺸴� �������� ������ ����
            //

            /*���� �˰� ������ ���Ⱚ(x�� y)�� �˰� ���� ��, �ڻ��� ����*/
            float x = Mathf.Cos(angleZ * Mathf.Deg2Rad); // AngleZ�� ���� x��  r * (x/r)�� ����
            float y = Mathf.Sin(angleZ * Mathf.Deg2Rad); // Anglez�� ���� y��

            Vector3 vFly = new Vector3(x, y, 0) * shootSpeed;

            Rigidbody2D rbody = arrowObj.GetComponent<Rigidbody2D>();
            rbody.AddForce(vFly, ForceMode2D.Impulse); // vFly ���Ⱚ��ŭ �ﰢ���� �߻��ϰڴ�.

            Invoke("StopAttack", shootDelay);
        }
    }
    public void StopAttack()
    {
        /*���� ȭ�� �߻縦 ����ϰڴ�.*/
        inAttack = false; // invoke�� shootDelay �ð� �ڿ� ȭ���� �߻� �����ϴٴ� �ڵ�.
    }
}


