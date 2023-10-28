public interface IPlaneteryObject
{
    double Mass { get; set; }
    float PlaneteryDistance { get; set; }
    float Radius { get; set; }
    float Speed { get; set; }
    MassClassEnum MassClass { get; set; }
    public void RotatePlanet(float deltaTime);
}