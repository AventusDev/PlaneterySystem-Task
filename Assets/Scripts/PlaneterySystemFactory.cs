using System.Collections.Generic;
using UnityEngine;
using MassClass = PlaneteryObjectMassClassSpecifications.MassClass;

public class PlaneterySystemFactory : MonoBehaviour, IPlaneterySystemFactory
{
    [SerializeField] PlaneterySystem _planeterySystem;
    [SerializeField] PlaneteryObject _planet;
    [SerializeField] PlaneteryObjectMassClassSpecifications planetMassSpecification;
    [SerializeField] private List<IPlaneteryObject> _planetObjects = new List<IPlaneteryObject>();
    [SerializeField][Range(0.00001f, 1000f)] private float totalSystemMass;
    [SerializeField] private float planetMassMultiplier = 2.2f;
    [SerializeField] private float planetDistanceMultiplier = 2.2f;

    void Start()
    {
        Create(totalSystemMass);
    }
    public IPlaneterySystem Create(double mass)
    {
        PlaneterySystem planetarySystem = Instantiate(_planeterySystem);
        CreatePlanets(mass, planetarySystem);

        return planetarySystem;
    }

    private void CreatePlanets(double mass, PlaneterySystem planeterySystem)
    {
        double planetSystemMass = 0;
        double maxPossiblePlanetMass = CalculateMaxPlanetMass(mass);

        while (planetSystemMass <= mass)
        {
            double planetMass = GenerateRandomPlanetMass(maxPossiblePlanetMass);
            MassClass massClass = planetMassSpecification.GenerateClassByMass(planetMass);

            if (planetSystemMass + planetMass <= mass)
            {
                CreatePlanet(planeterySystem, massClass, (float)planetMass);
                planetSystemMass += planetMass;
            }
            else
            {
                planetMass = mass - planetSystemMass;
                massClass = planetMassSpecification.GenerateClassByMass((float)planetMass);
                CreatePlanet(planeterySystem, massClass, (float)planetMass);
                break;
            }
        }
        planeterySystem.planeteryObjects = _planetObjects;
    }
    private void CreatePlanet(IPlaneterySystem planeterySystem, MassClass massClass, float mass)
    {
        IPlaneteryObject planetObject = Instantiate(_planet, planeterySystem.transform);
        InitializePlanets(planetObject, massClass, mass);
        _planetObjects.Add(planetObject);
    }
    private double GenerateRandomPlanetMass(double maxPlanetMass)
    {
        double minMass = planetMassSpecification.GetMinMass();
        return UnityEngine.Random.Range((float)minMass, (float)maxPlanetMass);
    }
    private double CalculateMaxPlanetMass(double totalMass)
    {
        double maxMass = planetMassSpecification.GetMaxMass();
        return (totalMass < maxMass) ? totalMass / planetMassMultiplier : maxMass;
    }

    private void InitializePlanets(IPlaneteryObject planetObject, MassClass massClass, float mass)
    {
        SetPlanetMass(planetObject, mass, massClass);
        SetPlanetRadius(planetObject, massClass, mass);
        SetPlanetDistance(planetObject);
    }

    private void SetPlanetMass(IPlaneteryObject planetObject, float mass, MassClass massClass)
    {
        planetObject.Mass = mass;
        planetObject.MassClass = massClass.massClassEnum;
    }

    private void SetPlanetRadius(IPlaneteryObject planetObject, MassClass massClass, float mass)
    {
        float massPercentage = CalculateMassPercentage(mass, massClass);
        float radius = CalculatePlanetRadius(massPercentage, massClass);
        planetObject.Radius = radius;
    }

    private float CalculateMassPercentage(float mass, MassClass massClass)
    {
        return (mass - massClass.massMin) / (massClass.massMax - massClass.massMin);
    }

    private float CalculatePlanetRadius(double massPercentage, MassClass massClass)
    {
        return Mathf.Lerp(massClass.radiusMin, massClass.radiusMax, (float)massPercentage) / 2;
    }

    private void SetPlanetDistance(IPlaneteryObject planetObject)
    {
        float distance = (_planetObjects.Count == 0)
            ? planetObject.Radius * planetDistanceMultiplier
            : CalculateDistanceWithPreviousPlanet(planetObject);

        planetObject.PlaneteryDistance = distance;
    }

    private float CalculateDistanceWithPreviousPlanet(IPlaneteryObject planetObject)
    {
        IPlaneteryObject prevPlanet = _planetObjects[_planetObjects.Count - 1];
        return prevPlanet.Radius + prevPlanet.PlaneteryDistance + planetObject.Radius * planetDistanceMultiplier;
    }
}