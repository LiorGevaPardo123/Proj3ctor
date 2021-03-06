﻿using ProjectorLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectorLogic.Data_Access_Layer.Logic
{
    interface IMissionsInteractionsDal
    {
        List<MissionInteractionEntity> GetAllMissionsInteractions();
        void CreateNewInteraction(int Id1, int Id2);
        void DeleteInteraction(int Id1, int Id2);
    }
}
