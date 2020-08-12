using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OVRTestInputs : MonoBehaviour
{
    /*
    public GameObject hand;
    CustomController_InputValuesAccess leftController;
    [SerializeField]
    Transform testCube;
    // Start is called before the first frame update
    void Start()
    {
        leftController = hand.GetComponent<CustomController_InputValuesAccess>();
        float i = leftController.OVR_LeftStickValues("x");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 leftstickValues = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        int maxSpeed = 15;
        float moveSpeedX = maxSpeed * leftstickValues.x;
        float moveSpeedY = maxSpeed * leftstickValues.y;

        if (leftstickValues.x >= 0.02f)
        {
            testCube.Translate(moveSpeedX * Time.deltaTime, 0f, 0f);
        }

        else if (leftstickValues.x <= -0.02f)
        {
            testCube.Translate(moveSpeedX * Time.deltaTime, 0f, 0f);
        }

        if (leftstickValues.y >= 0.02f)
        {
            testCube.Translate(0f, 0f, moveSpeedY * Time.deltaTime);
        }

        else if (leftstickValues.y <= -0.02f)
        {
            testCube.Translate(0f, 0f, moveSpeedY * Time.deltaTime);
        }

        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            SceneManager.LoadScene("firstControllerTestScene");
        }
    }
    */
}
