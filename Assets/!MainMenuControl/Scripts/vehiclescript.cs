using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum vehicleType
{
    RccCars,
    Bike,
    Train,
    AeroPlane,
    Other
}
public class vehiclescript : MonoBehaviour
{
    public vehicleType currentVehicle = vehicleType.RccCars;
    public GameManger gamePlay;
    public Button Button1;
    public GameObject CollidedCar;

    public RCC_CarControllerV3 otherControl;
    public BikeControl BikeControlSetup;
    void Start()
    {
        gamePlay = FindObjectOfType<GameManger>();
        Button1.onClick.AddListener(SwitchControls);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (currentVehicle == vehicleType.RccCars)
            {
                Button1.gameObject.SetActive(true);

                Debug.LogError("colissed");

                otherControl = collision.gameObject.GetComponent<RCC_CarControllerV3>();
                CollidedCar = collision.gameObject;

                Debug.LogError("getcompente");
            }
            else if (currentVehicle == vehicleType.Bike)
            {
                Transform collisionBike = collision.gameObject.transform;
                gamePlay.BikeCamera.GetComponent<BikeCamera>().target = collisionBike;

                gamePlay.RccControl.SetActive(false);
                gamePlay.BikeControl.SetActive(true);

                collision.gameObject.GetComponent<BikeControl>().enabled = true;
            }
        }
    }
    public void SwitchControls() => StartCoroutine(SwitchControlsC());
    IEnumerator SwitchControlsC()
    {
        switch (currentVehicle)
        {
            case vehicleType.RccCars:

                var rb = this.GetComponent<Rigidbody>();
                Button1.gameObject.SetActive(false);
                
                this.GetComponent<RCC_CarControllerV3>().enabled = false;
                otherControl.enabled = true;
                this.GetComponent<vehiclescript>().enabled = false;

                //CollidedCar.GetComponent<vehiclescript>().CollidedCar = null;
                //CollidedCar.GetComponent<vehiclescript>().otherControl = null;
                CollidedCar.GetComponent<vehiclescript>().enabled = true;
                rb.isKinematic = true;
                CollidedCar.GetComponent<Rigidbody>().isKinematic = false;
                //CollidedCar = null;
                //otherControl = null;
                //  this.transform.GetChild(1).gameObject.SetActive(true);
                // this.GetComponent<Rigidbody>().isKinematic = true;


                // otherControl.GetComponent<RCC_CarControllerV3>().enabled = true;
                //  otherControl.transform.GetChild(1).gameObject.SetActive(false);
                // otherControl.GetComponent<Rigidbody>().isKinematic = false;

                yield return new WaitForSeconds(2f);
                RCC_Camera cam = RCC_SceneManager.Instance.activePlayerCamera;

                Debug.LogError("controlschanged");
                // gamePlay.Button1.SetActive(false);
                break;

            case vehicleType.Bike:

                gamePlay.RccControl.SetActive(false);
                gamePlay.BikeControl.SetActive(true);
                break;
        }
    }
    void Update()
    {

    }
}