using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New Planet Mass Spec.", menuName = "Planets/MassSpecification")]
public class PlaneteryObjectMassClassSpecifications : ScriptableObject
{
    [SerializeField] MassClass[] massClasses;

    [System.Serializable]
    public class MassClass
    {
        [SerializeField] public MassClassEnum massClassEnum;
        [SerializeField] public float massMin;
        [SerializeField] public float massMax;
        [SerializeField] public float radiusMin;
        [SerializeField] public float radiusMax;
    }
    public MassClass GenerateClassByMass(double mass)
    {
        return massClasses.FirstOrDefault(massClass => mass >= massClass.massMin && mass <= massClass.massMax);
    }

    public float GetMaxMass()
    {
        return massClasses.Max(massClass => massClass.massMax);
    }

    public float GetMinMass()
    {
        return massClasses.Min(massClass => massClass.massMin);
    }
}
