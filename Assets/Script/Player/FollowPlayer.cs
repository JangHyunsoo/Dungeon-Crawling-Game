using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // Update is called once per frame
    void LateUpdate()
    {
        if (GameManager.instance.start_game)
        {
            var player_pos = PlayerMove.instance.gameObject.transform.position;
            transform.position = new Vector3(player_pos.x, player_pos.y - 2, transform.position.z);
        }
    }
}
