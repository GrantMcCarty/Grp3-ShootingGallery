using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    SpawnPoint spawn;
    public GameObject terrain;
    public GameObject fpsController;
    // Start is called before the first frame update
    void Start()
    {
        spawn = new SpawnPoint();
        
    }

    protected void OnTriggerEnter(Collider col) {
        if(col.gameObject.CompareTag("Player")) {
            Vector3 pos = spawn.Generate(col.gameObject, terrain);
            MoveTo(col.gameObject, pos);
        }
    }

    public void MoveTo(GameObject player, Vector3 pos) {
        // fpsController
        CharacterController controllerScript = player.GetComponent<CharacterController>();
        controllerScript.enabled = false;
        player.transform.position = pos;
        controllerScript.enabled = true;
        
    }
}
