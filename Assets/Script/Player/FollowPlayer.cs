using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    void LateUpdate()
    {
        if (GameManager.instance.start_game)
        {
            var player_pos = PlayerManager.instance.player_move.transform.position;
            transform.position = new Vector3(player_pos.x, player_pos.y - 1.2f, transform.position.z);
        }
    }
}
