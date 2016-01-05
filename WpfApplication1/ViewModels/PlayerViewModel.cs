using Othello.Controller;
using Othello.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfGUI.ViewModels
{
    public class PlayerViewModel : INotifyPropertyChanged
    {
        private string name;
        private DiscColor discColor;
        private int score;
        private List<ComboBoxDiscColor> colorListEnum = new List<ComboBoxDiscColor>();
        private List<ComboBoxDifficulty> difficultyListEnum = new List<ComboBoxDifficulty>();
        private ISubmitPlayer submitPlayer;
        private string opponentName;
        private Difficulty difficulty;

        public PlayerViewModel(ISubmitPlayer submitPlayer)
        {
            this.submitPlayer = submitPlayer;
            this.ValidatePlayerCommand = new ValidatePlayerCommand(this);
            this.InitializeComboBoxLists();
        }

        public PlayerViewModel()
        {

        }

        public PlayerViewModel(Player player)
        {
            this.name = player.Name;
            this.score = player.Score;
            this.discColor = player.Color;
        }

        private void InitializeComboBoxLists()
        {
            colorListEnum.Add(new ComboBoxDiscColor() { ValueColorEnum = DiscColor.None, ValueColorString = "Color" });
            colorListEnum.Add(new ComboBoxDiscColor() { ValueColorEnum = DiscColor.Black, ValueColorString = "Black" });
            colorListEnum.Add(new ComboBoxDiscColor() { ValueColorEnum = DiscColor.White, ValueColorString = "White" });
            difficultyListEnum.Add(new ComboBoxDifficulty() { ValueDifficultyEnum = Difficulty.NONE, ValueDifficultyString = "No AI" });
            difficultyListEnum.Add(new ComboBoxDifficulty() { ValueDifficultyEnum = Difficulty.EASY, ValueDifficultyString = "Easy"});
            difficultyListEnum.Add(new ComboBoxDifficulty() { ValueDifficultyEnum = Difficulty.MEDIUM, ValueDifficultyString = "Medium"});
            difficultyListEnum.Add(new ComboBoxDifficulty() { ValueDifficultyEnum = Difficulty.HARD, ValueDifficultyString = "Hard"});
        }

        public string Name 
        { 
            get { return this.name; }
            set 
            {
                if (value != this.name)
                {
                    this.name = value;
                    NotifyPropertyChanged("Name");
                } 
            } 
        }

        public DiscColor DiscColor
        {
            get { return this.discColor; }
            set
            {
                if (value != this.discColor)
                {
                    this.discColor = value;
                    NotifyPropertyChanged("DiscColor");
                }
            }
        }

        public int Score
        {
            get { return this.score; }
            set 
            { 
                this.score = value;
                NotifyPropertyChanged("Score");
            }
        }

        public string OpponentName
        {
            get { return this.opponentName; }
            set
            {
                if (value != this.name)
                {
                    this.opponentName = value;
                    NotifyPropertyChanged("OpponentName");
                }
            }
        }

        public Difficulty Difficulty
        {
            get { return this.difficulty; }
            set
            {
                this.difficulty = value;
                NotifyPropertyChanged("Difficulty");
            }
        }

        public List<ComboBoxDiscColor> ColorListEnum
        {
            get { return this.colorListEnum; }
            set
            {
                if (value != this.colorListEnum)
                {
                    this.colorListEnum = value;
                    NotifyPropertyChanged("ColorListEnum");
                }
            }
        }

        public List<ComboBoxDifficulty> DifficultyListEnum
        {
            get { return this.difficultyListEnum; }
            set
            {
                if (value != this.difficultyListEnum)
                {
                    this.difficultyListEnum = value;
                    NotifyPropertyChanged("DifficultyListEnum");
                }
            }
        }

        public ICommand ValidatePlayerCommand { get; private set; }

        public bool ValidatePlayer()
        {
            if (this.discColor != DiscColor.None
                && this.name != null
                && this.opponentName != null
                && !String.IsNullOrWhiteSpace(this.name)
                && !String.IsNullOrWhiteSpace(this.opponentName)
                && this.name.Length >= 3
                && this.opponentName.Length >= 3
                && !this.name.Equals(this.opponentName)) 
                return true;
            return false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void SubmitPlayer()
        {
            submitPlayer.SubmitPlayer();
        }
    }
}
