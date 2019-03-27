using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour
{
    public Transform towerParent;
    public int currentTower = 0;
    public GameObject[] towers;
    public GameObject[] holograms;

    void OnDrawGizmos()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(mouseRay.origin, mouseRay.origin + mouseRay.direction * 1000f);
    }

    /// <summary>
    /// Disables the GameObjects of all referenced holograms
    /// </summary>
    void DisableAllHolograms()
    {
        // Loop through all Holograms
        foreach (var holo in holograms)
        {
            // Disable hologram
            holo.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Disable all holograms at the start of each frame
        DisableAllHolograms();
        // Creates a ray
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        // Performing Raycast here
        if (Physics.Raycast(mouseRay, out hit))
        {
            // Try to get Placeable from thing we hit
            Placeable p = hit.collider.GetComponent<Placeable>();
            // Is it placable? ... AND placeable is Available
            if (p && p.isAvailable)
            {
                //>>Hover Mechanic<<
                // Get hologram of current tower
                GameObject hologram = holograms[currentTower];
                // Activate hologram
                hologram.SetActive(true);
                // Position hologram to tile
                hologram.transform.position = p.GetPivotPoint();


                //>>Placement Mechanic<<
                // If left mouse is down
                if (Input.GetMouseButtonDown(0))
                {
                    // Get the current tower prefab
                    GameObject towerPrefab = towers[currentTower];
                    // Spawn a new tower
                    GameObject tower = Instantiate(towerPrefab, towerParent);
                    // Position new tower to tile
                    tower.transform.position = p.GetPivotPoint();
                    // Placeable is not available anymore
                    p.isAvailable = false;
                }

        }
    }


    public void SelectTower(int tower)
    {
        // Is tower within range of array (towers)
        if (tower >= 0 && tower < towers.Length)
        {
            // set currentTower to tower
            currentTower = tower;
        }
    }
}
