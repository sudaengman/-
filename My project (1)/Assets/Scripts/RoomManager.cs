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
        /*Exit라는 태그가 달린 게임오브젝트를 모조리 찾는다.*/
        GameObject[] enters = GameObject.FindGameObjectsWithTag("Exit");
        for (int i = 0; i < enters.Length; i++)
        {
            /*Exit 달린 게임 오브젝트의 Exit 클래스 호출*/
            GameObject doorObj = enters[i];
            Exit exit = doorObj.GetComponent<Exit>();
            /**/
            if (doorNumber == exit.doorNumber)
            {
                /*Exit 태그, 도어넘버와 맞는 문의 위치값*/
                float x = doorObj.transform.position.x;
                float y = doorObj.transform.position.y;

                /*들어오는 문 방향설정을 up으로 할 경우*/
                if (exit.direction == ExitDirection.up)
                {
                    y += 1; // 해당 씬의 문 위치에서 y축으로 한 칸 위
                }
                /*들어오는 문 방향설정을 right으로 할 경우*/
                else if (exit.direction == ExitDirection.right)
                {
                    x += 1; // 해당 씬의 문 위치에서 x축으로 한 칸 오른쪽
                }
                /*들어오는 문 방향설정을 down으로 할 경우*/
                else if (exit.direction == ExitDirection.down)
                {
                    y -= 1; // 해당 씬의 문 위치에서 y축으로 한 칸 아래
                }
                /*들어오는 문 방향설정을 left으로 할 경우*/
                else if (exit.direction == ExitDirection.left)
                {
                    x -= 1; // 해당 씬의 문 위치에서 x축으로 한 칸 왼쪽
                }

                /*플레이어 위치값*/
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                player.transform.position = new Vector3(x, y);
                break;
            }
        }
    }

    /* static 변수인 doorNumber의 정보를 바꾸고, 씬을 바꾸는 함수 */
    public static void ChangeScene(string sceneName, int doornum)
    {
        doorNumber = doornum;
        SceneManager.LoadScene(sceneName);
    }
}
