using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for ResultWindow.xaml
    /// </summary>
    /// 
    
    public partial class ResultWindow : Window
    {
        private Boolean Multyplayer;
        private string winner;
        private Game gameWindow;



        public ResultWindow(Boolean multyplayer, string winner, Game window)
        {
            InitializeComponent();
            Multyplayer = multyplayer;
            this.gameWindow = window;
            if (winner != "C")
            {
                this.winner = winner;
                win.Content = "Player " + winner + " Won!";
            }
            else
            {
                win.Content = "Its a Draw!";
            }



        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.gameWindow.ResetGame();
            this.Close();


        }

        private void Exit_Button_Click(object sender, RoutedEventArgs e) { 
            this.gameWindow.Close();
            this.Close();
        }



    }
}
