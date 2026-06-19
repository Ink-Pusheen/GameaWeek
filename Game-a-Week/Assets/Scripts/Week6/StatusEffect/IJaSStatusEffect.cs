using System;

[Serializable]
public abstract class IJaSStatusEffect
{
    public IJaSHealth target;


    public abstract void StatusEffect();

}
