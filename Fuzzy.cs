using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AForge.Fuzzy;
using System;

public class Fuzzy : MonoBehaviour
{
    public Transform player;
    float distance, speed;

    FuzzySet fsNear, fsMed, fsFar;
    LinguisticVariable lvDistance;

    FuzzySet fsSlow, fsMedium, fsFast;
    LinguisticVariable lvSpeed;

    Database database;
    InferenceSystem infSystem;
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        SetDistanceFuzzySets();
        SetSpeedFuzzySets();
        AddToDatabase();
    }

    private void SetDistanceFuzzySets()
    {
        fsNear = new FuzzySet("Near", new TrapezoidalFunction(20, 40, TrapezoidalFunction.EdgeType.Right));
        fsMed = new FuzzySet("Med", new TrapezoidalFunction(20, 40, 50, 70));
        fsFar = new FuzzySet("Far", new TrapezoidalFunction(50, 70, TrapezoidalFunction.EdgeType.Left));

        lvDistance = new LinguisticVariable("Distance", 0, 100);

        lvDistance.AddLabel(fsNear);
        lvDistance.AddLabel(fsMed);
        lvDistance.AddLabel(fsFar);
    }
    private void SetSpeedFuzzySets()
    {
        fsSlow = new FuzzySet("Slow", new TrapezoidalFunction(30, 50, TrapezoidalFunction.EdgeType.Right));
        fsMedium = new FuzzySet("Medium", new TrapezoidalFunction(30, 50, 80, 100));
        fsFast = new FuzzySet("Fast", new TrapezoidalFunction(80, 100, TrapezoidalFunction.EdgeType.Left));

        lvSpeed = new LinguisticVariable("Speed", 0, 120);

        lvSpeed.AddLabel(fsSlow);
        lvSpeed.AddLabel(fsMedium);
        lvSpeed.AddLabel(fsFast);
    }
    private void AddToDatabase()
    {
        database = new Database();
        database.AddVariable(lvDistance);
        database.AddVariable(lvSpeed);

        infSystem = new InferenceSystem(database, new CentroidDefuzzifier(120));

        infSystem.NewRule("Rule 1", "IF Distance IS Near THEN Speed IS Slow");
        infSystem.NewRule("Rule 2", "IF Distance IS Med THEN Speed IS Medium");
        infSystem.NewRule("Rule 3", "IF Distance IS Far THEN Speed IS Fast");
    }
    // Update is called once per frame
    void Update()
    {
        Evaluate();
    }

    private void Evaluate()
    {
        Vector3 dir = (player.position - transform.position);
        distance = dir.magnitude;
        dir.Normalize();
        infSystem.SetInput("Distance", distance);
        speed = infSystem.Evaluate("Speed");

        transform.position += dir * speed * Time.deltaTime * 0.1f;
    }
}
