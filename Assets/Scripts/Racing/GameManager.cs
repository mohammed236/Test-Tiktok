using System.Collections.Generic;
using TikTokLiveSharp.Events.Objects;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Reflection;
namespace Arab
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager _instance { get; private set; }

        private Queue<CarController> carControllersQueueLikes = new Queue<CarController>();
        private Queue<CarController> carControllersQueueGifts = new Queue<CarController>();
        [SerializeField] private CarController[] carControllers;

        public Car carIndex;
        [SerializeField] private List<CarController> players = new List<CarController>();

        [SerializeField] private SortedDictionary<float, CarController> playersFinishedDic = new SortedDictionary<float, CarController>();

        private bool isPlayersSpawned = false;

        private const int MaxPlayersToPeak = 3;
        public int minLikeToJoin = 5;
        private CarController winner;

        private void Awake()
        {
            _instance = this;
            CarController.OnFinished += GameManager_OnFinished;

        }
        private void OnDisable()
        {
            CarController.OnFinished -= GameManager_OnFinished;

        }
        private void Update()
        {
            HandleInput();

            
        }

        private void HandleInput()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                AddPlayerTest();
            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                PeakPlayer();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                SpawnPlayerInRaceArea();
            }
        }

        public void AddPlayerTest()
        {
            foreach (var controller in carControllers)
            {
                if (this.carIndex == controller._car)
                {
                    carControllersQueueLikes.Enqueue(controller);
                    //Debug.Log($"{controller._car} index in the queue : {carControllersQueueLikes.Count}");
                    //Debug.Log($"{controller.playerProfile_Name.text} index in the queue : {carControllersQueueLikes.Count}");
                }
            }
            this.carIndex = (Car)(((int)this.carIndex + 1) % carControllers.Length);
        }

        public void AddPlayerLikes(string userName,Picture pic)
        {
            foreach (var controller in carControllers)
            {
                if (this.carIndex == controller._car)
                {
                    // Setup Player Name
                    controller.playerProfile_Name.text = ArabicSupport.ArabicFixer.Fix(userName);

                    //Setup Player Icon
                    SpriteRenderer sp = controller.playerProfile_SpriteRend;
                    Mediator._instance.RequestImage(sp,pic);

                    carControllersQueueLikes.Enqueue(controller);
                    Debug.Log($"{controller._car} index in the queue : {carControllersQueueLikes.Count}");
                    Debug.Log($"{controller.playerProfile_Name.text} index in the queue : {carControllersQueueLikes.Count}");
                }
            }
            this.carIndex = (Car)(((int)this.carIndex + 1) % carControllers.Length);
        }
        public void AddPlayerGifts(string userName, Picture pic)
        {
            foreach (var controller in carControllers)
            {
                if (this.carIndex == controller._car)
                {
                    // Setup Player Name
                    controller.playerProfile_Name.text = ArabicSupport.ArabicFixer.Fix(userName);

                    //Setup Player Icon
                    SpriteRenderer sp = controller.playerProfile_SpriteRend;
                    Mediator._instance.RequestImage(sp, pic);

                    carControllersQueueGifts.Enqueue(controller);
                    Debug.Log($"{controller._car} index in the queue : {carControllersQueueGifts.Count}");
                    Debug.Log($"{controller.playerProfile_Name.text} index in the queue : {carControllersQueueGifts.Count}");
                }
            }
            this.carIndex = (Car)(((int)this.carIndex + 1) % carControllers.Length);
        }

        public void PeakPlayer()
        {
            if (winner != null)
                players.Add(winner);

            for (int i = 1; i <= MaxPlayersToPeak; i++)
            {
                CarController car = null;
                print(players.Count);
                if(players.Count< MaxPlayersToPeak)
                {
                    if (carControllersQueueGifts.Count > 0)
                    {
                        car = carControllersQueueLikes.Dequeue();
                        Debug.Log($"car {i} from Gifts nbr is : {car._car}");
                    }
                    else if (carControllersQueueLikes.Count > 0)
                    {
                        car = carControllersQueueLikes.Dequeue();
                        Debug.Log($"car {i} from Likes is : {car._car}");
                    }
                    else
                    {
                        return;
                    }

                    if (car != null)
                        players.Add(car);
                }
                
            }
        }

        private void SpawnPlayerInRaceArea()
        {
            playersFinishedDic.Clear();

            if (players.Count == 0 || isPlayersSpawned)
            {
                FreeUpList(players);
                Debug.Log("Players List Is Empty Take Players From new Queue");
                return;
            }
            foreach (var player in players)
            {
                if (player != null)
                {
                    Instantiate(player.gameObject, player.transform.position, player.transform.rotation);
                }
            }
            isPlayersSpawned = true;
        }

        private void GameManager_OnFinished(CarController player,float finishTime)
        {
            playersFinishedDic.Add(finishTime,player);


            if (IsSessionFinished())
            {
                //Sorted Dic rom Low Time
                //var sortedDic = from data in playersFinishedDic orderby data.Value ascending select data;
                
                // peek Winer
                winner = playersFinishedDic.First().Value;

                foreach (var playerFinished in playersFinishedDic)
                {
                    print($"{playerFinished.Value} || {playerFinished.Value._car}");

                    CarController[] inGamePlayers = FindObjectsOfType<CarController>();
                    print(inGamePlayers.Length);

                    foreach (var inGamePlayer in inGamePlayers)
                    {
                        if(playerFinished.Value._car == inGamePlayer._car)
                        {                          
                            inGamePlayer.gameObject.SetActive(false);
                        }else
                            print("non correct");
                        if (playerFinished.Value._car == winner._car)
                            inGamePlayer.gameObject.SetActive(true);
                    }
                }

                // 
                FreeUpList(players);
            }
        }

        private void FreeUpList(List<CarController> list)
        {
            list.Clear();
            list = new List<CarController>();
            isPlayersSpawned = false;
        }
        private bool IsSessionFinished()
        {
            if(playersFinishedDic.Count >= MaxPlayersToPeak)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}