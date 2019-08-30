using UnityEngine;

// Hitable is the base class for characters. It defines how they react against damage
// By putting it under one base class, we can easily request the hitable component from 
// the script and get the children scripts from it.
public abstract class Hitable : MonoBehaviour
{
    public abstract void OnHit(Bullet bullet);
}