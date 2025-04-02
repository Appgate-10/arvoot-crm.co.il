
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;

namespace ControlPanel
{
    /// <summary>
    /// Summary description for DownloadFile
    /// </summary>
    public class DownloadFile : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {

            System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            System.Web.HttpRequest request = System.Web.HttpContext.Current.Request;
            string fileName = request.QueryString["fileName"];
            string ServiceRequestID = request.QueryString["ServiceRequestID"];
            string OfferID = request.QueryString["OfferID"];
            string ext = Path.GetExtension(fileName); //get file extension
            string type = "";

            // set known types based on file extension
            if (ext != null)
            {
                switch (ext.ToLower())
                {
                    case ".pdf":
                        type = "Application/pdf";
                        break;
                    case ".doc":
                        type = "Application/msword";
                        break;
                    case ".wav":
                        type = "audio/mpeg";
                        break;
                    case ".png":
                        type = "Application/png";
                        break;
                }
            }
            if (OfferID != null)
            {
                string path = "";
                if (request.QueryString["PdfFile"] !=null) 
                    path = Path.Combine(ConfigurationManager.AppSettings["MapPath"], "pdf", fileName);
                else  path = Path.Combine(ConfigurationManager.AppSettings["MapPath"], "OfferDocuments", fileName);
                //fileName = "4b38c7244721e01caee0aa73c4ba2a1d.pdf";
                if (File.Exists(path))
                {
                    //response.Redirect("OfferEdit.aspx");
                    response.ClearContent();
                    response.Clear();
                    response.ContentType = type;
                    response.AddHeader("Content-Disposition",
                                       "attachment; filename=" + fileName + ";");
                    response.TransmitFile(path);
                    response.Flush();
                    response.End();

                }
                else
                {
                    response.Redirect("OfferEdit.aspx?OfferID=" + OfferID);
                }
            }
            else
            {
                string path = Path.Combine(ConfigurationManager.AppSettings["MapPath"], "ServiceRequestDocuments", fileName);
                //fileName = "4b38c7244721e01caee0aa73c4ba2a1d.pdf";
                if (File.Exists(path))
                {
                    //response.Redirect("OfferEdit.aspx");
                    response.ClearContent();
                    response.Clear();
                    response.ContentType = type;
                    response.AddHeader("Content-Disposition",
                                       "attachment; filename=" + fileName + ";");
                    response.TransmitFile(path);
                    response.Flush();
                    response.End();

                }
                else
                {
                    response.Redirect("ServiceRequestEdit.aspx?ServiceRequestID=" + ServiceRequestID);
                }

            }


        }



        public bool IsReusable
        {
            get
            {
                return false;
            }
        }


    }
}












