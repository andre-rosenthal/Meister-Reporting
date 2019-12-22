using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using MeisterCore;

namespace MeisterReporting
{
    public partial class Main : System.Web.UI.Page
    {
        public MeisterException MeisterException = null;
        public List<string> AvailableReports { get; set; }

        public DateTime ThisDate { get; set; }
        public List<string> AvailableReportsDescr { get; set; }
        public List<string> ValidOptionsNH { get; set; }
        public List<string> ValidOptionsH { get; set; }
        public List<string> ParmChanges { get; set; }
        private bool DemoMode = false;
        public Model Model { get; set; }
        public UISupport MyVisibilities { get; set; }
        public bool ReportsShown { get; set; }
        public bool showSchedule { get; set; }
        public bool LookupShown { get; set; }
        public bool HasParmChose { get; set; }
        public const string Show = "Show My Reports";
        public const string Hide = "Hide My Reports";
        public string EDM { get; private set; }
        public string Content { get; private set; }
        private const string nl = @"\r\n";
        private const string SesShowRep = "ShownReports";
        private const string gridView2 = "ParmsList";
        private const string SesHasParm = "HasParmChosen";
        private const string gridView1 = "Matches";
        private const string gridView3 = "Reports";
        private const string ListOfVariants = "Variants";
        private const string ValidH = "ValidH";
        private const string ValidNH = "ValidNH";
        private const string ParmSet = "ParmSet";
        private const string VarParCnt = "VarParContent";
        private const string ParmsAltered = "ParmsAltered";
        private const string OriginalParms = "OriginalParms";
        private const string ReportName = "ReportName";
        private const string ShowSchedule = "ShowSchedule";
        private const string SaveSchedule = "SaveSchedule";
        private const string SelectedSchedule = "SelectedSchedule";
        private const string SavedNick = "SavedNick";
        private const string VarNameSaved = "VarNameSaved";
        private const string SavedScheduleForUpdate = "SavedScheduleForUpdate";
        private const string IsDemo = "IsDemo";
        private const string ShowA = "Show Scheduler";
        private const string HideA = "Hide Scheduler";
        private const string userName = "UserName";
        private const string theModel = "Model";
        private const string thisdate = "thisdate";
        private const string ReportParameterResponse = "ReportParametersResponse";

        protected void Page_Load(object sender, EventArgs e)
        {
            Model = Session[theModel] as Model;
            if (Model == null)
                this.Response.Redirect("~/login.aspx");
            object value = Session["od4"];
            bool bod4 = false;
            if (value != null)
                bod4 = (bool)value;
            Model.Controller.IsOD4 = bod4;
            ThisDate = DateTime.Today;
            SetMessage(String.Empty);
            AvailableReports = new List<string>();
            AvailableReportsDescr = new List<string>();
            AddRepors(AvailableReports);
            AddReporDescriptions(AvailableReportsDescr);
            ValidOptionsNH = AddOptions(false);
            ValidOptionsH = AddOptions(true);
            Session[ValidH] = ValidOptionsH;
            Session[ValidNH] = ValidOptionsNH;
            if (!this.IsPostBack)
            {
                SetMode("*");
                Calendar1.TodaysDate = DateTime.Today;
                Calendar1.SelectedDate = Calendar1.TodaysDate;
                SetDemo();
                Session[ParmsAltered] = new List<string>();
                MyVisibilities = new UISupport();
                MyVisibilities.SetOperations(UISupport.Operations.gurnist);
            }
            else
            {
                if (Session.Count > 0)
                {
                    foreach (var k in Session.Keys)
                    {
                        switch (k.ToString())
                        {
                            case SesShowRep:
                                {
                                    ReportsShown = (bool)Session[SesShowRep];
                                    break;
                                }
                            case SesHasParm:
                                {
                                    HasParmChose = (bool)Session[SesHasParm];
                                    break;
                                }
                            case ShowSchedule:
                                {
                                    showSchedule = (bool)Session[ShowSchedule];
                                    break;
                                }
                            default:
                                break;
                        }
                    }
                }
            }
        }

        private void SetDemo()
        {
            if (IsDemoMode())
            {
                DemoMode = true;
                ddpDemo.Visible = true;
                TextBox1.Text = ddpDemo.SelectedValue;
                int i = AvailableReports.IndexOf(ddpDemo.SelectedValue);
                if (i >= 0)
                {
                    TextBox8.Text = AvailableReportsDescr[i];
                }
            }
            else
                TextBox1.Visible = true;
        }

        private bool IsDemoMode()
        {
            if (Boolean.TryParse(ConfigurationManager.AppSettings[IsDemo], out DemoMode))
                return DemoMode;
            return false;
        }


        private string GetUserName()
        {
            return Session[userName] as string;
        }

        private List<string> AddOptions(bool high)
        {
            // if HIGH is false
            // EQ, NE, GT, LE, LT,CP, and NP
            // otherwise BT (BeTween) and NB (Not Between)
            List<string> l = new List<string>();
            if (high)
            {
                l.Add("BT");
                l.Add("NB");
            }
            else
            {
                l.Add("EQ");
                l.Add("NE");
                l.Add("GT");
                l.Add("LT");
                l.Add("CP");
                l.Add("NP");
            }
            return l;
        }

