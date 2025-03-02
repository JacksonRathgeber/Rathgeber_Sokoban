using UnityEngine;

public class ClingyScript : MonoBehaviour
{
    private GameObject[] all_cubes;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        all_cubes = GameObject.FindGameObjectsWithTag("Cube");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CheckAndMove(Vector2Int destination, int x_in, int y_in)
    {
        var grid_obj = GetComponent<GridObject>();
        GameObject cube_in_way = null;

        foreach (GameObject cube in all_cubes)
        {
            if (cube.GetComponent<GridObject>().gridPosition == destination)
            {
                cube_in_way = cube;
            }
        }
        if (cube_in_way != null)
        {
            switch (cube_in_way.name)
            {
                case "player":
                    return false;
                    break;

                case "wall":
                    return false;
                    break;

                case "clingy":
                    Vector2Int cube_dest = new Vector2Int(grid_obj.gridPosition.x + x_in, grid_obj.gridPosition.y + y_in);
                    if (cube_in_way.GetComponent<ClingyScript>().CheckAndMove(cube_dest, x_in, y_in) == true)
                    {
                        grid_obj.gridPosition = destination;
                        return true;
                    }
                    return false;
                    break;

                case "slick":
                case "smooth":
                    return false;
                    break;

                case "sticky":
                    return false;
                    break;

                default:
                    Debug.Log("ERROR: CLINGY");
                    return false;
                    break;
            }
        }
        else
        {
            grid_obj.gridPosition = destination;
            return true;
        }
    }
}
