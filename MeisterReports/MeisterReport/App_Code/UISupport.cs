using System.Collections.Generic;
namespace MeisterReporting
{
    public class MyReportUI
    {
        public string Guid { get; set; }
        public string Status { get; set; }
        public string UserName { get; set; }

        public string DateStamp { get; set; }

        public string TimeStamp { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public string ReportType { get; set; }

        public string Variant { get; set; }

        public bool WithMetadata { get; set; }

        public bool ColumnsNamed { get; set; }
    }
    /// <summary>
    /// Summary description for UI Support
    /// </summary>
    /// 
    public class UISupport
    {
        public enum Operations
        {
            gurnist,
            Starting,
            RetrieveReports,
            ScheduleReports,
            ShowHits,
            ShowParameters,
            ShowScheduledItems
        }

        public enum UIElements
        {
            ShowGetMyReports,
            ShowHintChoices,
            ShowHintResults,
            ShowParms,
            ShowScheduler
        }

        public Operations MyOperations { get; set; }

        public List<UIElements> MyUIs { get; set; }

        public UISupport()
        {
            MyOperations = Operations.gurnist;
            MyUIs = new List<UIElements>();
        }

        public void SetOperations(Operations op)
        {
            MyUIs.Clear();
            switch (op)
            {
                case Operations.gurnist:
                    break;
                case Operations.Starting:
                    UIElements u = new UIElements();
                    u = UIElements.ShowGetMyReports;
                    MyUIs.Add(u);
                    break;
                case Operations.RetrieveReports:
                    break;
                case Operations.ScheduleReports:
                    break;
                case Operations.ShowHits:
                    break;
                case Operations.ShowParameters:
                    break;
                case Operations.ShowScheduledItems:
                    break;
                default:
                    break;
            }

        }
    }
}