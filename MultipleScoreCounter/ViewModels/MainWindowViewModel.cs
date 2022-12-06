using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Reactive;
using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using MultipleScoreCounter.Models;
using MultipleScoreCounter.Views;
using ReactiveUI;
using static System.Math;

namespace MultipleScoreCounter.ViewModels
{
    public sealed class MainWindowViewModel : INotifyPropertyChanged
    {
        //public ReactiveCommand<Unit, Unit> NewGameCommand { get; }
        public ReactiveCommand<Unit, Unit> ExitCommand { get; }
        public ReactiveCommand<Unit, Unit> StartPreviousGameCommand { get; }
        public ReactiveCommand<Unit, Unit> StartNewGameCommand { get; }
        public object FirstGame => _firstGame;
        private bool _firstGame;
        private List<Player> Players { get; }
        private List<Card> cardDatabase { get; }

        /**
         * Slider Label Text property
         */
        public object SliderText => (Floor((double)_sliderValue).ToString(CultureInfo.InvariantCulture));

        /**
         * Slider value
         */
        private object _sliderValue;

        /**
         * Slider value property
         */
        public object SliderValue
        {
            get => _sliderValue;
            set
            {
                _sliderValue = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SliderValue)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SliderText)));
            }
        }

        /**
         * Main Window Constructor
         */
        public MainWindowViewModel()
        {
            this.cardDatabase = new List<Card>();
            Players = new List<Player>();
            _sliderValue = 1;
            _firstGame = true;
            ExitCommand = ReactiveCommand.Create(Exit);
            StartNewGameCommand = ReactiveCommand.Create(StartNewGame);
            StartPreviousGameCommand = ReactiveCommand.Create(StartPreviousGame);
        }

        private void fillCardDatabase()
        {
            cardDatabase.Add(new Card(
                "Škrty v rozpočtu - veřejná správa",
                21,
                1,
                new List<Tuple<int, int>>
                {
                    Tuple.Create(0, 3),
                    Tuple.Create(6, 2),
                    Tuple.Create(8, 1),
                    Tuple.Create(9, 1),
                    Tuple.Create(10, 2),
                    Tuple.Create(11, 1),
                    Tuple.Create(14, 2),
                    Tuple.Create(17, -5),
                }));
            cardDatabase.Add(new Card(
                "Škrty v rozpočtu - sociální a zdravotní",
                3,
                12,
                new List<Tuple<int, int>>
                {
                    Tuple.Create(0, 10),
                    Tuple.Create(4, 2),
                    Tuple.Create(5, -1),
                    Tuple.Create(6, -2),
                    Tuple.Create(8, -2),
                    Tuple.Create(9, -3),
                    Tuple.Create(10, -2),
                    Tuple.Create(14, -3),
                }));
            cardDatabase.Add(new Card(
                "Škrty v rozpočtu - zemědělství + ŽP",
                15,
                5,
                new List<Tuple<int, int>>
                {
                    Tuple.Create(0, 2),
                    Tuple.Create(5, -2),
                    Tuple.Create(6, 1),
                    Tuple.Create(9, 1),
                    Tuple.Create(10, 2),
                    Tuple.Create(12, -3),
                    Tuple.Create(14, 1),
                }));
            cardDatabase.Add(new Card(
                "Škrty v rozpočtu - věda, školství, kultura",
                -30,
                25,
                new List<Tuple<int, int>>
                {
                    Tuple.Create(0, -3),
                    Tuple.Create(5, -1),
                    Tuple.Create(6, 2),
                    Tuple.Create(8, -3),
                    Tuple.Create(10, 1),
                    Tuple.Create(11, -2),
                    Tuple.Create(14, 1),
                }));
            cardDatabase.Add(new Card(
                "Škrty v rozpočtu - doprava + obchod",
                5,
                3,
                new List<Tuple<int, int>>
                {
                    Tuple.Create(0, -2),
                    Tuple.Create(4, -2),
                    Tuple.Create(7, 2),
                    Tuple.Create(10, 1),
                    Tuple.Create(14, 2),
                }));
            cardDatabase.Add(new Card(
                "Škrty v rozpočtu - zahraničí + spravedlnost",
                14,
                2,
                new List<Tuple<int, int>>
                {
                    Tuple.Create(0, 3),
                    Tuple.Create(1, -1),
                    Tuple.Create(5, -2),
                    Tuple.Create(6, 2),
                    Tuple.Create(10, 2),
                    Tuple.Create(11, -3),
                    Tuple.Create(14, 3),
                }));
            cardDatabase.Add(new Card(
                "Navýšení rozpočtu - bezpečnost",
                10,
                2,
                new List<Tuple<int, int>>
                {
                    Tuple.Create(0, -5),
                    Tuple.Create(1, 2),
                    Tuple.Create(2, -1),
                    Tuple.Create(3, 1),
                    Tuple.Create(8, 1),
                    Tuple.Create(15, 3),
                }));
            cardDatabase.Add(new Card(
                "Navýšení rozpočtu - veřejná správa",
                15,
                2,
                new List<Tuple<int, int>>
                {
                    Tuple.Create(0, -4),
                    Tuple.Create(4, 2),
                    Tuple.Create(7, 2),
                    Tuple.Create(15, 2),
                    Tuple.Create(17, 3),
                }));
            cardDatabase.Add(new Card(
                "Navýšení rozpočtu - sociální a zdravotní",
                30,
                2,
                new List<Tuple<int, int>>
                {
                    Tuple.Create(0, 2),
                    Tuple.Create(1, -3),
                    Tuple.Create(4, -2),
                    Tuple.Create(6, 3),
                    Tuple.Create(9, 3),
                    Tuple.Create(10, 2),
                    Tuple.Create(14, 3),
                }));
            cardDatabase.Add(new Card(
                "Navýšení rozpočtu - zemědělství + ŽP",
                3,
                2,
                new List<Tuple<int, int>>
                {
                    Tuple.Create(0, -5),
                    Tuple.Create(5, 1),
                    Tuple.Create(10, -1),
                    Tuple.Create(12, 3),
                    Tuple.Create(14, 1),
                }));
            cardDatabase.Add(new Card(
                "Navýšení rozpočtu - věda, školství, kultura",
                8,
                2,
                new List<Tuple<int, int>>
                {
                    Tuple.Create(0, -7),
                    Tuple.Create(1, 2),
                    Tuple.Create(5, 3),
                    Tuple.Create(8, 2),
                    Tuple.Create(10, -1),
                    Tuple.Create(11, 3),
                }));
            cardDatabase.Add(new Card(
                "Navýšení rozpočtu - doprava + obchod",
                10,
                2,
                new List<Tuple<int, int>>
                {
                    Tuple.Create(0, -3),
                    Tuple.Create(1, 2),
                    Tuple.Create(4, 2),
                    Tuple.Create(12, 1),
                }));
            cardDatabase.Add(new Card(
                "Navýšení rozpočtu - zahraničí + spravedlnost",
                15,
                2,
                new List<Tuple<int, int>>
                {
                    Tuple.Create(0, -2),
                    Tuple.Create(1, 2),
                    Tuple.Create(2, 1),
                    Tuple.Create(11, 1),
                }));
            cardDatabase.Add(new Card(
                "Sleva na poplatníka",
                12,
                2,
                new List<Tuple<int, int>>
                {
                    Tuple.Create(0, -2),
                    Tuple.Create(1, 2),
                    Tuple.Create(4, 3),
                    Tuple.Create(7, 1),
                    Tuple.Create(8, 1),
                    Tuple.Create(9, 1),
                    Tuple.Create(12, 1),
                    Tuple.Create(17, 1),
                }));
            cardDatabase.Add(new Card(
                "Manželství pro všechny",
                2,
                0,
                new List<Tuple<int, int>>
                {
                    Tuple.Create(0, 1),
                    Tuple.Create(2, 5),
                    Tuple.Create(3, -1),
                    Tuple.Create(5, 2),
                    Tuple.Create(11, 2),
                    Tuple.Create(13, 3),
                    Tuple.Create(14, -3),
                }));
            cardDatabase.Add(new Card(
                "Vyznamenání za věrnost",
                3,
                5,
                new List<Tuple<int, int>>
                {
                    Tuple.Create(0, -1),
                    Tuple.Create(2, -3),
                    Tuple.Create(6, 1),
                    Tuple.Create(8, 1),
                    Tuple.Create(11, -1),
                    Tuple.Create(13, 4),
                    Tuple.Create(14, 2),
                }));
            cardDatabase.Add(new Card(
                "Ochrana hranic",
                8,
                5,
                new List<Tuple<int, int>>
                {
                    Tuple.Create(0, -2),
                    Tuple.Create(3, -2),
                    Tuple.Create(4, -1),
                    Tuple.Create(6, 2),
                    Tuple.Create(7, 1),
                    Tuple.Create(11, -1),
                    Tuple.Create(14, 5),
                    Tuple.Create(15, 2),
                    Tuple.Create(16, -1),
                }));
            cardDatabase.Add(new Card(
                "Podpora odborů",
                2,
                0,
                new List<Tuple<int, int>>
                {
                    Tuple.Create(0, 2),
                    Tuple.Create(1, -4),
                    Tuple.Create(4, -5),
                    Tuple.Create(7, 4),
                    Tuple.Create(8, 1),
                    Tuple.Create(9, 2),
                    Tuple.Create(10, 1),
                    Tuple.Create(12, -1),
                    Tuple.Create(16, 1),
                    Tuple.Create(17, 1),
                }));
            cardDatabase.Add(new Card(
                "Reforma zákoníku práce (seškrtání)",
                5,
                10,
                new List<Tuple<int, int>>
                {
                    Tuple.Create(0, 2),
                    Tuple.Create(1, 3),
                    Tuple.Create(4, 4),
                    Tuple.Create(7, -5),
                    Tuple.Create(9, -4),
                    Tuple.Create(12, 2),
                    Tuple.Create(17, -1),
                }));
            cardDatabase.Add(new Card(
                "Podpora veřejného vzdělání",
                15,
                1,
                new List<Tuple<int, int>>
                {
                    Tuple.Create(0, -5),
                    Tuple.Create(5, 5),
                    Tuple.Create(7, 4),
                    Tuple.Create(8, 2),
                    Tuple.Create(9, 1),
                    Tuple.Create(10, 3),
                    Tuple.Create(11, 1),
                    Tuple.Create(15, -1),
                    Tuple.Create(17, 2),
                }));
            cardDatabase.Add(new Card(
                "Školné",
                -10,
                12,
                new List<Tuple<int, int>>
                {
                    Tuple.Create(0, 8),
                    Tuple.Create(5, -3),
                    Tuple.Create(7, -4),
                    Tuple.Create(8, -2),
                    Tuple.Create(9, -4),
                    Tuple.Create(10, -2),
                    Tuple.Create(15, 2),
                }));
            cardDatabase.Add(new Card(
                "Izolacionismus",
                7,
                6,
                new List<Tuple<int, int>>
                {
                    Tuple.Create(0, 4),
                    Tuple.Create(1, -3),
                    Tuple.Create(4, -2),
                    Tuple.Create(5, -1),
                    Tuple.Create(6, 3),
                }));
            cardDatabase.Add(new Card(
                "Prohlubování zahraniční spolupráce - Čína",
                -2,
                3,
                new List<Tuple<int, int>>
                {
                    Tuple.Create(0, 2),
                    Tuple.Create(1, 1),
                    Tuple.Create(2, -1),
                    Tuple.Create(4, 3),
                    Tuple.Create(5, -1),
                    Tuple.Create(7, 1),
                    Tuple.Create(11, -2),
                    Tuple.Create(14, 1),
                }));
            cardDatabase.Add(new Card(
                "Prohlubování zahraniční spolupráce - Rusko",
                -5,
                3,
                new List<Tuple<int, int>>
                {
                    Tuple.Create(0, 1),
                    Tuple.Create(2, -3),
                    Tuple.Create(4, 2),
                    Tuple.Create(5, -2),
                    Tuple.Create(6, 4),
                    Tuple.Create(7, 1),
                    Tuple.Create(11, -4),
                    Tuple.Create(12, 1),
                    Tuple.Create(13, 2),
                    Tuple.Create(14, 5),
                    Tuple.Create(15, -2),
                    Tuple.Create(16, -1),
                }));
            cardDatabase.Add(new Card(
                "Dotace firmám",
                9,
                5,
                new List<Tuple<int, int>>
                {
                    Tuple.Create(0, -4),
                    Tuple.Create(1, 5),
                    Tuple.Create(4, 4),
                    Tuple.Create(7, -2),
                    Tuple.Create(12, 2),
                }));
            cardDatabase.Add(new Card(
                "Digitalizace",
                45,
                15,
                new List<Tuple<int, int>>
                {
                    Tuple.Create(0, -3),
                    Tuple.Create(1, 3),
                    Tuple.Create(4, 4),
                    Tuple.Create(5, 2),
                    Tuple.Create(11, 2),
                    Tuple.Create(16, 3),
                    Tuple.Create(17, 4),
                }));
            cardDatabase.Add(new Card(
                "Podpora IZS",
                13,
                5,
                new List<Tuple<int, int>>
                {
                    Tuple.Create(0, -3),
                    Tuple.Create(7, 1),
                    Tuple.Create(8, 2),
                    Tuple.Create(9, 1),
                    Tuple.Create(10, 3),
                    Tuple.Create(11, 2),
                    Tuple.Create(17, 4),
                }));
            cardDatabase.Add(new Card(
                "Privatizace IZS",
                -35,
                60,
                new List<Tuple<int, int>>
                {
                    Tuple.Create(0, 8),
                    Tuple.Create(1, 2),
                    Tuple.Create(7, -3),
                    Tuple.Create(9, -3),
                    Tuple.Create(10, -5),
                    Tuple.Create(11, -3),
                    Tuple.Create(17, -2),
                }));
            cardDatabase.Add(new Card(
                "Podpora bezpečnostních složek",
                3,
                8,
                new List<Tuple<int, int>>
                {
                    Tuple.Create(0, -3),
                    Tuple.Create(1, 2),
                    Tuple.Create(5, -2),
                    Tuple.Create(6, 1),
                    Tuple.Create(7, -1),
                    Tuple.Create(14, 3),
                    Tuple.Create(15, 5),
                    Tuple.Create(17, 1),
                }));
            cardDatabase.Add(new Card(
                "Sleva pro studenty/důchodce na mhd",
                10,
                8,
                new List<Tuple<int, int>>
                {
                    Tuple.Create(0, -3),
                    Tuple.Create(5, 3),
                    Tuple.Create(6, 4),
                    Tuple.Create(7, 1),
                    Tuple.Create(8, 2),
                    Tuple.Create(9, 3),
                    Tuple.Create(11, -3),
                }));
            cardDatabase.Add(new Card(
                "Uhlíková daň",
                15,
                10,
                new List<Tuple<int, int>>
                {
                    Tuple.Create(0, 15),
                    Tuple.Create(1, -4),
                    Tuple.Create(4, -3),
                    Tuple.Create(5, 2),
                    Tuple.Create(7, -2),
                    Tuple.Create(8, -3),
                    Tuple.Create(9, -3),
                    Tuple.Create(10, -1),
                    Tuple.Create(11, 4),
                    Tuple.Create(12, -4),
                    Tuple.Create(14, -5),
                }));
            cardDatabase.Add(new Card(
                "Podpora ekonomických diplomatických misí",
                -10,
                4,
                new List<Tuple<int, int>>
                {
                    Tuple.Create(0, -2),
                    Tuple.Create(1, 2),
                    Tuple.Create(4, 1),
                    Tuple.Create(5, 3),
                    Tuple.Create(9, -2),
                    Tuple.Create(10, -1),
                    Tuple.Create(11, 2),
                    Tuple.Create(12, 1),
                    Tuple.Create(14, -2),
                }));
            cardDatabase.Add(new Card(
                "Zvýšení daně na alko/tabák",
                30,
                10,
                new List<Tuple<int, int>>
                {
                    Tuple.Create(0, 11),
                    Tuple.Create(2, -1),
                    Tuple.Create(5, -2),
                    Tuple.Create(6, -1),
                    Tuple.Create(10, -2),
                    Tuple.Create(14, -3),
                    Tuple.Create(15, -1),
                }));
            cardDatabase.Add(new Card(
                "Snížení daně na alko/tabák",
                23,
                5,
                new List<Tuple<int, int>>
                {
                    Tuple.Create(0, -2),
                    Tuple.Create(5, 1),
                    Tuple.Create(6, 2),
                    Tuple.Create(10, 3),
                    Tuple.Create(11, -2),
                    Tuple.Create(14, 4),
                }));
            cardDatabase.Add(new Card(
                "Podpora výstavby bytů",
                5,
                10,
                new List<Tuple<int, int>>
                {
                    Tuple.Create(0, -4),
                    Tuple.Create(5, 1),
                    Tuple.Create(7, 1),
                    Tuple.Create(8, 2),
                    Tuple.Create(9, 2),
                    Tuple.Create(10, 1),
                    Tuple.Create(14, 1),
                }));
            cardDatabase.Add(new Card(
                "Církevní restituce",
                14,
                0,
                new List<Tuple<int, int>>
                {
                    Tuple.Create(0, 2),
                    Tuple.Create(1, 1),
                    Tuple.Create(3, 1),
                    Tuple.Create(4, -1),
                    Tuple.Create(5, -1),
                    Tuple.Create(6, 1),
                    Tuple.Create(8, -1),
                    Tuple.Create(9, -3),
                    Tuple.Create(13, 5),
                    Tuple.Create(14, 1),
                }));
            cardDatabase.Add(new Card(
                "Lustrační zákon",
                6,
                0,
                new List<Tuple<int, int>>
                {
                    Tuple.Create(0, 3),
                    Tuple.Create(11, 2),
                    Tuple.Create(14, -2),
                    Tuple.Create(17, -1),
                }));
            cardDatabase.Add(new Card(
                "Výstavba dálnic",
                13,
                7,
                new List<Tuple<int, int>>
                {
                    Tuple.Create(0, -3),
                    Tuple.Create(1, 2),
                    Tuple.Create(4, 1),
                    Tuple.Create(8, 1),
                    Tuple.Create(9, 2),
                    Tuple.Create(12, 2),
                    Tuple.Create(14, 1),
                }));
            cardDatabase.Add(new Card(
                "Výstavba rychlodráhy",
                22,
                2,
                new List<Tuple<int, int>>
                {
                    Tuple.Create(0, 4),
                    Tuple.Create(5, 1),
                    Tuple.Create(6, 1),
                    Tuple.Create(7, 1),
                    Tuple.Create(11, 2),
                }));
            cardDatabase.Add(new Card(
                "Redukce těžkého průmyslu",
                -30,
                30,
                new List<Tuple<int, int>>
                {
                    Tuple.Create(0, -2),
                    Tuple.Create(1, -2),
                    Tuple.Create(3, -3),
                    Tuple.Create(12, 2),
                }));
            cardDatabase.Add(new Card(
                "Podpora kulturních akcí",
                12,
                10,
                new List<Tuple<int, int>>
                {
                    Tuple.Create(0, -8),
                    Tuple.Create(2, 3),
                    Tuple.Create(3, 2),
                    Tuple.Create(5, 2),
                    Tuple.Create(6, 2),
                    Tuple.Create(8, 2),
                    Tuple.Create(9, 1),
                    Tuple.Create(11, 3),
                    Tuple.Create(14, 1),
                }));
            cardDatabase.Add(new Card(
                "Zubařské zákroky v rámci VZP",
                13,
                8,
                new List<Tuple<int, int>>
                {
                    Tuple.Create(0, -5),
                    Tuple.Create(1, -2),
                    Tuple.Create(3, 1),
                    Tuple.Create(4, -1),
                    Tuple.Create(8, 2),
                    Tuple.Create(9, 3),
                    Tuple.Create(10, 2),
                    Tuple.Create(14, 4),
                    Tuple.Create(17, 3),
                }));
            cardDatabase.Add(new Card(
                "Alternativní školy",
                -10,
                3,
                new List<Tuple<int, int>>
                {
                    Tuple.Create(0, -1),
                    Tuple.Create(1, 4),
                    Tuple.Create(2, 1),
                    Tuple.Create(5, 2),
                    Tuple.Create(6, -2),
                    Tuple.Create(7, -3),
                    Tuple.Create(8, 1),
                    Tuple.Create(9, -1),
                    Tuple.Create(11, 3),
                    Tuple.Create(14, -2),
                }));
            cardDatabase.Add(new Card(
                "Zřízení národního parku",
                10,
                1,
                new List<Tuple<int, int>>
                {
                    Tuple.Create(0, 1),
                    Tuple.Create(8, 2),
                    Tuple.Create(14, -1),
                }));
            cardDatabase.Add(new Card(
                "Udržitelné zemědělství",
                7,
                5,
                new List<Tuple<int, int>>
                {
                    Tuple.Create(0, -2),
                    Tuple.Create(6, -1),
                    Tuple.Create(10, -2),
                    Tuple.Create(12, 5),
                    Tuple.Create(14, 2),
                }));
            cardDatabase.Add(new Card(
                "Decentralizace státu",
                9,
                3,
                new List<Tuple<int, int>>
                {
                    Tuple.Create(0, 2),
                    Tuple.Create(5, 1),
                    Tuple.Create(6, -2),
                    Tuple.Create(7, -2),
                    Tuple.Create(11, 2),
                    Tuple.Create(17, 2),
                }));
            cardDatabase.Add(new Card(
                "Centralizace veřejné správy",
                8,
                2,
                new List<Tuple<int, int>>
                {
                    Tuple.Create(0, -1),
                    Tuple.Create(1, -1),
                    Tuple.Create(4, 2),
                    Tuple.Create(5, 1),
                    Tuple.Create(6, -2),
                    Tuple.Create(9, 2),
                    Tuple.Create(16, 3),
                    Tuple.Create(17, -2),
                }));
            cardDatabase.Add(new Card(
                "Dotace pro Agrofert",
                10,
                3,
                new List<Tuple<int, int>>
                {
                    Tuple.Create(0, 2),
                    Tuple.Create(4, -2),
                    Tuple.Create(7, 1),
                    Tuple.Create(11, -3),
                    Tuple.Create(14, 2),
                    Tuple.Create(17, 1),
                }));
            cardDatabase.Add(new Card(
                "Dávky v hmotné nouzi",
                6,
                6,
                new List<Tuple<int, int>>
                {
                    Tuple.Create(0, -5),
                    Tuple.Create(6, 1),
                    Tuple.Create(7, 4),
                    Tuple.Create(9, 1),
                    Tuple.Create(10, 2),
                    Tuple.Create(14, 3),
                }));
            cardDatabase.Add(new Card(
                "Podpora těžkého průmyslu",
                8,
                3,
                new List<Tuple<int, int>>
                {
                    Tuple.Create(0, -2),
                    Tuple.Create(1, 1),
                    Tuple.Create(2, 2),
                    Tuple.Create(5, 2),
                    Tuple.Create(10, 1),
                    Tuple.Create(14, 2),
                }));
            cardDatabase.Add(new Card(
                "Progresivní daň",
                4,
                5,
                new List<Tuple<int, int>>
                {
                    Tuple.Create(0, 3),
                    Tuple.Create(1, -3),
                    Tuple.Create(4, -4),
                    Tuple.Create(7, 1),
                    Tuple.Create(9, 1),
                    Tuple.Create(16, 2),
                }));
            cardDatabase.Add(new Card(
                "Otevřená azylová politika",
                25,
                3,
                new List<Tuple<int, int>>
                {
                    Tuple.Create(0, 2),
                    Tuple.Create(1, 1),
                    Tuple.Create(2, 1),
                    Tuple.Create(3, 4),
                    Tuple.Create(4, 1),
                    Tuple.Create(6, -1),
                    Tuple.Create(7, -2),
                    Tuple.Create(11, 1),
                    Tuple.Create(12, 1),
                    Tuple.Create(14, -4),
                    Tuple.Create(16, 2),
                }));
            cardDatabase.Add(new Card(
                "Zahraniční intervence",
                0,
                0,
                new List<Tuple<int, int>>
                {
                    Tuple.Create(5, -3),
                    Tuple.Create(6, -3),
                    Tuple.Create(7, -1),
                    Tuple.Create(13, 1),
                    Tuple.Create(14, -2),
                    Tuple.Create(15, 5),
                    Tuple.Create(16, 1),
                }));
            cardDatabase.Add(new Card(
                "Prohlubování zahraniční spolupráce - USA",
                0,
                0,
                new List<Tuple<int, int>>
                {
                    Tuple.Create(1, 2),
                    Tuple.Create(2, 2),
                    Tuple.Create(4, 1),
                    Tuple.Create(6, -2),
                    Tuple.Create(11, 1),
                    Tuple.Create(14, -4),
                    Tuple.Create(15, 1),
                    Tuple.Create(16, 1),
                }));
            cardDatabase.Add(new Card(
                "Prohlubování EU integrace",
                0,
                0,
                new List<Tuple<int, int>>
                {
                    Tuple.Create(1, 1),
                    Tuple.Create(2, 3),
                    Tuple.Create(3, 1),
                    Tuple.Create(4, 1),
                    Tuple.Create(5, 2),
                    Tuple.Create(6, -4),
                    Tuple.Create(11, 2),
                    Tuple.Create(12, 2),
                    Tuple.Create(14, -5),
                    Tuple.Create(15, 1),
                    Tuple.Create(16, 3),
                    Tuple.Create(17, 1),
                }));
            cardDatabase.Add(new Card(
                "EET",
                0,
                0,
                new List<Tuple<int, int>>
                {
                    Tuple.Create(1, -4),
                    Tuple.Create(4, -4),
                    Tuple.Create(14, 1),
                    Tuple.Create(17, 2),
                }));
            cardDatabase.Add(new Card(
                "Kurzarbeit",
                0,
                0,
                new List<Tuple<int, int>>
                {
                    Tuple.Create(7, 3),
                    Tuple.Create(8, 2),
                    Tuple.Create(9, 3),
                    Tuple.Create(10, 1),
                    Tuple.Create(12, -2),
                }));
            cardDatabase.Add(new Card(
                "Škrty v bezpečnostních složkách",
                0,
                0,
                new List<Tuple<int, int>>
                {
                    Tuple.Create(2, 2),
                    Tuple.Create(6, -2),
                    Tuple.Create(10, 1),
                    Tuple.Create(11, 1),
                    Tuple.Create(14, -2),
                    Tuple.Create(15, -5),
                    Tuple.Create(17, -1),
                }));
            cardDatabase.Add(new Card(
                "Sleva na dani na dítě",
                15,
                15,
                new List<Tuple<int, int>>
                {
                    Tuple.Create(0, -10),
                    Tuple.Create(3, 2),
                    Tuple.Create(7, 3),
                    Tuple.Create(8, 4),
                    Tuple.Create(9, 4),
                    Tuple.Create(13, 3),
                    Tuple.Create(14, 1),
                    Tuple.Create(17, 2),
                }));
            cardDatabase.Add(new Card(
                "MHD zdarma",
                12,
                10,
                new List<Tuple<int, int>>
                {
                    Tuple.Create(0, -8),
                    Tuple.Create(5, 2),
                    Tuple.Create(6, 1),
                    Tuple.Create(7, 3),
                    Tuple.Create(8, 2),
                    Tuple.Create(9, 4),
                    Tuple.Create(10, 3),
                    Tuple.Create(11, -4),
                }));
            cardDatabase.Add(new Card(
                "Zastropování energií",
                25,
                15,
                new List<Tuple<int, int>>
                {
                    Tuple.Create(0, -15),
                    Tuple.Create(1, 1),
                    Tuple.Create(4, 2),
                    Tuple.Create(6, 3),
                    Tuple.Create(7, 4),
                    Tuple.Create(8, 2),
                    Tuple.Create(9, 4),
                    Tuple.Create(10, 3),
                    Tuple.Create(12, 2),
                    Tuple.Create(14, 3),
                }));
            cardDatabase.Add(new Card(
                "Prohibice",
                35,
                5,
                new List<Tuple<int, int>>
                {
                    Tuple.Create(0, 15),
                    Tuple.Create(2, -2),
                    Tuple.Create(5, -3),
                    Tuple.Create(6, -2),
                    Tuple.Create(10, -3),
                    Tuple.Create(11, -4),
                    Tuple.Create(12, -2),
                    Tuple.Create(13, 2),
                    Tuple.Create(14, -4),
                    Tuple.Create(15, -3),
                }));
            cardDatabase.Add(new Card(
                "Kampaň \"žeru maso\"",
                0,
                0,
                new List<Tuple<int, int>>
                {
                    Tuple.Create(4, 2),
                    Tuple.Create(11, -1),
                    Tuple.Create(12, 2),
                    Tuple.Create(15, 2),
                }));
            cardDatabase.Add(new Card(
                "Standardizovaná maturita",
                0,
                0,
                new List<Tuple<int, int>>
                {
                    Tuple.Create(5, 1),
                    Tuple.Create(8, -2),
                    Tuple.Create(11, 4),
                    Tuple.Create(14, -3),
                    Tuple.Create(17, 2),
                }));
            cardDatabase.Add(new Card(
                "Navýšení počtu pracovníků veřejné správy",
                0,
                0,
                new List<Tuple<int, int>>
                {
                    Tuple.Create(6, -3),
                    Tuple.Create(7, -2),
                    Tuple.Create(8, -3),
                    Tuple.Create(9, -2),
                    Tuple.Create(11, -3),
                    Tuple.Create(14, -2),
                    Tuple.Create(17, 3),
                }));
            cardDatabase.Add(new Card(
                "Snížení počtu pracovníků veřejné správy",
                0,
                0,
                new List<Tuple<int, int>>
                {
                    Tuple.Create(6, 1),
                    Tuple.Create(7, 2),
                    Tuple.Create(8, 1),
                    Tuple.Create(9, 1),
                    Tuple.Create(11, 1),
                    Tuple.Create(14, 2),
                    Tuple.Create(17, -4),
                }));
            cardDatabase.Add(new Card(
                "Euroskepticismus",
                0,
                0,
                new List<Tuple<int, int>>
                {
                    Tuple.Create(2, -3),
                    Tuple.Create(3, -2),
                    Tuple.Create(5, -3),
                    Tuple.Create(6, 4),
                    Tuple.Create(7, 1),
                    Tuple.Create(11, -3),
                    Tuple.Create(12, -2),
                    Tuple.Create(14, 5),
                    Tuple.Create(15, -1),
                    Tuple.Create(16, -3),
                }));
            cardDatabase.Add(new Card(
                "Zavedení Eura",
                0,
                0,
                new List<Tuple<int, int>>
                {
                    Tuple.Create(1, 1),
                    Tuple.Create(4, 1),
                    Tuple.Create(6, -3),
                    Tuple.Create(7, -1),
                    Tuple.Create(9, -3),
                    Tuple.Create(11, 2),
                    Tuple.Create(14, -4),
                    Tuple.Create(16, 1),
                }));
            cardDatabase.Add(new Card(
                "Zvýšení důchodů",
                0,
                0,
                new List<Tuple<int, int>>
                {
                    Tuple.Create(5, -3),
                    Tuple.Create(6, 5),
                    Tuple.Create(7, 2),
                    Tuple.Create(9, 3),
                    Tuple.Create(11, -1),
                    Tuple.Create(14, 2),
                    Tuple.Create(15, 4),
                    Tuple.Create(17, 2),
                }));
            cardDatabase.Add(new Card(
                "Podpora elektromobility",
                0,
                0,
                new List<Tuple<int, int>>
                {
                    Tuple.Create(1, 1),
                    Tuple.Create(2, 3),
                    Tuple.Create(4, 1),
                    Tuple.Create(5, 1),
                    Tuple.Create(6, -2),
                    Tuple.Create(11, 3),
                    Tuple.Create(14, -4),
                    Tuple.Create(16, 1),
                }));
            cardDatabase.Add(new Card(
                "Vojenská podpora ukrajině",
                0,
                0,
                new List<Tuple<int, int>>
                {
                    Tuple.Create(2, 2),
                    Tuple.Create(3, 2),
                    Tuple.Create(6, -2),
                    Tuple.Create(7, -1),
                    Tuple.Create(16, 2),
                }));
            cardDatabase.Add(new Card(
                "Humanitární podpora ukrajině",
                0,
                0,
                new List<Tuple<int, int>>
                {
                    Tuple.Create(2, 5),
                    Tuple.Create(3, 5),
                    Tuple.Create(6, 1),
                    Tuple.Create(15, 2),
                }));
            cardDatabase.Add(new Card(
                "Hostitelství vědecké konference",
                0,
                0,
                new List<Tuple<int, int>>
                {
                    Tuple.Create(1, 1),
                    Tuple.Create(5, 2),
                    Tuple.Create(6, -1),
                    Tuple.Create(11, 4),
                    Tuple.Create(14, -3),
                }));
            cardDatabase.Add(new Card(
                "Justiční reforma",
                0,
                0,
                new List<Tuple<int, int>>
                {
                    Tuple.Create(1, -4),
                    Tuple.Create(2, -3),
                    Tuple.Create(3, 1),
                    Tuple.Create(11, 2),
                    Tuple.Create(14, 5),
                    Tuple.Create(17, 1),
                }));
            cardDatabase.Add(new Card(
                "Pořádání olympiády",
                0,
                0,
                new List<Tuple<int, int>>
                {
                    Tuple.Create(1, 1),
                    Tuple.Create(2, 3),
                    Tuple.Create(3, 2),
                    Tuple.Create(5, 1),
                    Tuple.Create(6, 1),
                    Tuple.Create(8, 2),
                    Tuple.Create(14, 2),
                    Tuple.Create(17, -2),
                }));
            cardDatabase.Add(new Card(
                "Podpora start-upů",
                80,
                40,
                new List<Tuple<int, int>>
                {
                    Tuple.Create(0, 20),
                    Tuple.Create(1, 1),
                    Tuple.Create(4, 3),
                    Tuple.Create(5, 2),
                    Tuple.Create(7, -1),
                    Tuple.Create(11, 5),
                    Tuple.Create(14, -1),
                }));
        }

        /**
         * Starts a new game
         */
        private void StartNewGame()
        {
            Players.Clear();
            var numberOfPlayers = (int)Floor((double)_sliderValue);
            for (var i = 1; i <= numberOfPlayers; ++i)
            {
                Players.Add(new Player(i));
            }

            if (cardDatabase.Count <= 0)
            {
                fillCardDatabase();
                cardDatabase.Sort(((cardLower, cardHigher) =>
                    (String.Compare(cardLower.Name, cardHigher.Name, StringComparison.Ordinal))));
            }

            _firstGame = false;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FirstGame)));

            if (Application.Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop) return;
            var ownerWindow = desktop.MainWindow;
            var window = new GameView
            {
                DataContext = new GameViewModel(Players, cardDatabase)
            };
            window.ShowDialog(ownerWindow);
        }

        /**
         * Start a previous game
         */
        private void StartPreviousGame()
        {
            if (Application.Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop) return;
            var ownerWindow = desktop.MainWindow;
            var window = new GameView
            {
                DataContext = new GameViewModel(Players, cardDatabase)
            };
            window.ShowDialog(ownerWindow);
        }

        /**
         * Exits the Application
         */
        private void Exit()
        {
            if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktopLifetime)
            {
                desktopLifetime.Shutdown();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}