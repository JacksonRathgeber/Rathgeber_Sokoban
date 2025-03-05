using UnityEngine;

public class SlickScript : MonoBehaviour
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
            Vector2Int cube_dest = new Vector2Int(grid_obj.gridPosition.x + (x_in * 2), grid_obj.gridPosition.y + (y_in * 2));

            switch (cube_in_way.name)
            {
                case "slick":
                case "smooth":
                    if (cube_in_way.GetComponent<SlickScript>().CheckAndMove(cube_dest, x_in, y_in) == true)
                    {
                        grid_obj.gridPosition = destination;
                        return true;
                    }
                    return false;

                case "sticky":
                    if (cube_in_way.GetComponent<StickyScript>().InWayCheckAndMove(cube_dest, x_in, y_in) == true)
                    {
                        grid_obj.gridPosition = destination;
                        return true;
                    }
                    return false;

                default:
                    return false;
            }
        }
        else
        {
            if(destination.x >= 1 && destination.x <= 10 &&
                destination.y >= 1 && destination.y <= 5)
            {
                grid_obj.gridPosition = destination;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
