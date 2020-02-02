using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using VRTools;

public class Item_hover : MonoBehaviour
{
    [SerializeField]
    private float triggeringDistance = 0.1f;
    [SerializeField]
    private Texture hoverTexture;
    [SerializeField]
    private Texture idleTexture;
    [SerializeField]
    private string identifier;
    [SerializeField]
    Grabing[] interactibles;
    [SerializeField]
    UnityEvent OnSelect;

    Material objectMaterial;
    // Start is called before the first frame update
    void Start()
    {
        objectMaterial = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        objectMaterial.SetTexture("_EmissionMap", idleTexture);
        foreach(Grabing interactible in interactibles)
        {
            if (Vector3.Distance(interactible.gameObject.transform.position, transform.position)<=triggeringDistance)
            {
                Debug.Log("Item Hover System: " + identifier + " texture should have changed");
                objectMaterial.SetTexture("_EmissionMap", hoverTexture);
                if (interactible.getCloseness() > 0.5)
                {
                    OnSelect.Invoke();
                }
                break;
            }
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
