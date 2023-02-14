using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*카메라가 플레이어의 실시간 위치값을 따라가겠다*/
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            return; // 플레이어가 없으면 작동하지 않겠다.
        }

        transform.position = new Vector3(player.transform.position.x,player.transform.position.y, -10);
        
    }
}
