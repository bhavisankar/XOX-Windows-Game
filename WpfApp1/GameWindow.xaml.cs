using System;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Game.xaml
    /// </summary>
    public partial class Game : Window
    {
        private readonly string PLAYER_A = "X";
        private readonly string PLAYER_B = "O";
        private readonly string DEFAULT_CONTENT = "D";
        private String current_player;
        private String[,] board;
        private int boardSize = 0;
        private Boolean isMultyPlayer;
        public Game(Boolean multyplayer)
        {
            InitializeComponent();
            initGame(multyplayer);
            current_player = PLAYER_A;
            
        }

        private void initGame(Boolean player) {
            GameLogger.logNewGame(player);
            this.isMultyPlayer = player;
            board = new string[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    board[i, j] = DEFAULT_CONTENT;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            setXO((Button)sender);
        }

        private void setXO(Button b) {
            if (PLAYER_A.Equals(current_player))
            {            
                if (fillArray(b, PLAYER_A))
                {
                    if (isXOX(PLAYER_A))
                    {
                        Close();
                        ResultWindow result = new ResultWindow(isMultyPlayer, "A");
                        result.Show();
                        return;
                    } else if (boardSize == 9) {
                        Close();
                        ResultWindow result = new ResultWindow(isMultyPlayer, "C");
                        result.Show();
                        return;
                    }
                    current_player = PLAYER_B;
                    if (!isMultyPlayer)
                    {

                        Boolean found = false;
                        int buttonindex = FindHorizontalOccupancy();
                        current_player = PLAYER_A;
                        boardSize++;
                        ComputerPlay(buttonindex);
                        found = true;

                        if (isXOX(PLAYER_B) || boardSize == 9)
                        {
                            Close();
                            ResultWindow result = new ResultWindow(isMultyPlayer, "B");
                            result.Show();
                            return;
                        }


                        if (boardSize == 9)
                        {
                            Close();
                            ResultWindow result = new ResultWindow(isMultyPlayer, "C");
                            result.Show();
                            return;
                        }

                        if (found)
                        {
                            return;
                        }

                    }

                }
            }
            else
            {
                
                if (fillArray(b, PLAYER_B))
                {
                    if (isXOX(PLAYER_B))
                    {
                        Close();
                        ResultWindow result = new ResultWindow(isMultyPlayer, "B");
                        result.Show();
                    }
                    else if (boardSize == 9)
                    {
                        Close();
                        ResultWindow result = new ResultWindow(isMultyPlayer, "C");
                        result.Show();
                    }
                    current_player = PLAYER_A;
                }
            }

        }

        private Boolean fillArray(Button b, string val) { 
            boardSize++;
            switch (b.Name) {
                case "buttonOne":
                    return updateBoardContent(0, 0, b, val);
                case "buttonTwo":
                    return updateBoardContent(0, 1, b, val);
                case "buttonThree":
                    return updateBoardContent(0, 2, b, val);
                case "buttonFour":
                    return updateBoardContent(1, 0, b, val);
                case "buttonFive":
                    return updateBoardContent(1, 1, b, val);
                case "buttonSix":
                    return updateBoardContent(1, 2, b, val);
                case "buttonSeven":
                    return updateBoardContent(2, 0, b, val);
                case "buttonEight":
                    return updateBoardContent(2, 1, b, val);
                case "buttonNine":
                    return updateBoardContent(2, 2, b, val);
            }
            return false;
            
        }

        public Boolean updateBoardContent(int i, int j, Button b, string val) {
            if (board[i, j] != DEFAULT_CONTENT)
            {
                return false;
            }
            b.Content = val;
            GameLogger.WriteLog(val, 2, 2);
            board[i, j] = val;
            return true;
        }

        private Boolean isXOX(string val) {

            //Horizontal traverse
            for (int i = 0; i < 3; i++) {
                int count = 0;
                for (int j = 0; j < 3; j++) {
                    if (board[i, j] == val)
                    {
                        count++;
                    }
                    else {
                        break;
                    }                   
                }
                if (count == 3) { 
                    return true;
                }
            }

            //Vertical traverse

            for (int i = 0; i < 3; i++)
            {
                int count = 0;
                for (int j = 0; j < 3; j++)
                {
                    if (board[j,i] == val)
                    {
                        count++;
                    }
                    else
                    {
                        break;
                    }

                }
                if (count == 3)
                {
                    return true;
                }
            }

            //diagonal traverse
            if (board[1, 1] == val) {
                if (board[0, 0] == val && board[2, 2] == val) {
                    return true;
                } else if (board[2,0] == val && board[0,2] ==val)
                {
                    return true;
                }
            }

            return false;

        }

        public static void closeWindow() { 
            Game.closeWindow();
        }

        private void findAppropriateMove() { 
           
        }

        private int FindHorizontalOccupancy() {
            
            int bIndex = 0;
            for (int i = 0; i < 3; i++)
            {
                
                if (board[i, 0] != PLAYER_A && board[i, 1] != PLAYER_A && board[i, 2] != PLAYER_A)
                {
                    bIndex = i==0 ? 1 : i==1 ? 4 :7; 
                    
                    if (board[i, 0] == DEFAULT_CONTENT)
                    {
                        if (board[i, 1].Equals(PLAYER_B) && board[i, 2].Equals(PLAYER_B))
                        {
                            board[i, 0] = PLAYER_B;
                            return bIndex;
                        }

                    }
                    if (board[i, 1] == DEFAULT_CONTENT)
                    {
                        bIndex = bIndex + 1;
                        if (board[i, 0].Equals(PLAYER_B) && board[i, 2].Equals(PLAYER_B))
                        {
                            board[i, 1] = PLAYER_B;
                            return bIndex;
                        }
                    }
                    if (board[i, 2] == DEFAULT_CONTENT)
                    {
                        bIndex = bIndex + 2;
                        if (board[i, 0].Equals(PLAYER_B) && board[i, 1].Equals(PLAYER_B))
                        {
                            board[i, 2] = PLAYER_B;
                            return bIndex;
                        }

                    }

                }

                if (board[0, i] != PLAYER_A && board[1, i] != PLAYER_A && board[2, i] != PLAYER_A)
                {
                    
                    if (board[0, i] == DEFAULT_CONTENT)
                    {
                        bIndex = i + 1;
                        if (board[1, i].Equals(PLAYER_B) && board[2,i].Equals(PLAYER_B))
                        {
                            board[0,i] = PLAYER_B;
                            return bIndex;
                        }

                    }
                    if (board[1, i] == DEFAULT_CONTENT)
                    {

                        bIndex = bIndex = i + 4;
                        if (board[0, i].Equals(PLAYER_B) && board[2, i].Equals(PLAYER_B))
                        {
                            board[1, i] = PLAYER_B;
                            return bIndex;
                        }

                    }
                    if (board[2, i] == DEFAULT_CONTENT)
                    {

                        bIndex = bIndex = i + 7;
                        if (board[0, i].Equals(PLAYER_B) && board[1, i].Equals(PLAYER_B))
                        {
                            board[0, i] = PLAYER_B;
                            return bIndex;
                        }

                    }

                }

            }
            return bIndex;
        }

        private void ComputerPlay(int i) { 
            
            switch (i)
            {
                case 1: buttonOne.Content = PLAYER_B;
                    board[0, 0] = PLAYER_B;
                    GameLogger.WriteLog(PLAYER_B, 0, 0);
                    break;
                case 2:
                    buttonTwo.Content = PLAYER_B;
                    board[0, 1] = PLAYER_B;
                    GameLogger.WriteLog(PLAYER_B, 0, 1);
                    break;
                case 3:
                    buttonThree.Content = PLAYER_B;
                    board[0, 2] = PLAYER_B;
                    GameLogger.WriteLog(PLAYER_B, 0, 2);
                    break;
                case 4:
                    buttonFour.Content = PLAYER_B;
                    board[1, 0] = PLAYER_B;
                    GameLogger.WriteLog(PLAYER_B, 1, 0);
                    break;
                case 5:
                    buttonFive.Content = PLAYER_B;
                    board[1, 1] = PLAYER_B;
                    GameLogger.WriteLog(PLAYER_B, 1, 1);
                    break;
                case 6:
                    buttonSix.Content = PLAYER_B;
                    board[1, 2] = PLAYER_B;
                    GameLogger.WriteLog(PLAYER_B, 1, 2);
                    break;
                case 7:
                    buttonSeven.Content = PLAYER_B;
                    board[2, 0] = PLAYER_B;
                    GameLogger.WriteLog(PLAYER_B, 2, 0);
                    break;
                case 8:
                    buttonEight.Content = PLAYER_B;
                    board[2, 1] = PLAYER_B;
                    GameLogger.WriteLog(PLAYER_B, 2, 1);
                    break;
                case 9:
                    buttonNine.Content = PLAYER_B;
                    board[2, 2] = PLAYER_B;
                    GameLogger.WriteLog(PLAYER_B, 2, 2);
                    break;
            }
        }

    }
}
