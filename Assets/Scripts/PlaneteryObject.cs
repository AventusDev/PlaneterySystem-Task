using UnityEngine;

public class PlaneteryObject : MonoBehaviour, IPlaneteryObject
{
    [SerializeField] private MassClassEnum massClass;
    public MassClassEnum MassClass { get => massClass; set => massClass = value; }
    [SerializeField] private double mass;
    public double Mass { get => mass; set => mass = value; }
    private float planeteryDistance;
    public float PlaneteryDistance { get => planeteryDistance; set => planeteryDistance = value; }
    private float radius;
    public float Radius { get => radius; set => radius = value; }
    private float speed;
    public float Speed { get => speed; set => speed = value; }
    [SerializeField] private GameObject _planet;
    private void Start()
    {
        InitializePlanet();
    }
    private void InitializePlanet()
    {
        SetPlanetPosition();
        SetPlanetScale();
    }
    private void SetPlanetPosition()
    {
        Vector3 planetPosition = new Vector3(transform.position.x, transform.position.y, planeteryDistance);
        _planet.transform.position = planetPosition;
    }

    private void SetPlanetScale()
    {
        _planet.transform.localScale = Vector3.one * radius * 2;
    }
    public void RotatePlanet(float deltaTime)
    {
        transform.Rotate(Vector3.up * speed * deltaTime);
    }
}