using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TicTacToe
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string[,] boardArray = new string[3, 3];
        string turn = "";
        int gameStep = 0;        
        bool isWin=false;
        int xWinCount = 0;
        int oWinCount = 0;
        string MessageBoxTitle = "Continue/Exit";
        string MessageBoxContent = "Want to Play Again?";
        void initial()
        {
            xWinCount = 0;
            oWinCount = 0;
            label6.Text = xWinCount.ToString();
            label8.Text = oWinCount.ToString();
            button4.Text = "Select Axis";
            resetGame();           
        }
        private void Form1_Load(object sender, EventArgs e)
        {            
            turn = "";
            initial();
        }
        void PrintBoard(string toFill = "")
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
            richTextBox1.Clear();
            for (int i = 0; i <= boardArray.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= boardArray.GetUpperBound(1); j++)
                {
                    if (toFill == " - ")
                    {
                        boardArray[i, j] = toFill;
                    }
                    richTextBox1.Text += boardArray[i,j];
                }
                richTextBox1.Text += "\n";
            }
        }
        string randomTurn()
        {
            Random randomObj = new Random();
            int randomNum = randomObj.Next(2);
            if (randomNum == 0)
            {
                turn = " X ";
            }
            else
            {
                turn = " O ";
            }
            return turn;
        }

        void clickEvent(int i, int j)
        {
            if (checkValidMove(i, j))
            {             
                CheckTie(gameStep);
                //gameStep++;
                if ((turn == " X ") && gameStep <= 8)
                {
                    boardArray[i, j] = " X ";
                    gameStep++;
                    PrintBoard(" X ");                    
                    if (winCheck(turn) == "")
                    {
                        turn = " O ";
                        button4.Text = turn + " turn";
                    }
                    return;
                }
                else if ((turn == " O ") && gameStep <= 8)
                {
                    boardArray[i, j] = " O ";
                    PrintBoard(" O ");
                    gameStep++;
                    if (winCheck(turn) == "")
                    {
                        turn = " X ";
                        button4.Text = turn + " turn";
                    }
                    return;
                }                 
            }
        }
       
        bool checkValidMove(int i, int j)
        {
            if (boardArray[i,j] == " X " || boardArray[i, j] ==" O ")
                {
                MessageBox.Show("Please make a valid move!");
                return false;               
                }
                else
                {
                  return true;
                }
        }

        void winEvent(string marker)
        {
            label1.Text = $"{marker} win!!! ,Game Over";
            if (marker==" X ")
            {
                xWinCount++;
                isWin = true;
            }
            else
            {
                oWinCount++;
                isWin = true;
            }
            label6.Text =xWinCount .ToString();
            label8.Text =oWinCount .ToString();
            
            //gameStep = 0;
            DialogResult dialogResult = MessageBox.Show(MessageBoxContent, MessageBoxTitle, MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {               
                resetGame();
                label1.Text = "Click Start Button To Start Game";
            }
            else if (dialogResult == DialogResult.No)
            {
                initial();               
            }
        }
        string winCheck(string marker)
        {
            // Check horizontal rows
            for (int i = 0; i < 3; i++)
            {
                if (boardArray[i, 0] == marker && boardArray[i, 1] == marker && boardArray[i, 2] == marker)
                {                    
                    winEvent(marker); 
                }
            }
            // Check vertical columns
            for (int j = 0; j < 3; j++)
            {
                if (boardArray[0, j] == marker && boardArray[1, j] == marker && boardArray[2, j] == marker)
                {                    
                    winEvent(marker);
                }
            }

            // Check diagonal from top-left to bottom-right
            if (boardArray[0, 0] == marker && boardArray[1, 1] == marker && boardArray[2, 2] == marker)
            {                
                winEvent(marker);
            }

            // Check diagonal from top-right to bottom-left
            if (boardArray[0, 2] == marker && boardArray[1, 1] == marker && boardArray[2, 0] == marker)
            {               
                winEvent(marker);
            }
            return "";
        }
        void CheckTie(int stepCount)
        {
            // Check if board is all filled and no player won
            if ((stepCount == 9) && !(isWin))
            {
                label1.Text = "Draw!!!";
                DialogResult dialogResult = MessageBox.Show("Play Again?", "Restart", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    resetGame();
                    label1.Text = "Click Start Button To Start Game";
                }
                else if (dialogResult == DialogResult.No)
                {
                    initial();
                }
            }
            else
            {
                return;
            }

        }

        void resetGame()
        {
            string[,] boardArray = new string[3, 3];
            richTextBox1.Clear();
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
            PrintBoard(" - ");
            gameStep = 0;           
            textBox1.Clear();
            textBox2.Clear(); 
            isWin = false;
            turn = "";
            button4.Text = "Select Axis";
            label1.Text = "Click Start Button To Start Game";
        }        

        private void button4_Click(object sender, EventArgs e)
        {
            
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Please input data ");
                return;
            }
            int x = Convert.ToInt32(textBox1.Text);
            int y = Convert.ToInt32(textBox2.Text);
            if (y == 1)
            {
                clickEvent(y+1,x-1); return; 
            }
            else if (y == 2)
            {
                clickEvent (y-1,x-1); return;
            }
            else if (y == 3)
            {
                clickEvent (y-3,x-1); return;
            }
            else{
                MessageBox.Show("Please input data from 1 to 3");
            }
        }

        private void PlayAgain_Click(object sender, EventArgs e)
        {
            resetGame();
            turn = randomTurn();
            button4.Text = turn + " turn";
        }

        private void Start_Click(object sender, EventArgs e)
        {
            initial();
            turn = randomTurn();
            button4.Text = turn + " turn";
        }       

        private void Exit_Click(object sender, EventArgs e)
        {
            initial();            
        }
    }
}
    

