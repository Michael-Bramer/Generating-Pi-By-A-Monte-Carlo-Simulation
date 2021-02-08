using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class SimulationManager : MonoBehaviour
{
    //Structure Declaration to Store Runtime Data
    [System.Serializable] public struct RuntimeData
    {
        public float   TotalCount;
        public float   InteriorCount;
        public double  GeneratedX;
        public double  GeneratedY;
        public double  EstimatedPi;
    }

    //Public Simulation Information Display Variables
    public Text TotalNumberOfPoints;
    public Text NumberOfInteriorPoints;
    public Text EstimationOfPi;
    public  int UpdateInterval = 5;

    //Public Simulation Control Variables
    public static bool SimulationPaused = false;
    public static bool SimulationReset  = false;
    public static bool SimulationSave   = false;

    //Public Simulation Input Parameter Variables
    public float Radius = 5.0f;

    //Private Simulation Trace Variables
    private List<RuntimeData> SimulationTrace = new List<RuntimeData>();

    //Private Simulation Calculation Variables
    private float    Total_N    = 0;
    private float    Interior_N = 0;
    private float    Estimate   = 0;

    // Start is called before the first frame update
    void Start()
    {
        SimulationPaused = true;
    }

    // Update is called once per frame
    void Update()
    {
        if( SimulationPaused )
        {
            if( SimulationSave)
            {
                SimulationSave = false;
            }
            if( SimulationReset)
            {
                //Reset Numbers
                Total_N    = 0;
                Interior_N = 0;
                Estimate   = 0;

                //Reset the Trace Log
                SimulationTrace.Clear();

                //Set the Description Labels
                TotalNumberOfPoints.text = Total_N.ToString();
                NumberOfInteriorPoints.text = Interior_N.ToString();
                EstimationOfPi.text = Estimate.ToString();

                SimulationReset = false;
            }     
        }
        else{
            
            //Generate the Uniform Random Number
            for(int i = 0; i < UpdateInterval; i++)
            {
                double RandomX = UnityEngine.Random.Range(0.0f, Radius);
                double RandomY = UnityEngine.Random.Range(0.0f, Radius);

                //Calculate the Result
                if (Math.Sqrt((RandomX * RandomX) + (RandomY * RandomY)) <= Radius)
                {
                    Interior_N += 1;
                }
                Total_N += 1;
           
            
            Estimate = 4*((float)Interior_N / Total_N);

            //Generate the Interval Record for the Runtime Trace
            RuntimeData TraceLineItem;
            TraceLineItem.GeneratedX    = RandomX;
            TraceLineItem.GeneratedY    = RandomY;
            TraceLineItem.EstimatedPi   = Estimate;
            TraceLineItem.InteriorCount = Interior_N;
            TraceLineItem.TotalCount    = Total_N;
            SimulationTrace.Add(TraceLineItem);
            }

            //Set the Description Labels
            TotalNumberOfPoints.text    = Total_N.ToString();
            NumberOfInteriorPoints.text = Interior_N.ToString();
            EstimationOfPi.text = Estimate.ToString();
        }
    }
    public void PlaySimulation()
    {
        SimulationPaused = false;
    }
    public void PauseSimulation()
    {
        SimulationPaused = true;
    }
    public void ResetSimulation()
    {
        SimulationReset = true;
    }
    public void SaveSimulation()
    {
        SimulationSave = true;
    }
}
