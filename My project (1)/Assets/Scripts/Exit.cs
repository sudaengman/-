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
    public string sceneName = ""; // ������� Scene
    public int doorNumber = 0; // ������� �� ��ȣ
    public ExitDirection direction = ExitDirection.down;
    // Start is called before the first frame update

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            /*static doorNumber�� ������� �� ��ȣ�� �Է��ϰ�, ���� ���� Scene���� �̵��ϰڴ�.*/
            RoomManager.ChangeScene(sceneName, doorNumber); 
        }
    }
}
