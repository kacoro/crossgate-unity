using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "MovePet",menuName = "Pet/Creat New Move")]
public class MoveBase : ScriptableObject
{
    [SerializeField] string name;

    [TextArea]
    [SerializeField] string description;

    [SerializeField] PetType type;
    [SerializeField] int power;
    [SerializeField] int accuracy; //准确性
    [SerializeField] int pp;

    public string Name{
        get { return name;}
    }
     public string Description{
        get { return description;}
    }
    public PetType Type{
        get { return type;}
    }
     public int Power{
        get { return power;}
    }
     public int Accuracy{
        get { return accuracy;}
    }
     public int PP{
        get { return pp;}
    }

    public bool IsSpecial{
        get{
            if(type == PetType.Fire || type == PetType.Water || type == PetType.Grass || type == PetType.Ice
                || type == PetType.Electric || type == PetType.Dragon)
                {return true;}else{
                    return false;
                }
        }
    }
}
