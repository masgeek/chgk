﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChGK.Core.Services
{
    public enum GACategory
    {
        PlayQuestion,
        QuestionsList,
        DealWithTeams,
    }

    public enum GAAction
    {
        Click,
        Open,
        Timer,
    }
    
    public interface IGAService
    {
        void ReportScreenEnter(string name);

        void ReportEvent(GACategory category, GAAction action, string label);
    }
}
