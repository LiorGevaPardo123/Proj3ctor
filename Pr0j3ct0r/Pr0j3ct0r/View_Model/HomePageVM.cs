using Pr0j3ct0r.Memory;
using ProjectorLogic.Business_Logic.Implementation;
using ProjectorLogic.Business_Logic.Logic;
using ProjectorLogic.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pr0j3ct0r.View_Model
{
    public class HomePageVM: INotifyPropertyChanged
    {
        private IPlayersBL playersBL;

        private PlayerVM player;
        public PlayerVM Player
        {
            get { return player; }
            set { player = value; OnPropertyRaised("Player"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyRaised(string propertyname)
        {
            if (PropertyChanged != null) 
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }
        public HomePageVM()
        {
            playersBL = new PlayersBL();           

            PlayerEntity p = playersBL.GetPlayerByUsername(Cache.Instance.username);
            //PlayerEntity p = playersBL.GetPlayerByUsername("BotBurg3r");    
            this.Player = new PlayerVM(p.Username, p.FirstName, p.LastName);
        }
    }
}
