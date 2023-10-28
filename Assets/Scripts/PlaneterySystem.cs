using System.Collections.Generic;
using UnityEngine;

public class PlaneterySystem : MonoBehaviour, IPlaneterySystem
{
    public List<IPlaneteryObject> planeteryObjectsList = new List<IPlaneteryObject>();
    public IEnumerable<IPlaneteryObject> planeteryObjects
    {
        get => planeteryObjectsList;
        set => planeteryObjectsList = new List<IPlaneteryObject>(value);
    }
    void Update()
    {
        UpdateSystem(Time.deltaTime);
    }
    public void UpdateSystem(float deltaTime)
    {
        planeteryObjectsList.ForEach(planetaryObject => planetaryObject.RotatePlanet(deltaTime));
    }
}