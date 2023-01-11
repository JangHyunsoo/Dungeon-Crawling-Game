using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerableAptitude
{
    private Dictionary<AptitudeType, int> base_aptitude_value_dic_ = new Dictionary<AptitudeType, int>();

    private Dictionary<AptitudeType, LevelData> aptitude_level_dic_ = new Dictionary<AptitudeType, LevelData>();
    public Dictionary<AptitudeType, LevelData> aptitude_level_dic { get => aptitude_level_dic_; }

    public void init()
    {
        foreach (var aptitude_pair in PlayerManager.instance.playerable.playerable_data.base_aptitude_value_arr)
        {
            base_aptitude_value_dic_.Add(aptitude_pair.aptitude_type, aptitude_pair.value);
            aptitude_level_dic_.Add(aptitude_pair.aptitude_type, new LevelData { cur_level = 0, cur_experience = 0, max_experience = 10 });
        }
    }

    public void increaseExp(AptitudeType _aptitude_type, float _value)
    {
        var aptitude = aptitude_level_dic[_aptitude_type];

        aptitude.setCurrentExp(_value + aptitude.cur_level);

        if(aptitude.cur_level >= aptitude.max_experience)
        {
            aptitude.increaseLevel();
            aptitude.setCurrentExp(0f);
            aptitude.setMaxExp(getRequirementExp(_aptitude_type));

            // exception for 'if max level' 
        }

    }

    // consider remove because not use
    private float getRequirementExp(AptitudeType _aptitude_type)
    {
        int cur_level = aptitude_level_dic_[_aptitude_type].cur_level;

        return (cur_level == 0) ? 10 : (5 + cur_level) * cur_level * cur_level + 10;
    }
}

public struct LevelData
{
    public int cur_level;
    public float cur_experience;
    public float max_experience;

    public void setCurrentExp(float _value) { cur_experience += _value; }
    public void setMaxExp(float _value) { max_experience = _value; }
    public void increaseLevel() { cur_level++; }
}