﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace ClassLibrary
{
    public class Game
    {
        bool isPlayerXturn;
        Grid mainGrid;

        public Game(Grid MainGrid)
        {
            this.isPlayerXturn = false;
            mainGrid = MainGrid;
            CreateGrid(3);
        }

        public void move(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;

            if(!b.Tag.Equals("button"))
                return;
            if (isPlayerXturn)
            {
                b.Content = "x";
                b.Tag = "x";
            }
            else
            {
                b.Content = "o";
                b.Tag = "o";
            }
            b.Background = Brushes.Beige;
            isPlayerXturn = !isPlayerXturn;
            Check();
        }

        public void Check()
        {

            int size = Convert.ToInt16(Math.Sqrt(mainGrid.Children.Count));
            Button[] buttons = mainGrid.Children.Cast<Button>().ToArray<Button>();
            char[,] arr = new char[size,size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    switch (buttons[size*i + j].Tag)
                    {
                        case "button":
                            arr[i, j] = ' ';
                            break;
                        case "x":
                            arr[i, j] = 'x';
                            break;
                        case "o":
                            arr[i, j] = 'o';
                            break;
                    }
                }
            }
            int[,] result = MatrixLogics.checkForWin(arr);
            foreach (int i in result)
            {
                if(i == 1)
                {

                    break;
                }
                    
                if(i == 2)
                {

                    break;
                }
            }
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if(result[i,j]!=0)
                    buttons[size * i + j].Content = result[i, j];
                }
            }
        }

        public void CreateGrid(int size)
        {
            MatrixLogics.Reset();

            mainGrid.Children.Clear();
            mainGrid.RowDefinitions.Clear();
            mainGrid.ColumnDefinitions.Clear();

            RowDefinition rd;
            ColumnDefinition cd;

            for(int i = 0; i < size; i++)
            {
                rd = new RowDefinition();
                rd.Height = new GridLength(1,GridUnitType.Star);
                mainGrid.RowDefinitions.Add(rd);
                cd = new ColumnDefinition();
                cd.Width = new GridLength(1, GridUnitType.Star);
                mainGrid.ColumnDefinitions.Add(cd);
            }


            

            for(int i = 0; i < size; i++)
            {

                for (int j = 0; j < size; j++)
                {

                    Button b = new Button();
                    b.Tag = "button";
                    b.FontFamily=new FontFamily("Comic Sans MS");
                    b.FontWeight = FontWeights.Bold;
                    b.FontSize = 200/size;
                    b.Foreground = Brushes.Orange;
                    b.Background = Brushes.White;
                    Style s = new Style();
                    
                    //Trigger t = new Trigger();
                    //t.Property = TextBlock.IsMouseOverProperty;
                    //t.Value = true;
                    //Setter setter = new Setter(TextBlock.ForegroundProperty, Brushes.Red);
                    //t.Setters.Add(setter);
                    //s.Triggers.Add(t);
                    b.Style = s;
                    b.Click += move;
                    Grid.SetRow(b, i);
                    Grid.SetColumn(b, j);
                    mainGrid.Children.Add(b);
                }

            }
        }
    }
}
