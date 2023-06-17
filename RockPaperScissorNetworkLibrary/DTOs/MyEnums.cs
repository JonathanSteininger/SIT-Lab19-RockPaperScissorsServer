using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissorNetworkLibrary
{

    //DTO enums
    public enum CommandType
    {
        QuitServer,
        QuitGame
    }
    public enum RequestType
    {
        GetGameInfo,
        GetAllGames
    }
    public enum DataSentType//requires response from server to check if it was successfull
    {
        PlayerInfo,
        CreateGame,
        GameMove,
        GameMessage
    }
    public enum ResponseType
    {
        ErrorMessage,
        GameInfo,
        AllGamesInfo,
        RoundResult,
    }


    //Game enums
    public enum RoundChoice
    {
        Rock,
        Paper,
        Scissors
    }
    public enum GameResult
    {
        Win,
        Lose,
        Draw
    }
}
