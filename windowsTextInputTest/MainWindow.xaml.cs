using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace windowsTextInputTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //***VARIABLES**//
        bool isTyping = false;
        uint move = 0;
        int CurrentDirection = (int)Direction.None;

        enum Direction { None, Clockwise, CounterClockwise};
       
        Button previousButton = null;
        Button currentButton = null;
        Button startButton = null;
        Button[] routingTable = new Button[4];

        char[] UpClockwise = {'i', 'd', 'g', 'z'};
        char[] UpCounter =  {'y', 'x', 'k', '\'' };

        char[] RightClockwise = {'o', 'u', 'w', '!'};
        char[] RightCounter =  {'a', 'r', 'f', '?'};

        char[] DownClockwise = { 'e', 'l', 'p', 'q' };
        char[] DownCounter = {'t', 'h', 'b', '@'};

        char[] LeftClockwise = { '.', 's', 'c', 'v' };
        char[] LeftCounter = {',', 'n', 'm', 'j' };
        //**************//

        public MainWindow()
        {
            InitializeComponent();
            textBox1.Focus();
            textBox1.FontSize = 16;
        }

        private void textInputRouting(Button activeButton)
        {
            if (isTyping)
            {
                if (currentButton != activeButton)
                {
                    if (previousButton == activeButton)
                    {
                        if (move > 0)
                        {
                            //move -= 1;
                            startButton = activeButton;
                            currentButton = activeButton;
                            previousButton = null;
                            //CurrentDirection = (int)Direction.None;
                            move = 0;
                        }
                    }
                    else
                    {
                        if (move == 0)
                        {
                            //**BUTTON_UP**//
                            if (startButton == buttonUp && activeButton == buttonRight)
                            {
                                CurrentDirection = (int)Direction.Clockwise;
                            }
                            else if (startButton == buttonUp && activeButton == buttonLeft)
                            {
                                CurrentDirection = (int)Direction.CounterClockwise;
                            }
                            //***BUTTON_RIGHT**//
                            else if (startButton == buttonRight && activeButton == buttonDown)
                            {
                                CurrentDirection = (int)Direction.Clockwise;
                            }
                            else if (startButton == buttonRight && activeButton == buttonUp)
                            {
                                CurrentDirection = (int)Direction.CounterClockwise;
                            }
                            //**BUTTON_DOWN**//
                            else if (startButton == buttonDown && activeButton == buttonLeft)
                            {
                                CurrentDirection = (int)Direction.Clockwise;
                            }
                            else if (startButton == buttonDown && activeButton == buttonRight)
                            {
                                CurrentDirection = (int)Direction.CounterClockwise;
                            }
                            //**BUTTON_LEFT**//
                            else if (startButton == buttonLeft && activeButton == buttonUp)
                            {
                                CurrentDirection = (int)Direction.Clockwise;
                            }
                            else if (startButton == buttonLeft && activeButton == buttonDown)
                            {
                                CurrentDirection = (int)Direction.CounterClockwise;
                            }

                            move += 1;
                        }
                        else if (move >= 4)
                        {
                            move = 1;
                            //CurrentDirection = (int)Direction.None;
                        }
                        else
                        {
                            move += 1;
                        }
                    }

                    previousButton = currentButton;
                    currentButton = activeButton;
                }
            }
            else
            {
                isTyping = true;
                startButton = activeButton;
                currentButton = activeButton;
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            textBox1.Focus();
            int index = textBox1.SelectionStart;
            textBox1.Text = textBox1.Text.Insert(textBox1.CaretIndex, "A");
            textBox1.Select(index + 1, 1);
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            textBox1.Focus();
            int index = textBox1.SelectionStart;
            
            if (index > 0)
            {
                textBox1.Text = textBox1.Text.Remove(index - 1, 1);
                textBox1.Select(index - 1, 1);
            }
        }

        //***TEXT_INPUT_BUTTONS***//
        private void buttonUp_MouseEnter(object sender, MouseEventArgs e)
        {
           // MessageBox.Show("buttonUP_MouseEnter");
            textInputRouting(buttonUp);
        }

        private void buttonRight_MouseEnter(object sender, MouseEventArgs e)
        {
            //MessageBox.Show("buttonRight_MouseEnter");
            textInputRouting(buttonRight);
        }

        private void buttonDown_MouseEnter(object sender, MouseEventArgs e)
        {
            //MessageBox.Show("buttonDown_MouseEnter");
            textInputRouting(buttonDown);
        }

        private void buttonLeft_MouseEnter(object sender, MouseEventArgs e)
        {
            //MessageBox.Show("buttonLeft_MouseEnter");
            textInputRouting(buttonLeft);
        }

        private char charFinder()
        {
            //**BUTTON_UP**//
            if (startButton == buttonUp && CurrentDirection == (int)Direction.Clockwise)
            {
                return UpClockwise[move - 1];
            }
            else if (startButton == buttonUp && CurrentDirection == (int)Direction.CounterClockwise)
            {
                return UpCounter[move - 1];
            }
            //***BUTTON_RIGHT**//
            else if (startButton == buttonRight && CurrentDirection == (int)Direction.Clockwise)
            {
                return RightClockwise[move - 1];
            }
            else if (startButton == buttonRight && CurrentDirection == (int)Direction.CounterClockwise)
            {
                return RightCounter[move - 1];
            }
            //**BUTTON_DOWN**//
            else if (startButton == buttonDown && CurrentDirection == (int)Direction.Clockwise)
            {
               return DownClockwise[move - 1];
            }
            else if (startButton == buttonDown && CurrentDirection == (int)Direction.CounterClockwise)
            {
                return DownCounter[move - 1];
            }
            //**BUTTON_LEFT**//
            else if (startButton == buttonLeft && CurrentDirection == (int)Direction.Clockwise)
            {
                return LeftClockwise[move - 1];
            }
            else if (startButton == buttonLeft && CurrentDirection == (int)Direction.CounterClockwise)
            {
                return LeftCounter[move - 1];
            }

            return 'X';
        }

        private void buttonCenter_MouseEnter(object sender, MouseEventArgs e)
        {
            //MessageBox.Show(sender.ToString());

            if (isTyping && move > 0)
            {
                textBox1.Focus();
                int index = textBox1.SelectionStart;
                textBox1.Text = textBox1.Text.Insert(textBox1.CaretIndex, "" + charFinder());
                textBox1.Select(index + 1, 1);
            }

            move = 0;
            isTyping = false;
            previousButton = null;
            currentButton = null;
            startButton = null;
            CurrentDirection = (int)Direction.None;
        }
    }
}