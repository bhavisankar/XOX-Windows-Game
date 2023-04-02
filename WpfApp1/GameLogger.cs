using System;
using System.IO;
using System.Text;
using System.Windows.Shapes;

namespace WpfApp1
{
    public sealed class GameLogger
    {
        private static int gameId = 0;
        private static int moveId = 0;
        private static readonly string fileName = "game.txt";
        private static GameLogger instance = null;
        private static readonly string COMMA = ",";
        private static Boolean multiplayer;


        public static GameLogger Instance{
            get
            {
                if (instance == null)
                {
                    instance = new GameLogger();
                }
                return instance;
            }
        }


        private GameLogger() {
            

        }

        public static void WriteLog(String user ,int i, int j) {
            moveId++;
            string pType = "Human";
            if (!multiplayer && user.Equals("O")) {
                pType = "Computer";
            } 
            StringBuilder sb = new StringBuilder("Game");
            string logInfo = sb.Append(gameId).Append(COMMA).
                Append("Move").Append(moveId).Append(COMMA)
                .Append(DateTime.Now).Append(COMMA)
                .Append(pType).Append(COMMA)
                .Append(user).Append(COMMA)
                .Append("[").Append(i).Append(COMMA).Append(j).Append("]").ToString();
            
            if (!File.Exists(fileName)) {
                File.Create(fileName);
            }
            TextWriter tw = new StreamWriter(fileName, true);
            tw.WriteLine(logInfo);
            tw.Close();
        }

        public static void logNewGame(Boolean player) {
            multiplayer = player;
            gameId++;
            moveId = 0;
        }
    }
}
