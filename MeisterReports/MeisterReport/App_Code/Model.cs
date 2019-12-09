using Newtonsoft.Json.Linq;
using System;
using MeisterReporting;
using System.Collections.Generic;

/// <summary>
/// Summary description for Model
/// </summary>
public class Model
{
    public enum Calls
    {
        unknown,
        Finder,
        GetParameters,
        Scheduler,
        Retriever,
        Variants,
        NewVariant,
        Agenda,
        AgendaAdd
    }
    Dictionary<Calls, string> Dict { get; set; }
    Dictionary<string, string> Payload { get; set; }
    public ResultSet ResultSet { get; set; }
    public ParmSet parmSet { get; set; }
    public SchedulerResponse schedulerResponses { get; set; }
    public ReportData ReportData { get; set; }
    public TimeSpan TimeSpan { get; private set; }
    public dynamic ReportDownload { get; set; }
    public VariantsSet Variants { get; set; }
    public Messages Messages { get; set; }
    public VariantValuesSet VariantValuesSet { get; set; }

    public Controller Controller { get; private set; }

    public MyReportsResponse myReports { get; set; }
    public VariantResponse variants { get; set; }
    public Model()
    {
        Dict = new Dictionary<Calls, string>();
        Controller = new Controller();
        Dict.Add(Calls.unknown, @"");
        Dict.Add(Calls.Finder, @"Meister.SDK.Report.Finder");
        Dict.Add(Calls.GetParameters, @"Meister.SDK.Report.Parameters");
        Dict.Add(Calls.Scheduler, @"Meister.SDK.Report.Scheduler");
        Dict.Add(Calls.Retriever, @"Meister.SDK.Report.Retrieval");
        Dict.Add(Calls.Variants, @"Meister.SDK.Report.Read.Variant");
        Dict.Add(Calls.NewVariant, @"Meister.SDK.Report.Create.Variant");
        Dict.Add(Calls.Agenda, @"Meister.SDK.Report.Agenda");
        Dict.Add(Calls.AgendaAdd, @"Meister.SDK.Report.Agenda.Add");
        Payload = new Dictionary<string, string>();
    }
    
    public EndPoint GetEndPoint(Calls key)
    {
        EndPoint ep = new EndPoint();
        string h = string.Empty;
        Dict.TryGetValue(key, out h);
        ep.Handler = h;
        ep.Parm = new Parm();
        return ep;
    }

    public void ReportFinder(string hint)
    {
        Dictionary<string, string> jsoncall = new Dictionary<string, string>();
        jsoncall.Add("HINT", hint);
        ResultSet = Controller.RetrieveEntity<ResultSet>(Dict[Calls.Finder], jsoncall);
    }

    public void Parameters(string rep)
    {
        Dictionary<string, string> jsoncall = new Dictionary<string, string>();
        jsoncall.Add("REP_TCODE", rep);
        jsoncall.Add("rep_tcode_type", "T");
        parmSet = Controller.RetrieveEntity<ParmSet>(Dict[Calls.GetParameters], jsoncall);
    }

    public void ScheduleReport(Scheduler s)
    {
        schedulerResponses = Controller.RetrieveEntity<SchedulerResponse>(Dict[Calls.Scheduler], s);
    }

    public void ReportRetriever(string s, bool hit = false, bool metadata = false)
    {
        if (hit == true)
            ReadReport(s, metadata);
        else
            MyReports(s);
    }

    private void ReadReport(string guid, bool metadata)
    {
        SchedulerSet sc = new SchedulerSet();
        sc.ReportGuid = guid;
        dynamic d = Controller.ExecuteRequest<dynamic, SchedulerSet>(Dict[Calls.Retriever], sc);
       // dynamic d = Controller.RetriveAsDynamic(Dict[Calls.Retriever],sc);
        JObject jo = null;
        // Special case under OData 4 - the dynamic is a list 
        if (Controller.IsOD4)
        {
            IEnumerable<dynamic> ie = d as IEnumerable<dynamic>;
            foreach (var item in ie)
            {
                jo = (JObject)item; 
            }
            
        }
        else
            jo = d;
        if (jo.Count == 1)
        {
            ReportJsonNamed rjn = new ReportJsonNamed();
            rjn.CONTENT = jo.First.ToString();
            ReportDownload = rjn;
        }
        else if(jo.Count == 2)
        {
            ReportJsonEDM rjd = new ReportJsonEDM();
            rjd.EDM = jo.First.ToString();
            rjd.CONTENT = jo.Last.ToString();
            ReportDownload = rjd;
        }
    }

    public void MyReports(string user)
    {
        //SchedulerSet sc = new SchedulerSet();
        //sc.UserId = user;
        //ReportData = new ReportData();
        //ReportData.ReportDatum = new List<ReportDatum>();
        //ReportDatum d = Controller.ExecuteRequest<ReportDatum, SchedulerSet>(Dict[Calls.Retriever], sc);
        //ReportData.ReportDatum.Add(d);
        MyReportsRequest req = new MyReportsRequest();
        req.userName = user;
        myReports = Controller.ExecuteRequest<MyReportsResponse, MyReportsRequest>(@"Meister.Reporting.MyReports", req);
    }

    public void VariantContents(string program, string variant)
    {
        VariantQuery vq = new VariantQuery();
        vq.ReportName = program;
        vq.VariantName = variant;
        VariantValuesSet = Controller.RetrieveEntity<VariantValuesSet>(Dict[Calls.Variants], vq);
    }
    public void FindVariants(string program)
    {
        //VariantQuery vq = new VariantQuery();
        //vq.ReportName = program;
        //Variants = Controller.RetrieveEntity<VariantsSet>(Dict[Calls.Variants], vq);
        VariantRequest req = new VariantRequest();
        req.reportName = program;
        variants = Controller.ExecuteRequest<VariantResponse, VariantRequest>(@"Meister.Reporting.Report.Variants", req);
    }

    public void SaveVariant(NewVariant nv)
    {
          Messages = Controller.RetrieveEntity<Messages>(Dict[Calls.NewVariant], nv);
    }

    public AgendaQuery UserAgenda(string us)
    {
        User u = new User();
        u.USERID = us;      
        return Controller.RetrieveEntity<AgendaQuery>(Dict[Calls.Agenda], u);
    }
    public AgendaResult UserSetAgenda(Agenda a)
    {
        return Controller.RetrieveEntity<AgendaResult>(Dict[Calls.AgendaAdd], a);
    }
}