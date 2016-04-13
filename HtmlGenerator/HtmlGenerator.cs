using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlGenerator
{
    public class HtmlGenerator : IHtmlGenerator
    {
        private readonly StringBuilder htmlheaderbegintag;

        private readonly StringBuilder htmlheaderendtag;

        private readonly StringBuilder htmlbodybegintag;

        private readonly StringBuilder htmlbodyendtag;

        private readonly StringBuilder reportheader;

        private BuildSummaryHtmlHolder BuildSummary; 
        public StringBuilder HtmlHeaderBeginTag
        {
            get { return htmlheaderbegintag; }
        }
        public StringBuilder HtmlHeaderEndTag
        {
            get { return htmlheaderendtag; }
        }
        public StringBuilder HtmlBodyBeginTag
        {
            get { return htmlbodybegintag; }
        }
        public StringBuilder HtmlBodyEndTag
        {
            get { return htmlbodyendtag; }
        }
        public StringBuilder ReportHeader
        {
            get { return reportheader; }
        }
        public HtmlGenerator()
        {
            htmlheaderbegintag = new StringBuilder();
            htmlheaderbegintag.Append("<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.0 Transitional//EN\">");
            htmlheaderbegintag.Append("<HTML><HEAD><META content=\"IE=5.0000\" http-equiv=\"X-UA-Compatible\">");
            htmlheaderbegintag.Append("<META http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">          ");
            htmlheaderbegintag.Append("<STYLE>");
            htmlheaderbegintag.Append("          /* Font Definitions */");
            htmlheaderbegintag.Append("          @font-face {");
            htmlheaderbegintag.Append("          font-family: SimSun;");
            htmlheaderbegintag.Append("          panose-1: 2 1 6 0 3 1 1 1 1 1;");
            htmlheaderbegintag.Append("          mso-font-alt: SimSun;");
            htmlheaderbegintag.Append("          mso-font-charset: 134;");
            htmlheaderbegintag.Append("          mso-generic-font-family: auto;");
            htmlheaderbegintag.Append("          mso-font-pitch: variable;");
            htmlheaderbegintag.Append("          mso-font-signature: 3 680460288 22 0 262145 0;");
            htmlheaderbegintag.Append("          }");
            htmlheaderbegintag.Append("          @font-face {");
            htmlheaderbegintag.Append("          font-family: \"Cambria Math\";");
            htmlheaderbegintag.Append("          panose-1: 2 4 5 3 5 4 6 3 2 4;");
            htmlheaderbegintag.Append("          mso-font-alt: \"Calisto MT\";");
            htmlheaderbegintag.Append("          mso-font-charset: 0;");
            htmlheaderbegintag.Append("          mso-generic-font-family: roman;");
            htmlheaderbegintag.Append("          mso-font-pitch: variable;");
            htmlheaderbegintag.Append("          mso-font-signature: -536870145 1107305727 0 0 415 0;");
            htmlheaderbegintag.Append("          }");
            htmlheaderbegintag.Append("          @font-face {");
            htmlheaderbegintag.Append("          font-family: Calibri;");
            htmlheaderbegintag.Append("          panose-1: 2 15 5 2 2 2 4 3 2 4;");
            htmlheaderbegintag.Append("          mso-font-alt: \"Times New Roman\";");
            htmlheaderbegintag.Append("          mso-font-charset: 0;");
            htmlheaderbegintag.Append("          mso-generic-font-family: swiss;");
            htmlheaderbegintag.Append("          mso-font-pitch: variable;");
            htmlheaderbegintag.Append("          mso-font-signature: -520092929 1073786111 9 0 415 0;");
            htmlheaderbegintag.Append("          }");
            htmlheaderbegintag.Append("          @font-face {");
            htmlheaderbegintag.Append("          font-family: \"\@SimSun\";");
            htmlheaderbegintag.Append("          panose-1: 2 1 6 0 3 1 1 1 1 1;");
            htmlheaderbegintag.Append("          mso-font-alt: \"\@Arial Unicode MS\";");
            htmlheaderbegintag.Append("          mso-font-charset: 134;");
            htmlheaderbegintag.Append("          mso-generic-font-family: auto;");
            htmlheaderbegintag.Append("          mso-font-pitch: variable;");
            htmlheaderbegintag.Append("          mso-font-signature: 3 680460288 22 0 262145 0;");
            htmlheaderbegintag.Append("          }");
            htmlheaderbegintag.Append("          /* Style Definitions */");
            htmlheaderbegintag.Append("          p.MsoNormal, li.MsoNormal, div.MsoNormal {");
            htmlheaderbegintag.Append("          mso-style-unhide: no;");
            htmlheaderbegintag.Append("          mso-style-qformat: yes;");
            htmlheaderbegintag.Append("          mso-style-parent: \"\";");
            htmlheaderbegintag.Append("          margin: 0in;");
            htmlheaderbegintag.Append("          margin-bottom: .0001pt;");
            htmlheaderbegintag.Append("          mso-pagination: widow-orphan;");
            htmlheaderbegintag.Append("          font-size: 11.0pt;");
            htmlheaderbegintag.Append("          font-family: \"Calibri\",\"sans-serif\";");
            htmlheaderbegintag.Append("          mso-fareast-font-family: SimSun;");
            htmlheaderbegintag.Append("          mso-fareast-theme-font: minor-fareast;");
            htmlheaderbegintag.Append("          }");
            htmlheaderbegintag.Append("          a:link, span.MsoHyperlink {");
            htmlheaderbegintag.Append("          mso-style-noshow: yes;");
            htmlheaderbegintag.Append("          mso-style-priority: 99;");
            htmlheaderbegintag.Append("          color: #0563C1;");
            htmlheaderbegintag.Append("          text-decoration: underline;");
            htmlheaderbegintag.Append("          text-underline: single;");
            htmlheaderbegintag.Append("          }");
            htmlheaderbegintag.Append("          a:visited, span.MsoHyperlinkFollowed {");
            htmlheaderbegintag.Append("          mso-style-noshow: yes;");
            htmlheaderbegintag.Append("          mso-style-priority: 99;");
            htmlheaderbegintag.Append("          color: #954F72;");
            htmlheaderbegintag.Append("          text-decoration: underline;");
            htmlheaderbegintag.Append("          text-underline: single;");
            htmlheaderbegintag.Append("          }");
            htmlheaderbegintag.Append("          span.EmailStyle17 {");
            htmlheaderbegintag.Append("          mso-style-type: personal;");
            htmlheaderbegintag.Append("          mso-style-noshow: yes;");
            htmlheaderbegintag.Append("          mso-style-unhide: no;");
            htmlheaderbegintag.Append("          font-family: \"Calibri\",\"sans-serif\";");
            htmlheaderbegintag.Append("          mso-ascii-font-family: Calibri;");
            htmlheaderbegintag.Append("          mso-hansi-font-family: Calibri;");
            htmlheaderbegintag.Append("          mso-bidi-font-family: Calibri;");
            htmlheaderbegintag.Append("          color: windowtext;");
            htmlheaderbegintag.Append("          }");
            htmlheaderbegintag.Append("          .MsoChpDefault {");
            htmlheaderbegintag.Append("          mso-style-type: export-only;");
            htmlheaderbegintag.Append("          mso-default-props: yes;");
            htmlheaderbegintag.Append("          font-size: 10.0pt;");
            htmlheaderbegintag.Append("          mso-ansi-font-size: 10.0pt;");
            htmlheaderbegintag.Append("          mso-bidi-font-size: 10.0pt;");
            htmlheaderbegintag.Append("          }");
            htmlheaderbegintag.Append("          @page WordSection1 {");
            htmlheaderbegintag.Append("          size: 8.5in 11.0in;");
            htmlheaderbegintag.Append("          margin: 1.0in 1.0in 1.0in 1.0in;");
            htmlheaderbegintag.Append("          mso-header-margin: .5in;");
            htmlheaderbegintag.Append("          mso-footer-margin: .5in;");
            htmlheaderbegintag.Append("          mso-paper-source: 0;");
            htmlheaderbegintag.Append("          }");
            htmlheaderbegintag.Append("          div.WordSection1 {");
            htmlheaderbegintag.Append("          page: WordSection1;");
            htmlheaderbegintag.Append("          }");
            htmlheaderbegintag.Append("          td {");
            htmlheaderbegintag.Append("          font-family: \"Calibri\",\"sans-serif\";");
            htmlheaderbegintag.Append("          font-size: 11.0pt;");
            htmlheaderbegintag.Append("          border: solid #5B9BD5 1.0pt;");
            htmlheaderbegintag.Append("          padding: 1pt 5.4pt 1pt 5.4pt;");
            htmlheaderbegintag.Append("          }");
            htmlheaderbegintag.Append("          pre {");
            htmlheaderbegintag.Append("          margin-bottom: .0001pt;");
            htmlheaderbegintag.Append("          font-size: 10.0pt;");
            htmlheaderbegintag.Append("          font-family: \"Verdana\",sans-serif;");
            htmlheaderbegintag.Append("          margin-left: 0in;");
            htmlheaderbegintag.Append("          margin-right: 0in;");
            htmlheaderbegintag.Append("          margin-top: 0in;");
            htmlheaderbegintag.Append("          }");
            htmlheaderbegintag.Append("          .auto-style1 {");
            htmlheaderbegintag.Append("          width: 90.0%;");
            htmlheaderbegintag.Append("          border-collapse: collapse;");
            htmlheaderbegintag.Append("          font-size: 10.0pt;");
            htmlheaderbegintag.Append("          font-family: \"Times New Roman\", serif;");
            htmlheaderbegintag.Append("          }");
            htmlheaderbegintag.Append("        </STYLE>");
            htmlheaderbegintag.Append("<META name=\"GENERATOR\" content=\"MSHTML 11.00.10586.162\"></HEAD> ");

            htmlbodybegintag = new StringBuilder();
            htmlbodybegintag.Append("<BODY lang=\"EN-US\" style=\"tab-interval: .5in;\" link=\"#0563c1\" vlink=\"#954f72\">");

            // Open Publishing Online Build Report
            reportheader = new StringBuilder();
            reportheader.Append("<P class=\"MsoNormal\"><SPAN style=\"font-size: 16pt;\"><B>Open Publishing Online Build Report</B></SPAN></P>");
            reportheader.Append("<HR>");
            reportheader.Append("<BR>");

            // Build Summary


            htmlbodybegintag = new StringBuilder();
            htmlbodybegintag.Append("</BODY>");

            htmlheaderendtag = new StringBuilder();
            htmlheaderendtag.Append("</HTML>");
        }
    }
}
