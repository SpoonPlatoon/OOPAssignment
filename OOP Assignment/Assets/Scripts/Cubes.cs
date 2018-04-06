using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cubes : MonoBehaviour
{

    public float time = 0.0f; 
    public float interpolationPeriod = 0.75f; //creates a time in which the loop happens every 0.75 of the time

    public GameObject[] Boxes; //A game object array with the name boxes
    Color[] colours = new Color[20]; //colour array to cover all 20 boxes
    public int[] height = new int[20]; //height array to cover all 20 boxes

    int[] width = new int[10];


    void Start()                  
    {

    }

    // Update is called once per frame
    void Update() 
    {

        time += Time.deltaTime;

        if (time >= interpolationPeriod)
        {
            time = time - interpolationPeriod;

            for (int i = 0; i < height.Length; i++) //For loop to gather a random height(length) for each individual box
            {
                height[i] = Random.Range(1, 10);
            }

            for (int i = 0; i < colours.Length; i++) //For loop to gather a random colour for each individual box
            {
                colours[i] = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            }

            for (int i = 0; i < Boxes.Length; i++) //For loop to gather a box height position and colour for each individual box
            {
                Boxes[i].transform.localScale = new Vector3(1, height[i], 1);
                Boxes[i].GetComponent<Renderer>().material.color = colours[i];
            }

        }
    }
}
