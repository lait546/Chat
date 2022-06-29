using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Timers;
using Random = System.Random;
//using Random = UnityEngine.Random;

public enum RoomState
{
    Preparation,
    Finished,
    Unfinished
}

namespace ChatServer
{
    public class RoomsManager
    {
        public static RoomsManager instance;

        public int MaxRooms = 30;
        public Dictionary<int, Room> Matchs = new Dictionary<int, Room>();
        Room room;

        public void CheckMatch(Client player)
        {
            if (instance == null)
                instance = this;

            bool joined = false;

            for (int i = 1; i <= Matchs.Count; i++)
                if (Matchs[i].roomState == RoomState.Preparation)
                {
                    Matchs[i].clients.Add(player.id, player);

                    //player.room = Matchs[i].numberRoom;
                    joined = true;

                    if (Matchs[i].clients.Count == 3)
                    {
                        Matchs[i].roomState = RoomState.Unfinished;

                        //Matchs[i].StartTimer();
                    }

                    return;
                }

            if (!joined)
            {
                CreateMatch();

                Matchs[Matchs.Count].clients.Add(player.id, player);
                //player.room = Matchs.Count;
            }
        }

        private void CreateMatch()
        {
            room = new Room(Matchs.Count + 1, RoomState.Preparation);
            Matchs.Add(Matchs.Count + 1, room);
            ////room.MatchID = Matchs.Count;
            //Debug.Log("Создалась комната: " + Matchs[Matchs.Count].numberRoom + "Количество комнат: " + Matchs.Count);
            //Console.WriteLine("Initialized packets.");

        }

        public void LeavingTheMatch(int playerId, int room)
        {
            Matchs[room].clients.Remove(playerId);

            //if (Matchs[room].clients.Count <= 0 && (Matchs[room].roomState != RoomState.Finished /*|| Matchs[player.room].roomState != RoomState.Finished*/)) //потом дать возможность восст. игрокам при не финишед
            //{
            //    //Debug.Log("--------------------------------------------------------------Удалилась комната ");
            //    Matchs[room].EndMatch();
            //}
        }

        //public void LeavingTheMatch(Player player)
        //{
        //    Matchs[player.roomNumber].clients.Remove(player.id);

        //    if (Matchs[player.roomNumber].clients.Count <= 0 && (Matchs[player.roomNumber].roomState != RoomState.Finished /*|| Matchs[player.room].roomState != RoomState.Finished*/)) //потом дать возможность восст. игрокам при не финишед
        //    {
        //        //Debug.Log("--------------------------------------------------------------Удалилась комната ");
        //        Matchs[player.roomNumber].EndMatch();
        //    }
        //}
    }
}

namespace ChatServer
{
    public class Room
    {
        public Room Instance;

        public Dictionary<int, Client> clients = new Dictionary<int, Client>();
        public List<Client> orderedClients = new List<Client>();
        public RoomState roomState = 0;
        public int MaxPlayersInRoom = 5, Timer = 2, numberRoom, Turn = 0, _round = 0, cardNetworkIdCounter = 0, FirstPlayerNumber = 0, countLivePlayers = 0;
        public bool waits = false, counterAttack = false;

        System.Timers.Timer timer = new System.Timers.Timer(1000);
        //private GameStadies stage = GameStadies.PreparationGame;

        public int CardNetworkIdCounter => cardNetworkIdCounter++;

        //public List<Duel> duel = new List<Duel>();

        public static Color[] playerColors = new Color[5]{
        new Color(255f,0f,0f,192f), //красный
        new Color(10f,255f,0f,192f), //зеленый
        new Color(255f,0f,189f,192f), //фиолетовый
        new Color(27f,0f,255f,192f), //синий
        new Color(255f,125f,0f,192f) //оранжевый
    };

        System.Random rnd = new System.Random();

        //public GameStadies Stage
        //{
        //    get { return stage; }
        //    set
        //    {
        //        stage = value;
        //    }
        //}

        public Room(int _numberRoom, RoomState _state)
        {
            roomState = _state;
            numberRoom = _numberRoom;
        }

        public void Init()
        {
            
        }

        public void StartGame() //отправка кто ходит и энд тёрн
        {
            if (Instance == null)
                Instance = this;

            Init();

            countLivePlayers = clients.Count;
        }


    }
}