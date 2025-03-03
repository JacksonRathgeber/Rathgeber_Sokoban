using UnityEngine;

public class PlayerScript : MonoBehaviour
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
        var grid_obj = GetComponent<GridObject>();

        int x_in = 0;
        int y_in = 0;

        if (Input.GetKeyDown(KeyCode.W) && grid_obj.gridPosition.y > 1)
        {
            y_in -= 1;
        }
        if (Input.GetKeyDown(KeyCode.S) && grid_obj.gridPosition.y < 5)
        {
            y_in += 1;
        }
        if (Input.GetKeyDown(KeyCode.D) && grid_obj.gridPosition.x < 10)
        {
            x_in += 1;
        }
        if (Input.GetKeyDown(KeyCode.A) && grid_obj.gridPosition.x > 1)
        {
            x_in -= 1;
        }

        if (x_in != 0 || y_in != 0)
        {
            GameObject cube_in_way = null;
            GameObject cube_trailing = null;
            Vector2Int destination = new Vector2Int(grid_obj.gridPosition.x + x_in, grid_obj.gridPosition.y + y_in);
            Vector2Int trail = new Vector2Int(grid_obj.gridPosition.x - x_in, grid_obj.gridPosition.y - y_in);
            Vector2Int old_pos = grid_obj.gridPosition;
            foreach (GameObject cube in all_cubes)
            {
                //var scripts = cube.GetComponents<MonoBehaviour>();
                
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
                switch (cube_in_way.name)
                {
                    case "wall": // [DONE] Immovable, stops all
                        break;

                    case "clingy": // Pullable BY ANYTHING, WILL REQUIRE REFACTOR
                        break;

                    case "slick": // [DONE] Pushable BY ANYTHING
                    case "smooth":
                        Vector2Int cube_dest = new Vector2Int(grid_obj.gridPosition.x + (x_in*2), grid_obj.gridPosition.y + (y_in*2));
                        if (cube_in_way.GetComponent<SlickScript>().CheckAndMove(cube_dest, x_in, y_in) == true)
                        {
                            grid_obj.gridPosition = destination;
                        }
                        break;

                    case "sticky":
                        // Mimics ALL adjacent blocks, WILL REQUIRE REFACTOR
                        break;

                    default: // Error case
                        Debug.Log("ERROR: PLAYER");
                        break;
                }
            }
            else
            {
                grid_obj.gridPosition = destination;
            }

            if (cube_trailing != null)
            {
                switch (cube_trailing.name)
                {
                    // MAKE ALL CUBES OBEY BOUNDS OF GRID
                    
                    case "clingy": // Pullable BY ANYTHING, WILL REQUIRE REFACTOR
                        cube_trailing.GetComponent<ClingyScript>().CheckAndMove(old_pos, x_in, y_in);
                        break;

                    case "sticky":
                        // Mimics ALL adjacent blocks, WILL REQUIRE REFACTOR
                        break;

                    default: // Pulling an unpullable
                        break;
                }
            }
        }
    }
}
