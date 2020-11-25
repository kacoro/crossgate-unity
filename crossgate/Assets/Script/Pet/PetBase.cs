using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pet",menuName = "Pet/Create New Pet")]
public class PetBase : ScriptableObject
{
    [SerializeField] string name;
   
    [TextArea]
    [SerializeField] string description;

    [SerializeField] Sprite sprite;
    
    [SerializeField] PetType type;


    //Base Stats
    [SerializeField] int maxHp;
    [SerializeField] int attack;
    [SerializeField] int defense;
    [SerializeField] int spAttack;
    [SerializeField] int spDefense;
    [SerializeField] int speed;


    [SerializeField] List<LearnableMove> learnableMoves;
    // public string GetName(){
    //     return name;
    // }

    public string Name{ // property
        get { return name;}
    }

    public string Description {
        get { return description;}
    }

    public Sprite Sprite {
        get { return sprite;}
    }

    public PetType Type {
        get { return type;}
    }

    public int MaxHp {
        get { return maxHp;}
    }

    public int Attack {
        get { return attack;}
    }

    public int Defense {
        get { return defense;}
    }
    public int SpAttack {
        get { return spAttack;}
    }
    public int SpDefense {
        get { return spDefense;}
    }
    public int Speed {
        get { return speed;}
    }
    public List<LearnableMove> LearnableMoves{
        get { return learnableMoves;}
    }
}

// 角色 Character 
// 力量 Strength 
// 耐力 Stamina/Patience 
// 智力 Intellect 
// 免疫力 Immune/Immunity 
// 生命力 Hit Point(HP) 
// 魔法力 Magic Point(MP) 
// 命中率 Accuracy 
// 速度 speed 
// 防御 Defense 
// 敏锐 Subtlety 
// 伤害 Damage 
// 护甲 Armor 
// 能量 power 
// 危险 danger 
// 安全 safety 
// 经验点数 Experience Point 
// 升级 Level Up

[System.Serializable]
public class LearnableMove{
    [SerializeField] MoveBase moveBase;
    [SerializeField] int level;

    public MoveBase Base{
        get {return moveBase;}
    }

    public int Level{
        get {return level;}
    }
}

// public enum PetType{ //人形系、野兽系、植物系、不死系、昆虫系、特殊系、金属系、龙系和飞行系
//     None,Normal,Fire,Water,Humanoid, Beast, Plant, Undead, Insect, Special, Metal, Dragon,Flying
// }

public enum PetType{ //宠物小精灵的类型
    None,Normal,Fire,Water,Electric, Grass, Ice,Fighting,Posison,Ground, Flying, Psychic, Bug, Rock, Ghost,Dragon
}

public enum Stat{
    Attack,
    Defense,
    SpAttack,
    SpDefense,
    Speed
}

public class TypeChart{ //属性克制表
    static float[][] chart = {

        //                  NOR   FIR    WAT    ELE    GRA    ICE    FIG    POI
        /*NOR*/ new float[]{1f,   1f,    1f,    1f,    1f,    1f,    1f,    1f   },
        /*FIR*/ new float[]{1f,   0.5f,  0.5f,  1f,    2f,    2f,    1f,    1f   },
        /*WAT*/ new float[]{1f,   2f,    0.5f,  2f,    0.5f,  1f,    1f,    1f   },
        /*ELE*/ new float[]{1f,   1f,    2f,    0.5f,  0.5f,  2f,    1f,    1f   },
        /*GRA*/ new float[]{1f,   0.5f,  2f,    2f,    0.5f,  1f,    1f,    0.5f },
        /*POI*/ new float[]{1f,   1f,    1f,    1f,    2f,    1f,    1f,    1f   },
      
    };
    public static float GetEffectiveness(PetType attackType,PetType defenseType){
        if(attackType == PetType.None || defenseType == PetType.None){
            return 1;
        }

        int row = (int)attackType -1;
        int col = (int)defenseType -1;
        
        return chart[row][col];
    }
}