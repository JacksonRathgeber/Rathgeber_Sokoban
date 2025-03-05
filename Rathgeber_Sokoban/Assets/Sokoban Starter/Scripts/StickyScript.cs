using UnityEngine;

public class StickyScript : MonoBehaviour
{
    private GameObject[] all_cubes;
    private bool moving = false;
    private bool resolved = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        all_cubes = GameObject.FindGameObjectsWithTag("Cube");
    }

    public bool InWayCheckAndMove(Vector2Int destination, int x_in, int y_in)
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
        Vector2Int old_pos = grid_obj.gridPosition;

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
                    cube.GetComponent<GridObject>().gridPosition == side_2 ||
                    cube.GetComponent<GridObject>().gridPosition == side_3 ||
                    cube.GetComponent<GridObject>().gridPosition == side_4)
            {

                cube_to_side = cube;
            }
        }
        if (cube_in_way != null)
        {
            switch (cube_in_way.name)
            {
                case "slick":
                case "smooth":
                    if (cube_in_way.GetComponent<SlickScript>().CheckAndMove(cube_dest, x_in, y_in) == true)
                    {
                        grid_obj.gridPosition = destination;
                        moving = true;
                        break;
                    }
                    else
                    {
                        moving = false;
                        break;
                    }

                case "sticky":
                    if (cube_in_way.GetComponent<StickyScript>().resolved == false)
                    {
                        if (cube_in_way.GetComponent<StickyScript>().InWayCheckAndMove(cube_dest, x_in, y_in) == true)
                        {
                            grid_obj.gridPosition = destination;
                            moving = true;
                            cube_in_way.GetComponent<StickyScript>().resolved = true;
                            break;
                        }
                        else
                        {
                            moving = false;
                            cube_in_way.GetComponent<StickyScript>().resolved = true;
                            break;
                        }
                    }
                    break;

                default:
                    moving = false;
                    break;
            }
        }
        else
        {
            if (destination.x >= 1 && destination.x <= 10 &&
                destination.y >= 1 && destination.y <= 5)
            {
                grid_obj.gridPosition = destination;
                moving = true;
            }
            else
            {
                moving = false;
            }
        }

        if (cube_trailing != null && moving)
        {
            switch (cube_trailing.name)
            {
                case "sticky":
                    if (cube_trailing.GetComponent<StickyScript>().resolved == false)
                    {
                        cube_trailing.GetComponent<StickyScript>().TrailingCheckAndMove(old_pos, x_in, y_in);
                        cube_trailing.GetComponent<StickyScript>().resolved = true;
                    }
                    break;

                default:
                    break;

            }
        }

        if (cube_to_side != null && moving)
        {
            cube_dest = new Vector2Int(cube_to_side.GetComponent<GridObject>().gridPosition.x + x_in,
                cube_to_side.GetComponent<GridObject>().gridPosition.y + y_in);

            switch (cube_to_side.name)
            {

                case "sticky":
                    if (cube_to_side.GetComponent<StickyScript>().resolved == false)
                    {
                        if (moving)
                        {
                            cube_to_side.GetComponent<StickyScript>().InWayCheckAndMove(cube_dest, x_in, y_in);
                        }
                        cube_to_side.GetComponent<StickyScript>().resolved = true;
                    }
                    break;

                default:
                    break;

            }
        }
        return moving;
    }


    public bool TrailingCheckAndMove(Vector2Int destination, int x_in, int y_in)
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
        Vector2Int old_pos = grid_obj.gridPosition;

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
            switch (cube_in_way.name)
            {
                case "sticky":
                    if (cube_in_way.GetComponent<StickyScript>().resolved == false)
                    {
                        if (cube_in_way.GetComponent<StickyScript>().TrailingCheckAndMove(old_pos, x_in, y_in) == true)
                        {
                            grid_obj.gridPosition = destination;
                            moving = true;
                            cube_in_way.GetComponent<StickyScript>().resolved = true;
                        }
                    }
                    break;

                default:
                    break;
            }
        }
        else
        {
            grid_obj.gridPosition = destination;

            if (cube_trailing != null)
            {
                switch (cube_trailing.name)
                {
                    case "clingy":
                        cube_trailing.GetComponent<ClingyScript>().CheckAndMove(old_pos, x_in, y_in);
                        break;

                    case "sticky":
                        if (cube_trailing.GetComponent<StickyScript>().resolved == false)
                        {
                            cube_trailing.GetComponent<StickyScript>().TrailingCheckAndMove(old_pos, x_in, y_in);
                            cube_trailing.GetComponent<StickyScript>().resolved = true;

                        }
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
                    if (cube_to_side.GetComponent<StickyScript>().resolved == false)
                    {
                        if (moving)
                        {
                            cube_to_side.GetComponent<StickyScript>().InWayCheckAndMove(cube_dest, x_in, y_in);
                            cube_to_side.GetComponent<StickyScript>().resolved = true;

                        }
                    }
                    break;

                default:
                    break;

            }
        }
        return moving;
    }

    
    void LateUpdate()
    {
        if(resolved == true)
        {
            resolved = false;
        }
    }
}
