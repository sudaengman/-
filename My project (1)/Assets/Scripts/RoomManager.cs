using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    public static int doorNumber = 0;
    // Start is called before the first frame update
    void Start()
    {
        /*Exit��� �±װ� �޸� ���ӿ�����Ʈ�� ������ ã�´�.*/
        GameObject[] enters = GameObject.FindGameObjectsWithTag("Exit");
        for (int i = 0; i < enters.Length; i++)
        {
            /*Exit �޸� ���� ������Ʈ�� Exit Ŭ���� ȣ��*/
            GameObject doorObj = enters[i];
            Exit exit = doorObj.GetComponent<Exit>();
            /**/
            if (doorNumber == exit.doorNumber)
            {
                /*Exit �±�, ����ѹ��� �´� ���� ��ġ��*/
                float x = doorObj.transform.position.x;
                float y = doorObj.transform.position.y;

                /*������ �� ���⼳���� up���� �� ���*/
                if (exit.direction == ExitDirection.up)
                {
                    y += 1; // �ش� ���� �� ��ġ���� y������ �� ĭ ��
                }
                /*������ �� ���⼳���� right���� �� ���*/
                else if (exit.direction == ExitDirection.right)
                {
                    x += 1; // �ش� ���� �� ��ġ���� x������ �� ĭ ������
                }
                /*������ �� ���⼳���� down���� �� ���*/
                else if (exit.direction == ExitDirection.down)
                {
                    y -= 1; // �ش� ���� �� ��ġ���� y������ �� ĭ �Ʒ�
                }
                /*������ �� ���⼳���� left���� �� ���*/
                else if (exit.direction == ExitDirection.left)
                {
                    x -= 1; // �ش� ���� �� ��ġ���� x������ �� ĭ ����
                }

                /*�÷��̾� ��ġ��*/
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                player.transform.position = new Vector3(x, y);
                break;
            }
        }
    }

    /* static ������ doorNumber�� ������ �ٲٰ�, ���� �ٲٴ� �Լ� */
    public static void ChangeScene(string sceneName, int doornum)
    {
        doorNumber = doornum;
        SceneManager.LoadScene(sceneName);
    }
}