        public List<string> GetOptions(bool high)
        {
            if (high)
                return ValidOptionsH;
            return ValidOptionsNH;
        }

        private void AddRepors(List<string> l)
        {
            l.Add("RM07RESLH");
            l.Add("S_ALR_87012326");
            l.Add("SD_SALES_ORDERS_VIEW");
            l.Add("S_ALR_87012332");
            l.Add("S_ALR_87012291");
        }

        private void AddReporDescriptions(List<string> l)
        {
            l.Add("List Inventory Mgmt - report");
            l.Add("Char of Accounts - TCode");
            l.Add("List of Sales Orders - Report");
            l.Add("Statement Cust/Vendor/GL Acctn - TCode");
            l.Add("Line Item Journal - TCode");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Cleanup();
            Label10.Visible = false;
            if (TextBox1.Text != string.Empty)
            {
                ReportFinderRequest req = new ReportFinderRequest();
                req.Criteria= TextBox1.Text.ToUpper();
                ReportFinderResponse response = Model.RunMeister<ReportFinderRequest, ReportFinderResponse>(req, @"Meister.Reporting.Report.Finder", out MeisterException);
                BindData<List<Finder>>(GridView1, response.Finder, gridView1);
                Grid1.Visible = true;
            }
        }

        private void Cleanup()
        {
            Grid4.Visible = false;
            Grid2.Visible = false;
            VariantSave.Visible = false;
            AfterB2.Visible = false;
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private string ToCamelCase(string s)
        {
            return char.ToUpper(s.First()) + s.Substring(1).ToLower();
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.RowIndex == GridView1.SelectedIndex)
                {
                    row.BackColor = ColorTranslator.FromHtml("#A1DCF2");
                    row.ToolTip = "Program Selected";
                    UpdateProgress1.Visible = true;
                    Session[ReportName] = GridView1.Rows[row.RowIndex].Cells[1].Text;
                    SetMessage("Reading Report and Variants ....");
                    ReportParametersRequest req = new ReportParametersRequest();
                    req.ReportName = GridView1.Rows[row.RowIndex].Cells[1].Text;
                    req.VariantName = "*";
                    ReportParametersResponse reportParameters = Model.RunMeister<ReportParametersRequest, ReportParametersResponse>(req, @"Meister.Reporting.Report.Parameters", out MeisterException);
                    BuildParameterList(reportParameters, GridView2);
                    BuildVariants(reportParameters, GridView4);
                    Grid2.Visible = true;
                    BeforeB2.Visible = false;
                    SearchSAP.Visible = true;
                    UpdateProgress1.Visible = false;
                    SetMessage("Done reading Report and Variants ....");
                    if (IsDemoMode())
                    {
                        Label10.Visible = true;
                    }
                }
                else
                {
                    row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                    row.ToolTip = "Click to select this row.";
                }
            }
        }

        private void BuildVariants(ReportParametersResponse variantInfo, GridView grv)
        {
            List<Variant> variants = new List<Variant>();
            if (variantInfo != null)
                foreach (var v in variantInfo.ReportMetadata.Variants)
                {
                    Variant variant = new Variant();
                    variant.Name = v.Name;
                    variant.Description = v.Description;
                    variants.Add(variant);
                }
            BindData<List<Variant>>(grv,variants, ListOfVariants);
            Grid4.Visible = true;
        }

        private void BuildParameterList(ReportParametersResponse reportParms, GridView gridView)
        {
            List<ParameterOut> parameters = new List<ParameterOut>();
            foreach (var item in reportParms.ReportMetadata.ReportParameters)
            {
                var parameter = new ParameterOut();
                parameter.Kind = DisplayKind(item.Kind);
                parameter.SelName = item.SelName;
                parameter.Description = item.Description;
                parameter.Option = "EQ";
                parameter.Low = "";
                parameter.High = "";
                parameters.Add(parameter);
            }
            Session[OriginalParms] = parameters;
            BindData<List<ParameterOut>>(gridView, parameters, gridView2);
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string rep = GridView1.Rows[GridView1.SelectedIndex].Cells[1].Text;
            string repType = GridView1.Rows[GridView1.SelectedIndex].Cells[3].Text;
            if (DemoMode)
                if (!AvailableReports.Contains(rep))
                    ShowAlert(DoMessage(rep, AvailableReports));
            AfterB2.Visible = true;
            SchedulerRequest req = new SchedulerRequest();
            req.Option = "N";
            req.Schedule.Variant = GridView4.Rows[GridView4.SelectedIndex].Cells[1].Text;
            if (RadioButtonList3.SelectedValue == "N")
                req.Schedule.ColumnsNamed = true;
            else
                req.Schedule.WithMetadata = true;
            foreach (GridViewRow g0 in GridView2.Rows)
                if (IsItValid(g0.Cells[6].Text) || IsItValid(g0.Cells[7].Text))
                {
                    MeisterReporting.Parameter ps = new MeisterReporting.Parameter();
                    ps.SelName = g0.Cells[1].Text;
                    ps.Sign = "I";
                    ps.Kind = SAPKind(g0.Cells[3].Text);
                    ps.Option = g0.Cells[5].Text;
                    ps.Low = g0.Cells[6].Text;
                    if (IsItValid(g0.Cells[7].Text))
                    {
                        if (IsItValid(g0.Cells[6].Text))
                        {
                            if (GetOptions(true).Contains(ps.Option))
                                ps.High = g0.Cells[7].Text;
                            else
                            {
                                ShowAlert(DoMessage(ps.Option, ValidOptionsH, true));
                                return;
                            }
                        }
                        else
                            if (GetOptions(false).Contains(ps.Option))
                            ps.High = ps.Low;
                        else
                        {
                            ShowAlert(DoMessage(ps.Option, ValidOptionsNH, true));
                            return;
                        }
                        req.Schedule.Parameters.Add(ps);
                    }
                }
            req.Schedule.ReportName = GridView1.Rows[GridView1.SelectedIndex].Cells[1].Text;
            req.UserName = GetUserName();
            if (RadioButtonList1.SelectedValue == "I")
            {
                ReportRunRequest request = new ReportRunRequest();
                request.UserName = req.UserName;
                request.Report.ColumnsNamed = req.Schedule.ColumnsNamed;
                request.Report.Name = req.Schedule.ReportName;
                request.Report.Parameters = req.Schedule.Parameters;
                request.Report.WithMetadata = req.Schedule.WithMetadata;
                request.Report.Variant = req.Schedule.Variant;
                request.Report.ReportType = repType;
                ReportRunResponse response = Model.RunMeister<ReportRunRequest, ReportRunResponse>(request, "Meister.Reporting.Run", out MeisterException);
                TextBox2.Text = response.Guid;
                TextBox3.Text = response.Messages.FirstOrDefault().Text;
            }
            else
            {
                SchedulerResponse schedulerResponse = Model.RunMeister<SchedulerRequest, SchedulerResponse>(req, @"Meister.Reporting.Scheduler", out MeisterException);
                TextBox2.Text = schedulerResponse.Guid;
                TextBox3.Text = schedulerResponse.Messages.FirstOrDefault().Text;
            }
        }

