using System;

[Serializable]
public abstract class IJaSStatusEffect
{
    public IJaSHealth target;

    public string statusType;

    public abstract void SetupStatus();

    public abstract void StatusEffect();
}
