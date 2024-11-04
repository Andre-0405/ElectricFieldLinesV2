using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using TMPro;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using System;
using System.Security.Permissions;
using System.Collections.Specialized;

public class Line_Generator : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdown;
    [SerializeField] private UnityEngine.UI.Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("GenerateScene", 0f, 1f);
        const bool makeNewChargesForDropdownChange = true;
        const bool makeNewChargesForSliderChange = false;
        int number_of_charges = 0;
        int[] charge_values_arr = {};
        Vector3[] charge_position_arr = {};

        GenerateScene(true);
        dropdown.onValueChanged.AddListener(delegate { GenerateScene(makeNewChargesForDropdownChange); });
        slider.onValueChanged.AddListener(delegate { GenerateScene(makeNewChargesForSliderChange); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateScene(bool makeNewCharges)
    {
        // destroy objects before creating new ones
        if (GameObject.FindGameObjectsWithTag("Destroyable") != null)
        {
            GameObject[] gos = GameObject.FindGameObjectsWithTag("Destroyable");
            foreach (GameObject go in gos)
                Destroy(go);
        }

        // here we can add an if/else statement to switch between set dropdown choices versus user modified values
        /*
        if(makeNewCharges == true)
        {
            (int number_of_charges, int[] charge_values_arr, Vector3[] charge_position_arr) = CreateChargesFromDropdown(dropdown.value);
        }
        */

        (int number_of_charges, int[] charge_values_arr, Vector3[] charge_position_arr) = CreateChargesFromDropdown(dropdown.value);
      

        GameObject[] sphere_arr = new GameObject[number_of_charges];
        for (int p = 0; p < number_of_charges; p++)
        {
            sphere_arr[p] = CreateSphere(sphere_arr[p], charge_values_arr[p], charge_position_arr[p], $"{p + 1}");
        }
        for (int l = 0; l < number_of_charges; l++)
        {
            GenerateFieldLines(charge_position_arr[l], charge_values_arr[l], l + 1, charge_values_arr, charge_position_arr);
        }
    }

    (int, int[], Vector3[]) CreateChargesFromDropdown(int dropdownValue)
    {
        int number_of_charges = 0;
        int[] charge_values_arr = {};
        Vector3[] charge_position_arr = {};
        
        if (dropdownValue == 0)
        {
            number_of_charges = 1;  // TEMPORARY HARDCODED INPUTS 
            charge_values_arr = new int[number_of_charges];
            charge_values_arr[0] = 1;
            charge_position_arr = new Vector3[number_of_charges];
            charge_position_arr[0] = new Vector3(0, 0, 0);
        }
        else if (dropdownValue == 1)
        {
            number_of_charges = 2;  // TEMPORARY HARDCODED INPUTS 
            charge_values_arr = new int[number_of_charges];
            charge_values_arr[0] = 1;
            charge_values_arr[1] = 1;
            charge_position_arr = new Vector3[number_of_charges];
            charge_position_arr[0] = new Vector3(-3, 0, 0);
            charge_position_arr[1] = new Vector3(3, 0, 0);
        }
        else if (dropdownValue == 2)
        {
            number_of_charges = 2;  // TEMPORARY HARDCODED INPUTS 
            charge_values_arr = new int[number_of_charges];
            charge_values_arr[0] = 1;
            charge_values_arr[1] = -1;
            charge_position_arr = new Vector3[number_of_charges];
            charge_position_arr[0] = new Vector3(-3, 0, 0);
            charge_position_arr[1] = new Vector3(3, 0, 0);
        }
        else if (dropdownValue == 3)
        {
            number_of_charges = 3;  // TEMPORARY HARDCODED INPUTS 
            charge_values_arr = new int[number_of_charges];
            charge_values_arr[0] = 1;
            charge_values_arr[1] = 1;
            charge_values_arr[2] = 1;
            charge_position_arr = new Vector3[number_of_charges];
            charge_position_arr[0] = new Vector3(-3, 0, 0);
            charge_position_arr[1] = new Vector3(3, 0, 0);
            charge_position_arr[2] = new Vector3(0, 0, 3 * Mathf.Sqrt(3));
        }
        else if (dropdownValue == 4)
        {
            number_of_charges = 3;  // TEMPORARY HARDCODED INPUTS 
            charge_values_arr = new int[number_of_charges];
            charge_values_arr[0] = 1;
            charge_values_arr[1] = 1;
            charge_values_arr[2] = -1;
            charge_position_arr = new Vector3[number_of_charges];
            charge_position_arr[0] = new Vector3(-3, 0, 0);
            charge_position_arr[1] = new Vector3(3, 0, 0);
            charge_position_arr[2] = new Vector3(0, 0, 3 * Mathf.Sqrt(3));

        }
        else if (dropdownValue == 5)
        {
            number_of_charges = 6;  // TEMPORARY HARDCODED INPUTS 
            charge_values_arr = new int[number_of_charges];
            charge_values_arr[0] = 1;
            charge_values_arr[1] = 1;
            charge_values_arr[2] = 1;
            charge_values_arr[3] = 1;
            charge_values_arr[4] = 1;
            charge_values_arr[5] = 1;
            charge_position_arr = new Vector3[number_of_charges];
            charge_position_arr[0] = new Vector3(-3, 0, 0);
            charge_position_arr[1] = new Vector3(3, 0, 0);
            charge_position_arr[2] = new Vector3(1.5f, 0, 2.598f);
            charge_position_arr[3] = new Vector3(-1.5f, 0, 2.598f);
            charge_position_arr[4] = new Vector3(-1.5f, 0, -2.598f);
            charge_position_arr[5] = new Vector3(1.5f, 0, -2.598f);
        }
        else if (dropdownValue == 6)
        {
            number_of_charges = 4;  // TEMPORARY HARDCODED INPUTS 
            charge_values_arr = new int[number_of_charges];
            charge_values_arr[0] = 1;
            charge_values_arr[1] = 1;
            charge_values_arr[2] = -1;
            charge_values_arr[3] = -1;
            charge_position_arr = new Vector3[number_of_charges];
            charge_position_arr[0] = new Vector3(1.5f, 0, 1.5f);
            charge_position_arr[1] = new Vector3(-1.5f, 0, -1.5f);
            charge_position_arr[2] = new Vector3(1.5f, 0, -1.5f);
            charge_position_arr[3] = new Vector3(-1.5f, 0, 1.5f);

        }
        else if (dropdownValue == 7)
        {
            number_of_charges = 8;  // TEMPORARY HARDCODED INPUTS 
            charge_values_arr = new int[number_of_charges];
            charge_values_arr[0] = 1;
            charge_values_arr[1] = -1;
            charge_values_arr[2] = -1;
            charge_values_arr[3] = -1;
            charge_values_arr[4] = -1;
            charge_values_arr[5] = 1;
            charge_values_arr[6] = 1;
            charge_values_arr[7] = 1;
            charge_position_arr = new Vector3[number_of_charges];
            charge_position_arr[0] = new Vector3(1.5f, 1.5f, 1.5f);
            charge_position_arr[1] = new Vector3(-1.5f, -1.5f, -1.5f);
            charge_position_arr[2] = new Vector3(1.5f, 1.5f, -1.5f);
            charge_position_arr[3] = new Vector3(-1.5f, 1.5f, 1.5f);
            charge_position_arr[4] = new Vector3(1.5f, -1.5f, 1.5f);
            charge_position_arr[5] = new Vector3(-1.5f, 1.5f, -1.5f);
            charge_position_arr[6] = new Vector3(1.5f, -1.5f, -1.5f);
            charge_position_arr[7] = new Vector3(-1.5f, -1.5f, 1.5f);

        }

        return (number_of_charges, charge_values_arr, charge_position_arr);
    }

    bool Is_Inside_Sphere(Vector3 position, Vector3[] centers)
    {
        for (int i = 0; i < centers.Length; i++)
        {
            if ((position - centers[i]).magnitude < 0.25f)
            {
                return true;
            }
        }
        return false;
    }

    Vector3 Field_Vector(int charge, Vector3 center, Vector3 position) // returns the field vector at said position relative to center of charge
    {
        //float k = 1 / (4 * Mathf.PI * (8.85E-12f));
        float k = 1;
        float magnitude = (k * charge) / Mathf.Pow((position - center).magnitude, 2);
        Vector3 field_vector = magnitude * (position - center);
        return field_vector;
    }

    Vector3 Net_Field_Vector(int[] charge_arr, Vector3[] center_arr, Vector3 position)
    {
        Vector3 net_field_vector = new Vector3(0f, 0f, 0f);
        //float k = 1 / (4 * Mathf.PI * (8.85E-12f));
        float k = 1;
        int num_charges = charge_arr.Length;
        var field_vector_arr = new Vector3[num_charges];
        for (int i = 0; i < num_charges; i++) // Loop to create individual field vector contributions of each charge at "position" and add them to an array
        {
            float magnitude = (k * charge_arr[i]) / Mathf.Pow((position - center_arr[i]).magnitude, 2);
            Vector3 unit_field_vector = (position - center_arr[i]) / (position - center_arr[i]).magnitude;

            field_vector_arr[i] = magnitude * unit_field_vector;
        }
        for (int j = 0; j < num_charges; j++) // Loop to add individual field vector contributions of each charge for net field vector at "position"
        {
            net_field_vector = net_field_vector + field_vector_arr[j];
        }
        return net_field_vector;
    }



    Vector3[] Fibonacci_Sphere(float r, int points, Vector3 displacement)
    {

        float epsilon;
        float goldenRatio;
        float theta;
        float phi;
        float x, y, z;
        var position_arr = new Vector3[points];
        if (points >= 890)
        {
            epsilon = 10f;
        }
        else if (points >= 177)
        {
            epsilon = 3.33f;
        }
        else if (points >= 24)
        {
            epsilon = 1.33f;
        }
        else
        {
            epsilon = 0.33f;
        }

        goldenRatio = (1 + Mathf.Sqrt(5)) / 2;

        for (int i = 0; i < points; i++)
        {
            theta = 2 * Mathf.PI * i / goldenRatio; // THETA IS POLAR ANGLE NOT AZIMUTHAL
            phi = Mathf.Acos(1 - 2 * (i + epsilon) / (points - 1 + 2 * epsilon));
            x = r * Mathf.Cos(theta) * Mathf.Sin(phi);
            y = r * Mathf.Sin(theta) * Mathf.Sin(phi);
            z = r * Mathf.Cos(phi);
            position_arr[i] = new Vector3(x, y, z) + displacement;
        }
        return position_arr;
    }

    Vector3 SphericalToCartesian(Vector3 vector) // (r,theta,phi) => (x,y,z)
    {
        float x = vector.x * Mathf.Sin(vector.y) * Mathf.Cos(vector.z);
        float y = vector.x * Mathf.Sin(vector.y) * Mathf.Sin(vector.z);
        float z = vector.x * Mathf.Cos(vector.y);
        return new Vector3(x, y, z);
    }

    Vector3 CartesianToSpherical(Vector3 vector)
    {
        float r = Mathf.Sqrt(Mathf.Pow(vector.x, 2f) + Mathf.Pow(vector.y, 2f) + Mathf.Pow(vector.z, 2f));
        float theta = Mathf.Acos(vector.z / r);
        float phi = 0;
        if (r == 0)
        {
            return new Vector3(0, 0, 0);
        }
        else if (Mathf.Pow(vector.x, 2f) + Mathf.Pow(vector.y, 2f) == 0)
        {
            return new Vector3(r, theta, phi);
        }
        else
        {
            phi = Signum(vector.y) * Mathf.Acos(vector.x / Mathf.Sqrt(Mathf.Pow(vector.x, 2f) + Mathf.Pow(vector.y, 2f)));
        }
        return new Vector3(r, theta, phi);
    }

    float Signum(float y)
    {
        if (y < 0)
        {
            return -1f;
        }
        else if (y > 0)
        {
            return 1f;
        }
        else
        {
            return 0f;
        }
    }

    void CreateArrow(GameObject prefab, Vector3 position, Vector3 direction)
    {
        GameObject arrow = Instantiate(prefab);
        arrow.transform.localScale = new Vector3(0.005f, 0.005f, 0.005f);
        arrow.transform.position = position;
        arrow.transform.rotation = Quaternion.LookRotation(direction);
    }
    GameObject CreateSphere(GameObject arb_sphere, int charge_value, Vector3 position, string Tag)
    {
        Material pos_mat = Resources.Load("Red") as Material;
        Material neg_mat = Resources.Load("Cyan") as Material;
        arb_sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        arb_sphere.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        //arb_sphere.transform.localScale = new Vector3(2, 2, 2);
        arb_sphere.name = "Sphere - " + Tag;
        arb_sphere.tag = "Destroyable";
        arb_sphere.GetComponent<Renderer>().material = new Material(Shader.Find("Sprites/Default"));
        if (charge_value > 0)
        {
            arb_sphere.GetComponent<Renderer>().material = pos_mat;
        }
        else
        {
            arb_sphere.GetComponent<Renderer>().material = neg_mat;

        }
        arb_sphere.transform.position = position;
        arb_sphere.SetActive(true);
        return arb_sphere;
    }

    void GenerateFieldLines(Vector3 sphere_center, int sphere_charge, int sphere_num, int[] charge_arr, Vector3[] center_arr)
    {
        //Material line_mat = Resources.Load("Green") as Material;
        GameObject arrow_prefab = Resources.Load("TruncatedCone") as GameObject;
        arrow_prefab.tag = "Destroyable";
        Vector3 center_point = new Vector3(0f, 0f, 0f);
        center_point = sphere_center;
        int line_number = 0;
        //int num_sphere_points = 50;
        int num_sphere_points = (int)Math.Round(slider.value,0);
        int num_field_line_positions = 500;
        int sign = sphere_charge / Mathf.Abs(sphere_charge);
        Vector3[] sphere_points = Fibonacci_Sphere(0.25f, num_sphere_points, center_point);
        for (int i = 0; i < num_sphere_points; i++)
        {
            GameObject newObject = new GameObject($" {sphere_num} - Line {line_number + 1}");
            newObject.tag = "Destroyable";
            line_number = line_number + 1;
            LineRenderer lineRenderer = newObject.AddComponent<LineRenderer>();
            lineRenderer.sortingOrder = 1;
            //lineRenderer.material = line_mat;
            lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            lineRenderer.material.color = Color.yellow;
            lineRenderer.SetWidth(0.01f, 0.01f);
            Vector3 position = sphere_points[i];
            var position_arr = new Vector3[num_field_line_positions];
            float increment = 0.1f; // increment is the same as ds or differential path element
            int flag = 0;
            int counter = 0;
            int charge_diversity_chk = 0;
            for (int k = 0; k < num_field_line_positions; k++)
            {
                if (flag < 2) // makes sure field lines stop just inside the sphere
                {
                    if (Is_Inside_Sphere(position, center_arr))
                    {
                        flag++;
                    }
                    position_arr[k] = position;                                                                   // these 3 lines do the field line algorithm
                    Vector3 net_field_vector = Net_Field_Vector(charge_arr, center_arr, position);                    
                    /*if (net_field_vector.magnitude<0.001) //to deal with the field line going to the center in the hexagonal config
                    {
                        increment = 1E-26f;
                    }*/
                    position = (position) + (sign * net_field_vector / net_field_vector.magnitude) * increment; // sign is my addition to accomodate negative charges
                    counter++;
                }
                if (flag == 2)
                {
                    break;
                }
            }
            var new_position_arr = new Vector3[counter]; // not sure if this new array is strictly required but making this just in case because it has a fixed size
            for (int pp = 0; pp < counter; pp++)
            {
                new_position_arr[pp] = position_arr[pp];
            }
            //lineRenderer.SetPositions(position_arr);
            lineRenderer.positionCount = counter;
            lineRenderer.SetPositions(new_position_arr);
            CreateArrow(arrow_prefab, position_arr[(lineRenderer.positionCount / 2) + 1], Net_Field_Vector(charge_arr, center_arr, position_arr[(lineRenderer.positionCount / 2) + 1]));
            for (int divchk = 0; divchk < charge_arr.Length; divchk++)
            {
                charge_diversity_chk += charge_arr[divchk];
            }
            if (charge_diversity_chk == charge_arr.Length * charge_arr[0] || charge_diversity_chk == -charge_arr.Length * charge_arr[0])
            {
                CreateArrow(arrow_prefab, position_arr[(lineRenderer.positionCount / 10) + 1], Net_Field_Vector(charge_arr, center_arr, position_arr[(lineRenderer.positionCount / 10) + 1]));
            } 
            //CreateArrow(arrow_prefab, position_arr[(lineRenderer.positionCount / 10) + 1], Net_Field_Vector(charge_arr, center_arr, position_arr[(lineRenderer.positionCount / 10) + 1]));
            //CreateArrow(arrow_prefab, position_arr[(lineRenderer.positionCount) - 15], Net_Field_Vector(charge_arr, center_arr, position_arr[(lineRenderer.positionCount) - 15]));
        }
    }

    public void SetDropDownValue(int number_of_charges, int[] charge_values_arr, Vector3[] charge_position_arr)
    {
        int pickedEntryIndex = dropdown.value;
        return;
    }

    /*
    public void SetCharges(int number_of_charges, int[] charge_values_arr, Vector3[] charge_position_arr)
    {
        float value = slider.value;
        int number_of_charges = 0;
        int[] charge_values_arr = { };
        Vector3[] charge_position_arr = { };
        return;
    }
    */
}

    

/*

        // SPHERICAL COORDINATES CODE (LACKS RADIAL SYMMETRY, POINTS AREN'T EVENLY SPACED OUT, THEY ARE CLOSER NEAR THE POLES)

        // Vector3 center_point = GameObject.Find("Sphere").transform.position;
        Vector3 center_point = new Vector3(0f, 0f, 0f);
        Vector3 spherical_starting_point = new Vector3(0.5f, 0, 0);
        float theta = (Mathf.PI) / 9;
        float phi = (Mathf.PI) / 9;
        int line_number = 0;
        // spherical_starting_point = spherical_starting_point + CartesianToSpherical(center_point);
        for (int i = 0; i < 20; i++)
        {
            Vector3 dupe_starting_point = spherical_starting_point; //spherical_starting_point refers to line starting point and dupe is the sp of the 1st line
            for (int j = 0; j < 10; j++)
            {
                GameObject newObject = new GameObject($"Line {line_number + 1}");
                line_number = line_number + 1;
                LineRenderer lineRenderer = newObject.AddComponent<LineRenderer>();
                lineRenderer.sortingOrder = 1;
                lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
                lineRenderer.material.color = Color.yellow;
                lineRenderer.SetWidth(0.01f, 0.01f);
                lineRenderer.positionCount = 20;
                Vector3 position = spherical_starting_point;
                float increment = 1f;
                var position_arr = new Vector3[lineRenderer.positionCount];
                for (int k = 0; k < lineRenderer.positionCount; k++)
                {
                    position_arr[k] = SphericalToCartesian(position);
                    Vector3 field_vector = Field_Vector(1, center_point, SphericalToCartesian(position));
                    position = CartesianToSpherical(SphericalToCartesian(position) + (field_vector / field_vector.magnitude) * increment);
                }
                lineRenderer.SetPositions(position_arr);

                spherical_starting_point = spherical_starting_point + new Vector3(0, theta, 0);
            }
            dupe_starting_point = dupe_starting_point + new Vector3(0,0,phi);
            spherical_starting_point = dupe_starting_point;

        }*/