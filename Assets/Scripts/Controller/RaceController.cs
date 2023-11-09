using System.Collections.Generic;
using UnityEngine;

namespace RaceLogic
{
    public class RaceController : MonoBehaviour
    {
        public static RaceController Instance;

        public delegate void MyDelegate();
        public MyDelegate myDelegate;

        [SerializeField] private Transform vehiclesContainer;

        [SerializeField] private List<Vehicle> vehiclesToSpawn = new();
        private List<Vehicle> inRaceSpawnedVehicle = new();

        public Vector2Int screenBounds;
        private bool raceOver = true;

        #region AwakeUpdate
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }

            screenBounds = new Vector2Int(Screen.width, Screen.height/2);
        }

        private void Update()
        {
            if (!raceOver)
            {
                foreach (Vehicle vehicle in inRaceSpawnedVehicle)
                {
                    vehicle.Drive();
                }

                if (inRaceSpawnedVehicle.Exists(v => v.transform.position.x > screenBounds.x))
                {
                    raceOver = true;

                    myDelegate.Invoke();
                }
            }
        }
        #endregion

        public void SpawnVehiclesNewRace(Sprite truckSprite, Sprite busSprite, Sprite raceCarSprite)
        {
            System.Random rnd = new System.Random();

            foreach (Vehicle vehicleObject in vehiclesToSpawn)
            {
                float randomYpos = rnd.Next(100, screenBounds.y);

                Vehicle vehicle  = Instantiate(vehicleObject, new Vector2(0, randomYpos), Quaternion.identity, vehiclesContainer);

                switch (vehicle)
                {
                    case Truck truck:              
                        truck.image.sprite = truckSprite;
                        break;
                    case Bus bus:
                        bus.image.sprite = busSprite;
                        break;
                    case RaceCar raceCar:
                        raceCar.image.sprite = raceCarSprite;
                        break;
                   
                    default:
                        break;
                }

                inRaceSpawnedVehicle.Add(vehicle);
            }

            raceOver = false;
        }

        public void RestartRace()
        {
            foreach (Vehicle vehicle in inRaceSpawnedVehicle)
            {
                vehicle.transform.position = new Vector2(0, vehicle.transform.position.y);
            }

            raceOver = false;
        }

        public void CloseRace()
        {
            raceOver = true;

            foreach (Vehicle vehicle in inRaceSpawnedVehicle)
            {
                Destroy(vehicle.gameObject);
            }

            inRaceSpawnedVehicle.Clear();
        }
    }
}
