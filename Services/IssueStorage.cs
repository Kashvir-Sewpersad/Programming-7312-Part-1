﻿using Programming_7312_Part_1.Models;
using System.Collections.Generic;

namespace Programming_7312_Part_1.Services
{
    public class IssueStorage
    {
        public LinkedList<Issue> ReportedIssues { get; } = new LinkedList<Issue>();
    }
}