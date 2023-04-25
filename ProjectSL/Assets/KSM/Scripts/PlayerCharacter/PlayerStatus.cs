using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus
{
    string _name;
    int _level;
    int _vigor;
    int _attunement;
    int _endurance;
    int _vitality;
    int _strength;
    int _dexterity;

    public PlayerStatus()
    {
        _name = string.Empty;
        _level = 1;
        _vigor = 1;
        _attunement = 1;
        _endurance = 1;
        _vitality = 1;
        _strength = 1;
        _dexterity = 1;
    }
    // property
    public string Name      { get { return _name; }         set { _name = value;  }         }
    public int Level        { get { return _level; }        set { _level = value; }         }
    public int Vigor        { get { return _vigor; }        set { _vigor = value; }         }
    public int Attunement   { get { return _attunement; }   set { _attunement = value; }    }
    public int Endurance    { get { return _endurance; }    set { _endurance = value; }     }
    public int Vitality     { get { return _vitality; }     set { _vitality = value; }      }
    public int Strength     { get { return _strength; }     set { _strength = value; }      }
    public int Dexterity    { get { return _dexterity; }    set { _dexterity = value; }     }
}
