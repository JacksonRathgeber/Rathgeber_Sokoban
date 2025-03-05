using UnityEngine;

public class ClingyScript : MonoBehaviour
{
    private GameObject[] all_cubes;
    private bool moving = false;

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
        GameObject cube_trailing = null;
        GameObject cube_to_side = null;
        Vector2Int trail = new Vector2Int(grid_obj.gridPosition.x - x_in, grid_obj.gridPosition.y - y_in);
        Vector2Int side_1 = new Vector2Int(grid_obj.gridPosition.x + 1, grid_obj.gridPosition.y);
        Vector2Int side_2 = new Vector2Int(grid_obj.gridPosition.x - 1, grid_obj.gridPosition.y);
        Vector2Int side_3 = new Vector2Int(grid_obj.gridPosition.x, grid_obj.gridPosition.y - 1);
        Vector2Int side_4 = new Vector2Int(grid_obj.gridPosition.x, grid_obj.gridPosition.y + 1);
        Vector2Int cube_dest = new Vector2Int(grid_obj.gridPosition.x + (x_in * 2), grid_obj.gridPosition.y + (y_in * 2));

        foreach (GameObject cube in all_cubes)
        {
            if (cube.GetComponent<GridObject>().gridPosition == destination)
            {
                cube_in_way = cube;
            }
            else if (cube.GetComponent<GridObject>().gridPosition == trail)
            {
                cube_trailing = cube;
            }
            else if (cube.GetComponent<GridObject>().gridPosition == side_1 ||
                    (cube.GetComponent<GridObject>().gridPosition == side_2) ||
                    cube.GetComponent<GridObject>().gridPosition == side_3 ||
                    (cube.GetComponent<GridObject>().gridPosition == side_4))
            {

                cube_to_side = cube;
            }
        }
        if (cube_in_way != null)
        {
            moving = false;
        }
        else
        {
            Vector2Int old_pos = grid_obj.gridPosition;
            grid_obj.gridPosition = destination;

            if (cube_trailing != null)
            {
                switch (cube_trailing.name)
                {
                    case "clingy":
                        cube_trailing.GetComponent<ClingyScript>().CheckAndMove(old_pos, x_in, y_in);
                        break;

                    case "sticky":
                        cube_trailing.GetComponent<StickyScript>().TrailingCheckAndMove(old_pos, x_in, y_in);
                        break;

                    default:
                        break;
                }
            }

            moving = true;
        }
        if (cube_to_side != null)
        {
            cube_dest = new Vector2Int(cube_to_side.GetComponent<GridObject>().gridPosition.x + x_in,
                cube_to_side.GetComponent<GridObject>().gridPosition.y + y_in);

            switch (cube_to_side.name)
            {

                case "sticky":
                    if (moving)
                    {
                        cube_to_side.GetComponent<StickyScript>().InWayCheckAndMove(cube_dest, x_in, y_in);
                    }
                    break;

                default:
                    break;

            }
        }
        return moving;
    }
}
