using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpf_whatifSample2
{
    public class WhatifApiResult
    {
        public string status { get; set; }
        public Whatisapiresultproperties properties { get; set; }

    }
    public class Whatisapiresultproperties
    {
        public string correlationId { get; set; }
        public deploychanges[] changes { get; set; }

    }
    public class deploychanges
    {
        public string resourceId { get; set; }
        public string changeType { get; set; }
        public resultobject before { get; set; }
        public resultobject after { get; set; }
    }
    public class resultobject
    {
        public string id { get; set; }
        public string location { get; set; }
        public string name { get; set; }
        public string type { get; set; }
    }
}
