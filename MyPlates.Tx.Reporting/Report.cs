using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Net;
using MyPlates.Tx.Reporting.ReportService2005;
using MyPlates.Tx.Reporting.ReportExecution2005;


namespace MyPlates.Tx.Reporting
{
    public class Report
    {
        private ReportingService2005 rs;
        private ReportExecutionService rsExec;

        public Report()
        {
            rs = new ReportingService2005();
            rsExec = new ReportExecutionService();
            NetworkCredential credentials = new NetworkCredential(ConfigurationManager.AppSettings["SSRS-WS-UserName"], ConfigurationManager.AppSettings["SSRS-WS-Password"], ConfigurationManager.AppSettings["SSRS-WS-Domain"]);

            rs.Url = ConfigurationManager.AppSettings["SSRS-WS-ReportService-URL"];
            rsExec.Url = ConfigurationManager.AppSettings["SSRS-WS-ReportExecution-URL"];

            rs.Credentials = credentials;
            rsExec.Credentials = credentials;
        }

        public Stream GetPDFOrderReceipt(int orderId)
        {
            string report = ConfigurationManager.AppSettings["ReceiptReportName"];
            MyPlates.Tx.Reporting.ReportService2005.ReportParameter[] _parameters = null;

            _parameters = rs.GetReportParameters(report, null, false, null, null);

            ExecutionInfo ei = rsExec.LoadReport(report, null);

            MyPlates.Tx.Reporting.ReportExecution2005.ParameterValue[] parameters = new MyPlates.Tx.Reporting.ReportExecution2005.ParameterValue[1];

            if (_parameters.Length > 0)
            {
                parameters[0] = new MyPlates.Tx.Reporting.ReportExecution2005.ParameterValue();
                parameters[0].Name = "orderId";
                parameters[0].Value = Convert.ToInt32(orderId).ToString();
            }
            rsExec.SetExecutionParameters(parameters, "en-us");

            string encoding = String.Empty;
            string mimeType = String.Empty;
            string extension = String.Empty;
            MyPlates.Tx.Reporting.ReportExecution2005.Warning[] warnings = null;
            string[] streamIDs = null;

            byte[] results = rsExec.Render("PDF", null, out extension, out encoding, out mimeType, out warnings, out streamIDs);

            Stream s = new MemoryStream(results);

            return s;
        }
    }
}
