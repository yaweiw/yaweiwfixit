using HtmlGenerator.Model;
using System.Collections.Generic;
using System.Text;

namespace HtmlGenerator
{
    public class ReportGenerator
    {
        private readonly StringBuilder htmlheaderbegintag;

        private readonly StringBuilder htmlheaderendtag;

        private readonly StringBuilder htmlbodybegintag;

        private readonly StringBuilder htmlbodyendtag;

        private readonly StringBuilder htmlreportheader;

        private readonly StringBuilder htmlrawlog;

        private readonly BuildSummaryHtmlPlaceHolder htmlbuildsummary;

        private readonly BuildFilesHtmlPlaceHolder htmlbuildfiles;

        private readonly BuildDetailsHtmlPlaceHolder htmlbuilddetails;
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
        public StringBuilder HtmlReportHeader
        {
            get { return htmlreportheader; }
        }
        public StringBuilder HtmlRawLog
        {
            get { return htmlrawlog; }
        }
        public StringBuilder HtmlBuildSummary => (htmlbuildsummary == null) ? null : htmlbuildsummary.HtmlTableGenerator();
        public StringBuilder HtmlBuildFiles => (htmlbuildfiles == null) ? null : htmlbuildfiles.HtmlTableGenerator();
        public StringBuilder HtmlBuildDetails => (htmlbuilddetails == null) ? null : htmlbuilddetails.HtmlTableGenerator();
        public ReportGenerator()
        {
            string buildsumhead = @"<P class=""MsoNormal""><SPAN style=""font-size: 14pt; ""><B>Build Summary</B></SPAN></P>";
            List<BuildSummaryTrModel> summary = new List<BuildSummaryTrModel>();
            summary.Add(new BuildSummaryTrModel(@"<A href=""https://github.com/Microsoft/win-cpub-itpro-docs/tree/hp-cleanup"">win-cpub-itpro-docs (hp-cleanup) </A>"));
            summary.Add(new BuildSummaryTrModel(@"<A href=""https://github.com/Microsoft/win-cpub-itpro-docs/commit/e074ca48d307e14e4590666cee8f6b116d15319a"">e074ca48d307e14e4590666cee8f6b116d15319a</A>"));
            summary.Add(new BuildSummaryTrModel(@"<A href=""mailto: heatherpoulsen@users.noreply.github.com"">heatherpoulsen@users.noreply.github.com</A>"));
            summary.Add(new BuildSummaryTrModel(@"0"));
            summary.Add(new BuildSummaryTrModel(@"0"));
            summary.Add(new BuildSummaryTrModel(@"2016-04-12 23:19:36"));
            summary.Add(new BuildSummaryTrModel(@"00:03:56"));

            htmlbuildsummary = new BuildSummaryHtmlPlaceHolder(buildsumhead, summary.ConvertAll<TrModelBase>(x => (TrModelBase)x));

            string buildfileshead = @"<P class=""MsoNormal""><SPAN style=""font-size: 14pt; ""><B><A name=""BuildFiles"">Build Files</A></B></SPAN></P>";
            List<BuildFilesTrModel> fil = new List<BuildFilesTrModel>();
            fil.Add(new BuildFilesTrModel(@"<A href=""https://github.com/Microsoft/win-cpub-itpro-docs/blob/e074ca48d307e14e4590666cee8f6b116d15319a/browsers/edge/images/edge-emie-registrysitelist.png"">1browsers/edge/images/edge-emie-registrysitelist.png</A>","0","0","0"));
            fil.Add(new BuildFilesTrModel(@"<A href=""https://github.com/Microsoft/win-cpub-itpro-docs/blob/e074ca48d307e14e4590666cee8f6b116d15319a/browsers/edge/images/edge-emie-registrysitelist.png"">2browsers/edge/images/edge-emie-registrysitelist.png</A>", "0", "0", "0"));
            fil.Add(new BuildFilesTrModel(@"<A href=""https://github.com/Microsoft/win-cpub-itpro-docs/blob/e074ca48d307e14e4590666cee8f6b116d15319a/browsers/edge/images/edge-emie-registrysitelist.png"">3browsers/edge/images/edge-emie-registrysitelist.png</A>", "0", "0", "0"));
            fil.Add(new BuildFilesTrModel(@"<A href=""https://github.com/Microsoft/win-cpub-itpro-docs/blob/e074ca48d307e14e4590666cee8f6b116d15319a/browsers/edge/images/edge-emie-registrysitelist.png"">4browsers/edge/images/edge-emie-registrysitelist.png</A>", "0", "0", "0"));
            fil.Add(new BuildFilesTrModel(@"<A href=""https://github.com/Microsoft/win-cpub-itpro-docs/blob/e074ca48d307e14e4590666cee8f6b116d15319a/browsers/edge/images/edge-emie-registrysitelist.png"">5browsers/edge/images/edge-emie-registrysitelist.png</A>", "0", "0", "0"));
            fil.Add(new BuildFilesTrModel(@"<A href=""https://github.com/Microsoft/win-cpub-itpro-docs/blob/e074ca48d307e14e4590666cee8f6b116d15319a/browsers/edge/images/edge-emie-registrysitelist.png"">6browsers/edge/images/edge-emie-registrysitelist.png</A>", "0", "0", "0"));
            fil.Add(new BuildFilesTrModel(@"<A href=""https://github.com/Microsoft/win-cpub-itpro-docs/blob/e074ca48d307e14e4590666cee8f6b116d15319a/browsers/edge/images/edge-emie-registrysitelist.png"">7browsers/edge/images/edge-emie-registrysitelist.png</A>", "0", "0", "0"));
            fil.Add(new BuildFilesTrModel(@"<A href=""https://github.com/Microsoft/win-cpub-itpro-docs/blob/e074ca48d307e14e4590666cee8f6b116d15319a/browsers/edge/images/edge-emie-registrysitelist.png"">8browsers/edge/images/edge-emie-registrysitelist.png</A>", "0", "0", "0"));
            htmlbuildfiles = new BuildFilesHtmlPlaceHolder(buildfileshead, fil.ConvertAll<TrModelBase>(x => (TrModelBase)x));

            string builddethead = @"<P class=""MsoNormal""><SPAN style=""font-size: 14pt;""><B>Build Details</B></SPAN></P>";
            List<BuildDetailsTrModel> det = new List<BuildDetailsTrModel>();
            det.Add(new BuildDetailsTrModel(@"<A id="" &#10;#@Info&#10;"">Info</A>",
                @"1Plug-in directory: W:\lluwccfd.1cl\sourW:\lluwccfd.1cl\source\.optemp\packages\docfx.msbuild.1.8.0-alpha-0031-g2a7dee7\tools\plugins_zplfb4q0.kc2\plugins, configuration file: W:\lluwccfd.1cl\source\.optemp\packages\docfx.msbuild.1.8.0-alpha-0031-g2a7dee7\tools\plugins_zplfb4q0.kc2\plugins\docfx.plugins.config",
                @"",
                @"<A href=""https://github.com/Microsoft/win-cpub-itpro-docs/blob/e074ca48d307e14e4590666cee8f6b116d15319a/#L"">xx</A>",
                @"",
                @"2016-04-12T23:16:27.3196693Z"
                ));
            det.Add(new BuildDetailsTrModel(@"<A id="" &#10;#@Info&#10;"">Info</A>",
                @"2Plug-in directory: W:\lluwccfd.1cl\sour",
                @"",
                @"<A href=""https://github.com/Microsoft/win-cpub-itpro-docs/blob/e074ca48d307e14e4590666cee8f6b116d15319a/#L"">xx</A>",
                @"",
                @"2016-04-12T23:16:27.3196693Z"
                ));
            det.Add(new BuildDetailsTrModel(@"<A id="" &#10;#@Info&#10;"">Info</A>",
                @"3Plug-in directory: W:\lluwccfd.1cl\sour",
                @"",
                @"<A href=""https://github.com/Microsoft/win-cpub-itpro-docs/blob/e074ca48d307e14e4590666cee8f6b116d15319a/#L"">xx</A>",
                @"",
                @"2016-04-12T23:16:27.3196693Z"
                ));
            det.Add(new BuildDetailsTrModel(@"<A id="" &#10;#@Info&#10;"">Info</A>",
                @"4Plug-in directory: W:\lluwccfd.1cl\sour",
                @"",
                @"<A href=""https://github.com/Microsoft/win-cpub-itpro-docs/blob/e074ca48d307e14e4590666cee8f6b116d15319a/#L"">xx</A>",
                @"",
                @"2016-04-12T23:16:27.3196693Z"
                ));
            det.Add(new BuildDetailsTrModel(@"<A id="" &#10;#@Info&#10;"">Info</A>",
                @"5Plug-in directory: W:\lluwccfd.1cl\sour",
                @"",
                @"<A href=""https://github.com/Microsoft/win-cpub-itpro-docs/blob/e074ca48d307e14e4590666cee8f6b116d15319a/#L"">xx</A>",
                @"",
                @"2016-04-12T23:16:27.3196693Z"
                ));
            htmlbuilddetails = new BuildDetailsHtmlPlaceHolder(builddethead, det.ConvertAll<TrModelBase>(x => (TrModelBase)x));

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
            htmlheaderbegintag.Append("          font-family: \"\\@SimSun\";");
            htmlheaderbegintag.Append("          panose-1: 2 1 6 0 3 1 1 1 1 1;");
            htmlheaderbegintag.Append("          mso-font-alt: \"\\@Arial Unicode MS\";");
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
            htmlreportheader = new StringBuilder();
            htmlreportheader.Append("<P class=\"MsoNormal\"><SPAN style=\"font-size: 16pt;\"><B>Open Publishing Online Build Report</B></SPAN></P>");
            htmlreportheader.Append("<HR>");
            htmlreportheader.Append("<BR>");

            // Build Summary

            // Build Files

            // Build Details


            htmlbodyendtag = new StringBuilder();
            htmlbodyendtag.Append("</BODY>");

            htmlrawlog = new StringBuilder();
            htmlrawlog.Append(@"<BR><A href=""https://opbuildstoragesandbox2.blob.core.windows.net/report/2016%5C4%5C12%5Cc1bb9ee0-549c-27c2-888b-50d73cbfb6e8%5CCommit%5C201604122315356766-hp-cleanup%5Crawlog.txt""> Build Raw Log </A>");
            htmlheaderendtag = new StringBuilder();
            htmlheaderendtag.Append("</HTML>");
        }
    }
}
