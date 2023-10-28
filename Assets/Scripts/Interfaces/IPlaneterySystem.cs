using System.Collections.Generic;
using UnityEngine;

public interface IPlaneterySystem
{
    IEnumerable<IPlaneteryObject> planeteryObjects { get; set;}
    void UpdateSystem(float deltaTime);
    public Transform transform { get; }
}