        public static void ShowAlert(string message)
        {
            string cleanMessage = message.Replace("'", "\'");

            Page page = HttpContext.Current.CurrentHandler as Page;
            string script = string.Format("alert('{0}');", cleanMessage);
            if (page != null && !page.ClientScript.IsClientScriptBlockRegistered("alert"))
            {
                ScriptManager.RegisterClientScriptBlock(page, page.GetType(), "alert", script, true /* addScriptTags */);
            }
        }
        private string DoMessage(string msg, List<string> l, bool option = false)
        {
            if (!option)
            {
                string s = "Selected report " + msg + " not availale on this demo" + nl;
                s += "Available Reports are:" + nl;
                foreach (var r in l)
                {
                    s += r + nl;
                }
                return s;
            }
            else
            {
                string s = "Selected Option " + msg + " invalid" + nl;
                s += "Available options are:" + nl;
                foreach (var r in l)
                {
                    s += r + nl;
                }
                return s;
            }
        }


        private bool IsItValid(string s)
        {
            return s != string.Empty && s != "&nbsp;" ? true : false;
        }
        protected void GridView2_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView2.EditIndex = e.NewEditIndex;
            List<Parameter> parameters = Session[gridView2] as List<Parameter>;
            var v = parameters.ElementAt(GridView2.EditIndex);
            if (v != null)
            {
                BindData<List<Parameter>>(GridView2, parameters, gridView2);
                Freshup(GridView4);
            }
            VariantSave.Visible = true;
            BeforeB2.Visible = true;
        }

        private void RebindData<T>(GridView gv, string ses)
        {
            T t = (T)Session[ses];
            gv.DataSource = t;
            gv.DataBind();

        }
        private void BindData<T>(GridView gv, T t, string ses)
        {
            gv.DataSource = t;
            gv.DataBind();
            Session[ses] = t;
        }

        protected void GridView2_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow gvr = GridView2.Rows[e.RowIndex];
            List<Parameter> parameters = Session[gridView2] as List<Parameter>;
            var v = parameters.ElementAt(GridView2.EditIndex);
            if (v != null)
            {
                if (IsItValid(GrabGridUpdate(gvr, 6)))
                {
                    if (!GetOptions(true).Contains(GrabGridUpdate(gvr, 4)))
                    {
                        ShowAlert(DoMessage(GrabGridUpdate(gvr, 4), ValidOptionsH, true));
                        e.Cancel = true;
                    }
                }
                else if (!GetOptions(false).Contains(GrabGridUpdate(gvr, 4)))
                {
                    ShowAlert(DoMessage(GrabGridUpdate(gvr, 4), ValidOptionsNH, true));
                    e.Cancel = true;
                }
                if (e.Cancel == false)
                {
                    ParmChanges = Session[ParmsAltered] as List<string>;
                    if (ParmChanges == null)
                        ParmChanges = new List<string>();
                    ParmChanges.Add(v.SelName);
                    v.Option = GrabGridUpdate(gvr, 4);
                    v.Low= GrabGridUpdate(gvr, 5);
                    v.High = GrabGridUpdate(gvr, 6);
                    GridView2.EditIndex = -1;
                    BindData<List<Parameter>>(GridView2,parameters, gridView2);
                    Session[SesHasParm] = true;
                    BeforeB2.Visible = true;
                    Session[ParmsAltered] = ParmChanges;
                }
            }
        }

        private string GrabGridUpdate(GridViewRow gvr, int idx)
        {
            string value = string.Empty;
            try
            {
                value = ((TextBox)(TextBox)(gvr.Cells[idx].Controls[0])).Text.ToUpper();
            }
            catch
            { }
            return value;
        }

        protected void GridView2_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView2.SelectedIndex = -1;
            GridView2.EditIndex = -1;
            RebindData<List<Parameter>>(GridView2, gridView2);
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            if (ReportsShown == true)
            {
                Button3.Text = Show;
                Session[SesShowRep] = false;
                Button7.Enabled = true;
                Grid3.Visible = false;
            }
            else
            {
                Grid3.Visible = true;
                Button7.Enabled = false;
                MyReportsRequest req = new MyReportsRequest();
                req.userName = GetUserName();
                MyReportsResponse myReports = Model.RunMeister<MyReportsRequest, MyReportsResponse>(req,"Meister.Reporting.MyReports", out MeisterException);
                List <MyReportUI> reps  = new List<MyReportUI>();
                BindData<List<MyReportUI>>(GridView3, reps, gridView3);
                if (myReports != null && myReports.MyReports != null)
                {
                    foreach (var rd in myReports.MyReports)
                    {
                        var rep = new MyReportUI();
                        rep.Guid = rd.Guid;
                        DateTime dt = FromStringDT(rd.DateStamp, rd.TimeStamp);
                        rep.DateStamp = String.Format("{0:yyyy/MM/dd}", dt);
                        rep.TimeStamp = String.Format("{0:HH:mm tt}", dt);
                        rep.Name = rd.Report.Name;
                        rep.UserName = rd.UserName;
                        rep.ColumnsNamed = rd.Report.ColumnsNamed;
                        rep.WithMetadata = rd.Report.WithMetadata;
                        rep.ReportType = System.Enum.GetName(typeof(Report.ReportTypes), rd.Report.ReportType.ToArray()[0]);
                        if (!string.IsNullOrEmpty((string)rd.Report.Description))
                            rep.Description = rd.Report.Description;
                        else
                            rep.Description = "Generated Report";
                        rep.Status = System.Enum.GetName(typeof(Report.Statuses), rd.Report.Status.ToArray()[0]);
                        reps.Add(rep);
                    }
                    GridView3.Caption = "Reports found for user";
                    BindData<List<MyReportUI>>(GridView3, reps, gridView3);
                }
                ReportsShown = true;
                Session[SesShowRep] = true;
                Button3.Text = Hide;
            }
        }

        private DateTime FromStringDT(string date, string time)
        {
            return new DateTime(Int32.Parse(date.Substring(0, 4)), Int32.Parse(date.Substring(4, 2)), Int32.Parse(date.Substring(6, 2)), Int32.Parse(time.Substring(0, 2)), Int32.Parse(time.Substring(2, 2)), Int32.Parse(time.Substring(4, 2)));
        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            SetDemo();
            Grid3.Visible = false;
            Button3.Enabled = true;
            Button4.Enabled = true;
            SearchSAP.Visible = true;
            Button7.Text = ShowA;
            Session[ShowSchedule] = false;
            Button3.Text = Show;
        }

        protected void GridView3_PageIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GridView3_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (DoingSchedule())
                NewPage<Schedule>(GridView3, e.NewPageIndex, SaveSchedule);
            else
                NewPage<ThisReport>(GridView3, e.NewPageIndex, gridView3);
        }

        protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            NewPage<Parameter>(GridView2, e.NewPageIndex, gridView2);
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            NewPage<MyReport>(GridView1, e.NewPageIndex, gridView1);
        }


        private void NewPage<T>(GridView grv, int newPageIndex, string ses)
        {
            grv.PageIndex = newPageIndex;
            List<T> l = Session[ses] as List<T>;
            grv.DataSource = l;
            grv.DataBind();
        }

        protected void GridView3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DoingSchedule())
            {
                BeforeB2.Visible = true;
                Button2.Visible = false;
                Button9.Visible = true;
                DOWs.Visible = true;
                Button8.Text = "Save Item";
                foreach (GridViewRow row in GridView3.Rows)
                {
                    if (row.RowIndex == GridView3.SelectedIndex)
                    {
                        string uuid = GridView3.Rows[row.RowIndex].Cells[5].Text;
                        row.BackColor = ColorTranslator.FromHtml("#A1DCF2");
                        row.ToolTip = "Retrieving Schedule for user";
                        Schedule schedule = FromSchedule(uuid);
                        if (schedule != null)
                        {
                            Button8.Enabled = true;
                            Session[SelectedSchedule] = uuid;
                            RadioButtonList2.Visible = false;
                            TextBox7.Text = schedule.TimeSlot;
                            txtNickName.Text = schedule.NickName;
                            lbDOW.Text = "Schedule Time Slot";
                            if (schedule.DayOfWeek != string.Empty)
                            {
                                RadioButtonList2.SelectedValue = schedule.DayOfWeek;
                                RadioButtonList2.Visible = true;
                                lbDOW.Text = "Schedule Day of the Week and Time Slot";
                            }
                            if (schedule.ScheduleType != string.Empty)
                                RadioButtonList1.SelectedValue = schedule.ScheduleType.Substring(0, 1);
                            else
                                RadioButtonList1.SelectedValue = "D";
                        }
                    }
                    else
                    {
                        row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                        row.ToolTip = "Click to select this row.";
                    }
                }
            }
            else
            {
                Button9.Visible = false;
                Dictionary<string, string> Files = new Dictionary<string, string>();
                foreach (GridViewRow row in GridView3.Rows)
                {
                    if (row.RowIndex == GridView3.SelectedIndex)
                    {
                        string name = GridView3.Rows[row.RowIndex].Cells[1].Text;
                        string zn = name + ".zip";
                        row.BackColor = ColorTranslator.FromHtml("#A1DCF2");
                        row.ToolTip = "Retrieving Report";
                        Thread.Sleep(100);
                        ReadReportRequest req = new ReadReportRequest();
                        req.Guid= GridView3.Rows[row.RowIndex].Cells[1].Text;
                        req.KeepReport = true;
                        ReadReportResponse readReport = Model.RunMeister<ReadReportRequest, ReadReportResponse>(req, "Meister.Reporting.Retrieve", out MeisterException);
                        if (readReport != null)
                        {
                            ThisReport rs = readReport.Report;
                            if (rs != null)
                                if (!string.IsNullOrEmpty(rs.Edm))
                                {
                                    WriteTempFile(name + "_EDM.json", rs.Edm, Files);
                                    WriteTempFile(name + "_Content.json", rs.Content, Files);
                                    DownloadZipFiles(zn, Files);
                                }
                                else
                                {
                                    WriteTempFile(name + "_Content.json", rs.Content, Files);
                                    DownloadZipFiles(zn, Files);
                                }
                        }
                    }
                    else
                    {
                        row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                        row.ToolTip = "Click to select this row.";
                    }
                }
            }
        }

        private Schedule FromSchedule(string uuid)
        {
            List<Schedule> abl = Session[SaveSchedule] as List<Schedule>;
            if (abl != null)
                return (from ab1 in abl where (ab1.Guid == uuid) select ab1).FirstOrDefault();
            return null;
        }

        public void DownloadZipFiles(string fileName, Dictionary<string, string> files)
        {
            string zipname = Path.Combine(Path.GetTempPath(), fileName);
            using (Ionic.Zip.ZipFile zipFile = new Ionic.Zip.ZipFile())
            {
                foreach (var file in files)
                {
                    zipFile.AddFile(file.Value, "");
                }
                zipFile.Save(zipname);
            }
            Response.Clear();
            Response.ContentType = "application/zip";
            Response.AddHeader("content-disposition", "attachment; filename=" + fileName);
            Response.WriteFile(zipname);
            Response.End();
        }

        public void WriteTempFile(string filename, string text, Dictionary<string, string> files)
        {
            string filePath = Path.Combine(Path.GetTempPath(), filename);
            string[] sa = { text };
            System.IO.File.WriteAllLines(filePath, sa);
            files.Add(filename, filePath);
        }

        protected void GridView4_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in GridView4.Rows)
            {
                if (row.RowIndex == GridView4.SelectedIndex)
                {
                    row.BackColor = ColorTranslator.FromHtml("#A1DCF2");
                    row.ToolTip = "Variant Selected";
                    ReportParametersRequest req = new ReportParametersRequest();
                    req.ReportName = GridView1.Rows[GridView1.SelectedIndex].Cells[1].Text;
                    req.VariantName = GridView4.Rows[row.RowIndex].Cells[1].Text;
                    ReportParametersResponse reportParameters = Model.RunMeister<ReportParametersRequest, ReportParametersResponse>(req, @"Meister.Reporting.Report.Parameters", out MeisterException);
                    FilldParameterList(reportParameters, GridView2, GridView4.Rows[row.RowIndex].Cells[1].Text);
                    Session[VarParCnt] = reportParameters;
                    VariantSave.Visible = false;
                    BeforeB2.Visible = true;
                }
                else
                {
                    row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                    row.ToolTip = "Click to select this row.";
                }
            }
        }

        private void FilldParameterList(ReportParametersResponse reportParameters, GridView grv, string varname)
        {
            List<ParameterOut> binds = reportParameters.ReportMetadata.ReportParameters;
            MeisterReporting.VariantOut vsel = (from x in reportParameters.ReportMetadata.Variants where (x.Name == varname) select x).FirstOrDefault();
            if (vsel.Parameters != null)
                foreach (var v in vsel.Parameters)
                {
                    foreach (var v0 in binds)
                    {
                        if (v0.SelName == v.SelName)
                        {
                            if (IsKosher(v.Option))
                                v0.Option = v.Option;
                            v0.Low= CheckContent(v.Low);
                            v0.High = CheckContent(v.High);
                        }
                        else
                        {
                            v0.Option = "EQ";
                            v0.Low = string.Empty;
                            v0.High = string.Empty;
                        }
                    }
                }
            Session[ReportParameterResponse] = reportParameters;
            BindData<List<ParameterOut>>(grv, binds, gridView2);
        }

        private string CheckContent(string s)
        {
            if (!string.IsNullOrEmpty(s))
            {
                string temp = s;
                s = s.Replace("0", "");
                s = s.Replace(".", "");
                s = s.Replace(" ", "");
                if (s != string.Empty)
                    return temp;
                else
                    return string.Empty;
            }
            else
                return string.Empty;
        }

        private bool IsKosher(string option)
        {
            return option != string.Empty;
        }

        protected void GridView2_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            Freshup(GridView4);
        }

        private void Freshup(GridView gv)
        {
            if (gv.SelectedIndex != -1)
            {
                gv.Rows[gv.SelectedIndex].BackColor = ColorTranslator.FromHtml("#FFFFFF");
                gv.SelectedIndex = -1;
            }
        }

        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Cells.Count > 1)
            {
                e.Row.Cells[1].Enabled = false;
                e.Row.Cells[2].Enabled = false;
            }
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            //check if exists ...
            List<Variant> ls = Session[ListOfVariants] as List<Variant>;
            foreach (var v0 in ls)
            {
                if (v0.Name == TextBox4.Text.ToUpper() && CheckBox1.Checked == false)
                {
                    ShowAlert("Variant " + v0.Name + " Already exists - cannot override");
                    return;
                }
            }
            ReportParametersResponse reportParameters = Session[ReportParameterResponse] as ReportParametersResponse;
            UpsertVariantRequest req = new UpsertVariantRequest();
            req.Parameters = new List<MeisterReporting.Parameter>();
            ParmChanges = Session[ParmsAltered] as List<string>;
            foreach (GridViewRow g0 in GridView2.Rows)
                if (ParmChanges.Contains(g0.Cells[1].Text))
                    if (IsItValid(g0.Cells[5].Text) || IsItValid(g0.Cells[6].Text))
                    {
                        if (reportParameters != null)
                        {
                            foreach (var v1 in reportParameters.ReportMetadata.ReportParameters)
                            {
                                if (v1.SelName == g0.Cells[1].Text && v1.Sign == "*")
                                {
                                    ShowAlert("Variant " + TextBox4.Text.ToUpper() + " has field " + v1.SelName + " locked for update");
                                    return;
                                }
                            }
                        }
                        MeisterReporting.Parameter parameter = new MeisterReporting.Parameter();
                        parameter.SelName = g0.Cells[1].Text;
                        parameter.Sign = "I";
                        parameter.Kind = SAPKind(g0.Cells[2].Text);
                        parameter.Option = g0.Cells[4].Text;
                        parameter.Low = g0.Cells[5].Text;
                        if (IsItValid(g0.Cells[6].Text))
                            parameter.High = g0.Cells[6].Text;
                        else
                            parameter.High = parameter.Low;
                        req.Parameters.Add(parameter);
                    }
            req.ReportName = GridView1.Rows[GridView1.SelectedIndex].Cells[2].Text;
            req.VariantName = TextBox4.Text.ToUpper();
            req.Description = TextBox5.Text;
            UpsertVariantResponse response = Model.RunMeister<UpsertVariantRequest, UpsertVariantResponse>(req, @"Meister.Reporting.Variant.Upsert", out MeisterException);
            SetMessage("Variant Saved ...");
        }

        /// <summary>
        /// returns the sap kind
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private string SAPKind(string text)
        {
            if (text == System.Enum.GetName(typeof(Parameter.KindTypes), Parameter.KindTypes.Selection))
                return "S";
            else
                return "P";
        }

        /// <summary>
        /// retuns the display kind
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private string DisplayKind(string text)
        {
            if (text == "S")
                return System.Enum.GetName(typeof(Parameter.KindTypes), Parameter.KindTypes.Selection);
            else
                return System.Enum.GetName(typeof(Parameter.KindTypes), Parameter.KindTypes.Parameter);
        }

        protected void TextBox4_TextChanged(object sender, EventArgs e)
        {
            Session["VarNameSet"] = true;
            if (TextBox4.Text != string.Empty && TextBox5.Text != string.Empty)
                Button6.Enabled = true;
            else
                Button6.Enabled = false;
        }

        protected void TextBox5_TextChanged(object sender, EventArgs e)
        {
            Session["VarNameDesc"] = true;
            if (TextBox4.Text != string.Empty && TextBox5.Text != string.Empty)
                Button6.Enabled = true;
            else
                Button6.Enabled = false;
        }

        /// <summary>
        /// recurrence
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DoingSchedule())
                Button8.Text = "Save Item";
            else
                Button8.Text = "Create Item";
            string nm = " for " + ToCamelCase(GetUserName()) + " ";
            int rb = RadioButtonList1.SelectedIndex;
            Cal.Visible = false;
            DOWs.Visible = false;
            Hours.Visible = false;
            if (rb != 0)
            {
                if (rb == 2)
                {
                    DOWs.Visible = true;
                    Hours.Visible = true;
                    lbDOW.Text = "Schedule Day of the Week and Time Slot";
                    RadioButtonList2.Visible = true;
                    txtNickName.Text = (Session[ReportName] as string) + nm + RadioButtonList1.Text;
                }
                else if (rb == 1)
                {
                    Cal.Visible = true;
                    Hours.Visible = true;
                    LbHou.Text = "Schedule Specifc Date and Time Slot";
                    RadioButtonList2.Visible = true;
                    ThisDate = Calendar1.SelectedDate;
                    txtNickName.Text = (Session[ReportName] as string) + nm + "on " + ThisDate.ToShortDateString();
                }
                else
                {
                    DOWs.Visible = true;
                    lbDOW.Text = "Schedule Time Slot";
                    RadioButtonList2.Visible = false;
                    txtNickName.Text = (Session[ReportName] as string) + nm + RadioButtonList1.SelectedItem.Text;
                }
                Session[SavedNick] = txtNickName.Text;
            }
        }

        private bool DoingSchedule()
        {
            try
            {
                return (bool)Session[ShowSchedule];
            }
            catch (Exception)
            {
                return false;
            }

        }

        protected void RadioButtonList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int rb = RadioButtonList2.SelectedIndex;
            string nm = " for " + ToCamelCase(GetUserName()) + " ";
            txtNickName.Text = (Session[ReportName] as string) + nm + RadioButtonList1.SelectedItem.Text + " on " + RadioButtonList2.SelectedItem.Text;
            Session[SavedNick] = txtNickName.Text;
        }

        protected void TextBox7_TextChanged(object sender, EventArgs e)
        {
            int slot = 0;
            string sn = Session[SavedNick] as string;
            if (Int32.TryParse(TextBox7.Text, out slot))
                if (slot <= 23 && slot >= 0)
                {
                    Button8.Enabled = true;
                    txtNickName.Text = sn + " at " + TextBox7.Text + " Hours";
                }
                else
                {
                    TextBox7.Text = string.Empty;
                    ShowAlert("Hour Slot needs to be between 00 and 23");
                }
            else
            {
                TextBox7.Text = string.Empty;
                ShowAlert("Hour Slot needs to be between 00 and 23");
            }
        }

        protected void Button7_Click(object sender, EventArgs e)
        {
            if (showSchedule == true)
            {
                SetMode("a");
                Button7.Text = ShowA;
                Button8.Enabled = true;
                Button3.Enabled = true;
                Button4.Enabled = true;
            }
            else
            {
                SetMode("A");
                Button3.Enabled = false;
                Button4.Enabled = false;
                Grid3.Visible = true;
                GridView3.Caption = "Schedule for " + GetUserName();
                SchedulerRequest req = new SchedulerRequest();
                req.UserName = GetUserName();
                req.Option = "R";
                SchedulerResponse schedulerResponse = Model.RunMeister<SchedulerRequest, SchedulerResponse>(req, @"Meister.Reporting.Scheduler", out MeisterException);
                List<Schedule> reps = new List<Schedule>();
                BindData<List<Schedule>>(GridView3, reps, SaveSchedule);
                if (schedulerResponse != null && schedulerResponse.Schedules != null)
                {
                    foreach (var l1 in schedulerResponse.Schedules)
                    {
                        var ab = new Schedule();
                        ab.NickName = l1.NickName;
                        ab.ScheduleType = GetScheduleType(l1.ScheduleType);
                        ab.TimeSlot = l1.TimeSlot;
                        if (ab.TimeSlot.Length == 1)
                            ab.TimeSlot = "0" + ab.TimeSlot;
                        ab.Guid = l1.Guid;
                        ab.DayOfWeek = GetDOW(l1.DayOfWeek);
                        ab.ReportName = l1.ReportName;
                        reps.Add(ab);
                    }
                    GridView3.Caption = "Schedule found for user";
                    BindData<List<Schedule>>(GridView3, reps, SaveSchedule);
                }
                ReportsShown = true;
                Session[ShowSchedule] = true;
                Button7.Text = HideA;
            }
        }

        private void SetMode(string v)
        {
            {
                UpdateProgress1.Visible = false;
                DemoMode = false;
                ddpDemo.Visible = false;
                LookupShown = false;
                showSchedule = false;
                CheckBox2.Visible = false;
                Button9.Visible = false;
                SearchSAP.Visible = false;
                Button8.Enabled = false;
                Session[SesShowRep] = false;
                ReportsShown = false;
                TextBox1.Visible = false;
                Button7.Text = ShowA;
                Session[ShowSchedule] = false;
                Grid3.Visible = false;
                Grid1.Visible = false;
                Grid4.Visible = false;
                Grid2.Visible = false;
                VariantSave.Visible = false;
                AfterB2.Visible = false;
                BeforeB2.Visible = false;
                DOWs.Visible = false;
                Cal.Visible = false;
                Hours.Visible = false;
            }
            switch (v)
            {
                case "a":
                    {
                        Button8.Enabled = true;
                        break;
                    }
                case "A":
                    {
                        Grid3.Visible = true;
                        ReportsShown = true;
                        Session[ShowSchedule] = true;
                        Button7.Text = HideA;
                        break;
                    }

                default:
                    break;
            }
        }

        private string GetDOW(string dw)
        {
            foreach (ListItem item in RadioButtonList2.Items)
            {
                if (item.Value == dw)
                    return item.Text;
            }
            return dw;
        }

        private string GetScheduleType(string at)
        {
            foreach (ListItem item in RadioButtonList1.Items)
            {
                if (item.Value == at)
                    return item.Text;
            }
            return at;
        }

        /// <summary>
        /// save Schedule
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button8_Click(object sender, EventArgs e)
        {
            var update = false;
            SchedulerRequest req = new SchedulerRequest();
            if (DoingSchedule())
            {
                Schedule ab = FromSchedule(Session[SelectedSchedule] as string);
                if (ab != null)
                {
                    req.Option = "U";
                    update = true;
                    req.Schedule.Guid = ab.Guid;
                }
            }
            else // new Schedule item ..
            {
                req.Option = "N";
            }
            req.Schedule.ScheduleType = RadioButtonList1.SelectedValue;
            if (req.Schedule.ScheduleType == "W")
                req.Schedule.DayOfWeek = RadioButtonList2.SelectedValue;
            req.Schedule.NickName = txtNickName.Text;
            req.Schedule.TimeSlot = TextBox7.Text;
            req.Schedule.ReportName = GridView1.Rows[GridView1.SelectedIndex].Cells[1].Text;
            req.UserName = GetUserName();
            if (RadioButtonList3.SelectedValue == "N")
                req.Schedule.WithMetadata = true;
            else
                req.Schedule.ColumnsNamed = true;
            if (IsItValid(TextBox4.Text))
                req.Schedule.Variant = TextBox4.Text;
            else
                req.Schedule.Variant = GridView4.Rows[GridView4.SelectedIndex].Cells[1].Text;
            if (!string.IsNullOrEmpty(req.Schedule.Variant))
            {
                req.Schedule.Parameters = new List<MeisterReporting.Parameter>();
                MeisterReporting.Parameter p = new MeisterReporting.Parameter();
            }
            SchedulerResponse schedulerResponse = Model.RunMeister<SchedulerRequest, SchedulerResponse>(req, "Meister.Reporting.Scheduler", out MeisterException);
            if (update)
                SetMessage("Schedule " + req.Schedule.NickName + " changed successfully");
            else
                SetMessage("Schedule " + req.Schedule.NickName + " created successfully");
        }

        protected void ddpDemo_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextBox1.Text = ddpDemo.SelectedValue;
            int i = AvailableReports.IndexOf(ddpDemo.SelectedValue);
            if (i >= 0)
            {
                TextBox8.Text = AvailableReportsDescr[i];
            }
        }

        /// <summary>
        /// delete a report ....
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridView3_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected bool DoWarning(string rep)
        {

            string title = "Confirm";
            string text = @"You are about to delete the scheduling for " + rep + " Click Yes(OK) or No(Cancel) buttons";
            MessageBox messageBox = new MessageBox(text, title, MessageBox.MessageBoxIcons.Question, MessageBox.MessageBoxButtons.OKCancel, MessageBox.MessageBoxStyle.StyleB);
            messageBox.SuccessEvent.Add("OkClick");
            messageBox.FailedEvent.Add("CancelClick");
            if (messageBox.Show(this) == string.Empty)
                return true;
            return false;
        }


        [WebMethod]
        public static string OkClick(object sender, EventArgs e)
        {
            return string.Empty;
        }


        [WebMethod]
        public static string CancelClick(object sender, EventArgs e)
        {
            return "X";
        }


        protected void ConfirmDelete_Click(object sender, EventArgs e)
        {
            if (DoingSchedule())
            {
                string uuid = string.Empty;
                foreach (GridViewRow row in GridView3.Rows)
                    if (row.RowIndex == GridView3.SelectedIndex)
                        uuid = GridView3.Rows[row.RowIndex].Cells[5].Text;
                if (uuid != string.Empty)
                {
                    Schedule ab = FromSchedule(uuid);
                    if (ab != null)
                    {
                        SchedulerRequest req = new SchedulerRequest();
                        req.Schedule = ab;
                        req.Option = "D";
                        Session[SavedScheduleForUpdate] = req;
                        CheckBox2.Visible = true;
                    }
                }
            }
        }

        protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox2.Checked == true)
            {
                SchedulerRequest req = Session[SavedScheduleForUpdate] as SchedulerRequest;
                if (req != null)
                {
                    CheckBox2.Visible = false;
                    CheckBox2.Checked = false;
                    Button9.Visible = false;
                    SchedulerResponse schedulerResponse = Model.RunMeister<SchedulerRequest, SchedulerResponse>(req, "Meister.Reporting.Scheduler", out MeisterException);
                    SetMessage("Schedule deleted");
                    Grid3.Visible = false;
                    BeforeB2.Visible = false;
                    DOWs.Visible = false;
                    showSchedule = false;
                    Thread.Sleep(100);
                    Button7_Click(Button7, null);
                }
            }
        }

        private void SetMessage(string v)
        {
            TextBox3.Text = v;
        }

        protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (!DoingSchedule())
                foreach (GridViewRow row in GridView3.Rows)
                {
                    TableCell selectCell = row.Cells[0];
                    if (selectCell.Controls.Count > 0)
                    {
                        LinkButton selectControl = selectCell.Controls[0] as LinkButton;
                        if (selectControl != null)
                        {
                            selectControl.Text = "Download";
                        }
                    }
                }
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            string nm = " for " + ToCamelCase(GetUserName()) + " ";
            this.ThisDate = Calendar1.SelectedDate.Date;
            Session[thisdate] = this.ThisDate;
            txtNickName.Text = (Session[ReportName] as string) + nm + "on " + ThisDate.ToShortDateString();
            Session[SavedNick] = txtNickName.Text;
            if (!String.IsNullOrEmpty(TextBox7.Text))
                txtNickName.Text = txtNickName.Text + " at " + TextBox7.Text + " Hours";
        }



        protected void txtNickName_TextChanged(object sender, EventArgs e)
        {

        }

        protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        {
            if (e.Day.Date.CompareTo(DateTime.Today) <= 0)
                e.Day.IsSelectable = false;
        }
    }
}