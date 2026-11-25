using UnityEngine;

class Knight: ISword,  IShield
{
    public string Damage { get; set; }
    public string DefensivePower { get; set; }
    public void Attack() 
    { 
    }
    public void Defend() 
    { 
    }
}

interface ISword{
    public string Damage { get; set; }
    public void Attack();
}
interface IShield{
    public string DefensivePower { get; set; }
    public void Defend();
}
   