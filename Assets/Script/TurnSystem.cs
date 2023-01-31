using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : Singleton<TurnSystem>
{
    private List<Enemy> actable_enemy_list_ = new List<Enemy>();

    public void runTurn(float _action_value)
    {
        Queue<Enemy> action_queue = new Queue<Enemy>();

        foreach (var cur_enemy in actable_enemy_list_)
        {
            cur_enemy.increaseCurActionValue(_action_value);

            if (cur_enemy.action_value <= cur_enemy.cur_action_value)
            {
                action_queue.Enqueue(cur_enemy);
            }
        }

        while (action_queue.Count != 0)
        {
            var cur_enemy = action_queue.Dequeue();
            
            cur_enemy.enemy_ai.act();

            if (cur_enemy.action_value < cur_enemy.cur_action_value)
            {
                action_queue.Enqueue(cur_enemy);
            }
        }
    }

    public void addActableEnemy(Enemy _enemy)
    {
        for (int i = 0; i < actable_enemy_list_.Count; i++)
        {
            if (_enemy.action_value < actable_enemy_list_[i].action_value)
            {
                actable_enemy_list_.Insert(i, _enemy);
                return;
            }
        }

        actable_enemy_list_.Add(_enemy);
    }
}
