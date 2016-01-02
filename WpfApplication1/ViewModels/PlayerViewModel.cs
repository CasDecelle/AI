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
        private String name;
        private DiscColor discColor;
        private int score;
        private List<ComboBoxDiscColor> colorListEnum = new List<ComboBoxDiscColor>();
        private ISubmitPlayer submitPlayer;

        public PlayerViewModel(ISubmitPlayer submitPlayer)
        {
            this.submitPlayer = submitPlayer;
            colorListEnum.Add(new ComboBoxDiscColor() { ValueColorEnum = DiscColor.None, ValueColorString = "Color" });
            colorListEnum.Add(new ComboBoxDiscColor() { ValueColorEnum = DiscColor.Black, ValueColorString = "Black" });
            colorListEnum.Add(new ComboBoxDiscColor() { ValueColorEnum = DiscColor.White, ValueColorString = "White" });
            this.ValidatePlayerCommand = new ValidatePlayerCommand(this);
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

        public String Name 
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

        public List<ComboBoxDiscColor> ColorListEnum
        {
            get { return this.colorListEnum; }
            set
            {
                if (value != this.colorListEnum)
                {
                    this.colorListEnum = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public ICommand ValidatePlayerCommand { get; private set; }

        public bool ValidatePlayer()
        {
            if (this.discColor == DiscColor.None) return false;
            if (this.name == null) return false;
            return this.name.Length <= 10 ? !String.IsNullOrWhiteSpace(this.name) : false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
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
