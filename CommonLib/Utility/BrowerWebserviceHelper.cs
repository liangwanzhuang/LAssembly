using System;
using System.Net;
using System.IO;
using System.CodeDom;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Web.Services.Description;
using System.Xml.Serialization;
using System.Reflection;
using System.Text;

namespace CommonLib.Utility
{
    public class BrowerWebserviceHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param 接口地址www.xxx.asmx="ServerUrl"></param>
        /// <param 请求方法名 ="methodname"></param>
        /// <param 接口参数 参数名称=参数值 ="args"></param>
        /// <returns></returns>
        public static object InvokeWebService(string ServerUrl, string methodname, object[] args)
        {
            string name = "EnterpriseServerBase.WebService.DynamicWebCalling";
            string[] parts = ServerUrl.Split('/');
            string[] pps = parts[parts.Length - 1].Split('.');
            string classname = pps[0];

            try
            {
                WebClient wc = new WebClient();
                Stream stream = wc.OpenRead(ServerUrl + "?WSDL");
                ServiceDescription sd = ServiceDescription.Read(stream);
                ServiceDescriptionImporter sdi = new ServiceDescriptionImporter();
                sdi.AddServiceDescription(sd, "", "");
                CodeNamespace cn = new CodeNamespace(name);
                //生成客户端代理类代码          
                CodeCompileUnit ccu = new CodeCompileUnit();
                ccu.Namespaces.Add(cn);
                sdi.Import(cn, ccu);
                CSharpCodeProvider icc = new CSharpCodeProvider();
                //设定编译参数                 
                CompilerParameters cplist = new CompilerParameters();
                cplist.GenerateExecutable = false;
                cplist.GenerateInMemory = true;
                cplist.ReferencedAssemblies.Add("System.dll");
                cplist.ReferencedAssemblies.Add("System.XML.dll");
                cplist.ReferencedAssemblies.Add("System.Web.Services.dll");
                cplist.ReferencedAssemblies.Add("System.Data.dll");
                //编译代理类                 
                CompilerResults cr = icc.CompileAssemblyFromDom(cplist, ccu);
                if (true == cr.Errors.HasErrors)
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    foreach (System.CodeDom.Compiler.CompilerError ce in cr.Errors)
                    {
                        sb.Append(ce.ToString());
                        sb.Append(System.Environment.NewLine);
                    }
                    throw new Exception(sb.ToString());
                }
                //生成代理实例，并调用方法   
                System.Reflection.Assembly assembly = cr.CompiledAssembly;
                Type t = assembly.GetType(name + "." + classname, true, true);
                object obj = Activator.CreateInstance(t);
                System.Reflection.MethodInfo mi = t.GetMethod(methodname);
                var ObjReturn = mi.Invoke(obj, args);
                return ObjReturn;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param 接口参数 参数名称=参数值 ="args"></param>
        /// <param 接口地址www.xxx.asmx="ServerUrl"></param>
        /// <param 请求方法名 ="methodname"></param>
        /// <returns></returns>
        public static string HttpPostToWebService(string args, string ServerUrl, string methodname)
        {
            string result = string.Empty;
            try
            {
                string url = ServerUrl + "/" + methodname;
                byte[] data = System.Text.Encoding.ASCII.GetBytes(args);
                WebRequest request = HttpWebRequest.Create(
url);
                request.Method = "POST";
                request.ContentLength = data.Length;
                request.ContentType = "application/x-www-form-urlencoded";
                Stream str = request.GetRequestStream();
                str.Write(data, 0, data.Length);
                str.Flush();
                WebResponse response = request.GetResponse();
                StreamReader reader = new System.IO.StreamReader(response.GetResponseStream());
                result = reader.ReadToEnd();

            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.ToString(), new string[] { "确定" });
            }
            return result;
        }
    }
}
