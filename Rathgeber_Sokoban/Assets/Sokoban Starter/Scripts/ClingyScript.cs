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
        GameObject cube_trailing = null;
        Vector2Int trail = new Vector2Int(grid_obj.gridPosition.x - x_in, grid_obj.gridPosition.y - y_in);

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
        }
        if (cube_in_way != null)
        {
            return false;
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
            
            return true;
        }
    }
}
