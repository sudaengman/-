using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ExitDirection
{ 
    right,
    left,
    down,
    up,
}

public class Exit : MonoBehaviour
{
    public string sceneName = ""; // 가고싶은 Scene
    public int doorNumber = 0; // 가고싶은 룸 번호
    public ExitDirection direction = ExitDirection.down;
    // Start is called before the first frame update

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            /*static doorNumber에 가고싶은 룸 번호를 입력하고, 가고 싶은 Scene으로 이동하겠다.*/
            RoomManager.ChangeScene(sceneName, doorNumber); 
        }
    }
}
